using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class ServiciosAdicionalesForm : Form
    {
        private Label lblTitulo;
        private Label lblNombre;
        private Label lblPrecio;
        private Label lblBuscar;

        private TextBox txtNombre;
        private TextBox txtPrecio;
        private TextBox txtBuscar;

        private CheckBox chkEstado;

        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvServicios;

        private int idServicioSeleccionado = 0;

        private EventHandler _themeHandler;

        public ServiciosAdicionalesForm()
        {
            // ✅ PRIMERO inicializar componentes
            InicializarComponentes();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            // ✅ DESPUÉS registrar animación
            AnimationEngine.Register(this);

            Load += ServiciosAdicionalesForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
            if (dgvServicios != null) Theme.ApplyToGrid(dgvServicios);
            if (txtNombre != null) Theme.ApplyTextBox(txtNombre);
            if (txtPrecio != null) Theme.ApplyTextBox(txtPrecio);
            if (txtBuscar != null) Theme.ApplyTextBox(txtBuscar);
            Button[] botones = { btnNuevo, btnGuardar, btnEditar, btnEliminar, btnBuscar, btnLimpiar };
            foreach (Button b in botones) { if (b != null) Theme.ApplyButton(b); }
            if (lblTitulo != null) lblTitulo.ForeColor = Theme.TextPrimary;
        }

        private void ServiciosAdicionalesForm_Load(object sender, EventArgs e) => CargarServicios();

        private void InicializarComponentes()
        {
            Text = "Servicios adicionales";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(900, 560);
            MaximizeBox = false;

            lblTitulo = new Label { Text = "Módulo de servicios adicionales", Font = new Font("Segoe UI", 16F, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) };
            lblNombre = new Label { Text = "Nombre del servicio", Location = new Point(25, 70) };
            txtNombre = new TextBox { Location = new Point(25, 95), Width = 260 };
            lblPrecio = new Label { Text = "Precio", Location = new Point(310, 70) };
            txtPrecio = new TextBox { Location = new Point(310, 95), Width = 140 };
            chkEstado = new CheckBox { Text = "Servicio activo", Location = new Point(480, 97), AutoSize = true, Checked = true };

            btnNuevo = CrearBotonBlanco("Nuevo", 25, 145); btnNuevo.Click += BtnNuevo_Click;
            btnGuardar = CrearBotonBlanco("Guardar", 125, 145); btnGuardar.Click += BtnGuardar_Click;
            btnEditar = CrearBotonBlanco("Editar", 225, 145); btnEditar.Click += BtnEditar_Click;
            btnEliminar = CrearBotonBlanco("Eliminar", 325, 145); btnEliminar.Click += BtnEliminar_Click;

            lblBuscar = new Label { Text = "Buscar", Location = new Point(470, 125) };
            txtBuscar = new TextBox { Location = new Point(470, 150), Width = 180 };
            btnBuscar = CrearBotonBlanco("Buscar", 670, 145, 60, 32); btnBuscar.Click += BtnBuscar_Click;
            btnLimpiar = CrearBotonBlanco("Limpiar", 740, 145, 60, 32); btnLimpiar.Click += BtnLimpiar_Click;

            dgvServicios = new DataGridView { Location = new Point(25, 205), Size = new Size(820, 280), AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None, ScrollBars = ScrollBars.Both, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, RowHeadersWidth = 25 };
            dgvServicios.CellClick += DgvServicios_CellClick;

            Controls.AddRange(new Control[] { lblTitulo, lblNombre, txtNombre, lblPrecio, txtPrecio, chkEstado, btnNuevo, btnGuardar, btnEditar, btnEliminar, lblBuscar, txtBuscar, btnBuscar, btnLimpiar, dgvServicios });
            ApplyTheme();
        }

        private Button CrearBotonBlanco(string texto, int x, int y, int ancho = 90, int alto = 32)
        {
            Button btn = new Button { Text = texto, Location = new Point(x, y), Size = new Size(ancho, alto), FlatStyle = FlatStyle.Flat, UseVisualStyleBackColor = false, Cursor = Cursors.Hand, Font = Theme.AppFont };
            Theme.ApplyButton(btn);
            return btn;
        }

        private void CargarServicios()
        {
            try { dgvServicios.DataSource = ReservasExtrasData.ObtenerServiciosAdicionalesAdmin(); ConfigurarGrid(); dgvServicios.ClearSelection(); }
            catch (Exception ex) { MessageBox.Show("Error al cargar servicios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ConfigurarGrid()
        {
            if (dgvServicios.Columns.Count == 0) return;
            dgvServicios.Columns["IdServicioAdicional"].Width = 110;
            dgvServicios.Columns["NombreServicio"].Width = 250;
            dgvServicios.Columns["Precio"].Width = 120;
            dgvServicios.Columns["Estado"].Visible = false;
            dgvServicios.Columns["EstadoTexto"].Width = 110;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { MessageBox.Show("Debe escribir el nombre del servicio."); txtNombre.Focus(); return false; }
            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || precio < 0) { MessageBox.Show("Debe escribir un precio válido."); txtPrecio.Focus(); return false; }
            return true;
        }

        private void LimpiarCampos() { idServicioSeleccionado = 0; txtNombre.Clear(); txtPrecio.Clear(); txtBuscar.Clear(); chkEstado.Checked = true; dgvServicios.ClearSelection(); txtNombre.Focus(); }
        private void BtnNuevo_Click(object sender, EventArgs e) => LimpiarCampos();

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try { ReservasExtrasData.InsertarServicioAdicional(txtNombre.Text.Trim(), Convert.ToDecimal(txtPrecio.Text.Trim()), chkEstado.Checked); MessageBox.Show("Servicio guardado correctamente.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information); CargarServicios(); LimpiarCampos(); }
            catch (Exception ex) { MessageBox.Show("Error al guardar servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idServicioSeleccionado == 0) { MessageBox.Show("Debe seleccionar un servicio para editar."); return; }
            if (!ValidarCampos()) return;
            try { ReservasExtrasData.ActualizarServicioAdicional(idServicioSeleccionado, txtNombre.Text.Trim(), Convert.ToDecimal(txtPrecio.Text.Trim()), chkEstado.Checked); MessageBox.Show("Servicio actualizado correctamente.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information); CargarServicios(); LimpiarCampos(); }
            catch (Exception ex) { MessageBox.Show("Error al editar servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idServicioSeleccionado == 0) { MessageBox.Show("Debe seleccionar un servicio para inactivar."); return; }
            if (MessageBox.Show("¿Está seguro de inactivar este servicio?", "Confirmar inactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try { ReservasExtrasData.EliminarServicioAdicional(idServicioSeleccionado); MessageBox.Show("Servicio inactivado correctamente.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information); CargarServicios(); LimpiarCampos(); }
            catch (Exception ex) { MessageBox.Show("Error al inactivar servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try { string texto = txtBuscar.Text.Trim(); if (string.IsNullOrWhiteSpace(texto)) { CargarServicios(); return; } dgvServicios.DataSource = ReservasExtrasData.BuscarServiciosAdicionales(texto); ConfigurarGrid(); }
            catch (Exception ex) { MessageBox.Show("Error al buscar servicios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e) { LimpiarCampos(); CargarServicios(); }

        private void DgvServicios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvServicios.CurrentRow == null) return;
            DataGridViewRow fila = dgvServicios.CurrentRow;
            idServicioSeleccionado = Convert.ToInt32(fila.Cells["IdServicioAdicional"].Value);
            txtNombre.Text = fila.Cells["NombreServicio"].Value?.ToString();
            txtPrecio.Text = fila.Cells["Precio"].Value?.ToString();
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