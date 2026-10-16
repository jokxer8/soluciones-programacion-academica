using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    /// <summary>
    /// Panel form that shows notifications for the current user.
    /// Loaded into MainForm.panelContenido.
    /// </summary>
    public class NotificacionesForm : Form
    {
        private Label lblTitulo;
        private Label lblContador;
        private DataGridView dgvNotificaciones;
        private Button btnMarcarLeida;
        private Button btnMarcarTodas;
        private Button btnRefrescar;

        // Pagination
        private Button btnAnterior;
        private Button btnSiguiente;
        private Label lblPagina;
        private int _paginaActual = 0;
        private const int TamPagina = 50;

        private EventHandler _themeHandler;

        public NotificacionesForm()
        {
            InicializarComponentes();
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
            ApplyTheme();
            Load += NotificacionesForm_Load;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Theme.OnThemeChanged -= _themeHandler;
            base.Dispose(disposing);
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
        }

        private void NotificacionesForm_Load(object sender, EventArgs e)
        {
            CargarNotificaciones();
        }

        private void InicializarComponentes()
        {
            AutoScroll = true;

            lblTitulo = new Label();
            lblTitulo.Text = "Notificaciones";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblContador = new Label();
            lblContador.AutoSize = true;
            lblContador.Font = Theme.AppFontBold;
            lblContador.Location = new Point(200, 30);

            btnRefrescar = new Button();
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Location = new Point(20, 60);
            btnRefrescar.Size = new Size(110, 30);
            btnRefrescar.FlatStyle = FlatStyle.Flat;
            btnRefrescar.Cursor = Cursors.Hand;
            btnRefrescar.Click += (s, e) => { _paginaActual = 0; CargarNotificaciones(); };

            btnMarcarLeida = new Button();
            btnMarcarLeida.Text = "Marcar leida";
            btnMarcarLeida.Location = new Point(140, 60);
            btnMarcarLeida.Size = new Size(120, 30);
            btnMarcarLeida.FlatStyle = FlatStyle.Flat;
            btnMarcarLeida.Cursor = Cursors.Hand;
            btnMarcarLeida.Click += BtnMarcarLeida_Click;

            btnMarcarTodas = new Button();
            btnMarcarTodas.Text = "Marcar todas leidas";
            btnMarcarTodas.Location = new Point(270, 60);
            btnMarcarTodas.Size = new Size(160, 30);
            btnMarcarTodas.FlatStyle = FlatStyle.Flat;
            btnMarcarTodas.Cursor = Cursors.Hand;
            btnMarcarTodas.Click += BtnMarcarTodas_Click;

            dgvNotificaciones = new DataGridView();
            dgvNotificaciones.Location = new Point(20, 110);
            dgvNotificaciones.Size = new Size(960, 480);
            dgvNotificaciones.AllowUserToAddRows = false;
            dgvNotificaciones.AllowUserToDeleteRows = false;
            dgvNotificaciones.ReadOnly = true;
            dgvNotificaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvNotificaciones.ScrollBars = ScrollBars.Both;
            dgvNotificaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNotificaciones.MultiSelect = false;
            dgvNotificaciones.RowHeadersWidth = 25;

            // Pagination controls
            btnAnterior = new Button();
            btnAnterior.Text = "< Anterior";
            btnAnterior.Location = new Point(20, 605);
            btnAnterior.Size = new Size(100, 30);
            btnAnterior.FlatStyle = FlatStyle.Flat;
            btnAnterior.Cursor = Cursors.Hand;
            btnAnterior.Click += (s, e) => { if (_paginaActual > 0) { _paginaActual--; CargarNotificaciones(); } };

            lblPagina = new Label();
            lblPagina.Text = "Pag. 1";
            lblPagina.AutoSize = true;
            lblPagina.Font = Theme.AppFontBold;
            lblPagina.Location = new Point(130, 611);

            btnSiguiente = new Button();
            btnSiguiente.Text = "Siguiente >";
            btnSiguiente.Location = new Point(200, 605);
            btnSiguiente.Size = new Size(100, 30);
            btnSiguiente.FlatStyle = FlatStyle.Flat;
            btnSiguiente.Cursor = Cursors.Hand;
            btnSiguiente.Click += (s, e) => { _paginaActual++; CargarNotificaciones(); };

            Controls.Add(lblTitulo);
            Controls.Add(lblContador);
            Controls.Add(btnRefrescar);
            Controls.Add(btnMarcarLeida);
            Controls.Add(btnMarcarTodas);
            Controls.Add(dgvNotificaciones);
            Controls.Add(btnAnterior);
            Controls.Add(lblPagina);
            Controls.Add(btnSiguiente);
        }

        private void CargarNotificaciones()
        {
            try
            {
                int idUsuario = SesionUsuario.IdUsuario;
                DataTable dt = NotificacionData.ObtenerPorUsuario(idUsuario, _paginaActual, TamPagina);
                dgvNotificaciones.DataSource = dt;
                ConfigurarColumnas();

                int noLeidas = NotificacionData.ContarNoLeidas(idUsuario);
                lblContador.Text = noLeidas > 0
                    ? "(" + noLeidas + " sin leer)"
                    : "(todas leidas)";
                lblContador.ForeColor = noLeidas > 0 ? Color.OrangeRed : Color.Green;

                lblPagina.Text = "Pag. " + (_paginaActual + 1);
                btnAnterior.Enabled = _paginaActual > 0;
                btnSiguiente.Enabled = dt.Rows.Count == TamPagina;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar notificaciones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvNotificaciones.Columns.Count == 0) return;

            if (dgvNotificaciones.Columns.Contains("IdNotificacion"))
                dgvNotificaciones.Columns["IdNotificacion"].Width = 80;
            if (dgvNotificaciones.Columns.Contains("Tipo"))
                dgvNotificaciones.Columns["Tipo"].Width = 100;
            if (dgvNotificaciones.Columns.Contains("Titulo"))
                dgvNotificaciones.Columns["Titulo"].Width = 200;
            if (dgvNotificaciones.Columns.Contains("Mensaje"))
                dgvNotificaciones.Columns["Mensaje"].Width = 350;
            if (dgvNotificaciones.Columns.Contains("Leida"))
                dgvNotificaciones.Columns["Leida"].Width = 60;
            if (dgvNotificaciones.Columns.Contains("FechaCreacion"))
                dgvNotificaciones.Columns["FechaCreacion"].Width = 140;
            if (dgvNotificaciones.Columns.Contains("FechaLectura"))
                dgvNotificaciones.Columns["FechaLectura"].Width = 140;

            // Highlight unread rows
            foreach (DataGridViewRow row in dgvNotificaciones.Rows)
            {
                if (row.DataGridView.Columns.Contains("Leida"))
                {
                    object leidaVal = row.Cells["Leida"].Value;
                    bool leida = leidaVal != null && leidaVal != DBNull.Value && Convert.ToBoolean(leidaVal);
                    if (!leida)
                    {
                        row.DefaultCellStyle.BackColor = Theme.Current == AppTheme.Dark
                            ? Color.FromArgb(60, 50, 20)
                            : Color.FromArgb(255, 248, 220);
                        row.DefaultCellStyle.ForeColor = Theme.Current == AppTheme.Dark
                            ? Color.FromArgb(255, 220, 100)
                            : Color.FromArgb(130, 90, 0);
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                }
            }
        }

        private void BtnMarcarLeida_Click(object sender, EventArgs e)
        {
            if (dgvNotificaciones.CurrentRow == null) return;
            if (dgvNotificaciones.Columns.Count == 0) return;

            if (!dgvNotificaciones.Columns.Contains("IdNotificacion")) return;

            object idVal = dgvNotificaciones.CurrentRow.Cells["IdNotificacion"].Value;
            if (idVal == null || idVal == DBNull.Value) return;

            try
            {
                NotificacionData.MarcarLeida(Convert.ToInt32(idVal));
                CargarNotificaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMarcarTodas_Click(object sender, EventArgs e)
        {
            try
            {
                NotificacionData.MarcarTodasLeidas(SesionUsuario.IdUsuario);
                CargarNotificaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
