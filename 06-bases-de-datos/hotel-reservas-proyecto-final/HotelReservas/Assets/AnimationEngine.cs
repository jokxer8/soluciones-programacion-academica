using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace HotelReservas.Assets
{
    /// <summary>
    /// Animated background engine with manual double buffering (.NET Framework compatible)
    /// </summary>
    public static class AnimationEngine
    {
        private static readonly Timer _timer;
        private static int _tick = 0;
        private static readonly object _lock = new object();
        private static readonly List<WeakReference<Control>> _targets =
            new List<WeakReference<Control>>();

        // Cache de bitmaps offscreen
        private static readonly Dictionary<IntPtr, BitmapInfo> _bufferCache =
            new Dictionary<IntPtr, BitmapInfo>();

        private class BitmapInfo
        {
            public Bitmap Bitmap;
            public int Width;
            public int Height;
        }

        static AnimationEngine()
        {
            _timer = new Timer();
            _timer.Interval = 16; // ~60 FPS
            _timer.Tick += OnTick;
        }

        // ── Public API ────────────────────────────────────────────────────

        public static void Register(Control target)
        {
            if (target == null) return;

            lock (_lock)
            {
                // Evitar duplicados
                foreach (var wr in _targets)
                {
                    if (wr.TryGetTarget(out var existing) && existing == target)
                        return;
                }

                // Remover handler previo si existe
                target.Paint -= AnimatedPaint;
                target.Paint += AnimatedPaint;

                _targets.Add(new WeakReference<Control>(target));

                // Usar reflexión para SetStyle (es protected)
                SetStyle(target,
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.ResizeRedraw,
                    true);

                // Usar reflexión para UpdateStyles (es protected)
                UpdateStyles(target);

                // Eventos de limpieza
                target.Disposed -= Target_Disposed;
                target.Disposed += Target_Disposed;

                // ✅ También limpiar cuando se cierra el formulario
                target.HandleDestroyed -= Target_HandleDestroyed;
                target.HandleDestroyed += Target_HandleDestroyed;
            }

            if (!_timer.Enabled)
                _timer.Start();
        }

        public static void Unregister(Control target)
        {
            if (target == null) return;

            lock (_lock)
            {
                target.Paint -= AnimatedPaint;
                target.Disposed -= Target_Disposed;
                target.HandleDestroyed -= Target_HandleDestroyed;

                SafeReleaseBuffer(target);
            }
        }

        public static void Stop()
        {
            _timer.Stop();

            lock (_lock)
            {
                SafeClearAllBuffers();
            }
        }

        public static void Start()
        {
            _timer.Start();
        }

        // ── Reflexión para métodos protected ──────────────────────────────

        private static void SetStyle(Control control, ControlStyles styles, bool value)
        {
            try
            {
                control.GetType()
                    .GetMethod("SetStyle",
                        BindingFlags.Instance | BindingFlags.NonPublic,
                        null,
                        new[] { typeof(ControlStyles), typeof(bool) },
                        null)
                    ?.Invoke(control, new object[] { styles, value });
            }
            catch
            {
                // Ignorar si falla
            }
        }

        private static void UpdateStyles(Control control)
        {
            try
            {
                control.GetType()
                    .GetMethod("UpdateStyles",
                        BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(control, null);
            }
            catch
            {
                // Ignorar si falla
            }
        }

        // ── Event Handlers ────────────────────────────────────────────────

        private static void Target_Disposed(object sender, EventArgs e)
        {
            if (sender is Control ctrl)
            {
                ctrl.Paint -= AnimatedPaint;
                ctrl.Disposed -= Target_Disposed;
                ctrl.HandleDestroyed -= Target_HandleDestroyed;

                lock (_lock)
                {
                    SafeReleaseBuffer(ctrl);
                    RemoveTarget(ctrl);
                }
            }
        }

        private static void Target_HandleDestroyed(object sender, EventArgs e)
        {
            if (sender is Control ctrl)
            {
                ctrl.Paint -= AnimatedPaint;
                ctrl.Disposed -= Target_Disposed;
                ctrl.HandleDestroyed -= Target_HandleDestroyed;

                lock (_lock)
                {
                    SafeReleaseBuffer(ctrl);
                    RemoveTarget(ctrl);
                }
            }
        }

        private static void RemoveTarget(Control ctrl)
        {
            for (int i = _targets.Count - 1; i >= 0; i--)
            {
                if (_targets[i].TryGetTarget(out var existing) && existing == ctrl)
                {
                    _targets.RemoveAt(i);
                    break;
                }
            }
        }

        private static void OnTick(object sender, EventArgs e)
        {
            _tick++;

            List<Control> toInvalidate = new List<Control>();

            lock (_lock)
            {
                // ✅ Recorrer de atrás hacia adelante para remover de forma segura
                for (int i = _targets.Count - 1; i >= 0; i--)
                {
                    var wr = _targets[i];

                    if (!wr.TryGetTarget(out var control))
                    {
                        // Referencia muerta - remover
                        _targets.RemoveAt(i);
                        continue;
                    }

                    // ✅ Verificar si el control fue dispuesto PRIMERO
                    if (control.IsDisposed)
                    {
                        // Control dispuesto - limpiar y remover
                        control.Paint -= AnimatedPaint;
                        control.Disposed -= Target_Disposed;
                        control.HandleDestroyed -= Target_HandleDestroyed;
                        SafeReleaseBuffer(control);
                        _targets.RemoveAt(i);
                        continue;
                    }

                    // ✅ Verificar si el handle fue destruido
                    if (!control.IsHandleCreated)
                    {
                        // Handle destruido pero no dispuesto - remover de la lista
                        control.Paint -= AnimatedPaint;
                        control.Disposed -= Target_Disposed;
                        control.HandleDestroyed -= Target_HandleDestroyed;
                        SafeReleaseBuffer(control);
                        _targets.RemoveAt(i);
                        continue;
                    }

                    // ✅ Control válido y visible - agregar para invalidar
                    if (control.Visible)
                    {
                        toInvalidate.Add(control);
                    }
                }

                // ✅ Si no hay controles, detener el timer para ahorrar recursos
                if (_targets.Count == 0)
                {
                    _timer.Stop();
                    SafeClearAllBuffers();
                }
            }

            // ✅ Invalidar FUERA del lock con protección
            foreach (var control in toInvalidate)
            {
                try
                {
                    if (!control.IsDisposed && control.IsHandleCreated)
                    {
                        control.Invalidate();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // Control fue dispuesto mientras iterábamos - ignorar
                }
                catch (Exception)
                {
                    // Cualquier otro error - ignorar
                }
            }
        }

        // ── Paint Handler con Double Buffer Manual ───────────────────────

        private static void AnimatedPaint(object sender, PaintEventArgs e)
        {
            Control ctrl = (Control)sender;

            int w = ctrl.Width;
            int h = ctrl.Height;

            if (w < 10 || h < 10) return;

            // ✅ Obtener buffer de forma segura
            Bitmap buffer = SafeGetOrCreateBuffer(ctrl, w, h);
            if (buffer == null) return;

            try
            {
                // Dibujar en buffer offscreen
                using (Graphics g = Graphics.FromImage(buffer))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.Low;
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.PixelOffsetMode = PixelOffsetMode.Half;

                    // 1. Dibujar fondo sólido PRIMERO
                    Color bgColor = ctrl.BackColor;
                    using (SolidBrush bgBrush = new SolidBrush(bgColor))
                    {
                        g.FillRectangle(bgBrush, 0, 0, w, h);
                    }

                    // 2. Calcular posiciones de blobs
                    double t = _tick * 0.012;

                    float x1 = w * (0.35f + 0.25f * (float)Math.Sin(t));
                    float y1 = h * (0.40f + 0.30f * (float)Math.Cos(t * 0.65));
                    float r1 = Math.Min(w, h) * 0.70f;

                    float x2 = w * (0.65f + 0.20f * (float)Math.Cos(t * 0.80));
                    float y2 = h * (0.58f + 0.22f * (float)Math.Sin(t * 0.90));
                    float r2 = Math.Min(w, h) * 0.55f;

                    // 3. Determinar colores según tema
                    Color c1, c2;
                    if (Theme.Current == AppTheme.Dark)
                    {
                        c1 = Color.FromArgb(45, 80, 160, 255);
                        c2 = Color.FromArgb(35, 37, 99, 235);
                    }
                    else
                    {
                        c1 = Color.FromArgb(40, 37, 99, 235);
                        c2 = Color.FromArgb(30, 99, 180, 255);
                    }

                    // 4. Dibujar blobs
                    DrawBlob(g, x1, y1, r1, c1);
                    DrawBlob(g, x2, y2, r2, c2);
                }

                // 5. Copiar buffer al control en UN SOLO paso
                e.Graphics.DrawImageUnscaled(buffer, 0, 0);
            }
            catch (Exception)
            {
                // Si algo falla durante el paint, no hacer nada
                // Esto previene crashes por ObjectDisposedException
            }
        }

        private static void DrawBlob(Graphics g, float cx, float cy, float radius, Color centerColor)
        {
            radius = Math.Max(10, radius);

            cx = Math.Max(-radius, Math.Min(cx, float.MaxValue - radius));
            cy = Math.Max(-radius, Math.Min(cy, float.MaxValue - radius));

            RectangleF rect = new RectangleF(cx - radius, cy - radius, radius * 2, radius * 2);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);

                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = centerColor;
                    brush.CenterPoint = new PointF(cx, cy);
                    brush.SurroundColors = new[] { Color.Transparent };
                    brush.WrapMode = WrapMode.Clamp;

                    g.FillPath(brush, path);
                }
            }
        }

        // ── Buffer Management (Thread-Safe) ────────────────────────────

        private static Bitmap SafeGetOrCreateBuffer(Control control, int width, int height)
        {
            // ✅ No intentar acceder al Handle si el control está dispuesto
            if (control.IsDisposed || !control.IsHandleCreated)
            {
                return null;
            }

            IntPtr handle;

            try
            {
                handle = control.Handle;
            }
            catch (ObjectDisposedException)
            {
                return null;
            }

            if (handle == IntPtr.Zero)
            {
                return null;
            }

            lock (_lock)
            {
                if (_bufferCache.TryGetValue(handle, out BitmapInfo info))
                {
                    if (info.Width == width && info.Height == height && info.Bitmap != null)
                    {
                        return info.Bitmap;
                    }

                    // Tamaño cambió - dispose old
                    info.Bitmap?.Dispose();
                    _bufferCache.Remove(handle);
                }

                // Crear nuevo buffer
                Bitmap newBitmap;
                try
                {
                    newBitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                }
                catch (Exception)
                {
                    return null;
                }

                _bufferCache[handle] = new BitmapInfo
                {
                    Bitmap = newBitmap,
                    Width = width,
                    Height = height
                };

                return newBitmap;
            }
        }

        private static void SafeReleaseBuffer(Control control)
        {
            if (control.IsDisposed)
            {
                // No podemos obtener el Handle - limpiar por referencia
                return;
            }

            if (!control.IsHandleCreated)
            {
                return;
            }

            IntPtr handle;

            try
            {
                handle = control.Handle;
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            if (handle == IntPtr.Zero)
            {
                return;
            }

            ReleaseBuffer(handle);
        }

        private static void ReleaseBuffer(IntPtr handle)
        {
            if (handle == IntPtr.Zero) return;

            if (_bufferCache.TryGetValue(handle, out BitmapInfo info))
            {
                info.Bitmap?.Dispose();
                _bufferCache.Remove(handle);
            }
        }

        private static void SafeClearAllBuffers()
        {
            foreach (var kvp in _bufferCache)
            {
                try
                {
                    kvp.Value.Bitmap?.Dispose();
                }
                catch
                {
                    // Ignorar errores al dispose
                }
            }
            _bufferCache.Clear();
        }

        // ── Cleanup ──────────────────────────────────────────────────────

        public static void Cleanup()
        {
            try
            {
                _timer.Stop();
                _timer.Dispose();
            }
            catch
            {
                // Ignorar
            }

            lock (_lock)
            {
                SafeClearAllBuffers();
                _targets.Clear();
            }
        }
    }
}