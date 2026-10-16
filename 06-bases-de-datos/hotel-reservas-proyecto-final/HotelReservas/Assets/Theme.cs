using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelReservas.Assets
{
    public enum AppTheme
    {
        Light,
        Dark
    }

    public static class Theme
    {
        private static AppTheme _current = AppTheme.Light;

        public static AppTheme Current
        {
            get => _current;
            private set => _current = value;
        }

        public static event EventHandler OnThemeChanged;

        // ═══════════════════════════════════════════════════════════════════
        //  Fuentes
        // ═══════════════════════════════════════════════════════════════════

        public static readonly Font AppFont          = new Font("Segoe UI", 9F);
        public static readonly Font AppFontBold      = new Font("Segoe UI", 9F, FontStyle.Bold);
        public static readonly Font AppFontSmall     = new Font("Segoe UI", 8F);
        public static readonly Font AppFontLarge     = new Font("Segoe UI", 11F);
        public static readonly Font AppFontLargeBold = new Font("Segoe UI", 11F, FontStyle.Bold);

        // ═══════════════════════════════════════════════════════════════════
        //  Paleta TEMA CLARO
        // ═══════════════════════════════════════════════════════════════════

        private static class Light
        {
            // Fondos
            public static readonly Color FormBackground    = Color.FromArgb(247, 248, 252);
            public static readonly Color PanelBackground   = Color.FromArgb(237, 240, 250);
            public static readonly Color ContentBackground = Color.FromArgb(255, 255, 255);

            // Acento / Primario
            public static readonly Color AccentBackground  = Color.FromArgb(37, 99, 235);
            public static readonly Color AccentHover       = Color.FromArgb(59, 117, 245);
            public static readonly Color AccentDown        = Color.FromArgb(29, 82, 215);
            public static readonly Color AccentText        = Color.White;
            public static readonly Color AccentBorder      = Color.FromArgb(37, 99, 235);
            public static readonly Color AccentLabel       = Color.FromArgb(20, 30, 65);

            // Botones secundarios
            public static readonly Color ButtonBackground  = Color.FromArgb(235, 238, 250);
            public static readonly Color ButtonHover       = Color.FromArgb(218, 222, 242);
            public static readonly Color ButtonDown        = Color.FromArgb(200, 205, 230);
            public static readonly Color ButtonText        = Color.FromArgb(35, 42, 70);
            public static readonly Color ButtonBorder      = Color.FromArgb(198, 204, 228);

            // Texto
            public static readonly Color TextPrimary       = Color.FromArgb(15, 20, 45);
            public static readonly Color TextSecondary     = Color.FromArgb(82, 94, 130);

            // Inputs
            public static readonly Color InputBackground   = Color.FromArgb(255, 255, 255);
            public static readonly Color InputForeground   = Color.FromArgb(15, 20, 45);
            public static readonly Color InputBorder       = Color.FromArgb(198, 204, 228);
            public static readonly Color ComboBackground   = Color.FromArgb(255, 255, 255);
            public static readonly Color ComboForeground   = Color.FromArgb(15, 20, 45);

            // Grid
            public static readonly Color GridHeaderBackground = Color.FromArgb(235, 238, 250);
            public static readonly Color GridLine             = Color.FromArgb(218, 222, 242);
            public static readonly Color GridSelectionBg      = Color.FromArgb(37, 99, 235);
            public static readonly Color GridSelectionText    = Color.White;

            // Status bar
            public static readonly Color StatusBarBackground  = Color.FromArgb(235, 238, 250);
            public static readonly Color StatusBarText        = Color.FromArgb(82, 94, 130);

            // Sidebar — siempre oscuro en ambos temas
            public static readonly Color SidebarBackground   = Color.FromArgb(22, 27, 46);
            public static readonly Color SidebarBorder       = Color.FromArgb(37, 99, 235);
            public static readonly Color SidebarButtonText   = Color.FromArgb(195, 207, 235);
            public static readonly Color SidebarButtonHover  = Color.FromArgb(34, 41, 68);
            public static readonly Color SidebarButtonDown   = Color.FromArgb(46, 55, 88);
        }

        // ═══════════════════════════════════════════════════════════════════
        //  Paleta TEMA OSCURO
        // ═══════════════════════════════════════════════════════════════════

        private static class Dark
        {
            // Fondos
            public static readonly Color FormBackground    = Color.FromArgb(13, 15, 26);
            public static readonly Color PanelBackground   = Color.FromArgb(18, 22, 36);
            public static readonly Color ContentBackground = Color.FromArgb(22, 27, 46);

            // Acento / Primario
            public static readonly Color AccentBackground  = Color.FromArgb(37, 99, 235);
            public static readonly Color AccentHover       = Color.FromArgb(59, 117, 245);
            public static readonly Color AccentDown        = Color.FromArgb(29, 82, 215);
            public static readonly Color AccentText        = Color.White;
            public static readonly Color AccentBorder      = Color.FromArgb(59, 117, 245);
            public static readonly Color AccentLabel       = Color.FromArgb(175, 198, 255);

            // Botones secundarios
            public static readonly Color ButtonBackground  = Color.FromArgb(30, 36, 60);
            public static readonly Color ButtonHover       = Color.FromArgb(40, 48, 78);
            public static readonly Color ButtonDown        = Color.FromArgb(52, 62, 98);
            public static readonly Color ButtonText        = Color.FromArgb(195, 207, 235);
            public static readonly Color ButtonBorder      = Color.FromArgb(48, 58, 90);

            // Texto
            public static readonly Color TextPrimary       = Color.FromArgb(222, 228, 248);
            public static readonly Color TextSecondary     = Color.FromArgb(135, 150, 188);

            // Inputs
            public static readonly Color InputBackground   = Color.FromArgb(25, 30, 52);
            public static readonly Color InputForeground   = Color.FromArgb(215, 222, 245);
            public static readonly Color InputBorder       = Color.FromArgb(48, 58, 90);
            public static readonly Color ComboBackground   = Color.FromArgb(25, 30, 52);
            public static readonly Color ComboForeground   = Color.FromArgb(215, 222, 245);

            // Grid
            public static readonly Color GridHeaderBackground = Color.FromArgb(30, 36, 60);
            public static readonly Color GridLine             = Color.FromArgb(40, 48, 78);
            public static readonly Color GridSelectionBg      = Color.FromArgb(59, 117, 245);
            public static readonly Color GridSelectionText    = Color.White;

            // Status bar
            public static readonly Color StatusBarBackground  = Color.FromArgb(13, 15, 26);
            public static readonly Color StatusBarText        = Color.FromArgb(108, 120, 158);

            // Sidebar
            public static readonly Color SidebarBackground   = Color.FromArgb(14, 17, 30);
            public static readonly Color SidebarBorder       = Color.FromArgb(37, 99, 235);
            public static readonly Color SidebarButtonText   = Color.FromArgb(182, 195, 228);
            public static readonly Color SidebarButtonHover  = Color.FromArgb(26, 32, 54);
            public static readonly Color SidebarButtonDown   = Color.FromArgb(36, 44, 72);
        }

        // ═══════════════════════════════════════════════════════════════════
        //  Propiedades dinámicas
        // ═══════════════════════════════════════════════════════════════════

        public static Color FormBackground    => _current == AppTheme.Light ? Light.FormBackground    : Dark.FormBackground;
        public static Color PanelBackground   => _current == AppTheme.Light ? Light.PanelBackground   : Dark.PanelBackground;
        public static Color ContentBackground => _current == AppTheme.Light ? Light.ContentBackground : Dark.ContentBackground;

        public static Color AccentBackground  => _current == AppTheme.Light ? Light.AccentBackground  : Dark.AccentBackground;
        public static Color AccentHover       => _current == AppTheme.Light ? Light.AccentHover       : Dark.AccentHover;
        public static Color AccentDown        => _current == AppTheme.Light ? Light.AccentDown        : Dark.AccentDown;
        public static Color AccentText        => _current == AppTheme.Light ? Light.AccentText        : Dark.AccentText;
        public static Color AccentBorder      => _current == AppTheme.Light ? Light.AccentBorder      : Dark.AccentBorder;
        public static Color AccentLabel       => _current == AppTheme.Light ? Light.AccentLabel       : Dark.AccentLabel;

        public static Color ButtonBackground  => _current == AppTheme.Light ? Light.ButtonBackground  : Dark.ButtonBackground;
        public static Color ButtonHover       => _current == AppTheme.Light ? Light.ButtonHover       : Dark.ButtonHover;
        public static Color ButtonDown        => _current == AppTheme.Light ? Light.ButtonDown        : Dark.ButtonDown;
        public static Color ButtonText        => _current == AppTheme.Light ? Light.ButtonText        : Dark.ButtonText;
        public static Color ButtonBorder      => _current == AppTheme.Light ? Light.ButtonBorder      : Dark.ButtonBorder;

        public static Color TextPrimary       => _current == AppTheme.Light ? Light.TextPrimary       : Dark.TextPrimary;
        public static Color TextSecondary     => _current == AppTheme.Light ? Light.TextSecondary     : Dark.TextSecondary;

        public static Color InputBackground   => _current == AppTheme.Light ? Light.InputBackground   : Dark.InputBackground;
        public static Color InputForeground   => _current == AppTheme.Light ? Light.InputForeground   : Dark.InputForeground;
        public static Color InputBorder       => _current == AppTheme.Light ? Light.InputBorder       : Dark.InputBorder;
        public static Color ComboBackground   => _current == AppTheme.Light ? Light.ComboBackground   : Dark.ComboBackground;
        public static Color ComboForeground   => _current == AppTheme.Light ? Light.ComboForeground   : Dark.ComboForeground;

        public static Color GridHeaderBackground => _current == AppTheme.Light ? Light.GridHeaderBackground : Dark.GridHeaderBackground;
        public static Color GridLine             => _current == AppTheme.Light ? Light.GridLine             : Dark.GridLine;
        public static Color GridSelectionBg      => _current == AppTheme.Light ? Light.GridSelectionBg      : Dark.GridSelectionBg;
        public static Color GridSelectionText    => _current == AppTheme.Light ? Light.GridSelectionText    : Dark.GridSelectionText;

        public static Color StatusBarBackground  => _current == AppTheme.Light ? Light.StatusBarBackground  : Dark.StatusBarBackground;
        public static Color StatusBarText        => _current == AppTheme.Light ? Light.StatusBarText        : Dark.StatusBarText;

        public static Color SidebarBackground    => _current == AppTheme.Light ? Light.SidebarBackground    : Dark.SidebarBackground;
        public static Color SidebarBorder        => _current == AppTheme.Light ? Light.SidebarBorder        : Dark.SidebarBorder;
        public static Color SidebarButtonText    => _current == AppTheme.Light ? Light.SidebarButtonText    : Dark.SidebarButtonText;
        public static Color SidebarButtonHover   => _current == AppTheme.Light ? Light.SidebarButtonHover   : Dark.SidebarButtonHover;
        public static Color SidebarButtonDown    => _current == AppTheme.Light ? Light.SidebarButtonDown    : Dark.SidebarButtonDown;

        public static string ToggleLabel => _current == AppTheme.Light ? "☀ Modo Claro" : "🌙 Modo Oscuro";

        // ═══════════════════════════════════════════════════════════════════
        //  Toggle
        // ═══════════════════════════════════════════════════════════════════

        public static void Toggle()
        {
            Current = Current == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
            OnThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        // ═══════════════════════════════════════════════════════════════════
        //  Helpers de aplicación
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>Applies base theme (background + foreground) to a Form and all its input controls.</summary>
        public static void Apply(Form form)
        {
            if (form == null) return;
            try
            {
                form.BackColor = FormBackground;
                form.ForeColor = TextPrimary;
                ApplyRecursive(form);
            }
            catch { }
        }

        /// <summary>Alias for Apply(Form) — backwards compatibility.</summary>
        public static void ApplyToForm(Form form) => Apply(form);

        public static void SetTitleBarTheme(Form form)
        {
            // Reserved — DWM title bar theming not applied (avoids rendering issues on .NET 4.8)
        }

        /// <summary>Styles a sidebar / navigation button (always-dark sidebar context).</summary>
        public static void ApplyMenuButton(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = SidebarBackground;
            btn.ForeColor = SidebarButtonText;
            btn.FlatAppearance.MouseOverBackColor = SidebarButtonHover;
            btn.FlatAppearance.MouseDownBackColor = SidebarButtonDown;
        }

        /// <summary>Styles a primary CTA (accent) button.</summary>
        public static void ApplyAccentButton(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = AccentBackground;
            btn.ForeColor = AccentText;
            btn.FlatAppearance.MouseOverBackColor = AccentHover;
            btn.FlatAppearance.MouseDownBackColor = AccentDown;
        }

        /// <summary>Styles a secondary (ghost) button.</summary>
        public static void ApplyButton(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = ButtonBackground;
            btn.ForeColor = ButtonText;
            btn.FlatAppearance.BorderColor = ButtonBorder;
            btn.FlatAppearance.MouseOverBackColor = ButtonHover;
            btn.FlatAppearance.MouseDownBackColor = ButtonDown;
        }

        /// <summary>Styles a content-area panel (white / dark surface).</summary>
        public static void ApplyContentPanel(Panel panel)
        {
            if (panel == null) return;
            panel.BackColor = ContentBackground;
        }

        /// <summary>Styles a TextBox input.</summary>
        public static void ApplyTextBox(TextBox txt)
        {
            if (txt == null) return;
            txt.BackColor = InputBackground;
            txt.ForeColor = InputForeground;
        }

        /// <summary>Styles a ComboBox.</summary>
        public static void ApplyComboBox(ComboBox cbo)
        {
            if (cbo == null) return;
            cbo.BackColor = ComboBackground;
            cbo.ForeColor = ComboForeground;
        }

        /// <summary>
        /// Recursively walks all child controls and applies input-level theming.
        /// Targets only interactive input controls — containers and labels are left to
        /// their parent's BackColor to avoid clobbering custom panel colors.
        /// </summary>
        public static void ApplyRecursive(Control parent)
        {
            if (parent == null) return;
            foreach (Control c in parent.Controls)
            {
                try
                {
                    switch (c)
                    {
                        case TextBox txt:
                            txt.BackColor = InputBackground;
                            txt.ForeColor = InputForeground;
                            break;
                        case RichTextBox rtb:
                            rtb.BackColor = InputBackground;
                            rtb.ForeColor = InputForeground;
                            break;
                        case ComboBox cbo:
                            cbo.BackColor = ComboBackground;
                            cbo.ForeColor = ComboForeground;
                            break;
                        case NumericUpDown nud:
                            nud.BackColor = InputBackground;
                            nud.ForeColor = InputForeground;
                            break;
                        case DateTimePicker dtp:
                            dtp.BackColor = InputBackground;
                            dtp.ForeColor = InputForeground;
                            dtp.CalendarMonthBackground = InputBackground;
                            dtp.CalendarForeColor       = InputForeground;
                            dtp.CalendarTitleBackColor  = AccentBackground;
                            dtp.CalendarTitleForeColor  = AccentText;
                            break;
                        case CheckedListBox clb:
                            clb.BackColor = InputBackground;
                            clb.ForeColor = InputForeground;
                            break;
                        case ListBox lb:
                            lb.BackColor = InputBackground;
                            lb.ForeColor = InputForeground;
                            break;
                    }
                }
                catch { }

                if (c.HasChildren)
                    ApplyRecursive(c);
            }
        }

        // ═══════════════════════════════════════════════════════════════════
        //  Aplicar a Grid
        // ═══════════════════════════════════════════════════════════════════

        public static void ApplyToGrid(DataGridView dgv)
        {
            if (dgv == null || dgv.IsDisposed) return;

            try
            {
                dgv.BackgroundColor = FormBackground;
                dgv.GridColor = GridLine;
                dgv.ForeColor = TextPrimary;

                if (dgv.DefaultCellStyle != null)
                {
                    dgv.DefaultCellStyle.BackColor          = ContentBackground;
                    dgv.DefaultCellStyle.ForeColor          = TextPrimary;
                    dgv.DefaultCellStyle.SelectionBackColor = GridSelectionBg;
                    dgv.DefaultCellStyle.SelectionForeColor = GridSelectionText;
                    dgv.DefaultCellStyle.Font               = AppFont;
                }

                if (dgv.RowHeadersDefaultCellStyle != null)
                {
                    dgv.RowHeadersDefaultCellStyle.BackColor = GridHeaderBackground;
                    dgv.RowHeadersDefaultCellStyle.ForeColor = TextSecondary;
                }

                dgv.EnableHeadersVisualStyles = false;

                if (dgv.Columns != null)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (col == null) continue;

                        col.SortMode = DataGridViewColumnSortMode.NotSortable;

                        if (col.HeaderCell?.Style != null)
                        {
                            col.HeaderCell.Style.BackColor = GridHeaderBackground;
                            col.HeaderCell.Style.ForeColor = TextSecondary;
                            col.HeaderCell.Style.Font      = AppFontBold;
                        }

                        if (col.DefaultCellStyle != null)
                        {
                            col.DefaultCellStyle.BackColor = ContentBackground;
                            col.DefaultCellStyle.ForeColor = TextPrimary;
                        }
                    }
                }
            }
            catch { }
        }

        // ═══════════════════════════════════════════════════════════════════
        //  Utilidades
        // ═══════════════════════════════════════════════════════════════════

        public static void SetDoubleBuffered(Control control, bool value)
        {
            if (control == null || control.IsDisposed) return;
            try
            {
                control.GetType()
                    .GetProperty("DoubleBuffered",
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    ?.SetValue(control, value, null);
            }
            catch { }
        }
    }
}
