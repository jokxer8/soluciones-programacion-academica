using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HotelReservas.Assets
{
    /// <summary>
    /// Smooth hover/click color animation helper for WinForms buttons.
    /// Uses a single shared timer (16ms / ~60fps) for all registered buttons.
    /// Colors must come from Theme — never pass inline Color.FromArgb here.
    /// </summary>
    public static class UIAnimations
    {
        // ── Step per tick: 0.18 * 16ms ≈ 150ms total transition ──────────────
        private const float LerpStep = 0.18f;

        private static readonly Timer _timer;
        private static readonly Dictionary<Button, HoverState> _states =
            new Dictionary<Button, HoverState>();

        private class HoverState
        {
            public Color NormalColor;
            public Color HoverColor;
            public Color DownColor;

            // Current interpolated values (float for sub-pixel precision)
            public float R;
            public float G;
            public float B;

            public bool IsHovering;
            public bool IsDown;
        }

        static UIAnimations()
        {
            _timer = new Timer { Interval = 16 };
            _timer.Tick += OnTick;
        }

        // ── Public API ────────────────────────────────────────────────────────

        /// <summary>
        /// Attach smooth hover animation to a button.
        /// Overrides FlatAppearance.MouseOverBackColor — must be called AFTER
        /// any Theme.Apply* call that sets those properties.
        /// </summary>
        public static void AttachHover(Button btn, Color normalColor, Color hoverColor)
        {
            AttachHover(btn, normalColor, hoverColor, hoverColor);
        }

        /// <summary>
        /// Attach smooth hover + click animation to a button.
        /// </summary>
        public static void AttachHover(Button btn, Color normalColor, Color hoverColor, Color downColor)
        {
            if (btn == null || btn.IsDisposed) return;

            // Remove previous state if re-attaching (theme change scenario)
            Detach(btn);

            var state = new HoverState
            {
                NormalColor = normalColor,
                HoverColor  = hoverColor,
                DownColor   = downColor,
                R = normalColor.R,
                G = normalColor.G,
                B = normalColor.B,
                IsHovering = false,
                IsDown     = false
            };

            _states[btn] = state;

            // Disable WinForms built-in hover color so it doesn't fight the animation
            btn.UseVisualStyleBackColor = false;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;

            // Wire events (remove first to avoid duplicates on re-attach)
            btn.MouseEnter -= Btn_MouseEnter;
            btn.MouseLeave -= Btn_MouseLeave;
            btn.MouseDown  -= Btn_MouseDown;
            btn.MouseUp    -= Btn_MouseUp;
            btn.Disposed   -= Btn_Disposed;

            btn.MouseEnter += Btn_MouseEnter;
            btn.MouseLeave += Btn_MouseLeave;
            btn.MouseDown  += Btn_MouseDown;
            btn.MouseUp    += Btn_MouseUp;
            btn.Disposed   += Btn_Disposed;

            // Apply normal color immediately (no flash on first render)
            btn.BackColor = normalColor;

            EnsureTimerRunning();
        }

        /// <summary>
        /// Remove animation from a button and restore normal color.
        /// </summary>
        public static void Detach(Button btn)
        {
            if (btn == null) return;

            btn.MouseEnter -= Btn_MouseEnter;
            btn.MouseLeave -= Btn_MouseLeave;
            btn.MouseDown  -= Btn_MouseDown;
            btn.MouseUp    -= Btn_MouseUp;
            btn.Disposed   -= Btn_Disposed;

            if (_states.ContainsKey(btn))
            {
                btn.BackColor = _states[btn].NormalColor;
                _states.Remove(btn);
            }

            if (_states.Count == 0)
                _timer.Stop();
        }

        // ── Timer tick ────────────────────────────────────────────────────────

        private static void OnTick(object sender, EventArgs e)
        {
            if (_states.Count == 0)
            {
                _timer.Stop();
                return;
            }

            // Collect keys to avoid modifying dict while iterating
            Button[] buttons = new Button[_states.Count];
            _states.Keys.CopyTo(buttons, 0);

            foreach (Button btn in buttons)
            {
                if (btn == null || btn.IsDisposed)
                {
                    _states.Remove(btn);
                    continue;
                }

                HoverState s = _states[btn];

                // Determine target color
                Color target = s.IsDown ? s.DownColor
                             : s.IsHovering ? s.HoverColor
                             : s.NormalColor;

                float tr = target.R;
                float tg = target.G;
                float tb = target.B;

                // Lerp current toward target
                s.R += (tr - s.R) * LerpStep;
                s.G += (tg - s.G) * LerpStep;
                s.B += (tb - s.B) * LerpStep;

                // Clamp and apply
                int r = Clamp((int)Math.Round(s.R));
                int g = Clamp((int)Math.Round(s.G));
                int b = Clamp((int)Math.Round(s.B));

                try
                {
                    btn.BackColor = Color.FromArgb(r, g, b);
                }
                catch (ObjectDisposedException)
                {
                    _states.Remove(btn);
                }
            }

            if (_states.Count == 0)
                _timer.Stop();
        }

        // ── Mouse event handlers ──────────────────────────────────────────────

        private static void Btn_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn && _states.TryGetValue(btn, out HoverState s))
                s.IsHovering = true;
        }

        private static void Btn_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && _states.TryGetValue(btn, out HoverState s))
            {
                s.IsHovering = false;
                s.IsDown = false;
            }
        }

        private static void Btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left &&
                sender is Button btn && _states.TryGetValue(btn, out HoverState s))
                s.IsDown = true;
        }

        private static void Btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is Button btn && _states.TryGetValue(btn, out HoverState s))
                s.IsDown = false;
        }

        private static void Btn_Disposed(object sender, EventArgs e)
        {
            if (sender is Button btn)
                _states.Remove(btn);

            if (_states.Count == 0)
                _timer.Stop();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static void EnsureTimerRunning()
        {
            if (!_timer.Enabled)
                _timer.Start();
        }

        private static int Clamp(int value) =>
            value < 0 ? 0 : value > 255 ? 255 : value;
    }
}
