using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace HotelReservas.Assets
{
    /// <summary>
    /// Provides the application logo icon and image loaded from the Assets/ folder.
    /// Falls back to programmatic generation if the files are not yet present.
    ///
    /// File locations (relative to exe):
    ///   Assets/logo.ico  — embedded as ApplicationIcon in the assembly
    ///   Assets/logo.png  — saved by EnsurePng() on first startup
    /// </summary>
    public static class AppLogo
    {
        // ── Public API ────────────────────────────────────────────────────────

        /// <summary>
        /// Returns the window icon.
        /// Tries: Assets/logo.ico → root logo.ico → exe icon → generated fallback.
        /// Caller is responsible for disposing.
        /// </summary>
        public static Icon GetWindowIcon()
        {
            // 1. Try Assets/logo.ico next to the executable
            string assetIco = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "logo.ico");
            if (File.Exists(assetIco))
                try { return new Icon(assetIco); } catch { }

            // 2. Try root logo.ico next to the executable
            string rootIco = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.ico");
            if (File.Exists(rootIco))
                try { return new Icon(rootIco); } catch { }

            // 3. Extract from the embedded application icon
            try { return Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch { }

            // 4. Last resort: generate programmatically
            return GenerateIcon(32);
        }

        /// <summary>
        /// Returns a logo Bitmap at the requested size.
        /// Tries: Assets/logo.png → generated fallback.
        /// Caller is responsible for disposing.
        /// </summary>
        public static Bitmap GetLogoImage(int size = 48)
        {
            string pngPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "logo.png");
            if (File.Exists(pngPath))
            {
                try
                {
                    using (Bitmap src = new Bitmap(pngPath))
                    {
                        Bitmap scaled = new Bitmap(size, size, PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(scaled))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(src, 0, 0, size, size);
                        }
                        return scaled;
                    }
                }
                catch { }
            }

            // Fallback: generate
            return Generate(size);
        }

        /// <summary>
        /// Saves a 128-px PNG copy of the generated logo to Assets/logo.png.
        /// Also copies logo.ico into Assets/ if not already there.
        /// Call once from Program.Main. Safe to call multiple times.
        /// </summary>
        public static void EnsureAssets()
        {
            try
            {
                string exeDir   = AppDomain.CurrentDomain.BaseDirectory;
                string assetDir = Path.Combine(exeDir, "Assets");
                Directory.CreateDirectory(assetDir);

                // Save logo.png if missing
                string pngPath = Path.Combine(assetDir, "logo.png");
                if (!File.Exists(pngPath))
                    using (Bitmap bmp = Generate(128))
                        bmp.Save(pngPath, ImageFormat.Png);

                // Copy logo.ico into Assets/ if it exists in the root but not in Assets/
                string srcIco  = Path.Combine(exeDir, "logo.ico");
                string dstIco  = Path.Combine(assetDir, "logo.ico");
                if (File.Exists(srcIco) && !File.Exists(dstIco))
                    File.Copy(srcIco, dstIco);
            }
            catch { /* Non-critical */ }
        }

        // ── Programmatic generation (internal / fallback) ─────────────────────

        internal static Bitmap Generate(int size = 48)
        {
            Bitmap bmp = new Bitmap(size, size, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode      = SmoothingMode.AntiAlias;
                g.InterpolationMode  = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode    = PixelOffsetMode.HighQuality;
                g.TextRenderingHint  = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.Clear(Color.Transparent);

                var bgRect = new Rectangle(0, 0, size, size);
                int radius = Math.Max(4, size / 7);

                using (var bgBrush = new LinearGradientBrush(
                    bgRect, Color.FromArgb(255, 12, 30, 65),
                    Color.FromArgb(255, 22, 55, 110), LinearGradientMode.ForwardDiagonal))
                using (GraphicsPath bgPath = RoundedRect(bgRect, radius))
                    g.FillPath(bgBrush, bgPath);

                int barH = Math.Max(3, size / 12);
                using (var barBrush = new SolidBrush(Color.FromArgb(255, 245, 197, 24)))
                using (GraphicsPath barPath = RoundedRect(new Rectangle(0, 0, size, barH * 3), radius))
                {
                    g.SetClip(new Rectangle(0, 0, size, barH));
                    g.FillPath(barBrush, barPath);
                    g.ResetClip();
                }

                float fontSize = size * 0.36f;
                using (Font font = new Font("Calibri", fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
                using (var textBrush = new SolidBrush(Color.White))
                {
                    SizeF ts = g.MeasureString("HR", font);
                    g.DrawString("HR", font, textBrush,
                        (size - ts.Width)  / 2 + 0.5f,
                        (size - ts.Height) / 2 + barH * 0.4f);
                }

                using (Pen pen = new Pen(Color.FromArgb(60, 255, 255, 255), 1))
                using (GraphicsPath bp = RoundedRect(new Rectangle(1, 1, size - 2, size - 2), radius - 1))
                    g.DrawPath(pen, bp);
            }
            return bmp;
        }

        internal static Icon GenerateIcon(int size = 32)
        {
            using (Bitmap bmp = Generate(size))
                return Icon.FromHandle(bmp.GetHicon());
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static GraphicsPath RoundedRect(Rectangle b, int r)
        {
            int d = r * 2;
            var path = new GraphicsPath();
            path.AddArc(b.X,         b.Y,          d, d, 180, 90);
            path.AddArc(b.Right - d, b.Y,          d, d, 270, 90);
            path.AddArc(b.Right - d, b.Bottom - d, d, d,   0, 90);
            path.AddArc(b.X,         b.Bottom - d, d, d,  90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
