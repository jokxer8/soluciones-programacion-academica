using System;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class MainForm : Form
    {
        private Panel panelMenu;
        private Panel panelContenido;
        private Panel panelHeader;
        private PictureBox picLogo;
        private Label lblMenuTitle;
        private Label lblBienvenida;
        private Label lblRol;

        private Button btnClientes;
        private Button btnHabitaciones;
        private Button btnReservas;
        private Button btnFacturas;
        private Button btnReportes;
        private Button btnAdministracion;
        private Button btnMantenimiento;
        private Button btnConfiguracion;
        private Button btnCerrarSesion;
        private Button btnNotificaciones;
        private Label lblBadgeNotif;
        private Button btnTema;
        private Timer _timerNotificaciones;

        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblCreditos;
        private ToolStripStatusLabel lblSpacerIzq;
        private ToolStripStatusLabel lblSpacerDer;

        public bool CerrarSesionSolicitada { get; private set; }

        private Timer timerSidebar;
        private Timer _slideInTimer;

        // ── Session inactivity timeout ─────────────────────────────────────
        private Timer _timerInactividad;
        private IMessageFilter _inactividadFilter;
        private EventHandler _themeHandler;
        private Timer _timerContadorAviso;
        private const int InactividadMs    = 15 * 60 * 1000; // 15 minutes
        private const int AvisoMs          = 60 * 1000;       // 1 minute warning
        private int _segundosRestantes     = 60;
        private bool _avisoVisible         = false;

        public MainForm()
        {
            SesionUsuario.ExigirAcceso("Operaciones");
            InicializarComponentes();

            // Actualizar tema en respuesta al toggle sin reabrir el formulario
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;

            // ✅ NO registrar animación - los paneles cubren todo el fondo
            // AnimationEngine.Register(this);  // ❌ REMOVIDO

            IniciarTimerInactividad();

            // Intercept all mouse/keyboard at application level to reset inactivity
            _inactividadFilter = new InactividadMessageFilter(ResetearInactividad);
            Application.AddMessageFilter(_inactividadFilter);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Sidebar slide-in animation
        // ═════════════════════════════════════════════════════════════════════
        private void StartSidebarAnimation()
        {
            panelMenu.Width = 0;
            timerSidebar = new Timer { Interval = 10 };
            timerSidebar.Tick += (s, e) =>
            {
                int remaining = 240 - panelMenu.Width;
                if (remaining <= 3)
                {
                    panelMenu.Width = 240;
                    timerSidebar.Stop();
                    timerSidebar.Dispose();
                }
                else
                {
                    panelMenu.Width += Math.Max(2, remaining / 3);
                }
            };
            timerSidebar.Start();
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Build UI
        // ═════════════════════════════════════════════════════════════════════
        private void InicializarComponentes()
        {
            Text = "Sistema de Reservas - Panel Principal";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Font = Theme.AppFont;

            try { Icon = AppLogo.GetWindowIcon(); } catch { }

            this.Load += MainForm_Load;

            // ── Status bar ────────────────────────────────────────────────────
            statusStrip = new StatusStrip();
            statusStrip.Dock = DockStyle.Bottom;
            statusStrip.SizingGrip = false;
            statusStrip.Stretch = true;

            lblSpacerIzq = new ToolStripStatusLabel { Spring = true, Text = "" };

            lblCreditos = new ToolStripStatusLabel();
            lblCreditos.Text = "Desarrollado por: Kelvin Del Castillo, Gabriel Galasso y Camila Vasquez";
            lblCreditos.Font = Theme.AppFontBold;
            lblCreditos.TextAlign = ContentAlignment.MiddleCenter;

            lblSpacerDer = new ToolStripStatusLabel { Spring = true, Text = "" };

            statusStrip.Items.Add(lblSpacerIzq);
            statusStrip.Items.Add(lblCreditos);
            statusStrip.Items.Add(lblSpacerDer);

            // ── Left sidebar ──────────────────────────────────────────────────
            panelMenu = new Panel();
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Width = 240;
            panelMenu.Paint += PanelMenu_Paint;
            Theme.SetDoubleBuffered(panelMenu, true);

            // ── Sidebar header ────────────────────────────────────────────────
            panelHeader = new Panel();
            panelHeader.Size = new Size(240, 130);
            panelHeader.Dock = DockStyle.Top;

            picLogo = new PictureBox();
            picLogo.Size = new Size(40, 40);
            picLogo.Location = new Point(12, 10);
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.BackColor = Color.Transparent;
            picLogo.Image = AppLogo.GetLogoImage(40);
            picLogo.Tag = "logo";

            lblMenuTitle = new Label();
            lblMenuTitle.Text = "HOTEL RESERVAS";
            lblMenuTitle.Font = Theme.AppFontBold;
            lblMenuTitle.AutoSize = false;
            lblMenuTitle.Size = new Size(174, 24);
            lblMenuTitle.Location = new Point(58, 15);
            lblMenuTitle.TextAlign = ContentAlignment.MiddleLeft;

            Panel headerSep = new Panel();
            headerSep.Size = new Size(216, 1);
            headerSep.Location = new Point(12, 58);
            headerSep.BackColor = Color.FromArgb(80, 255, 255, 255);
            headerSep.Tag = "skip";

            lblBienvenida = new Label();
            lblBienvenida.Text = SesionUsuario.NombreCompleto;
            lblBienvenida.Font = Theme.AppFontBold;
            lblBienvenida.Location = new Point(12, 65);
            lblBienvenida.Size = new Size(216, 20);
            lblBienvenida.AutoSize = false;
            lblBienvenida.TextAlign = ContentAlignment.MiddleLeft;

            lblRol = new Label();
            lblRol.Text = "Rol: " + SesionUsuario.Rol;
            lblRol.Font = Theme.AppFontSmall;
            lblRol.Size = new Size(216, 18);
            lblRol.Location = new Point(12, 89);
            lblRol.AutoSize = false;

            panelHeader.Controls.Add(picLogo);
            panelHeader.Controls.Add(lblMenuTitle);
            panelHeader.Controls.Add(headerSep);
            panelHeader.Controls.Add(lblBienvenida);
            panelHeader.Controls.Add(lblRol);

            // ── Nav buttons ───────────────────────────────────────────────────
            int btnY = 138;
            const int btnStep = 46;

            btnClientes = CrearBotonMenu("  Clientes", btnY); btnY += btnStep;
            btnHabitaciones = CrearBotonMenu("  Habitaciones", btnY); btnY += btnStep;
            btnReservas = CrearBotonMenu("  Reservas", btnY); btnY += btnStep;
            btnFacturas = CrearBotonMenu("  Facturas", btnY); btnY += btnStep;
            btnReportes = CrearBotonMenu("  Informes / Rendimiento", btnY); btnY += btnStep;
            btnAdministracion = CrearBotonMenu("  Administracion", btnY); btnY += btnStep;
            btnMantenimiento = CrearBotonMenu("  Mantenimiento", btnY); btnY += btnStep;
            btnConfiguracion = CrearBotonMenu("  Configuracion", btnY); btnY += btnStep;
            btnNotificaciones = CrearBotonMenu("  Notificaciones", btnY); btnY += btnStep;
            btnCerrarSesion = CrearBotonMenu("  Cerrar sesion", btnY); btnY += btnStep;

            // Badge label — shows unread count on top of notif button
            lblBadgeNotif = new Label();
            lblBadgeNotif.Size = new Size(20, 20);
            lblBadgeNotif.Location = new Point(180, btnNotificaciones.Top + 4);
            lblBadgeNotif.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblBadgeNotif.TextAlign = ContentAlignment.MiddleCenter;
            lblBadgeNotif.BackColor = Color.OrangeRed;
            lblBadgeNotif.ForeColor = Color.White;
            lblBadgeNotif.Visible = false;

            btnClientes.Click += (s, e) => AbrirFormularioEnPanel(new ClientesForm());
            btnHabitaciones.Click += (s, e) => AbrirFormularioEnPanel(new HabitacionesForm());
            btnReservas.Click += (s, e) => AbrirFormularioEnPanel(new ReservasForm());
            btnFacturas.Click += (s, e) => AbrirFormularioEnPanel(new FacturasForm());
            btnReportes.Click += (s, e) => { using (ReportesForm frm = new ReportesForm()) frm.ShowDialog(); };
            btnAdministracion.Click += (s, e) => AbrirFormularioEnPanel(new AdministracionForm());
            btnMantenimiento.Click += (s, e) => AbrirFormularioEnPanel(new MantenimientoForm());
            btnConfiguracion.Click += (s, e) => { using (ConfiguracionForm frm = new ConfiguracionForm()) frm.ShowDialog(); };
            btnNotificaciones.Click += (s, e) => AbrirFormularioEnPanel(new NotificacionesForm());
            btnCerrarSesion.Click += BtnCerrarSesion_Click;

            // ── Theme toggle ──────────────────────────────────────────────────
            btnTema = new Button();
            btnTema.Size = new Size(200, 34);
            btnTema.Location = new Point(20, btnY + 14);
            btnTema.Font = Theme.AppFontBold;
            btnTema.FlatStyle = FlatStyle.Flat;
            btnTema.FlatAppearance.BorderSize = 1;
            btnTema.Cursor = Cursors.Hand;
            btnTema.TextAlign = ContentAlignment.MiddleCenter;
            btnTema.Click += (s, e) => Theme.Toggle();
            UIAnimations.AttachHover(btnTema, Theme.AccentBackground, Theme.AccentHover, Theme.AccentDown);

            // ── Content area ──────────────────────────────────────────────────
            panelContenido = new Panel();
            panelContenido.Dock = DockStyle.Fill;
            // ✅ Fondo sólido normal - WinForms no soporta transparencia en Panel
            panelContenido.BackColor = Theme.ContentBackground;

            panelMenu.Controls.Add(panelHeader);
            panelMenu.Controls.Add(btnClientes);
            panelMenu.Controls.Add(btnHabitaciones);
            panelMenu.Controls.Add(btnReservas);
            panelMenu.Controls.Add(btnFacturas);
            panelMenu.Controls.Add(btnReportes);
            panelMenu.Controls.Add(btnAdministracion);
            panelMenu.Controls.Add(btnMantenimiento);
            panelMenu.Controls.Add(btnConfiguracion);
            panelMenu.Controls.Add(btnNotificaciones);
            panelMenu.Controls.Add(lblBadgeNotif);
            panelMenu.Controls.Add(btnCerrarSesion);
            panelMenu.Controls.Add(btnTema);

            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            Controls.Add(statusStrip);

            ApplyTheme();
        }

        private void PanelMenu_Paint(object sender, PaintEventArgs e)
        {
            int x = panelMenu.Width - 2;
            using (Pen pen = new Pen(Theme.SidebarBorder, 2))
                e.Graphics.DrawLine(pen, x, 0, x, panelMenu.Height);
        }

        private Button CrearBotonMenu(string texto, int top)
        {
            Button btn = new Button();
            btn.Text = texto;
            btn.Size = new Size(200, 38);
            btn.Location = new Point(20, top);
            btn.Font = Theme.AppFontBold;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;

            btn.BackColor = Theme.SidebarBackground;
            btn.ForeColor = Theme.SidebarButtonText;
            btn.FlatAppearance.MouseOverBackColor = Theme.SidebarButtonHover;
            btn.FlatAppearance.MouseDownBackColor = Theme.SidebarButtonDown;

            UIAnimations.AttachHover(btn, Theme.SidebarBackground, Theme.SidebarButtonHover, Theme.SidebarButtonDown);

            return btn;
        }

        private void ApplyTheme()
        {
            BackColor = Theme.FormBackground;

            panelMenu.BackColor = Theme.SidebarBackground;
            panelMenu.Invalidate();

            panelHeader.BackColor = Theme.AccentBackground;
            lblMenuTitle.ForeColor = Theme.AccentText;
            lblBienvenida.ForeColor = Color.FromArgb(220, 255, 255, 255);
            lblRol.ForeColor = Color.FromArgb(160, 255, 255, 255);

            Button[] navBtns = { btnClientes, btnHabitaciones, btnReservas,
                                  btnFacturas, btnReportes, btnAdministracion, btnMantenimiento,
                                  btnConfiguracion, btnNotificaciones, btnCerrarSesion };
            foreach (Button b in navBtns)
            {
                b.BackColor = Theme.SidebarBackground;
                b.ForeColor = Theme.SidebarButtonText;
                b.FlatAppearance.MouseOverBackColor = Theme.SidebarButtonHover;
                b.FlatAppearance.MouseDownBackColor = Theme.SidebarButtonDown;
                // Re-attach animation with updated theme colors
                UIAnimations.AttachHover(b, Theme.SidebarBackground, Theme.SidebarButtonHover, Theme.SidebarButtonDown);
            }

            btnTema.Text = Theme.ToggleLabel;
            btnTema.BackColor = Theme.AccentBackground;
            btnTema.ForeColor = Theme.AccentText;
            btnTema.FlatAppearance.BorderColor = Theme.AccentBorder;
            btnTema.FlatAppearance.MouseOverBackColor = Theme.AccentHover;
            btnTema.FlatAppearance.MouseDownBackColor = Theme.AccentDown;
            UIAnimations.AttachHover(btnTema, Theme.AccentBackground, Theme.AccentHover, Theme.AccentDown);

            // ✅ Fondo sólido normal
            panelContenido.BackColor = Theme.ContentBackground;

            statusStrip.BackColor = Theme.StatusBarBackground;
            lblCreditos.ForeColor = Theme.StatusBarText;
        }

        private bool EsAdmin() => string.Equals(SesionUsuario.Rol, "ADMIN", StringComparison.OrdinalIgnoreCase);
        private bool EsSoporte() => string.Equals(SesionUsuario.Rol, "SOPORTE", StringComparison.OrdinalIgnoreCase);
        private bool EsEmpleado() => string.Equals(SesionUsuario.Rol, "EMPLEADO", StringComparison.OrdinalIgnoreCase);
        private bool EsRecepcion() => string.Equals(SesionUsuario.Rol, "RECEPCION", StringComparison.OrdinalIgnoreCase);

        private void ConfigurarSegunRol()
        {
            // Informes/Rendimiento: solo ADMIN
            btnReportes.Visible = SesionUsuario.PuedeAcceder("Reportes");

            // Administración: ADMIN y SOPORTE
            btnAdministracion.Visible = SesionUsuario.PuedeAcceder("Administracion");

            // Mantenimiento: ADMIN y SOPORTE
            btnMantenimiento.Visible = SesionUsuario.PuedeAcceder("Mantenimiento");

            // Configuracion: solo ADMIN
            btnConfiguracion.Visible = SesionUsuario.PuedeAcceder("Configuracion");

            // EMPLEADO y RECEPCION: módulos base únicamente
            if (EsEmpleado() || EsRecepcion())
            {
                btnReportes.Visible = false;
                btnAdministracion.Visible = false;
                btnMantenimiento.Visible = false;
                btnConfiguracion.Visible = false;
            }
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "¿Desea cerrar la sesión?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                FinalizarSesion();
            }
        }

        private void AbrirFormularioEnPanel(Form formulario)
        {
            // Cancelar animación previa si está en curso
            if (_slideInTimer != null)
            {
                _slideInTimer.Stop();
                _slideInTimer.Dispose();
                _slideInTimer = null;
            }

            // Descartar formularios hijos anteriores
            foreach (Control c in panelContenido.Controls)
            {
                try { c.Dispose(); } catch { }
            }
            panelContenido.Controls.Clear();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.BackColor = Theme.ContentBackground;

            // Posicionar ligeramente a la derecha para el slide-in
            int pw = panelContenido.ClientSize.Width;
            int ph = panelContenido.ClientSize.Height;
            formulario.Bounds = new Rectangle(40, 0, pw, ph);

            panelContenido.Controls.Add(formulario);
            formulario.Show();

            // Ease-out slide: 40px → 0 con ~9 pasos a 12ms = ~110ms
            _slideInTimer = new Timer { Interval = 12 };
            _slideInTimer.Tick += (s, e) =>
            {
                if (formulario.IsDisposed)
                {
                    _slideInTimer.Stop();
                    _slideInTimer.Dispose();
                    _slideInTimer = null;
                    return;
                }
                int left = formulario.Left;
                if (left <= 1)
                {
                    formulario.Left = 0;
                    formulario.Dock = DockStyle.Fill;
                    _slideInTimer.Stop();
                    _slideInTimer.Dispose();
                    _slideInTimer = null;
                }
                else
                {
                    formulario.Left -= Math.Max(1, left / 3);
                }
            };
            _slideInTimer.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigurarSegunRol();

            // Ensure new tables exist
            try { NotificacionData.EnsureTableExists(); } catch { }
            try { RatingData.EnsureTableExists(); } catch { }

            // Start notification badge polling (every 60 seconds)
            ActualizarBadgeNotificaciones();
            _timerNotificaciones = new Timer { Interval = 60 * 1000 };
            _timerNotificaciones.Tick += (s, ev) => ActualizarBadgeNotificaciones();
            _timerNotificaciones.Start();

            AbrirFormularioEnPanel(new DashboardForm());
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Notification badge (F2.1)
        // ═════════════════════════════════════════════════════════════════════
        private void ActualizarBadgeNotificaciones()
        {
            try
            {
                if (SesionUsuario.IdUsuario <= 0) return;
                int noLeidas = NotificacionData.ContarNoLeidas(SesionUsuario.IdUsuario);

                if (lblBadgeNotif.InvokeRequired)
                {
                    lblBadgeNotif.Invoke(new Action(() => MostrarBadge(noLeidas)));
                }
                else
                {
                    MostrarBadge(noLeidas);
                }
            }
            catch { /* Silently ignore — badge is non-critical */ }
        }

        private void MostrarBadge(int noLeidas)
        {
            if (noLeidas > 0)
            {
                lblBadgeNotif.Text = noLeidas > 99 ? "99+" : noLeidas.ToString();
                lblBadgeNotif.Visible = true;
            }
            else
            {
                lblBadgeNotif.Visible = false;
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Inactivity timeout (F2.5)
        // ═════════════════════════════════════════════════════════════════════
        private void IniciarTimerInactividad()
        {
            _timerInactividad = new Timer { Interval = InactividadMs };
            _timerInactividad.Tick += TimerInactividad_Tick;
            _timerInactividad.Start();
        }

        public void ResetearInactividad()
        {
            if (_avisoVisible) return; // Don't reset during warning countdown

            _timerInactividad?.Stop();
            _timerInactividad?.Start();
        }

        private void TimerInactividad_Tick(object sender, EventArgs e)
        {
            _timerInactividad.Stop();
            MostrarAvisoInactividad();
        }

        private void MostrarAvisoInactividad()
        {
            if (_avisoVisible) return;
            _avisoVisible = true;
            _segundosRestantes = 60;

            Form frmAviso = new Form();
            frmAviso.Text = "Sesion a punto de cerrar";
            frmAviso.StartPosition = FormStartPosition.CenterScreen;
            frmAviso.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmAviso.MaximizeBox = false;
            frmAviso.MinimizeBox = false;
            frmAviso.Size = new System.Drawing.Size(420, 200);
            frmAviso.TopMost = true;
            frmAviso.BackColor = Theme.FormBackground;

            Label lblMsg = new Label();
            lblMsg.Text = "Su sesion se cerrara por inactividad en 60 segundos.";
            lblMsg.Font = Theme.AppFontBold;
            lblMsg.AutoSize = false;
            lblMsg.Size = new System.Drawing.Size(380, 50);
            lblMsg.Location = new System.Drawing.Point(15, 20);
            lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            Label lblCuentaRegresiva = new Label();
            lblCuentaRegresiva.Text = "60";
            lblCuentaRegresiva.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            lblCuentaRegresiva.AutoSize = false;
            lblCuentaRegresiva.Size = new System.Drawing.Size(380, 60);
            lblCuentaRegresiva.Location = new System.Drawing.Point(15, 70);
            lblCuentaRegresiva.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblCuentaRegresiva.ForeColor = System.Drawing.Color.OrangeRed;

            Button btnContinuar = new Button();
            btnContinuar.Text = "Continuar sesion";
            btnContinuar.Location = new System.Drawing.Point(120, 140);
            btnContinuar.Size = new System.Drawing.Size(160, 34);
            btnContinuar.FlatStyle = FlatStyle.Flat;
            btnContinuar.Font = Theme.AppFontBold;
            btnContinuar.Cursor = Cursors.Hand;

            frmAviso.Controls.Add(lblMsg);
            frmAviso.Controls.Add(lblCuentaRegresiva);
            frmAviso.Controls.Add(btnContinuar);

            _timerContadorAviso = new Timer { Interval = 1000 };
            _timerContadorAviso.Tick += (s, ev) =>
            {
                _segundosRestantes--;
                lblCuentaRegresiva.Text = _segundosRestantes.ToString();

                if (_segundosRestantes <= 0)
                {
                    _timerContadorAviso.Stop();
                    _timerContadorAviso.Dispose();
                    _timerContadorAviso = null;
                    frmAviso.DialogResult = DialogResult.Cancel;
                    frmAviso.Close();
                }
            };

            btnContinuar.Click += (s, ev) =>
            {
                _timerContadorAviso?.Stop();
                _timerContadorAviso?.Dispose();
                _timerContadorAviso = null;
                frmAviso.DialogResult = DialogResult.OK;
                frmAviso.Close();
            };

            frmAviso.FormClosed += (s, ev) =>
            {
                _timerContadorAviso?.Stop();
                _timerContadorAviso?.Dispose();
                _timerContadorAviso = null;
                _avisoVisible = false;
            };

            _timerContadorAviso.Start();
            DialogResult resultado = frmAviso.ShowDialog(this);
            frmAviso.Dispose();
            // Cerrar al propietario después de que termine el diálogo evita
            // la reentrada de FormClosed al expirar el aviso de inactividad.
            if (resultado == DialogResult.OK) _timerInactividad?.Start();
            else if (!IsDisposed) FinalizarSesion();
        }

        private void FinalizarSesion()
        {
            SesionUsuario.Cerrar();
            CerrarSesionSolicitada = true;
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_inactividadFilter != null) Application.RemoveMessageFilter(_inactividadFilter);
                if (_themeHandler != null) Theme.OnThemeChanged -= _themeHandler;
                timerSidebar?.Dispose();
                _slideInTimer?.Dispose();
                _timerInactividad?.Dispose();
                _timerContadorAviso?.Dispose();
                _timerNotificaciones?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}