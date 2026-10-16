using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class HabitacionesForm : Form
    {
        private Label lblTitulo;
        private Label lblSucursal;
        private Label lblTipo;
        private Label lblEstado;
        private Label lblNumero;
        private Label lblPiso;
        private Label lblPrecio;
        private Label lblDescripcion;

        private ComboBox cboSucursal;
        private ComboBox cboTipo;
        private ComboBox cboEstado;
        private TextBox txtNumero;
        private TextBox txtPiso;
        private TextBox txtPrecio;
        private TextBox txtDescripcion;

        private Button btnNuevaSucursal;
        private Button btnNuevoTipo;

        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvHabitaciones;

        private Button btnPaginaAnterior;
        private Button btnPaginaSiguiente;
        private Label lblPaginaActual;
        private int _paginaActual = 0;
        private const int TamPagina = 50;
        private bool _modosBusqueda = false;

        private int idHabitacionSeleccionada = 0;

        private EventHandler _themeHandler;

        public HabitacionesForm()
        {
            InicializarComponentes();
            Load += HabitacionesForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Theme.OnThemeChanged -= _themeHandler;
            base.Dispose(disposing);
        }

        private void HabitacionesForm_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarHabitaciones();
            ConfigurarPermisosPorRol();
        }

        private void InicializarComponentes()
        {
            AutoScroll = true;

            lblTitulo = new Label();
            lblTitulo.Text = "Módulo de habitaciones";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblSucursal = new Label();
            lblSucursal.Text = "Sucursal";
            lblSucursal.Location = new Point(25, 70);

            cboSucursal = new ComboBox();
            cboSucursal.Location = new Point(25, 95);
            cboSucursal.Width = 180;
            cboSucursal.DropDownStyle = ComboBoxStyle.DropDownList;

            btnNuevaSucursal = CrearBotonBlanco("+", 212, 92, 25, 25);
            btnNuevaSucursal.Click += BtnNuevaSucursal_Click;

            lblTipo = new Label();
            lblTipo.Text = "Tipo";
            lblTipo.Location = new Point(275, 70);

            cboTipo = new ComboBox();
            cboTipo.Location = new Point(275, 95);
            cboTipo.Width = 170;
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;

            btnNuevoTipo = CrearBotonBlanco("+", 452, 92, 25, 25);
            btnNuevoTipo.Click += BtnNuevoTipo_Click;

            lblEstado = new Label();
            lblEstado.Text = "Estado";
            lblEstado.Location = new Point(515, 70);

            cboEstado = new ComboBox();
            cboEstado.Location = new Point(515, 95);
            cboEstado.Width = 180;
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            lblNumero = new Label();
            lblNumero.Text = "Número";
            lblNumero.Location = new Point(25, 135);

            txtNumero = new TextBox();
            txtNumero.Location = new Point(25, 160);
            txtNumero.Width = 120;

            lblPiso = new Label();
            lblPiso.Text = "Piso";
            lblPiso.Location = new Point(170, 135);

            txtPiso = new TextBox();
            txtPiso.Location = new Point(170, 160);
            txtPiso.Width = 100;

            lblPrecio = new Label();
            lblPrecio.Text = "Precio por noche";
            lblPrecio.Location = new Point(295, 135);

            txtPrecio = new TextBox();
            txtPrecio.Location = new Point(295, 160);
            txtPrecio.Width = 140;

            lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción";
            lblDescripcion.Location = new Point(25, 200);

            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(25, 225);
            txtDescripcion.Width = 670;

            btnNuevo = CrearBotonBlanco("Nuevo", 25, 275);
            btnNuevo.Click += BtnNuevo_Click;

            btnGuardar = CrearBotonBlanco("Guardar", 125, 275);
            btnGuardar.Click += BtnGuardar_Click;

            btnEditar = CrearBotonBlanco("Editar", 225, 275);
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = CrearBotonBlanco("Eliminar", 325, 275);
            btnEliminar.Click += BtnEliminar_Click;

            btnBuscar = CrearBotonBlanco("Buscar", 425, 275);
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBotonBlanco("Limpiar", 525, 275);
            btnLimpiar.Click += BtnLimpiar_Click;

            dgvHabitaciones = new DataGridView();
            dgvHabitaciones.Location = new Point(25, 330);
            dgvHabitaciones.Size = new Size(980, 270);
            dgvHabitaciones.AllowUserToAddRows = false;
            dgvHabitaciones.AllowUserToDeleteRows = false;
            dgvHabitaciones.ReadOnly = true;
            dgvHabitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvHabitaciones.ScrollBars = ScrollBars.Both;
            dgvHabitaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHabitaciones.MultiSelect = false;
            dgvHabitaciones.RowHeadersWidth = 25;
            dgvHabitaciones.CellClick += DgvHabitaciones_CellClick;

            Controls.Add(lblTitulo);
            Controls.Add(lblSucursal);
            Controls.Add(cboSucursal);
            Controls.Add(btnNuevaSucursal);
            Controls.Add(lblTipo);
            Controls.Add(cboTipo);
            Controls.Add(btnNuevoTipo);
            Controls.Add(lblEstado);
            Controls.Add(cboEstado);
            Controls.Add(lblNumero);
            Controls.Add(txtNumero);
            Controls.Add(lblPiso);
            Controls.Add(txtPiso);
            Controls.Add(lblPrecio);
            Controls.Add(txtPrecio);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvHabitaciones);

            btnPaginaAnterior = CrearBotonBlanco("< Anterior", 25, 610, 100, 30);
            btnPaginaAnterior.Click += (s, e) => {
                if (_paginaActual > 0 && !_modosBusqueda) { _paginaActual--; CargarHabitaciones(); }
            };

            lblPaginaActual = new Label();
            lblPaginaActual.Text = "Pag. 1";
            lblPaginaActual.AutoSize = true;
            lblPaginaActual.Font = Theme.AppFontBold;
            lblPaginaActual.Location = new Point(135, 616);

            btnPaginaSiguiente = CrearBotonBlanco("Siguiente >", 195, 610, 110, 30);
            btnPaginaSiguiente.Click += (s, e) => {
                if (!_modosBusqueda) { _paginaActual++; CargarHabitaciones(); }
            };

            Controls.Add(btnPaginaAnterior);
            Controls.Add(lblPaginaActual);
            Controls.Add(btnPaginaSiguiente);

            ApplyTheme();
        }

        private Button CrearBotonBlanco(string texto, int x, int y, int ancho = 90, int alto = 32)
        {
            Button btn = new Button();
            btn.Text = texto;
            btn.Location = new Point(x, y);
            btn.Size = new Size(ancho, alto);
            btn.FlatStyle = FlatStyle.Flat;
            btn.UseVisualStyleBackColor = false;
            btn.Cursor = Cursors.Hand;
            btn.Font = Theme.AppFont;
            Theme.ApplyButton(btn);
            return btn;
        }

        private bool EsAdmin()
        {
            return string.Equals(SesionUsuario.Rol, "ADMIN", StringComparison.OrdinalIgnoreCase);
        }

        private bool EsSoporte()
        {
            return string.Equals(SesionUsuario.Rol, "SOPORTE", StringComparison.OrdinalIgnoreCase);
        }

        private bool PuedeAdministrarCatalogos()
        {
            return EsAdmin() || EsSoporte();
        }

        private void ConfigurarPermisosPorRol()
        {
            bool puedeAdministrarCatalogos = PuedeAdministrarCatalogos();

            btnNuevaSucursal.Visible = puedeAdministrarCatalogos;
            btnNuevaSucursal.Enabled = puedeAdministrarCatalogos;

            btnNuevoTipo.Visible = puedeAdministrarCatalogos;
            btnNuevoTipo.Enabled = puedeAdministrarCatalogos;
        }

        private int? ObtenerValorCombo(ComboBox combo)
        {
            if (combo.SelectedValue == null)
                return null;

            if (combo.SelectedValue is DataRowView)
                return null;

            if (int.TryParse(combo.SelectedValue.ToString(), out int valor))
                return valor;

            return null;
        }

        private void CargarCombos(int? idSucursalSeleccionada = null, int? idTipoSeleccionado = null, int? idEstadoSeleccionado = null)
        {
            try
            {
                DataTable dtSucursales = Database.ObtenerSucursales();
                cboSucursal.DataSource = dtSucursales;
                cboSucursal.DisplayMember = "NombreSucursal";
                cboSucursal.ValueMember = "IdSucursal";

                DataTable dtTipos = Database.ObtenerTiposHabitacion();
                cboTipo.DataSource = dtTipos;
                cboTipo.DisplayMember = "NombreTipo";
                cboTipo.ValueMember = "IdTipoHabitacion";

                DataTable dtEstados = Database.ObtenerEstadosHabitacion();
                cboEstado.DataSource = dtEstados;
                cboEstado.DisplayMember = "NombreEstado";
                cboEstado.ValueMember = "IdEstadoHabitacion";

                if (idSucursalSeleccionada.HasValue)
                    cboSucursal.SelectedValue = idSucursalSeleccionada.Value;
                else
                    cboSucursal.SelectedIndex = -1;

                if (idTipoSeleccionado.HasValue)
                    cboTipo.SelectedValue = idTipoSeleccionado.Value;
                else
                    cboTipo.SelectedIndex = -1;

                if (idEstadoSeleccionado.HasValue)
                    cboEstado.SelectedValue = idEstadoSeleccionado.Value;
                else
                    cboEstado.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHabitaciones()
        {
            try
            {
                _modosBusqueda = false;
                DataTable dt = Database.ObtenerHabitacionesPaginado(_paginaActual, TamPagina);
                dgvHabitaciones.DataSource = dt;
                ConfigurarColumnasGrid();
                dgvHabitaciones.ClearSelection();

                lblPaginaActual.Text = "Pag. " + (_paginaActual + 1);
                btnPaginaAnterior.Enabled = _paginaActual > 0;
                btnPaginaSiguiente.Enabled = dt.Rows.Count == TamPagina;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar habitaciones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasGrid()
        {
            if (dgvHabitaciones.Columns.Count == 0)
                return;

            dgvHabitaciones.Columns["IdHabitacion"].Width = 90;
            dgvHabitaciones.Columns["IdSucursal"].Visible = false;
            dgvHabitaciones.Columns["NombreSucursal"].Width = 150;
            dgvHabitaciones.Columns["IdTipoHabitacion"].Visible = false;
            dgvHabitaciones.Columns["NombreTipo"].Width = 120;
            dgvHabitaciones.Columns["IdEstadoHabitacion"].Visible = false;
            dgvHabitaciones.Columns["NombreEstado"].Width = 120;
            dgvHabitaciones.Columns["Numero"].Width = 80;
            dgvHabitaciones.Columns["Piso"].Width = 70;
            dgvHabitaciones.Columns["PrecioPorNoche"].Width = 120;
            dgvHabitaciones.Columns["Descripcion"].Width = 250;
        }

        private bool ValidarCampos()
        {
            if (cboSucursal.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar la sucursal.");
                cboSucursal.Focus();
                return false;
            }

            if (cboTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el tipo de habitación.");
                cboTipo.Focus();
                return false;
            }

            if (cboEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el estado.");
                cboEstado.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Debe escribir el número de habitación.");
                txtNumero.Focus();
                return false;
            }

            if (!int.TryParse(txtPiso.Text.Trim(), out int piso) || piso <= 0)
            {
                MessageBox.Show("Debe escribir un piso válido.");
                txtPiso.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || precio < 0)
            {
                MessageBox.Show("Debe escribir un precio válido.");
                txtPrecio.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            idHabitacionSeleccionada = 0;
            cboSucursal.SelectedIndex = -1;
            cboTipo.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;
            txtNumero.Clear();
            txtPiso.Clear();
            txtPrecio.Clear();
            txtDescripcion.Clear();
            dgvHabitaciones.ClearSelection();
            txtNumero.Focus();
        }

        private void BtnNuevaSucursal_Click(object sender, EventArgs e)
        {
            if (!PuedeAdministrarCatalogos())
            {
                MessageBox.Show("No tiene permisos para crear sucursales.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idTipoActual = ObtenerValorCombo(cboTipo);
            int? idEstadoActual = ObtenerValorCombo(cboEstado);

            using (SucursalesForm frm = new SucursalesForm())
            {
                frm.ShowDialog(this);
            }

            CargarCombos(null, idTipoActual, idEstadoActual);
        }

        private void BtnNuevoTipo_Click(object sender, EventArgs e)
        {
            if (!PuedeAdministrarCatalogos())
            {
                MessageBox.Show("No tiene permisos para crear tipos de habitación.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idSucursalActual = ObtenerValorCombo(cboSucursal);
            int? idEstadoActual = ObtenerValorCombo(cboEstado);

            using (TiposHabitacionForm frm = new TiposHabitacionForm())
            {
                frm.ShowDialog(this);
            }

            CargarCombos(idSucursalActual, null, idEstadoActual);
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                Database.InsertarHabitacion(
                    Convert.ToInt32(cboSucursal.SelectedValue),
                    Convert.ToInt32(cboTipo.SelectedValue),
                    Convert.ToInt32(cboEstado.SelectedValue),
                    txtNumero.Text.Trim(),
                    Convert.ToInt32(txtPiso.Text.Trim()),
                    Convert.ToDecimal(txtPrecio.Text.Trim()),
                    txtDescripcion.Text.Trim()
                );

                MessageBox.Show("Habitación guardada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarHabitaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar habitación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idHabitacionSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una habitación para editar.");
                return;
            }

            if (!ValidarCampos())
                return;

            try
            {
                Database.ActualizarHabitacion(
                    idHabitacionSeleccionada,
                    Convert.ToInt32(cboSucursal.SelectedValue),
                    Convert.ToInt32(cboTipo.SelectedValue),
                    Convert.ToInt32(cboEstado.SelectedValue),
                    txtNumero.Text.Trim(),
                    Convert.ToInt32(txtPiso.Text.Trim()),
                    Convert.ToDecimal(txtPrecio.Text.Trim()),
                    txtDescripcion.Text.Trim()
                );

                MessageBox.Show("Habitación actualizada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarHabitaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar habitación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idHabitacionSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una habitación para eliminar.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Está seguro de eliminar esta habitación?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes)
                return;

            try
            {
                Database.EliminarHabitacion(idHabitacionSeleccionada);

                MessageBox.Show("Habitación eliminada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarHabitaciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar habitación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string textoBuscar = txtNumero.Text.Trim();

                if (string.IsNullOrWhiteSpace(textoBuscar))
                {
                    CargarHabitaciones();
                    return;
                }

                _modosBusqueda = true;
                dgvHabitaciones.DataSource = Database.BuscarHabitaciones(textoBuscar);
                ConfigurarColumnasGrid();
                btnPaginaAnterior.Enabled = false;
                btnPaginaSiguiente.Enabled = false;
                lblPaginaActual.Text = "Busqueda";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar habitaciones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarHabitaciones();
        }

        private void DgvHabitaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvHabitaciones.CurrentRow == null)
                return;

            DataGridViewRow fila = dgvHabitaciones.CurrentRow;

            idHabitacionSeleccionada = Convert.ToInt32(fila.Cells["IdHabitacion"].Value);
            cboSucursal.SelectedValue = Convert.ToInt32(fila.Cells["IdSucursal"].Value);
            cboTipo.SelectedValue = Convert.ToInt32(fila.Cells["IdTipoHabitacion"].Value);
            cboEstado.SelectedValue = Convert.ToInt32(fila.Cells["IdEstadoHabitacion"].Value);
            txtNumero.Text = fila.Cells["Numero"].Value?.ToString();
            txtPiso.Text = fila.Cells["Piso"].Value?.ToString();
            txtPrecio.Text = fila.Cells["PrecioPorNoche"].Value?.ToString();
            txtDescripcion.Text = fila.Cells["Descripcion"].Value?.ToString();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HabitacionesForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "HabitacionesForm";
            this.Load += new System.EventHandler(this.HabitacionesForm_Load_1);
            this.ResumeLayout(false);

        }

        private void HabitacionesForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}