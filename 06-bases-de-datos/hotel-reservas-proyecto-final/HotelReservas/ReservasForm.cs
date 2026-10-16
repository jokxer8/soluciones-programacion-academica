using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class ReservasForm : Form
    {
        private Label lblTitulo;
        private Label lblCliente;
        private Label lblHabitacion;
        private Label lblEmpleado;
        private Label lblEstado;
        private Label lblEntrada;
        private Label lblSalida;
        private Label lblHuespedes;
        private Label lblNoches;
        private Label lblPrecioNoche;
        private Label lblTotalHospedaje;
        private Label lblObservacion;
        private Label lblBuscar;

        private ComboBox cboCliente;
        private ComboBox cboHabitacion;
        private ComboBox cboEmpleado;
        private ComboBox cboEstado;

        private DateTimePicker dtEntrada;
        private DateTimePicker dtSalida;

        private TextBox txtHuespedes;
        private TextBox txtNoches;
        private TextBox txtPrecioNoche;
        private TextBox txtTotalHospedaje;
        private TextBox txtObservacion;
        private TextBox txtBuscar;

        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnCheckIn;
        private Button btnCheckOut;

        private Label lblServiciosTotal;
        private Label lblDescuentoPromo;
        private Label lblTotalGeneral;

        private TextBox txtServiciosTotal;
        private TextBox txtDescuentoPromo;
        private TextBox txtTotalGeneral;

        private DataGridView dgvReservas;

        private Label lblServiciosTitulo;
        private Label lblServicio;
        private Label lblCantidadServicio;

        private ComboBox cboServicio;
        private TextBox txtCantidadServicio;
        private Button btnNuevoServicioCatalogo;
        private Button btnAgregarServicio;
        private Button btnQuitarServicio;
        private DataGridView dgvServiciosReserva;

        private Label lblPromocionesTitulo;
        private Label lblPromocion;

        private ComboBox cboPromocion;
        private Button btnNuevaPromocionCatalogo;
        private Button btnAplicarPromocion;
        private Button btnQuitarPromocion;
        private DataGridView dgvPromocionesReserva;

        private int idReservaSeleccionada = 0;

        private EventHandler _themeHandler;

        public ReservasForm()
        {
            InicializarComponentes();
            Load += ReservasForm_Load;
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

        private void ReservasForm_Load(object sender, EventArgs e)
        {
            CargarCombosBase();
            dtEntrada.Value = DateTime.Today;
            dtSalida.Value = DateTime.Today.AddDays(1);
            CargarPromocionesActivas();
            CargarHabitacionesDisponibles();
            CargarReservas();
            ActualizarCalculoBase();
            LimpiarSeccionesExtras();
            ConfigurarPermisosPorRol();
        }

        private void InicializarComponentes()
        {
            AutoScroll = true;

            lblTitulo = new Label();
            lblTitulo.Text = "Módulo de reservas";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblCliente = new Label();
            lblCliente.Text = "Cliente";
            lblCliente.Location = new Point(25, 70);

            cboCliente = new ComboBox();
            cboCliente.Location = new Point(25, 95);
            cboCliente.Width = 240;
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;

            lblHabitacion = new Label();
            lblHabitacion.Text = "Habitación disponible";
            lblHabitacion.Location = new Point(290, 70);

            cboHabitacion = new ComboBox();
            cboHabitacion.Location = new Point(290, 95);
            cboHabitacion.Width = 220;
            cboHabitacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHabitacion.SelectedIndexChanged += Recalcular_Evento;

            lblEmpleado = new Label();
            lblEmpleado.Text = "Empleado";
            lblEmpleado.Location = new Point(535, 70);

            cboEmpleado = new ComboBox();
            cboEmpleado.Location = new Point(535, 95);
            cboEmpleado.Width = 220;
            cboEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;

            lblEstado = new Label();
            lblEstado.Text = "Estado";
            lblEstado.Location = new Point(780, 70);

            cboEstado = new ComboBox();
            cboEstado.Location = new Point(780, 95);
            cboEstado.Width = 160;
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            lblEntrada = new Label();
            lblEntrada.Text = "Fecha entrada";
            lblEntrada.Location = new Point(25, 135);

            dtEntrada = new DateTimePicker();
            dtEntrada.Location = new Point(25, 160);
            dtEntrada.Width = 170;
            dtEntrada.Format = DateTimePickerFormat.Short;
            dtEntrada.ValueChanged += FechasCambiaron_Evento;

            lblSalida = new Label();
            lblSalida.Text = "Fecha salida";
            lblSalida.Location = new Point(220, 135);

            dtSalida = new DateTimePicker();
            dtSalida.Location = new Point(220, 160);
            dtSalida.Width = 170;
            dtSalida.Format = DateTimePickerFormat.Short;
            dtSalida.ValueChanged += FechasCambiaron_Evento;

            lblHuespedes = new Label();
            lblHuespedes.Text = "Cantidad Personas";
            lblHuespedes.Location = new Point(415, 135);

            txtHuespedes = new TextBox();
            txtHuespedes.Location = new Point(415, 160);
            txtHuespedes.Width = 110;

            lblNoches = new Label();
            lblNoches.Text = "Noches";
            lblNoches.Location = new Point(550, 135);

            txtNoches = new TextBox();
            txtNoches.Location = new Point(550, 160);
            txtNoches.Width = 90;
            txtNoches.ReadOnly = true;

            lblPrecioNoche = new Label();
            lblPrecioNoche.Text = "Precio noche";
            lblPrecioNoche.Location = new Point(665, 135);

            txtPrecioNoche = new TextBox();
            txtPrecioNoche.Location = new Point(665, 160);
            txtPrecioNoche.Width = 110;
            txtPrecioNoche.ReadOnly = true;

            lblTotalHospedaje = new Label();
            lblTotalHospedaje.Text = "Hospedaje";
            lblTotalHospedaje.Location = new Point(800, 135);

            txtTotalHospedaje = new TextBox();
            txtTotalHospedaje.Location = new Point(800, 160);
            txtTotalHospedaje.Width = 140;
            txtTotalHospedaje.ReadOnly = true;

            lblObservacion = new Label();
            lblObservacion.Text = "Observación";
            lblObservacion.Location = new Point(25, 200);

            txtObservacion = new TextBox();
            txtObservacion.Location = new Point(25, 225);
            txtObservacion.Width = 915;

            btnNuevo = CrearBotonBlanco("Nuevo", 25, 275);
            btnNuevo.Click += BtnNuevo_Click;

            btnGuardar = CrearBotonBlanco("Guardar", 125, 275);
            btnGuardar.Click += BtnGuardar_Click;

            btnEditar = CrearBotonBlanco("Editar", 225, 275);
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = CrearBotonBlanco("Eliminar", 325, 275);
            btnEliminar.Click += BtnEliminar_Click;

            lblBuscar = new Label();
            lblBuscar.Text = "Buscar";
            lblBuscar.Location = new Point(455, 255);

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(455, 280);
            txtBuscar.Width = 220;

            btnBuscar = CrearBotonBlanco("Buscar", 690, 275);
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBotonBlanco("Limpiar", 790, 275);
            btnLimpiar.Click += BtnLimpiar_Click;

            btnCheckIn = CrearBotonBlanco("Check-in", 900, 275, 100, 30);
            btnCheckIn.BackColor = Color.FromArgb(34, 139, 34);
            btnCheckIn.ForeColor = Color.White;
            btnCheckIn.FlatAppearance.BorderColor = Color.DarkGreen;
            btnCheckIn.Click += BtnCheckIn_Click;

            btnCheckOut = CrearBotonBlanco("Check-out", 1010, 275, 100, 30);
            btnCheckOut.BackColor = Color.FromArgb(178, 34, 34);
            btnCheckOut.ForeColor = Color.White;
            btnCheckOut.FlatAppearance.BorderColor = Color.DarkRed;
            btnCheckOut.Click += BtnCheckOut_Click;

            lblServiciosTotal = new Label();
            lblServiciosTotal.Text = "Servicios";
            lblServiciosTotal.Location = new Point(25, 325);

            txtServiciosTotal = new TextBox();
            txtServiciosTotal.Location = new Point(25, 350);
            txtServiciosTotal.Width = 140;
            txtServiciosTotal.ReadOnly = true;

            lblDescuentoPromo = new Label();
            lblDescuentoPromo.Text = "Desc. promociones";
            lblDescuentoPromo.Location = new Point(190, 325);

            txtDescuentoPromo = new TextBox();
            txtDescuentoPromo.Location = new Point(190, 350);
            txtDescuentoPromo.Width = 160;
            txtDescuentoPromo.ReadOnly = true;

            lblTotalGeneral = new Label();
            lblTotalGeneral.Text = "Total general estimado";
            lblTotalGeneral.Location = new Point(375, 325);

            txtTotalGeneral = new TextBox();
            txtTotalGeneral.Location = new Point(375, 350);
            txtTotalGeneral.Width = 180;
            txtTotalGeneral.ReadOnly = true;

            dgvReservas = new DataGridView();
            dgvReservas.Location = new Point(25, 400);
            dgvReservas.Size = new Size(1030, 240);
            dgvReservas.AllowUserToAddRows = false;
            dgvReservas.AllowUserToDeleteRows = false;
            dgvReservas.ReadOnly = true;
            dgvReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvReservas.ScrollBars = ScrollBars.Both;
            dgvReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservas.MultiSelect = false;
            dgvReservas.RowHeadersWidth = 25;
            dgvReservas.CellClick += DgvReservas_CellClick;

            lblServiciosTitulo = new Label();
            lblServiciosTitulo.Text = "Servicios adicionales de la reserva";
            lblServiciosTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblServiciosTitulo.AutoSize = true;
            lblServiciosTitulo.Location = new Point(25, 665);

            lblServicio = new Label();
            lblServicio.Text = "Servicio";
            lblServicio.Location = new Point(25, 705);

            cboServicio = new ComboBox();
            cboServicio.Location = new Point(25, 730);
            cboServicio.Width = 220;
            cboServicio.DropDownStyle = ComboBoxStyle.DropDownList;

            btnNuevoServicioCatalogo = CrearBotonBlanco("+", 250, 726, 25, 25);
            btnNuevoServicioCatalogo.Click += BtnNuevoServicioCatalogo_Click;

            lblCantidadServicio = new Label();
            lblCantidadServicio.Text = "Cantidad";
            lblCantidadServicio.Width = 60;
            lblCantidadServicio.Location = new Point(305, 705);

            txtCantidadServicio = new TextBox();
            txtCantidadServicio.Location = new Point(305, 730);
            txtCantidadServicio.Width = 60;
            txtCantidadServicio.Text = "1";

            btnAgregarServicio = CrearBotonBlanco("Agregar", 390, 720, 60, 30);
            btnAgregarServicio.Click += BtnAgregarServicio_Click;

            btnQuitarServicio = CrearBotonBlanco("Quitar", 460, 720, 60, 30);
            btnQuitarServicio.Click += BtnQuitarServicio_Click;

            dgvServiciosReserva = new DataGridView();
            dgvServiciosReserva.Location = new Point(25, 775);
            dgvServiciosReserva.Size = new Size(500, 200);
            dgvServiciosReserva.AllowUserToAddRows = false;
            dgvServiciosReserva.AllowUserToDeleteRows = false;
            dgvServiciosReserva.ReadOnly = true;
            dgvServiciosReserva.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvServiciosReserva.ScrollBars = ScrollBars.Both;
            dgvServiciosReserva.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServiciosReserva.MultiSelect = false;
            dgvServiciosReserva.RowHeadersWidth = 25;

            lblPromocionesTitulo = new Label();
            lblPromocionesTitulo.Text = "Promociones aplicadas";
            lblPromocionesTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPromocionesTitulo.AutoSize = true;
            lblPromocionesTitulo.Location = new Point(555, 665);

            lblPromocion = new Label();
            lblPromocion.Text = "Promoción activa";
            lblPromocion.Location = new Point(555, 705);

            cboPromocion = new ComboBox();
            cboPromocion.Location = new Point(555, 730);
            cboPromocion.Width = 220;
            cboPromocion.DropDownStyle = ComboBoxStyle.DropDownList;

            btnNuevaPromocionCatalogo = CrearBotonBlanco("+", 780, 728, 25, 25);
            btnNuevaPromocionCatalogo.Click += BtnNuevaPromocionCatalogo_Click;

            btnAplicarPromocion = CrearBotonBlanco("Aplicar", 820, 726, 60, 30);
            btnAplicarPromocion.Click += BtnAplicarPromocion_Click;

            btnQuitarPromocion = CrearBotonBlanco("Quitar", 890, 726, 60, 30);
            btnQuitarPromocion.Click += BtnQuitarPromocion_Click;

            dgvPromocionesReserva = new DataGridView();
            dgvPromocionesReserva.Location = new Point(555, 775);
            dgvPromocionesReserva.Size = new Size(500, 200);
            dgvPromocionesReserva.AllowUserToAddRows = false;
            dgvPromocionesReserva.AllowUserToDeleteRows = false;
            dgvPromocionesReserva.ReadOnly = true;
            dgvPromocionesReserva.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPromocionesReserva.ScrollBars = ScrollBars.Both;
            dgvPromocionesReserva.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromocionesReserva.MultiSelect = false;
            dgvPromocionesReserva.RowHeadersWidth = 25;

            Controls.Add(lblTitulo);
            Controls.Add(lblCliente);
            Controls.Add(cboCliente);
            Controls.Add(lblHabitacion);
            Controls.Add(cboHabitacion);
            Controls.Add(lblEmpleado);
            Controls.Add(cboEmpleado);
            Controls.Add(lblEstado);
            Controls.Add(cboEstado);
            Controls.Add(lblEntrada);
            Controls.Add(dtEntrada);
            Controls.Add(lblSalida);
            Controls.Add(dtSalida);
            Controls.Add(lblHuespedes);
            Controls.Add(txtHuespedes);
            Controls.Add(lblNoches);
            Controls.Add(txtNoches);
            Controls.Add(lblPrecioNoche);
            Controls.Add(txtPrecioNoche);
            Controls.Add(lblTotalHospedaje);
            Controls.Add(txtTotalHospedaje);
            Controls.Add(lblObservacion);
            Controls.Add(txtObservacion);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCheckIn);
            Controls.Add(btnCheckOut);
            Controls.Add(lblServiciosTotal);
            Controls.Add(txtServiciosTotal);
            Controls.Add(lblDescuentoPromo);
            Controls.Add(txtDescuentoPromo);
            Controls.Add(lblTotalGeneral);
            Controls.Add(txtTotalGeneral);
            Controls.Add(dgvReservas);

            Controls.Add(lblServiciosTitulo);
            Controls.Add(lblServicio);
            Controls.Add(cboServicio);
            Controls.Add(btnNuevoServicioCatalogo);
            Controls.Add(lblCantidadServicio);
            Controls.Add(txtCantidadServicio);
            Controls.Add(btnAgregarServicio);
            Controls.Add(btnQuitarServicio);
            Controls.Add(dgvServiciosReserva);

            Controls.Add(lblPromocionesTitulo);
            Controls.Add(lblPromocion);
            Controls.Add(cboPromocion);
            Controls.Add(btnNuevaPromocionCatalogo);
            Controls.Add(btnAplicarPromocion);
            Controls.Add(btnQuitarPromocion);
            Controls.Add(dgvPromocionesReserva);
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

            btnNuevoServicioCatalogo.Visible = puedeAdministrarCatalogos;
            btnNuevoServicioCatalogo.Enabled = puedeAdministrarCatalogos;

            btnNuevaPromocionCatalogo.Visible = puedeAdministrarCatalogos;
            btnNuevaPromocionCatalogo.Enabled = puedeAdministrarCatalogos;
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

        private void CargarCombosBase()
        {
            try
            {
                DataTable dtClientes = Database.ObtenerClientesCombo();
                cboCliente.DataSource = dtClientes;
                cboCliente.DisplayMember = "Cliente";
                cboCliente.ValueMember = "IdCliente";
                cboCliente.SelectedIndex = -1;

                DataTable dtEmpleados = Database.ObtenerEmpleadosCombo();
                cboEmpleado.DataSource = dtEmpleados;
                cboEmpleado.DisplayMember = "Empleado";
                cboEmpleado.ValueMember = "IdEmpleado";
                cboEmpleado.SelectedIndex = -1;

                DataTable dtEstados = Database.ObtenerEstadosReservaCombo();
                cboEstado.DataSource = dtEstados;
                cboEstado.DisplayMember = "NombreEstado";
                cboEstado.ValueMember = "IdEstadoReserva";
                cboEstado.SelectedIndex = -1;

                CargarServiciosCatalogo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarServiciosCatalogo(int? idServicioSeleccionado = null)
        {
            DataTable dtServicios = ReservasExtrasData.ObtenerServiciosAdicionalesCombo();
            cboServicio.DataSource = dtServicios;
            cboServicio.DisplayMember = "NombreServicio";
            cboServicio.ValueMember = "IdServicioAdicional";

            if (idServicioSeleccionado.HasValue)
                cboServicio.SelectedValue = idServicioSeleccionado.Value;
            else
                cboServicio.SelectedIndex = -1;
        }

        private void CargarPromocionesActivas(int? idPromocionSeleccionada = null)
        {
            try
            {
                DataTable dtPromos = ReservasExtrasData.ObtenerPromocionesActivasCombo(dtEntrada.Value.Date);
                cboPromocion.DataSource = dtPromos;
                cboPromocion.DisplayMember = "NombrePromocion";
                cboPromocion.ValueMember = "IdPromocion";

                if (idPromocionSeleccionada.HasValue)
                    cboPromocion.SelectedValue = idPromocionSeleccionada.Value;
                else
                    cboPromocion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar promociones activas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHabitacionesDisponibles(int? idHabitacionSeleccionada = null)
        {
            try
            {
                if (dtSalida.Value.Date <= dtEntrada.Value.Date)
                {
                    cboHabitacion.DataSource = null;
                    return;
                }

                DataTable dtHabitaciones = ReservasExtrasData.ObtenerHabitacionesDisponiblesCombo(
                    dtEntrada.Value.Date,
                    dtSalida.Value.Date,
                    idReservaSeleccionada);

                cboHabitacion.DataSource = dtHabitaciones;
                cboHabitacion.DisplayMember = "Habitacion";
                cboHabitacion.ValueMember = "IdHabitacion";

                if (idHabitacionSeleccionada.HasValue)
                    cboHabitacion.SelectedValue = idHabitacionSeleccionada.Value;
                else
                    cboHabitacion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar habitaciones disponibles: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReservas()
        {
            try
            {
                dgvReservas.DataSource = Database.ObtenerReservas();
                ConfigurarColumnasGrid();
                dgvReservas.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reservas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasGrid()
        {
            if (dgvReservas.Columns.Count == 0)
                return;

            dgvReservas.Columns["IdReserva"].Width = 80;
            dgvReservas.Columns["IdCliente"].Visible = false;
            dgvReservas.Columns["Cliente"].Width = 170;
            dgvReservas.Columns["IdHabitacion"].Visible = false;
            dgvReservas.Columns["Habitacion"].Width = 90;
            dgvReservas.Columns["IdEmpleado"].Visible = false;
            dgvReservas.Columns["Empleado"].Width = 150;
            dgvReservas.Columns["IdEstadoReserva"].Visible = false;
            dgvReservas.Columns["NombreEstado"].Width = 110;
            dgvReservas.Columns["FechaEntrada"].Width = 95;
            dgvReservas.Columns["FechaSalida"].Width = 95;
            dgvReservas.Columns["Noches"].Width = 70;
            dgvReservas.Columns["CantidadHuespedes"].Width = 105;
            dgvReservas.Columns["Subtotal"].Width = 95;
            dgvReservas.Columns["Descuento"].Width = 90;
            dgvReservas.Columns["Total"].Width = 90;
            dgvReservas.Columns["Observacion"].Width = 180;
            dgvReservas.Columns["FechaReserva"].Width = 130;
        }

        private void ConfigurarGridServicios()
        {
            if (dgvServiciosReserva.Columns.Count == 0)
                return;

            if (dgvServiciosReserva.Columns.Contains("IdReserva"))
                dgvServiciosReserva.Columns["IdReserva"].Visible = false;

            if (dgvServiciosReserva.Columns.Contains("IdServicioAdicional"))
                dgvServiciosReserva.Columns["IdServicioAdicional"].Visible = false;

            dgvServiciosReserva.Columns["NombreServicio"].Width = 160;
            dgvServiciosReserva.Columns["Cantidad"].Width = 70;
            dgvServiciosReserva.Columns["PrecioUnitario"].Width = 100;
            dgvServiciosReserva.Columns["Subtotal"].Width = 100;
        }

        private void ConfigurarGridPromociones()
        {
            if (dgvPromocionesReserva.Columns.Count == 0)
                return;

            if (dgvPromocionesReserva.Columns.Contains("IdReserva"))
                dgvPromocionesReserva.Columns["IdReserva"].Visible = false;

            if (dgvPromocionesReserva.Columns.Contains("IdPromocion"))
                dgvPromocionesReserva.Columns["IdPromocion"].Visible = false;

            dgvPromocionesReserva.Columns["NombrePromocion"].Width = 180;
            dgvPromocionesReserva.Columns["TipoDescuento"].Width = 90;
            dgvPromocionesReserva.Columns["ValorDescuento"].Width = 100;
            dgvPromocionesReserva.Columns["DescuentoAplicado"].Width = 120;
        }

        private bool ValidarCampos()
        {
            if (cboCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente.");
                cboCliente.Focus();
                return false;
            }

            if (cboHabitacion.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una habitación disponible.");
                cboHabitacion.Focus();
                return false;
            }

            if (cboEmpleado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un empleado.");
                cboEmpleado.Focus();
                return false;
            }

            if (cboEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado.");
                cboEstado.Focus();
                return false;
            }

            if (!int.TryParse(txtHuespedes.Text.Trim(), out int huespedes) || huespedes <= 0)
            {
                MessageBox.Show("Debe escribir una cantidad de huéspedes válida.");
                txtHuespedes.Focus();
                return false;
            }

            if (dtSalida.Value.Date <= dtEntrada.Value.Date)
            {
                MessageBox.Show("La fecha de salida debe ser mayor que la fecha de entrada.");
                dtSalida.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarSeccionesExtras()
        {
            txtServiciosTotal.Text = "0.00";
            txtDescuentoPromo.Text = "0.00";
            txtTotalGeneral.Text = txtTotalHospedaje.Text == "" ? "0.00" : txtTotalHospedaje.Text;
            txtCantidadServicio.Text = "1";
            cboServicio.SelectedIndex = -1;
            cboPromocion.SelectedIndex = -1;
            dgvServiciosReserva.DataSource = null;
            dgvPromocionesReserva.DataSource = null;
        }

        private void LimpiarCampos()
        {
            idReservaSeleccionada = 0;

            cboCliente.SelectedIndex = -1;
            cboEmpleado.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;

            dtEntrada.Value = DateTime.Today;
            dtSalida.Value = DateTime.Today.AddDays(1);

            txtHuespedes.Clear();
            txtNoches.Clear();
            txtPrecioNoche.Clear();
            txtTotalHospedaje.Clear();
            txtObservacion.Clear();
            txtBuscar.Clear();

            CargarPromocionesActivas();
            CargarHabitacionesDisponibles();

            dgvReservas.ClearSelection();
            LimpiarSeccionesExtras();
        }

        private void ActualizarCalculoBase()
        {
            if (dtSalida.Value.Date <= dtEntrada.Value.Date)
            {
                txtNoches.Text = "0";
                txtPrecioNoche.Text = "0.00";
                txtTotalHospedaje.Text = "0.00";
                txtTotalGeneral.Text = "0.00";
                return;
            }

            int noches = (dtSalida.Value.Date - dtEntrada.Value.Date).Days;
            txtNoches.Text = noches.ToString();

            int? idHabitacion = ObtenerValorCombo(cboHabitacion);

            if (!idHabitacion.HasValue)
            {
                txtPrecioNoche.Text = "0.00";
                txtTotalHospedaje.Text = "0.00";

                if (idReservaSeleccionada == 0)
                    txtTotalGeneral.Text = "0.00";

                return;
            }

            decimal precioNoche = ReservasExtrasData.ObtenerPrecioHabitacion(idHabitacion.Value);
            decimal totalHospedaje = precioNoche * noches;

            txtPrecioNoche.Text = precioNoche.ToString("N2");
            txtTotalHospedaje.Text = totalHospedaje.ToString("N2");

            if (idReservaSeleccionada == 0)
                txtTotalGeneral.Text = totalHospedaje.ToString("N2");
        }

        private void CargarServiciosReserva()
        {
            try
            {
                if (idReservaSeleccionada == 0)
                {
                    dgvServiciosReserva.DataSource = null;
                    return;
                }

                dgvServiciosReserva.DataSource = ReservasExtrasData.ObtenerServiciosReserva(idReservaSeleccionada);
                ConfigurarGridServicios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios de la reserva: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPromocionesReserva()
        {
            try
            {
                if (idReservaSeleccionada == 0)
                {
                    dgvPromocionesReserva.DataSource = null;
                    return;
                }

                dgvPromocionesReserva.DataSource = ReservasExtrasData.ObtenerPromocionesReserva(idReservaSeleccionada);
                ConfigurarGridPromociones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar promociones de la reserva: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarResumenExtras()
        {
            try
            {
                if (idReservaSeleccionada == 0)
                {
                    txtServiciosTotal.Text = "0.00";
                    txtDescuentoPromo.Text = "0.00";
                    txtTotalGeneral.Text = txtTotalHospedaje.Text == "" ? "0.00" : txtTotalHospedaje.Text;
                    return;
                }

                DataTable dt = ReservasExtrasData.ObtenerTotalesExtrasReserva(idReservaSeleccionada);

                if (dt.Rows.Count == 0)
                {
                    txtServiciosTotal.Text = "0.00";
                    txtDescuentoPromo.Text = "0.00";
                    txtTotalGeneral.Text = txtTotalHospedaje.Text == "" ? "0.00" : txtTotalHospedaje.Text;
                    return;
                }

                DataRow row = dt.Rows[0];
                txtServiciosTotal.Text = Convert.ToDecimal(row["TotalServicios"]).ToString("N2");
                txtDescuentoPromo.Text = Convert.ToDecimal(row["DescuentoPromociones"]).ToString("N2");
                txtTotalGeneral.Text = Convert.ToDecimal(row["TotalGeneral"]).ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar resumen de extras: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FechasCambiaron_Evento(object sender, EventArgs e)
        {
            int? habitacionActual = ObtenerValorCombo(cboHabitacion);
            int? promoActual = ObtenerValorCombo(cboPromocion);

            CargarPromocionesActivas(promoActual);
            CargarHabitacionesDisponibles(habitacionActual);
            ActualizarCalculoBase();
            CargarResumenExtras();
        }

        private void Recalcular_Evento(object sender, EventArgs e)
        {
            ActualizarCalculoBase();
            CargarResumenExtras();
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
                Database.InsertarReserva(
                    Convert.ToInt32(cboCliente.SelectedValue),
                    Convert.ToInt32(cboHabitacion.SelectedValue),
                    Convert.ToInt32(cboEmpleado.SelectedValue),
                    Convert.ToInt32(cboEstado.SelectedValue),
                    dtEntrada.Value.Date,
                    dtSalida.Value.Date,
                    Convert.ToInt32(txtHuespedes.Text.Trim()),
                    txtObservacion.Text.Trim()
                );

                MessageBox.Show("Reserva guardada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarReservas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar reserva: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una reserva para editar.");
                return;
            }

            if (!ValidarCampos())
                return;

            try
            {
                Database.ActualizarReserva(
                    idReservaSeleccionada,
                    Convert.ToInt32(cboCliente.SelectedValue),
                    Convert.ToInt32(cboHabitacion.SelectedValue),
                    Convert.ToInt32(cboEmpleado.SelectedValue),
                    Convert.ToInt32(cboEstado.SelectedValue),
                    dtEntrada.Value.Date,
                    dtSalida.Value.Date,
                    Convert.ToInt32(txtHuespedes.Text.Trim()),
                    txtObservacion.Text.Trim()
                );

                ReservasExtrasData.RecalcularPromocionesReserva(idReservaSeleccionada);

                MessageBox.Show("Reserva actualizada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarReservas();
                CargarServiciosReserva();
                CargarPromocionesReserva();
                CargarResumenExtras();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar reserva: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una reserva para eliminar.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Está seguro de eliminar esta reserva?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes)
                return;

            try
            {
                Database.EliminarReserva(idReservaSeleccionada);

                MessageBox.Show("Reserva eliminada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarReservas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar reserva: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(texto))
                {
                    CargarReservas();
                    return;
                }

                dgvReservas.DataSource = Database.BuscarReservas(texto);
                ConfigurarColumnasGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar reservas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarReservas();
        }

        private void BtnCheckIn_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una reserva de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string obs = Microsoft.VisualBasic.Interaction.InputBox(
                "Observación de check-in (opcional):", "Check-in", "");

            try
            {
                CheckInOutData.RegistrarCheckIn(idReservaSeleccionada, obs);
                Database.RegistrarAccion("CHECK_IN", "Reserva", idReservaSeleccionada, obs);
                MessageBox.Show("Check-in registrado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarReservas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar check-in: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una reserva de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string obs = Microsoft.VisualBasic.Interaction.InputBox(
                "Observación de check-out (opcional):", "Check-out", "");

            try
            {
                CheckInOutData.RegistrarCheckOut(idReservaSeleccionada, obs);
                Database.RegistrarAccion("CHECK_OUT", "Reserva", idReservaSeleccionada, obs);
                MessageBox.Show("Check-out registrado. Factura generada si no existía.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarReservas();

                // Prompt for stay rating if not already submitted
                if (!RatingData.ExisteRating(idReservaSeleccionada))
                {
                    int idReservaParaRating = idReservaSeleccionada;
                    using (RatingForm ratingFrm = new RatingForm(idReservaParaRating))
                    {
                        ratingFrm.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar check-out: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevoServicioCatalogo_Click(object sender, EventArgs e)
        {
            if (!PuedeAdministrarCatalogos())
            {
                MessageBox.Show("No tiene permisos para crear servicios adicionales.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idServicioActual = ObtenerValorCombo(cboServicio);

            using (ServiciosAdicionalesForm frm = new ServiciosAdicionalesForm())
            {
                frm.ShowDialog(this);
            }

            CargarServiciosCatalogo(idServicioActual);
        }

        private void BtnNuevaPromocionCatalogo_Click(object sender, EventArgs e)
        {
            if (!PuedeAdministrarCatalogos())
            {
                MessageBox.Show("No tiene permisos para crear promociones.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idPromoActual = ObtenerValorCombo(cboPromocion);

            using (PromocionesForm frm = new PromocionesForm())
            {
                frm.ShowDialog(this);
            }

            CargarPromocionesActivas(idPromoActual);
        }

        private void BtnAgregarServicio_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0)
            {
                MessageBox.Show("Primero debe guardar o seleccionar una reserva.");
                return;
            }

            if (cboServicio.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un servicio adicional.");
                cboServicio.Focus();
                return;
            }

            if (!int.TryParse(txtCantidadServicio.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Debe escribir una cantidad válida.");
                txtCantidadServicio.Focus();
                return;
            }

            try
            {
                ReservasExtrasData.AgregarServicioAReserva(
                    idReservaSeleccionada,
                    Convert.ToInt32(cboServicio.SelectedValue),
                    cantidad
                );

                MessageBox.Show("Servicio adicional agregado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarServiciosReserva();
                CargarResumenExtras();
                txtCantidadServicio.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar servicio adicional: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnQuitarServicio_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0 || dgvServiciosReserva.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un servicio de la reserva.");
                return;
            }

            try
            {
                int idServicio = Convert.ToInt32(dgvServiciosReserva.CurrentRow.Cells["IdServicioAdicional"].Value);

                ReservasExtrasData.QuitarServicioReserva(idReservaSeleccionada, idServicio);

                MessageBox.Show("Servicio adicional quitado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarServiciosReserva();
                CargarResumenExtras();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar servicio adicional: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAplicarPromocion_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0)
            {
                MessageBox.Show("Primero debe guardar o seleccionar una reserva.");
                return;
            }

            if (cboPromocion.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una promoción activa.");
                cboPromocion.Focus();
                return;
            }

            try
            {
                ReservasExtrasData.AplicarPromocionAReserva(
                    idReservaSeleccionada,
                    Convert.ToInt32(cboPromocion.SelectedValue)
                );

                MessageBox.Show("Promoción aplicada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPromocionesReserva();
                CargarResumenExtras();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar promoción: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnQuitarPromocion_Click(object sender, EventArgs e)
        {
            if (idReservaSeleccionada == 0 || dgvPromocionesReserva.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una promoción aplicada.");
                return;
            }

            try
            {
                int idPromocion = Convert.ToInt32(dgvPromocionesReserva.CurrentRow.Cells["IdPromocion"].Value);

                ReservasExtrasData.QuitarPromocionReserva(idReservaSeleccionada, idPromocion);

                MessageBox.Show("Promoción quitada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPromocionesReserva();
                CargarResumenExtras();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar promoción: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvReservas.CurrentRow == null)
                return;

            DataGridViewRow fila = dgvReservas.CurrentRow;

            idReservaSeleccionada = Convert.ToInt32(fila.Cells["IdReserva"].Value);

            cboCliente.SelectedValue = Convert.ToInt32(fila.Cells["IdCliente"].Value);
            cboEmpleado.SelectedValue = Convert.ToInt32(fila.Cells["IdEmpleado"].Value);
            cboEstado.SelectedValue = Convert.ToInt32(fila.Cells["IdEstadoReserva"].Value);

            dtEntrada.Value = Convert.ToDateTime(fila.Cells["FechaEntrada"].Value);
            dtSalida.Value = Convert.ToDateTime(fila.Cells["FechaSalida"].Value);

            int idHabitacion = Convert.ToInt32(fila.Cells["IdHabitacion"].Value);
            CargarPromocionesActivas();
            CargarHabitacionesDisponibles(idHabitacion);
            cboHabitacion.SelectedValue = idHabitacion;

            txtHuespedes.Text = fila.Cells["CantidadHuespedes"].Value?.ToString();
            txtNoches.Text = fila.Cells["Noches"].Value?.ToString();
            txtObservacion.Text = fila.Cells["Observacion"].Value?.ToString();

            ActualizarCalculoBase();
            CargarServiciosReserva();
            CargarPromocionesReserva();
            CargarResumenExtras();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ReservasForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "ReservasForm";
            this.Load += new System.EventHandler(this.ReservasForm_Load_1);
            this.ResumeLayout(false);

        }

        private void ReservasForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}