using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class ConfiguracionForm : Form
    {
        private Label lblTitulo;
        private Label lblClave;
        private Label lblValor;
        private Label lblDescripcion;

        private TextBox txtClave;
        private TextBox txtValor;
        private TextBox txtDescripcion;

        private Button btnGuardar;
        private Button btnLimpiar;

        private DataGridView dgvConfiguracion;

        private EventHandler _themeHandler;

        public ConfiguracionForm()
        {
            InicializarComponentes();
            Load += ConfiguracionForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
            Theme.ApplyToGrid(dgvConfiguracion);
            Theme.ApplyTextBox(txtValor);
            if (txtClave != null)
            {
                txtClave.BackColor = Theme.InputBackground;
                txtClave.ForeColor = Theme.TextSecondary;
            }
            if (txtDescripcion != null)
            {
                txtDescripcion.BackColor = Theme.InputBackground;
                txtDescripcion.ForeColor = Theme.TextSecondary;
            }
            Theme.ApplyAccentButton(btnGuardar);
            Theme.ApplyButton(btnLimpiar);
            if (lblTitulo != null) lblTitulo.ForeColor = Theme.TextPrimary;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Theme.OnThemeChanged -= _themeHandler;
            base.Dispose(disposing);
        }

        private void ConfiguracionForm_Load(object sender, EventArgs e)
        {
            if (!SesionUsuario.EsAdmin())
            {
                MessageBox.Show("Solo los administradores pueden acceder a la configuración del sistema.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            CargarConfiguracion();
        }

        private void InicializarComponentes()
        {
            Text = "Configuración del sistema";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 600);
            MinimumSize = new Size(700, 500);

            lblTitulo = new Label();
            lblTitulo.Text = "Configuración del sistema";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblClave = new Label();
            lblClave.Text = "Clave";
            lblClave.Location = new Point(20, 70);

            txtClave = new TextBox();
            txtClave.Location = new Point(20, 92);
            txtClave.Width = 180;
            txtClave.ReadOnly = true;
            txtClave.BackColor = Theme.InputBackground;

            lblValor = new Label();
            lblValor.Text = "Valor";
            lblValor.Location = new Point(220, 70);

            txtValor = new TextBox();
            txtValor.Location = new Point(220, 92);
            txtValor.Width = 200;

            lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción";
            lblDescripcion.Location = new Point(440, 70);

            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(440, 92);
            txtDescripcion.Width = 280;
            txtDescripcion.ReadOnly = true;
            txtDescripcion.BackColor = Theme.InputBackground;

            btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new Point(20, 130);
            btnGuardar.Size = new Size(90, 30);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Font = Theme.AppFont;
            btnGuardar.BackColor = Theme.AccentBackground;
            btnGuardar.ForeColor = Theme.AccentText;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;

            btnLimpiar = new Button();
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Location = new Point(125, 130);
            btnLimpiar.Size = new Size(90, 30);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.Font = Theme.AppFont;
            btnLimpiar.BackColor = Theme.ButtonBackground;
            btnLimpiar.ForeColor = Theme.ButtonText;
            btnLimpiar.FlatAppearance.BorderColor = Theme.ButtonBorder;
            btnLimpiar.Click += BtnLimpiar_Click;

            dgvConfiguracion = new DataGridView();
            dgvConfiguracion.Location = new Point(20, 180);
            dgvConfiguracion.Size = new Size(740, 350);
            dgvConfiguracion.AllowUserToAddRows = false;
            dgvConfiguracion.AllowUserToDeleteRows = false;
            dgvConfiguracion.ReadOnly = true;
            dgvConfiguracion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConfiguracion.MultiSelect = false;
            dgvConfiguracion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvConfiguracion.RowHeadersVisible = false;
            dgvConfiguracion.CellClick += DgvConfiguracion_CellClick;

            Controls.Add(lblTitulo);
            Controls.Add(lblClave);
            Controls.Add(txtClave);
            Controls.Add(lblValor);
            Controls.Add(txtValor);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(btnGuardar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvConfiguracion);

            ApplyTheme();
        }

        private void CargarConfiguracion()
        {
            try
            {
                DataTable dt = Database.ObtenerTodasConfiguraciones();
                dgvConfiguracion.DataSource = dt;
                dgvConfiguracion.ApplyStandard();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando configuración: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvConfiguracion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvConfiguracion.Rows[e.RowIndex];

            txtClave.Text = row.Cells["Clave"].Value == DBNull.Value
                ? "" : row.Cells["Clave"].Value.ToString();

            txtValor.Text = row.Cells["Valor"].Value == DBNull.Value
                ? "" : row.Cells["Valor"].Value.ToString();

            txtDescripcion.Text = row.Cells["Descripcion"].Value == DBNull.Value
                ? "" : row.Cells["Descripcion"].Value.ToString();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!SesionUsuario.EsAdmin())
            {
                MessageBox.Show("Solo los administradores pueden modificar la configuración.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Seleccione un parámetro de la lista.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("El valor no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Database.ActualizarConfiguracion(txtClave.Text.Trim(), txtValor.Text.Trim());
                Database.RegistrarAccion("UPDATE", "ConfiguracionSistema", null,
                    txtClave.Text + " = " + txtValor.Text);
                CargarConfiguracion();
                MessageBox.Show("Configuración actualizada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtClave.Clear();
            txtValor.Clear();
            txtDescripcion.Clear();
        }
    }
}
