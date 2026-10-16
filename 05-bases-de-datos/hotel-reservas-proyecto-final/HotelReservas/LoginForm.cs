using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class LoginForm : Form
    {
        private Panel panelIzq;
        private Panel panelDer;

        private PictureBox picLogoLeft;
        private Label lblNombreHotel;
        private Label lblTagline;
        private Label lblVersion;

        private Label lblBienvenido;
        private Label lblSubtitulo;
        private Label lblUsuario;
        private Label lblClave;
        private TextBox txtUsuario;
        private TextBox txtClave;
        private CheckBox chkMostrarClave;
        private Button btnEntrar;
        private Button btnSalir;
        private Button btnTema;

        private Timer timerFade;
        private Timer timerSlide;
        private int slideOffsetY = 30;

        public LoginForm()
        {
            InicializarComponentes();

            // ✅ LOGINFORM: Sí puede tener animación porque panelDer no cubre TODO el fondo
            // El panelIzq tiene su propio Paint que dibuja el gradiente
            AnimationEngine.Register(this);

            Theme.OnThemeChanged += OnThemeChangedHandler;
        }

        private void InicializarComponentes()
        {
            Text = "Login - Hotel Reservas";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 520);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Font = Theme.AppFont;
            Opacity = 0;

            try { Icon = AppLogo.GetWindowIcon(); } catch { }

            btnTema = new Button();
            btnTema.Size = new Size(120, 28);
            btnTema.Location = new Point(Width - 138, 8);
            btnTema.FlatStyle = FlatStyle.Flat;
            btnTema.FlatAppearance.BorderSize = 1;
            btnTema.Font = new Font("Calibri", 8.5F, FontStyle.Bold);
            btnTema.Cursor = Cursors.Hand;
            btnTema.Click += (s, e) => Theme.Toggle();
            Controls.Add(btnTema);

            // ── LEFT PANEL (con Paint propio - no necesita animación externa) ──
            panelIzq = new Panel();
            panelIzq.Size = new Size(300, ClientSize.Height);
            panelIzq.Location = new Point(0, 0);
            panelIzq.Paint += PanelIzq_Paint;

            picLogoLeft = new PictureBox();
            picLogoLeft.Size = new Size(80, 80);
            picLogoLeft.Location = new Point(110, 80);
            picLogoLeft.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogoLeft.BackColor = Color.Transparent;
            picLogoLeft.Image = AppLogo.GetLogoImage(80);
            panelIzq.Controls.Add(picLogoLeft);

            lblNombreHotel = new Label();
            lblNombreHotel.Text = "HOTEL\nRESERVAS";
            lblNombreHotel.Font = new Font("Calibri", 18F, FontStyle.Bold);
            lblNombreHotel.Size = new Size(260, 70);
            lblNombreHotel.Location = new Point(20, 175);
            lblNombreHotel.TextAlign = ContentAlignment.MiddleCenter;
            lblNombreHotel.ForeColor = Color.White;
            lblNombreHotel.BackColor = Color.Transparent;
            panelIzq.Controls.Add(lblNombreHotel);

            lblTagline = new Label();
            lblTagline.Text = "Sistema de Gestión\nHotelera Profesional";
            lblTagline.Font = new Font("Calibri", 10F, FontStyle.Italic);
            lblTagline.Size = new Size(260, 50);
            lblTagline.Location = new Point(20, 265);
            lblTagline.TextAlign = ContentAlignment.MiddleCenter;
            lblTagline.ForeColor = Color.FromArgb(180, 210, 255);
            lblTagline.BackColor = Color.Transparent;
            panelIzq.Controls.Add(lblTagline);

            lblVersion = new Label();
            lblVersion.Text = "v2.0";
            lblVersion.Font = new Font("Calibri", 9F);
            lblVersion.Size = new Size(260, 20);
            lblVersion.Location = new Point(20, ClientSize.Height - 38);
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            lblVersion.ForeColor = Color.FromArgb(130, 165, 210);
            lblVersion.BackColor = Color.Transparent;
            panelIzq.Controls.Add(lblVersion);

            Controls.Add(panelIzq);

            // ── RIGHT PANEL (fondo sólido normal) ──
            panelDer = new Panel();
            panelDer.Location = new Point(300, 0);
            panelDer.Size = new Size(ClientSize.Width - 300, ClientSize.Height);
            panelDer.AutoScroll = false;
            // ✅ Fondo sólido - WinForms no soporta transparencia en Panel
            panelDer.BackColor = Theme.FormBackground;

            int cx = 40;
            int w = 340;

            lblBienvenido = new Label();
            lblBienvenido.Text = "Bienvenido";
            lblBienvenido.Font = new Font("Calibri", 22F, FontStyle.Bold);
            lblBienvenido.Size = new Size(w, 40);
            lblBienvenido.Location = new Point(cx, 70);
            lblBienvenido.BackColor = Color.Transparent;
            panelDer.Controls.Add(lblBienvenido);

            lblSubtitulo = new Label();
            lblSubtitulo.Text = "Inicia sesión en tu cuenta";
            lblSubtitulo.Font = new Font("Calibri", 10.5F);
            lblSubtitulo.Size = new Size(w, 22);
            lblSubtitulo.Location = new Point(cx, 116);
            lblSubtitulo.BackColor = Color.Transparent;
            panelDer.Controls.Add(lblSubtitulo);

            Panel sep = new Panel();
            sep.Size = new Size(w, 2);
            sep.Location = new Point(cx, 148);
            sep.Tag = "sep";
            panelDer.Controls.Add(sep);

            lblUsuario = new Label();
            lblUsuario.Text = "USUARIO";
            lblUsuario.Font = new Font("Calibri", 8.5F, FontStyle.Bold);
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(cx, 170);
            lblUsuario.BackColor = Color.Transparent;
            panelDer.Controls.Add(lblUsuario);

            txtUsuario = new TextBox();
            txtUsuario.Location = new Point(cx, 192);
            txtUsuario.Size = new Size(w, 30);
            txtUsuario.Font = new Font("Calibri", 11F);
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            panelDer.Controls.Add(txtUsuario);

            lblClave = new Label();
            lblClave.Text = "CONTRASEÑA";
            lblClave.Font = new Font("Calibri", 8.5F, FontStyle.Bold);
            lblClave.AutoSize = true;
            lblClave.Location = new Point(cx, 238);
            lblClave.BackColor = Color.Transparent;
            panelDer.Controls.Add(lblClave);

            txtClave = new TextBox();
            txtClave.Location = new Point(cx, 260);
            txtClave.Size = new Size(w, 30);
            txtClave.UseSystemPasswordChar = true;
            txtClave.Font = new Font("Calibri", 11F);
            txtClave.BorderStyle = BorderStyle.FixedSingle;
            panelDer.Controls.Add(txtClave);

            chkMostrarClave = new CheckBox();
            chkMostrarClave.Text = "Mostrar contraseña";
            chkMostrarClave.AutoSize = true;
            chkMostrarClave.Location = new Point(cx, 300);
            chkMostrarClave.Font = new Font("Calibri", 9F);
            chkMostrarClave.CheckedChanged += ChkMostrarClave_CheckedChanged;
            panelDer.Controls.Add(chkMostrarClave);

            btnEntrar = new Button();
            btnEntrar.Text = "INGRESAR";
            btnEntrar.Size = new Size(w, 46);
            btnEntrar.Location = new Point(cx, 338);
            btnEntrar.Font = new Font("Calibri", 11F, FontStyle.Bold);
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.FlatAppearance.BorderSize = 0;
            btnEntrar.Cursor = Cursors.Hand;
            btnEntrar.Click += BtnEntrar_Click;
            UIAnimations.AttachHover(btnEntrar, Theme.AccentBackground, Theme.AccentHover, Theme.AccentDown);
            panelDer.Controls.Add(btnEntrar);

            btnSalir = new Button();
            btnSalir.Text = "Salir";
            btnSalir.Size = new Size(w, 32);
            btnSalir.Location = new Point(cx, 396);
            btnSalir.Font = new Font("Calibri", 9.5F);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.FlatAppearance.BorderSize = 1;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.Click += BtnSalir_Click;
            UIAnimations.AttachHover(btnSalir, Theme.ButtonBackground, Theme.ButtonHover, Theme.ButtonDown);
            panelDer.Controls.Add(btnSalir);

            Controls.Add(panelDer);

            AcceptButton = btnEntrar;
            CancelButton = btnSalir;

            this.Load += LoginForm_Load;

            ApplyTheme();
        }

        private void PanelIzq_Paint(object sender, PaintEventArgs e)
        {
            Color c1 = Color.FromArgb(10, 25, 55);
            Color c2 = Color.FromArgb(20, 55, 110);

            using (LinearGradientBrush br = new LinearGradientBrush(
                panelIzq.ClientRectangle, c1, c2, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(br, panelIzq.ClientRectangle);
            }

            int cx = panelIzq.Width / 2;
            int cy = picLogoLeft.Top + picLogoLeft.Height / 2;
            int r = 52;
            using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(50, 255, 255, 255)))
                e.Graphics.FillEllipse(circleBrush, cx - r, cy - r, r * 2, r * 2);
            using (Pen circlePen = new Pen(Color.FromArgb(100, 255, 255, 255), 2))
                e.Graphics.DrawEllipse(circlePen, cx - r, cy - r, r * 2, r * 2);

            using (Pen dp = new Pen(Color.FromArgb(25, 255, 255, 255), 1))
            {
                e.Graphics.DrawEllipse(dp, -60, panelIzq.Height - 160, 200, 200);
                e.Graphics.DrawEllipse(dp, 100, panelIzq.Height - 80, 120, 120);
                e.Graphics.DrawEllipse(dp, 140, -40, 140, 140);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            StartAnimations();
        }

        private void StartAnimations()
        {
            timerFade = new Timer();
            timerFade.Interval = 14;
            timerFade.Tick += TimerFade_Tick;
            timerFade.Start();

            panelDer.Top = slideOffsetY;
            timerSlide = new Timer();
            timerSlide.Interval = 12;
            timerSlide.Tick += TimerSlide_Tick;
            timerSlide.Start();
        }

        private void TimerFade_Tick(object sender, EventArgs e)
        {
            double remaining = 1.0 - Opacity;
            if (remaining < 0.015)
            {
                Opacity = 1.0;
                timerFade.Stop();
                timerFade.Dispose();
            }
            else
            {
                Opacity += remaining * 0.14; // ease-out exponencial
            }
        }

        private void TimerSlide_Tick(object sender, EventArgs e)
        {
            if (panelDer.Top <= 0)
            {
                panelDer.Top = 0;
                timerSlide.Stop();
                timerSlide.Dispose();
            }
            else
            {
                panelDer.Top -= Math.Max(1, panelDer.Top / 3); // ease-out
            }
        }

        private void OnThemeChangedHandler(object sender, EventArgs e)
        {
            ApplyTheme();
            panelIzq.Invalidate();
        }

        private void ApplyTheme()
        {
            BackColor = Theme.FormBackground;
            Theme.SetTitleBarTheme(this);

            btnTema.Text = Theme.ToggleLabel;
            btnTema.BackColor = Theme.ButtonBackground;
            btnTema.ForeColor = Theme.ButtonText;
            btnTema.FlatAppearance.BorderColor = Theme.ButtonBorder;

            panelIzq.Invalidate();

            panelDer.BackColor = Theme.FormBackground;

            lblBienvenido.ForeColor = Theme.AccentLabel;
            lblSubtitulo.ForeColor = Theme.TextSecondary;
            lblUsuario.ForeColor = Theme.TextSecondary;
            lblClave.ForeColor = Theme.TextSecondary;

            foreach (Control c in panelDer.Controls)
                if (c is Panel sp && sp.Tag?.ToString() == "sep")
                    sp.BackColor = Theme.AccentLabel;

            txtUsuario.BackColor = Theme.InputBackground;
            txtUsuario.ForeColor = Theme.InputForeground;
            txtClave.BackColor = Theme.InputBackground;
            txtClave.ForeColor = Theme.InputForeground;

            chkMostrarClave.ForeColor = Theme.TextSecondary;
            chkMostrarClave.BackColor = Color.Transparent;

            btnEntrar.BackColor = Theme.AccentBackground;
            btnEntrar.ForeColor = Theme.AccentText;

            btnSalir.BackColor = Theme.ButtonBackground;
            btnSalir.ForeColor = Theme.ButtonText;
            btnSalir.FlatAppearance.BorderColor = Theme.ButtonBorder;
        }

        private void ChkMostrarClave_CheckedChanged(object sender, EventArgs e)
        {
            txtClave.UseSystemPasswordChar = !chkMostrarClave.Checked;
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show("Debe completar usuario y clave.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool acceso = Database.ValidarLogin(usuario, clave);

                if (!acceso)
                {
                    MessageBox.Show("Usuario o clave incorrectos.", "Acceso denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Clear();
                    txtClave.Focus();
                    return;
                }

                MainForm main = new MainForm();
                Hide();
                main.ShowDialog();

                if (main.CerrarSesionSolicitada)
                {
                    SesionUsuario.Cerrar();
                    LimpiarCampos();
                    Show();
                    txtUsuario.Focus();
                    return;
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtClave.Clear();
            chkMostrarClave.Checked = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timerFade?.Dispose();
                timerSlide?.Dispose();
                AnimationEngine.Unregister(this);
                Theme.OnThemeChanged -= OnThemeChangedHandler;
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load_1);
            this.ResumeLayout(false);

        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}