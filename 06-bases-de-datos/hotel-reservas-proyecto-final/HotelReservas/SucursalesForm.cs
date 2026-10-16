using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class SucursalesForm : Form
    {
        private Label lblTitulo;
        private Label lblNombreSucursal;
        private Label lblCiudad;
        private Label lblDireccion;
        private Label lblTelefono;
        private Label lblEmail;
        private Label lblBuscar;

        private TextBox txtNombreSucursal;
        private TextBox txtCiudad;
        private TextBox txtDireccion;
        private TextBox txtTelefono;
        private TextBox txtEmail;
        private TextBox txtBuscar;

        private CheckBox chkEstado;

        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvSucursales;

        private int idSucursalSeleccionada = 0;

        private EventHandler _themeHandler;

        public SucursalesForm()
        {
            // ✅ PRIMERO inicializar componentes
            InicializarComponentes();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            // ✅ DESPUÉS registrar animación
            AnimationEngine.Register(this);

            Load += SucursalesForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
            if (dgvSucursales != null) Theme.ApplyToGrid(dgvSucursales);
            TextBox[] inputs = { txtNombreSucursal, txtCiudad, txtDireccion, txtTelefono, txtEmail, txtBuscar };
            foreach (TextBox t in inputs) { if (t != null) Theme.ApplyTextBox(t); }
            Button[] botones = { btnNuevo, btnGuardar, btnEditar, btnEliminar, btnBuscar, btnLimpiar };
            foreach (Button b in botones) { if (b != null) Theme.ApplyButton(b); }
            if (lblTitulo != null) lblTitulo.ForeColor = Theme.TextPrimary;
        }

        private void SucursalesForm_Load(object sender, EventArgs e) => CargarSucursales();

        private void InicializarComponentes()
        {
            Text = "Módulo de sucursales";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1050, 650);
            MaximizeBox = false;

            lblTitulo = new Label { Text = "Módulo de sucursales", Font = new Font("Segoe UI", 16F, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) };
            lblNombreSucursal = new Label { Text = "Nombre sucursal", Location = new Point(25, 70) };
            txtNombreSucursal = new TextBox { Location = new Point(25, 95), Width = 250 };
            lblCiudad = new Label { Text = "Ciudad", Location = new Point(300, 70) };
            txtCiudad = new TextBox { Location = new Point(300, 95), Width = 180 };
            lblDireccion = new Label { Text = "Dirección", Location = new Point(505, 70) };
            txtDireccion = new TextBox { Location = new Point(505, 95), Width = 260 };
            lblTelefono = new Label { Text = "Teléfono", Location = new Point(25, 140) };
            txtTelefono = new TextBox { Location = new Point(25, 165), Width = 180 };
            lblEmail = new Label { Text = "Email", Location = new Point(230, 140) };
            txtEmail = new TextBox { Location = new Point(230, 165), Width = 250 };
            chkEstado = new CheckBox { Text = "Sucursal activa", Location = new Point(505, 165), AutoSize = true, Checked = true };

            btnNuevo = CrearBotonBlanco("Nuevo", 25, 220); btnNuevo.Click += BtnNuevo_Click;
            btnGuardar = CrearBotonBlanco("Guardar", 125, 220); btnGuardar.Click += BtnGuardar_Click;
            btnEditar = CrearBotonBlanco("Editar", 225, 220); btnEditar.Click += BtnEditar_Click;
            btnEliminar = CrearBotonBlanco("Eliminar", 325, 220); btnEliminar.Click += BtnEliminar_Click;

            lblBuscar = new Label { Text = "Buscar", Location = new Point(520, 200) };
            txtBuscar = new TextBox { Location = new Point(520, 225), Width = 220 };
            btnBuscar = CrearBotonBlanco("Buscar", 755, 220); btnBuscar.Click += BtnBuscar_Click;
            btnLimpiar = CrearBotonBlanco("Limpiar", 855, 220); btnLimpiar.Click += BtnLimpiar_Click;

            dgvSucursales = new DataGridView { Location = new Point(25, 280), Size = new Size(980, 300), AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None, ScrollBars = ScrollBars.Both, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, RowHeadersWidth = 25 };
            dgvSucursales.CellClick += DgvSucursales_CellClick;

            Controls.AddRange(new Control[] { lblTitulo, lblNombreSucursal, txtNombreSucursal, lblCiudad, txtCiudad, lblDireccion, txtDireccion, lblTelefono, txtTelefono, lblEmail, txtEmail, chkEstado, btnNuevo, btnGuardar, btnEditar, btnEliminar, lblBuscar, txtBuscar, btnBuscar, btnLimpiar, dgvSucursales });
            ApplyTheme();
        }

        private Button CrearBotonBlanco(string texto, int x, int y, int ancho = 90, int alto = 32)
        {
            Button btn = new Button { Text = texto, Location = new Point(x, y), Size = new Size(ancho, alto), FlatStyle = FlatStyle.Flat, UseVisualStyleBackColor = false, Cursor = Cursors.Hand, Font = Theme.AppFont };
            Theme.ApplyButton(btn);
            return btn;
        }

        private void CargarSucursales()
        {
            try { dgvSucursales.DataSource = Database.ObtenerSucursalesAdmin(); ConfigurarColumnasGrid(); dgvSucursales.ClearSelection(); }
            catch (Exception ex) { MessageBox.Show("Error al cargar sucursales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ConfigurarColumnasGrid()
        {
            if (dgvSucursales.Columns.Count == 0) return;
            dgvSucursales.Columns["IdSucursal"].Width = 80;
            dgvSucursales.Columns["NombreSucursal"].Width = 180;
            dgvSucursales.Columns["Ciudad"].Width = 120;
            dgvSucursales.Columns["Direccion"].Width = 240;
            dgvSucursales.Columns["Telefono"].Width = 120;
            dgvSucursales.Columns["Email"].Width = 180;
            dgvSucursales.Columns["Estado"].Visible = false;
            dgvSucursales.Columns["EstadoTexto"].Width = 100;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreSucursal.Text)) { MessageBox.Show("Debe escribir el nombre de la sucursal."); txtNombreSucursal.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtCiudad.Text)) { MessageBox.Show("Debe escribir la ciudad."); txtCiudad.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtDireccion.Text)) { MessageBox.Show("Debe escribir la dirección."); txtDireccion.Focus(); return false; }
            return true;
        }

        private void LimpiarCampos() { idSucursalSeleccionada = 0; txtNombreSucursal.Clear(); txtCiudad.Clear(); txtDireccion.Clear(); txtTelefono.Clear(); txtEmail.Clear(); txtBuscar.Clear(); chkEstado.Checked = true; dgvSucursales.ClearSelection(); txtNombreSucursal.Focus(); }
        private void BtnNuevo_Click(object sender, EventArgs e) => LimpiarCampos();

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try { Database.InsertarSucursal(txtNombreSucursal.Text.Trim(), txtCiudad.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtEmail.Text.Trim(), chkEstado.Checked); MessageBox.Show("Sucursal guardada correctamente.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information); CargarSucursales(); LimpiarCampos(); }
            catch (Exception ex) { MessageBox.Show("Error al guardar sucursal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idSucursalSeleccionada == 0) { MessageBox.Show("Debe seleccionar una sucursal para editar."); return; }
            if (!ValidarCampos()) return;
            try { Database.ActualizarSucursal(idSucursalSeleccionada, txtNombreSucursal.Text.Trim(), txtCiudad.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtEmail.Text.Trim(), chkEstado.Checked); MessageBox.Show("Sucursal actualizada correctamente.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information); CargarSucursales(); LimpiarCampos(); }
            catch (Exception ex) { MessageBox.Show("Error al editar sucursal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idSucursalSeleccionada == 0) { MessageBox.Show("Debe seleccionar una sucursal para eliminar."); return; }
            if (MessageBox.Show("¿Está seguro de inactivar esta sucursal?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try { Database.EliminarSucursal(idSucursalSeleccionada); MessageBox.Show("Sucursal inactivada correctamente.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information); CargarSucursales(); LimpiarCampos(); }
            catch (Exception ex) { MessageBox.Show("Error al eliminar sucursal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try { string texto = txtBuscar.Text.Trim(); if (string.IsNullOrWhiteSpace(texto)) { CargarSucursales(); return; } dgvSucursales.DataSource = Database.BuscarSucursales(texto); ConfigurarColumnasGrid(); }
            catch (Exception ex) { MessageBox.Show("Error al buscar sucursales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e) { LimpiarCampos(); CargarSucursales(); }

        private void DgvSucursales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvSucursales.CurrentRow == null) return;
            DataGridViewRow fila = dgvSucursales.CurrentRow;
            idSucursalSeleccionada = Convert.ToInt32(fila.Cells["IdSucursal"].Value);
            txtNombreSucursal.Text = fila.Cells["NombreSucursal"].Value?.ToString();
            txtCiudad.Text = fila.Cells["Ciudad"].Value?.ToString();
            txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();
            txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString();
            txtEmail.Text = fila.Cells["Email"].Value?.ToString();
            chkEstado.Checked = Convert.ToBoolean(fila.Cells["Estado"].Value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Theme.OnThemeChanged -= _themeHandler;
                AnimationEngine.Unregister(this);
            }
            base.Dispose(disposing);
        }
    }
}