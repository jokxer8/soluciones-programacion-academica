using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class ClientesForm : Form
    {
        private Label lblTitulo;
        private Label lblNombres;
        private Label lblApellidos;
        private Label lblTipoDocumento;
        private Label lblDocumento;
        private Label lblTelefono;
        private Label lblCorreo;
        private Label lblDireccion;

        private TextBox txtNombres;
        private TextBox txtApellidos;
        private ComboBox cboTipoDocumento;
        private TextBox txtDocumento;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtDireccion;

        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvClientes;

        private Button btnPaginaAnterior;
        private Button btnPaginaSiguiente;
        private Label lblPaginaActual;
        private int _paginaActual = 0;
        private const int TamPagina = 50;
        private bool _modosBusqueda = false;

        private int idClienteSeleccionado = 0;

        private EventHandler _themeHandler;

        public ClientesForm()
        {
            InicializarComponentes();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            Load += ClientesForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
        }

        private void ClientesForm_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void InicializarComponentes()
        {
            AutoScroll = true;

            lblTitulo = new Label();
            lblTitulo.Text = "Módulo de clientes";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblNombres = new Label();
            lblNombres.Text = "Nombres";
            lblNombres.Location = new Point(25, 70);

            txtNombres = new TextBox();
            txtNombres.Location = new Point(25, 95);
            txtNombres.Width = 220;

            lblApellidos = new Label();
            lblApellidos.Text = "Apellidos";
            lblApellidos.Location = new Point(270, 70);

            txtApellidos = new TextBox();
            txtApellidos.Location = new Point(270, 95);
            txtApellidos.Width = 220;

            lblTipoDocumento = new Label();
            lblTipoDocumento.Text = "Tipo documento";
            lblTipoDocumento.Location = new Point(515, 65);

            cboTipoDocumento = new ComboBox();
            cboTipoDocumento.Location = new Point(515, 95);
            cboTipoDocumento.Width = 180;
            cboTipoDocumento.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoDocumento.Items.Add("CEDULA");
            cboTipoDocumento.Items.Add("PASAPORTE");

            lblDocumento = new Label();
            lblDocumento.Text = "Documento";
            lblDocumento.Location = new Point(25, 135);

            txtDocumento = new TextBox();
            txtDocumento.Location = new Point(25, 160);
            txtDocumento.Width = 220;

            lblTelefono = new Label();
            lblTelefono.Text = "Teléfono";
            lblTelefono.Location = new Point(270, 135);

            txtTelefono = new TextBox();
            txtTelefono.Location = new Point(270, 160);
            txtTelefono.Width = 220;

            lblCorreo = new Label();
            lblCorreo.Text = "Correo";
            lblCorreo.Location = new Point(515, 135);

            txtCorreo = new TextBox();
            txtCorreo.Location = new Point(515, 160);
            txtCorreo.Width = 220;

            lblDireccion = new Label();
            lblDireccion.Text = "Dirección";
            lblDireccion.Location = new Point(25, 200);

            txtDireccion = new TextBox();
            txtDireccion.Location = new Point(25, 225);
            txtDireccion.Width = 710;

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

            dgvClientes = new DataGridView();
            dgvClientes.Location = new Point(25, 330);
            dgvClientes.Size = new Size(950, 260);
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvClientes.ScrollBars = ScrollBars.Both;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.RowHeadersWidth = 25;
            dgvClientes.CellClick += DgvClientes_CellClick;

            // Pagination controls
            btnPaginaAnterior = CrearBotonBlanco("< Anterior", 25, 600);
            btnPaginaAnterior.Width = 100;
            btnPaginaAnterior.Click += (s, e) => {
                if (_paginaActual > 0 && !_modosBusqueda) { _paginaActual--; CargarClientes(); }
            };

            lblPaginaActual = new Label();
            lblPaginaActual.Text = "Pag. 1";
            lblPaginaActual.AutoSize = true;
            lblPaginaActual.Font = Theme.AppFontBold;
            lblPaginaActual.Location = new Point(135, 606);

            btnPaginaSiguiente = CrearBotonBlanco("Siguiente >", 195, 600);
            btnPaginaSiguiente.Width = 110;
            btnPaginaSiguiente.Click += (s, e) => {
                if (!_modosBusqueda) { _paginaActual++; CargarClientes(); }
            };

            Controls.Add(lblTitulo);
            Controls.Add(lblNombres);
            Controls.Add(txtNombres);
            Controls.Add(lblApellidos);
            Controls.Add(txtApellidos);
            Controls.Add(lblTipoDocumento);
            Controls.Add(cboTipoDocumento);
            Controls.Add(lblDocumento);
            Controls.Add(txtDocumento);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Controls.Add(lblDireccion);
            Controls.Add(txtDireccion);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvClientes);
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

        private void CargarClientes()
        {
            try
            {
                _modosBusqueda = false;
                DataTable dt = Database.ObtenerClientesPaginado(_paginaActual, TamPagina);
                dgvClientes.DataSource = dt;
                ConfigurarColumnasGrid();
                dgvClientes.ClearSelection();

                lblPaginaActual.Text = "Pag. " + (_paginaActual + 1);
                btnPaginaAnterior.Enabled = _paginaActual > 0;
                btnPaginaSiguiente.Enabled = dt.Rows.Count == TamPagina;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasGrid()
        {
            if (dgvClientes.Columns.Count == 0)
                return;

            dgvClientes.Columns["IdCliente"].Width = 80;
            dgvClientes.Columns["Nombres"].Width = 140;
            dgvClientes.Columns["Apellidos"].Width = 140;
            dgvClientes.Columns["TipoDocumento"].Width = 110;
            dgvClientes.Columns["Documento"].Width = 140;
            dgvClientes.Columns["Telefono"].Width = 120;
            dgvClientes.Columns["Correo"].Width = 160;
            dgvClientes.Columns["Direccion"].Width = 260;
            dgvClientes.Columns["FechaRegistro"].Width = 140;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                MessageBox.Show("Debe escribir los nombres.");
                txtNombres.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("Debe escribir los apellidos.");
                txtApellidos.Focus();
                return false;
            }

            if (cboTipoDocumento.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el tipo de documento.");
                cboTipoDocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Debe escribir el documento.");
                txtDocumento.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            idClienteSeleccionado = 0;
            txtNombres.Clear();
            txtApellidos.Clear();
            cboTipoDocumento.SelectedIndex = -1;
            txtDocumento.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            dgvClientes.ClearSelection();
            txtNombres.Focus();
        }

        private void BtnNuevo_Click(object sender, EventArgs e) => LimpiarCampos();

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                Database.InsertarCliente(
                    txtNombres.Text.Trim(),
                    txtApellidos.Text.Trim(),
                    cboTipoDocumento.Text,
                    txtDocumento.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    txtDireccion.Text.Trim()
                );

                MessageBox.Show("Cliente guardado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cliente: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un cliente para editar.");
                return;
            }

            if (!ValidarCampos()) return;

            try
            {
                Database.ActualizarCliente(
                    idClienteSeleccionado,
                    txtNombres.Text.Trim(),
                    txtApellidos.Text.Trim(),
                    cboTipoDocumento.Text,
                    txtDocumento.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    txtDireccion.Text.Trim()
                );

                MessageBox.Show("Cliente actualizado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar cliente: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un cliente para eliminar.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Está seguro de eliminar este cliente?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;

            try
            {
                Database.EliminarCliente(idClienteSeleccionado);

                MessageBox.Show("Cliente eliminado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cliente: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string textoBuscar = txtNombres.Text.Trim();

                if (string.IsNullOrWhiteSpace(textoBuscar))
                {
                    CargarClientes();
                    return;
                }

                _modosBusqueda = true;
                dgvClientes.DataSource = Database.BuscarClientes(textoBuscar);
                ConfigurarColumnasGrid();
                btnPaginaAnterior.Enabled = false;
                btnPaginaSiguiente.Enabled = false;
                lblPaginaActual.Text = "Busqueda";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar clientes: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarClientes();
        }

        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvClientes.CurrentRow == null)
                return;

            DataGridViewRow fila = dgvClientes.CurrentRow;

            idClienteSeleccionado = Convert.ToInt32(fila.Cells["IdCliente"].Value);
            txtNombres.Text = fila.Cells["Nombres"].Value?.ToString();
            txtApellidos.Text = fila.Cells["Apellidos"].Value?.ToString();
            cboTipoDocumento.Text = fila.Cells["TipoDocumento"].Value?.ToString();
            txtDocumento.Text = fila.Cells["Documento"].Value?.ToString();
            txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString();
            txtCorreo.Text = fila.Cells["Correo"].Value?.ToString();
            txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Theme.OnThemeChanged -= _themeHandler;
            base.Dispose(disposing);
        }
    }
}