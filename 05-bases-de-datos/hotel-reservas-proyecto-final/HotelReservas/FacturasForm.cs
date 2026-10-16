using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class FacturasForm : Form
    {
        private Label lblTitulo;
        private Label lblReserva;
        private Label lblCliente;
        private Label lblHabitacion;
        private Label lblPorcImpuesto;
        private Label lblSubtotal;
        private Label lblDescuento;
        private Label lblImpuesto;
        private Label lblTotal;
        private Label lblEstado;
        private Label lblBuscar;

        private ComboBox cboReserva;
        private TextBox txtCliente;
        private TextBox txtHabitacion;
        private TextBox txtPorcImpuesto;
        private TextBox txtSubtotal;
        private TextBox txtDescuento;
        private TextBox txtImpuesto;
        private TextBox txtTotal;
        private TextBox txtEstado;
        private TextBox txtBuscar;

        private Button btnCalcular;
        private Button btnGenerar;
        private Button btnVerDetalle;
        private Button btnImprimirPdf;
        private Button btnExportarPdf;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvFacturas;

        // Sección de pagos
        private Label lblPagoTitulo;
        private Label lblFacturaSeleccionada;
        private Label lblTotalFactura;
        private Label lblTotalPagado;
        private Label lblBalance;
        private Label lblCambio;
        private Label lblMetodoPago;
        private Label lblMontoPago;
        private Label lblReferencia;
        private Label lblObservacionPago;

        private TextBox txtFacturaSeleccionada;
        private TextBox txtTotalFacturaPago;
        private TextBox txtTotalPagado;
        private TextBox txtBalance;
        private TextBox txtCambio;
        private ComboBox cboMetodoPago;
        private TextBox txtMontoPago;
        private TextBox txtReferencia;
        private TextBox txtObservacionPago;
        private Button btnRegistrarPago;

        private DataGridView dgvPagos;

        private bool cargandoReservas = false;
        private int idFacturaSeleccionada = 0;

        private Button btnPaginaAnterior;
        private Button btnPaginaSiguiente;
        private Label lblPaginaActual;
        private int _paginaActual = 0;
        private const int TamPagina = 50;
        private bool _modosBusqueda = false;

        private PrintDocument printDocumentFactura;
        private string textoImpresionFactura = "";

        private EventHandler _themeHandler;

        public FacturasForm()
        {
            InicializarComponentes();
            Load += FacturasForm_Load;
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

        private void FacturasForm_Load(object sender, EventArgs e)
        {
            CargarReservasSinFactura();
            CargarFacturas();
            CargarMetodosPago();
            ActualizarEstadoBotonesFactura();
        }

        private void InicializarComponentes()
        {
            AutoScroll = true;

            lblTitulo = new Label();
            lblTitulo.Text = "Módulo de facturas";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblReserva = new Label();
            lblReserva.Text = "Reserva";
            lblReserva.Location = new Point(25, 70);

            cboReserva = new ComboBox();
            cboReserva.Location = new Point(25, 95);
            cboReserva.Width = 320;
            cboReserva.DropDownStyle = ComboBoxStyle.DropDownList;
            cboReserva.SelectedIndexChanged += CboReserva_SelectedIndexChanged;

            lblCliente = new Label();
            lblCliente.Text = "Cliente";
            lblCliente.Location = new Point(370, 70);

            txtCliente = new TextBox();
            txtCliente.Location = new Point(370, 95);
            txtCliente.Width = 240;
            txtCliente.ReadOnly = true;

            lblHabitacion = new Label();
            lblHabitacion.Text = "Habitación";
            lblHabitacion.Location = new Point(635, 70);

            txtHabitacion = new TextBox();
            txtHabitacion.Location = new Point(635, 95);
            txtHabitacion.Width = 120;
            txtHabitacion.ReadOnly = true;

            lblPorcImpuesto = new Label();
            lblPorcImpuesto.Text = "% Impuesto";
            lblPorcImpuesto.Location = new Point(780, 70);

            txtPorcImpuesto = new TextBox();
            txtPorcImpuesto.Location = new Point(780, 95);
            txtPorcImpuesto.Width = 80;
            txtPorcImpuesto.Text = "18";

            lblSubtotal = new Label();
            lblSubtotal.Text = "Subtotal";
            lblSubtotal.Location = new Point(25, 140);

            txtSubtotal = new TextBox();
            txtSubtotal.Location = new Point(25, 165);
            txtSubtotal.Width = 140;
            txtSubtotal.ReadOnly = true;

            lblDescuento = new Label();
            lblDescuento.Text = "Descuento";
            lblDescuento.Location = new Point(190, 140);

            txtDescuento = new TextBox();
            txtDescuento.Location = new Point(190, 165);
            txtDescuento.Width = 140;
            txtDescuento.ReadOnly = true;

            lblImpuesto = new Label();
            lblImpuesto.Text = "Impuesto";
            lblImpuesto.Location = new Point(355, 140);

            txtImpuesto = new TextBox();
            txtImpuesto.Location = new Point(355, 165);
            txtImpuesto.Width = 140;
            txtImpuesto.ReadOnly = true;

            lblTotal = new Label();
            lblTotal.Text = "Total";
            lblTotal.Location = new Point(520, 140);

            txtTotal = new TextBox();
            txtTotal.Location = new Point(520, 165);
            txtTotal.Width = 140;
            txtTotal.ReadOnly = true;

            lblEstado = new Label();
            lblEstado.Text = "Estado";
            lblEstado.Location = new Point(685, 140);

            txtEstado = new TextBox();
            txtEstado.Location = new Point(685, 165);
            txtEstado.Width = 140;
            txtEstado.ReadOnly = true;

            btnCalcular = CrearBotonBlanco("Calcular", 25, 215, 100, 32);
            btnCalcular.Click += BtnCalcular_Click;

            btnGenerar = CrearBotonBlanco("Generar factura", 135, 215, 130, 32);
            btnGenerar.Click += BtnGenerar_Click;

            btnVerDetalle = CrearBotonBlanco("Ver detalle", 275, 215, 110, 32);
            btnVerDetalle.Click += BtnVerDetalle_Click;

            btnImprimirPdf = CrearBotonBlanco("Imprimir / PDF", 395, 215, 130, 32);
            btnImprimirPdf.Click += BtnImprimirPdf_Click;

            btnExportarPdf = CrearBotonBlanco("Exportar PDF", 535, 215, 120, 32);
            btnExportarPdf.Click += BtnExportarPdf_Click;

            lblBuscar = new Label();
            lblBuscar.Text = "Buscar";
            lblBuscar.Location = new Point(670, 195);

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(670, 220);
            txtBuscar.Width = 180;

            btnBuscar = CrearBotonBlanco("Buscar", 860, 215, 80, 32);
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBotonBlanco("Limpiar", 950, 215, 80, 32);
            btnLimpiar.Click += BtnLimpiar_Click;

            dgvFacturas = new DataGridView();
            dgvFacturas.Location = new Point(25, 275);
            dgvFacturas.Size = new Size(980, 250);
            dgvFacturas.AllowUserToAddRows = false;
            dgvFacturas.AllowUserToDeleteRows = false;
            dgvFacturas.ReadOnly = true;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvFacturas.ScrollBars = ScrollBars.Both;
            dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFacturas.MultiSelect = false;
            dgvFacturas.RowHeadersWidth = 25;
            dgvFacturas.CellClick += DgvFacturas_CellClick;

            btnPaginaAnterior = CrearBotonBlanco("< Anterior", 25, 535, 100, 28);
            btnPaginaAnterior.Click += (s, e) => {
                if (_paginaActual > 0 && !_modosBusqueda) { _paginaActual--; CargarFacturas(); }
            };

            lblPaginaActual = new Label();
            lblPaginaActual.Text = "Pag. 1";
            lblPaginaActual.AutoSize = true;
            lblPaginaActual.Font = Theme.AppFontBold;
            lblPaginaActual.Location = new Point(135, 540);

            btnPaginaSiguiente = CrearBotonBlanco("Siguiente >", 195, 535, 110, 28);
            btnPaginaSiguiente.Click += (s, e) => {
                if (!_modosBusqueda) { _paginaActual++; CargarFacturas(); }
            };

            lblPagoTitulo = new Label();
            lblPagoTitulo.Text = "Registro de pagos";
            lblPagoTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPagoTitulo.AutoSize = true;
            lblPagoTitulo.Location = new Point(25, 550);

            lblFacturaSeleccionada = new Label();
            lblFacturaSeleccionada.Text = "Factura seleccionada";
            lblFacturaSeleccionada.Location = new Point(25, 585);

            txtFacturaSeleccionada = new TextBox();
            txtFacturaSeleccionada.Location = new Point(25, 610);
            txtFacturaSeleccionada.Width = 130;
            txtFacturaSeleccionada.ReadOnly = true;

            lblTotalFactura = new Label();
            lblTotalFactura.Text = "Total factura";
            lblTotalFactura.Location = new Point(180, 585);

            txtTotalFacturaPago = new TextBox();
            txtTotalFacturaPago.Location = new Point(180, 610);
            txtTotalFacturaPago.Width = 130;
            txtTotalFacturaPago.ReadOnly = true;

            lblTotalPagado = new Label();
            lblTotalPagado.Text = "Total pagado";
            lblTotalPagado.Location = new Point(335, 585);

            txtTotalPagado = new TextBox();
            txtTotalPagado.Location = new Point(335, 610);
            txtTotalPagado.Width = 130;
            txtTotalPagado.ReadOnly = true;

            lblBalance = new Label();
            lblBalance.Text = "Balance";
            lblBalance.Location = new Point(490, 585);

            txtBalance = new TextBox();
            txtBalance.Location = new Point(490, 610);
            txtBalance.Width = 130;
            txtBalance.ReadOnly = true;

            lblCambio = new Label();
            lblCambio.Text = "Cambio";
            lblCambio.Location = new Point(645, 585);

            txtCambio = new TextBox();
            txtCambio.Location = new Point(645, 610);
            txtCambio.Width = 130;
            txtCambio.ReadOnly = true;

            lblMetodoPago = new Label();
            lblMetodoPago.Text = "Método de pago";
            lblMetodoPago.Location = new Point(25, 650);

            cboMetodoPago = new ComboBox();
            cboMetodoPago.Location = new Point(25, 675);
            cboMetodoPago.Width = 180;
            cboMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMetodoPago.SelectedIndexChanged += MetodoOMontoCambio_Evento;

            lblMontoPago = new Label();
            lblMontoPago.Text = "Monto a pagar";
            lblMontoPago.Location = new Point(230, 650);

            txtMontoPago = new TextBox();
            txtMontoPago.Location = new Point(230, 675);
            txtMontoPago.Width = 120;
            txtMontoPago.TextChanged += MetodoOMontoCambio_Evento;

            lblReferencia = new Label();
            lblReferencia.Text = "Referencia";
            lblReferencia.Location = new Point(375, 650);

            txtReferencia = new TextBox();
            txtReferencia.Location = new Point(375, 675);
            txtReferencia.Width = 180;

            lblObservacionPago = new Label();
            lblObservacionPago.Text = "Observación";
            lblObservacionPago.Location = new Point(580, 650);

            txtObservacionPago = new TextBox();
            txtObservacionPago.Location = new Point(580, 675);
            txtObservacionPago.Width = 250;

            btnRegistrarPago = CrearBotonBlanco("Registrar pago", 850, 670, 130, 32);
            btnRegistrarPago.Click += BtnRegistrarPago_Click;

            dgvPagos = new DataGridView();
            dgvPagos.Location = new Point(25, 730);
            dgvPagos.Size = new Size(980, 220);
            dgvPagos.AllowUserToAddRows = false;
            dgvPagos.AllowUserToDeleteRows = false;
            dgvPagos.ReadOnly = true;
            dgvPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPagos.ScrollBars = ScrollBars.Both;
            dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPagos.MultiSelect = false;
            dgvPagos.RowHeadersWidth = 25;

            printDocumentFactura = new PrintDocument();
            printDocumentFactura.PrintPage += PrintDocumentFactura_PrintPage;

            Controls.Add(lblTitulo);
            Controls.Add(lblReserva);
            Controls.Add(cboReserva);
            Controls.Add(lblCliente);
            Controls.Add(txtCliente);
            Controls.Add(lblHabitacion);
            Controls.Add(txtHabitacion);
            Controls.Add(lblPorcImpuesto);
            Controls.Add(txtPorcImpuesto);
            Controls.Add(lblSubtotal);
            Controls.Add(txtSubtotal);
            Controls.Add(lblDescuento);
            Controls.Add(txtDescuento);
            Controls.Add(lblImpuesto);
            Controls.Add(txtImpuesto);
            Controls.Add(lblTotal);
            Controls.Add(txtTotal);
            Controls.Add(lblEstado);
            Controls.Add(txtEstado);
            Controls.Add(btnCalcular);
            Controls.Add(btnGenerar);
            Controls.Add(btnVerDetalle);
            Controls.Add(btnImprimirPdf);
            Controls.Add(btnExportarPdf);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvFacturas);
            Controls.Add(btnPaginaAnterior);
            Controls.Add(lblPaginaActual);
            Controls.Add(btnPaginaSiguiente);

            Controls.Add(lblPagoTitulo);
            Controls.Add(lblFacturaSeleccionada);
            Controls.Add(txtFacturaSeleccionada);
            Controls.Add(lblTotalFactura);
            Controls.Add(txtTotalFacturaPago);
            Controls.Add(lblTotalPagado);
            Controls.Add(txtTotalPagado);
            Controls.Add(lblBalance);
            Controls.Add(txtBalance);
            Controls.Add(lblCambio);
            Controls.Add(txtCambio);
            Controls.Add(lblMetodoPago);
            Controls.Add(cboMetodoPago);
            Controls.Add(lblMontoPago);
            Controls.Add(txtMontoPago);
            Controls.Add(lblReferencia);
            Controls.Add(txtReferencia);
            Controls.Add(lblObservacionPago);
            Controls.Add(txtObservacionPago);
            Controls.Add(btnRegistrarPago);
            Controls.Add(dgvPagos);

            btnVerDetalle.Enabled = false;
            btnImprimirPdf.Enabled = false;
            btnRegistrarPago.Enabled = false;
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

        private void CargarReservasSinFactura()
        {
            try
            {
                cargandoReservas = true;

                DataTable dt = Database.ObtenerReservasSinFacturaCombo();
                cboReserva.DataSource = null;
                cboReserva.DisplayMember = "";
                cboReserva.ValueMember = "";

                cboReserva.DataSource = dt;
                cboReserva.DisplayMember = "Reserva";
                cboReserva.ValueMember = "IdReserva";
                cboReserva.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reservas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cargandoReservas = false;
            }
        }

        private void CargarFacturas()
        {
            try
            {
                _modosBusqueda = false;
                DataTable dt = Database.ObtenerFacturasPaginado(_paginaActual, TamPagina);
                dgvFacturas.DataSource = dt;
                ConfigurarColumnasFacturas();
                dgvFacturas.ClearSelection();

                lblPaginaActual.Text = "Pag. " + (_paginaActual + 1);
                btnPaginaAnterior.Enabled = _paginaActual > 0;
                btnPaginaSiguiente.Enabled = dt.Rows.Count == TamPagina;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar facturas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMetodosPago()
        {
            try
            {
                DataTable dt = Database.ObtenerMetodosPago();
                cboMetodoPago.DataSource = dt;
                cboMetodoPago.DisplayMember = "NombreMetodo";
                cboMetodoPago.ValueMember = "IdMetodoPago";
                cboMetodoPago.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar métodos de pago: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPagosFactura()
        {
            try
            {
                if (idFacturaSeleccionada == 0)
                {
                    dgvPagos.DataSource = null;
                    return;
                }

                dgvPagos.DataSource = Database.ObtenerPagosPorFactura(idFacturaSeleccionada);
                ConfigurarColumnasPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasFacturas()
        {
            if (dgvFacturas.Columns.Count == 0)
                return;

            dgvFacturas.Columns["IdFactura"].Width = 80;
            dgvFacturas.Columns["IdReserva"].Width = 80;
            dgvFacturas.Columns["Cliente"].Width = 180;
            dgvFacturas.Columns["Habitacion"].Width = 90;
            dgvFacturas.Columns["FechaFactura"].Width = 130;
            dgvFacturas.Columns["Subtotal"].Width = 100;
            dgvFacturas.Columns["Descuento"].Width = 100;
            dgvFacturas.Columns["Impuesto"].Width = 100;
            dgvFacturas.Columns["Total"].Width = 100;
            dgvFacturas.Columns["Estado"].Width = 100;
        }

        private void ConfigurarColumnasPagos()
        {
            if (dgvPagos.Columns.Count == 0)
                return;

            if (dgvPagos.Columns.Contains("IdFactura"))
                dgvPagos.Columns["IdFactura"].Visible = false;

            dgvPagos.Columns["IdPago"].Width = 80;
            dgvPagos.Columns["MetodoPago"].Width = 140;
            dgvPagos.Columns["FechaPago"].Width = 140;
            dgvPagos.Columns["Monto"].Width = 100;
            dgvPagos.Columns["Referencia"].Width = 180;
            dgvPagos.Columns["Observacion"].Width = 260;
        }

        private bool ValidarImpuesto(out decimal porcImpuesto)
        {
            porcImpuesto = 0;

            if (!decimal.TryParse(txtPorcImpuesto.Text.Trim(), out porcImpuesto))
            {
                MessageBox.Show("Debe escribir un porcentaje de impuesto válido.");
                txtPorcImpuesto.Focus();
                return false;
            }

            if (porcImpuesto < 0)
            {
                MessageBox.Show("El porcentaje de impuesto no puede ser negativo.");
                txtPorcImpuesto.Focus();
                return false;
            }

            return true;
        }

        private bool ObtenerIdReservaSeleccionada(out int idReserva)
        {
            idReserva = 0;

            if (cboReserva.SelectedValue == null)
                return false;

            if (cboReserva.SelectedValue is DataRowView)
                return false;

            return int.TryParse(cboReserva.SelectedValue.ToString(), out idReserva);
        }

        private void LimpiarPreviewGeneracion()
        {
            txtCliente.Clear();
            txtHabitacion.Clear();
            txtSubtotal.Clear();
            txtDescuento.Clear();
            txtImpuesto.Clear();
            txtTotal.Clear();
            txtEstado.Clear();
            ActualizarVisualEstado();
        }

        private void LimpiarSeccionPago()
        {
            idFacturaSeleccionada = 0;
            txtFacturaSeleccionada.Clear();
            txtTotalFacturaPago.Clear();
            txtTotalPagado.Clear();
            txtBalance.Clear();
            txtCambio.Clear();
            cboMetodoPago.SelectedIndex = -1;
            txtMontoPago.Clear();
            txtReferencia.Clear();
            txtObservacionPago.Clear();
            dgvPagos.DataSource = null;
            ActualizarVisualBalance();
            ActualizarVisualCambio();
            ActualizarEstadoBotonesFactura();
        }

        private void LimpiarCampos()
        {
            cargandoReservas = true;
            cboReserva.SelectedIndex = -1;
            cargandoReservas = false;

            txtBuscar.Clear();
            txtPorcImpuesto.Text = "18";
            LimpiarPreviewGeneracion();
            LimpiarSeccionPago();
            dgvFacturas.ClearSelection();
        }

        private void MostrarPreviewFactura()
        {
            LimpiarPreviewGeneracion();

            if (cargandoReservas)
                return;

            if (!ObtenerIdReservaSeleccionada(out int idReserva))
                return;

            if (!ValidarImpuesto(out decimal porcImpuesto))
                return;

            try
            {
                DataTable dt = Database.ObtenerDetalleFacturaPreview(idReserva, porcImpuesto);

                if (dt.Rows.Count == 0)
                    return;

                DataRow fila = dt.Rows[0];

                txtCliente.Text = fila["Cliente"].ToString();
                txtHabitacion.Text = fila["Habitacion"].ToString();
                txtSubtotal.Text = Convert.ToDecimal(fila["Subtotal"]).ToString("N2");
                txtDescuento.Text = Convert.ToDecimal(fila["Descuento"]).ToString("N2");
                txtImpuesto.Text = Convert.ToDecimal(fila["Impuesto"]).ToString("N2");
                txtTotal.Text = Convert.ToDecimal(fila["Total"]).ToString("N2");
                txtEstado.Text = "PENDIENTE";
                ActualizarVisualEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular factura: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarVisualEstado()
        {
            bool dark = Assets.Theme.Current == Assets.AppTheme.Dark;
            txtEstado.BackColor = Assets.Theme.InputBackground;
            txtEstado.ForeColor = Assets.Theme.InputForeground;

            string estado = txtEstado.Text.Trim().ToUpper();

            if (estado == "PAGADA")
            {
                txtEstado.BackColor = dark ? Color.FromArgb(20, 70, 40)  : Color.Honeydew;
                txtEstado.ForeColor = dark ? Color.FromArgb(100, 220, 140) : Color.DarkGreen;
            }
            else if (estado == "PENDIENTE")
            {
                txtEstado.BackColor = dark ? Color.FromArgb(70, 55, 10)  : Color.LemonChiffon;
                txtEstado.ForeColor = dark ? Color.FromArgb(240, 195, 80) : Color.DarkGoldenrod;
            }
            else if (estado == "ANULADA")
            {
                txtEstado.BackColor = dark ? Color.FromArgb(80, 20, 20)  : Color.MistyRose;
                txtEstado.ForeColor = dark ? Color.FromArgb(220, 100, 100) : Color.DarkRed;
            }
        }

        private void ActualizarVisualBalance()
        {
            bool dark = Assets.Theme.Current == Assets.AppTheme.Dark;
            txtBalance.BackColor = Assets.Theme.InputBackground;
            txtBalance.ForeColor = Assets.Theme.InputForeground;

            if (decimal.TryParse(txtBalance.Text.Trim(), out decimal balance))
            {
                if (balance <= 0)
                {
                    txtBalance.BackColor = dark ? Color.FromArgb(20, 70, 40)  : Color.Honeydew;
                    txtBalance.ForeColor = dark ? Color.FromArgb(100, 220, 140) : Color.DarkGreen;
                }
                else
                {
                    txtBalance.BackColor = dark ? Color.FromArgb(70, 55, 10)  : Color.LemonChiffon;
                    txtBalance.ForeColor = dark ? Color.FromArgb(240, 195, 80) : Color.DarkGoldenrod;
                }
            }
        }

        private void ActualizarVisualCambio()
        {
            bool dark = Assets.Theme.Current == Assets.AppTheme.Dark;
            txtCambio.BackColor = Assets.Theme.InputBackground;
            txtCambio.ForeColor = Assets.Theme.InputForeground;

            if (decimal.TryParse(txtCambio.Text.Trim(), out decimal cambio) && cambio > 0)
            {
                txtCambio.BackColor = dark ? Color.FromArgb(20, 70, 40)  : Color.Honeydew;
                txtCambio.ForeColor = dark ? Color.FromArgb(100, 220, 140) : Color.DarkGreen;
            }
        }

        private void ActualizarEstadoBotonesFactura()
        {
            bool hayFactura = idFacturaSeleccionada > 0;
            btnVerDetalle.Enabled = hayFactura;
            btnImprimirPdf.Enabled = hayFactura;
            btnExportarPdf.Enabled = hayFactura;

            bool puedePagar = false;

            if (hayFactura && decimal.TryParse(txtBalance.Text.Trim(), out decimal balance))
                puedePagar = balance > 0;

            btnRegistrarPago.Enabled = puedePagar;
        }

        private bool MetodoEsEfectivo()
        {
            string metodo = cboMetodoPago.Text.Trim().ToUpper();
            return metodo.Contains("EFECTIVO");
        }

        private void RecalcularCambio()
        {
            txtCambio.Text = "0.00";

            if (!decimal.TryParse(txtBalance.Text.Trim(), out decimal balance))
            {
                ActualizarVisualCambio();
                return;
            }

            if (!decimal.TryParse(txtMontoPago.Text.Trim(), out decimal montoIngresado))
            {
                ActualizarVisualCambio();
                return;
            }

            if (!MetodoEsEfectivo())
            {
                ActualizarVisualCambio();
                return;
            }

            if (montoIngresado > balance)
                txtCambio.Text = (montoIngresado - balance).ToString("N2");

            ActualizarVisualCambio();
        }

        private void MetodoOMontoCambio_Evento(object sender, EventArgs e)
        {
            RecalcularCambio();
        }

        private void CboReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarPreviewFactura();
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            MostrarPreviewFactura();
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            if (!ObtenerIdReservaSeleccionada(out int idReserva))
            {
                MessageBox.Show("Debe seleccionar una reserva válida.");
                return;
            }

            if (!ValidarImpuesto(out decimal porcImpuesto))
                return;

            try
            {
                Database.GenerarFactura(idReserva, porcImpuesto);

                MessageBox.Show("Factura generada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarFacturas();
                CargarReservasSinFactura();
                LimpiarPreviewGeneracion();
                LimpiarSeccionPago();
                dgvFacturas.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar factura: " + ex.Message,
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
                    CargarFacturas();
                    return;
                }

                _modosBusqueda = true;
                dgvFacturas.DataSource = Database.BuscarFacturas(texto);
                ConfigurarColumnasFacturas();
                dgvFacturas.ClearSelection();
                LimpiarSeccionPago();
                btnPaginaAnterior.Enabled = false;
                btnPaginaSiguiente.Enabled = false;
                lblPaginaActual.Text = "Busqueda";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar facturas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarFacturas();
            CargarReservasSinFactura();
        }

        private void CargarResumenPagoDesdeFila(DataGridViewRow fila)
        {
            idFacturaSeleccionada = Convert.ToInt32(fila.Cells["IdFactura"].Value);

            decimal totalFactura = Convert.ToDecimal(fila.Cells["Total"].Value);
            decimal totalPagado = Database.ObtenerTotalPagadoFactura(idFacturaSeleccionada);
            decimal balance = totalFactura - totalPagado;

            txtFacturaSeleccionada.Text = idFacturaSeleccionada.ToString();
            txtTotalFacturaPago.Text = totalFactura.ToString("N2");
            txtTotalPagado.Text = totalPagado.ToString("N2");
            txtBalance.Text = balance.ToString("N2");
            txtCambio.Text = "0.00";
            txtMontoPago.Clear();
            txtReferencia.Clear();
            txtObservacionPago.Clear();

            CargarPagosFactura();
            ActualizarVisualBalance();
            ActualizarVisualCambio();
            ActualizarEstadoBotonesFactura();
        }

        private void DgvFacturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvFacturas.CurrentRow == null)
                return;

            DataGridViewRow fila = dgvFacturas.CurrentRow;

            txtCliente.Text = fila.Cells["Cliente"].Value?.ToString();
            txtHabitacion.Text = fila.Cells["Habitacion"].Value?.ToString();
            txtSubtotal.Text = Convert.ToDecimal(fila.Cells["Subtotal"].Value).ToString("N2");
            txtDescuento.Text = Convert.ToDecimal(fila.Cells["Descuento"].Value).ToString("N2");
            txtImpuesto.Text = Convert.ToDecimal(fila.Cells["Impuesto"].Value).ToString("N2");
            txtTotal.Text = Convert.ToDecimal(fila.Cells["Total"].Value).ToString("N2");
            txtEstado.Text = fila.Cells["Estado"].Value?.ToString();

            ActualizarVisualEstado();
            CargarResumenPagoDesdeFila(fila);
        }

        private void BtnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (idFacturaSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una factura del grid.");
                return;
            }

            if (cboMetodoPago.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un método de pago.");
                cboMetodoPago.Focus();
                return;
            }

            if (!decimal.TryParse(txtMontoPago.Text.Trim(), out decimal montoIngresado))
            {
                MessageBox.Show("Debe escribir un monto válido.");
                txtMontoPago.Focus();
                return;
            }

            if (!decimal.TryParse(txtBalance.Text.Trim(), out decimal balance))
            {
                MessageBox.Show("No se pudo calcular el balance.");
                return;
            }

            if (balance <= 0)
            {
                MessageBox.Show("La factura ya está saldada.");
                return;
            }

            if (montoIngresado <= 0)
            {
                MessageBox.Show("El monto del pago debe ser mayor que cero.");
                txtMontoPago.Focus();
                return;
            }

            bool esEfectivo = MetodoEsEfectivo();
            decimal montoRegistrar = montoIngresado;

            if (!esEfectivo && montoIngresado > balance)
            {
                MessageBox.Show("El monto excede el balance pendiente.");
                txtMontoPago.Focus();
                return;
            }

            if (esEfectivo && montoIngresado > balance)
                montoRegistrar = balance;

            try
            {
                Database.RegistrarPago(
                    idFacturaSeleccionada,
                    Convert.ToInt32(cboMetodoPago.SelectedValue),
                    montoRegistrar,
                    txtReferencia.Text.Trim(),
                    txtObservacionPago.Text.Trim()
                );

                decimal cambio = 0;

                if (esEfectivo && montoIngresado > balance)
                    cambio = montoIngresado - balance;

                MessageBox.Show(
                    cambio > 0
                        ? "Pago registrado correctamente. Cambio: " + cambio.ToString("N2")
                        : "Pago registrado correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                int facturaActual = idFacturaSeleccionada;

                CargarFacturas();
                CargarReservasSinFactura();

                foreach (DataGridViewRow row in dgvFacturas.Rows)
                {
                    if (row.Cells["IdFactura"].Value != null &&
                        Convert.ToInt32(row.Cells["IdFactura"].Value) == facturaActual)
                    {
                        row.Selected = true;
                        dgvFacturas.CurrentCell = row.Cells["IdFactura"];
                        CargarResumenPagoDesdeFila(row);
                        txtEstado.Text = row.Cells["Estado"].Value?.ToString();
                        txtCliente.Text = row.Cells["Cliente"].Value?.ToString();
                        txtHabitacion.Text = row.Cells["Habitacion"].Value?.ToString();
                        txtSubtotal.Text = Convert.ToDecimal(row.Cells["Subtotal"].Value).ToString("N2");
                        txtDescuento.Text = Convert.ToDecimal(row.Cells["Descuento"].Value).ToString("N2");
                        txtImpuesto.Text = Convert.ToDecimal(row.Cells["Impuesto"].Value).ToString("N2");
                        txtTotal.Text = Convert.ToDecimal(row.Cells["Total"].Value).ToString("N2");
                        ActualizarVisualEstado();
                        txtCambio.Text = cambio.ToString("N2");
                        ActualizarVisualCambio();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar pago: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable ObtenerResumenDetalleFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        f.IdFactura,
                        f.IdReserva,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        h.Numero AS Habitacion,
                        r.FechaEntrada,
                        r.FechaSalida,
                        DATEDIFF(DAY, r.FechaEntrada, r.FechaSalida) AS Noches,
                        ISNULL(r.Subtotal, 0) AS Hospedaje,
                        ISNULL(srv.TotalServicios, 0) AS Servicios,
                        ISNULL(pro.TotalPromociones, 0) AS Promociones,
                        f.Subtotal,
                        f.Descuento,
                        f.Impuesto,
                        f.Total,
                        f.Estado,
                        f.FechaFactura
                    FROM dbo.Factura f
                    INNER JOIN dbo.Reserva r ON r.IdReserva = f.IdReserva
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    OUTER APPLY
                    (
                        SELECT SUM(Subtotal) AS TotalServicios
                        FROM dbo.ReservaServicio
                        WHERE IdReserva = r.IdReserva
                    ) srv
                    OUTER APPLY
                    (
                        SELECT SUM(DescuentoAplicado) AS TotalPromociones
                        FROM dbo.ReservaPromocion
                        WHERE IdReserva = r.IdReserva
                    ) pro
                    WHERE f.IdFactura = @IdFactura;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private DataTable ObtenerServiciosDetalleFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        sa.NombreServicio,
                        rs.Cantidad,
                        rs.PrecioUnitario,
                        rs.Subtotal
                    FROM dbo.Factura f
                    INNER JOIN dbo.ReservaServicio rs ON rs.IdReserva = f.IdReserva
                    INNER JOIN dbo.ServicioAdicional sa ON sa.IdServicioAdicional = rs.IdServicioAdicional
                    WHERE f.IdFactura = @IdFactura
                    ORDER BY sa.NombreServicio;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private DataTable ObtenerPromocionesDetalleFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        p.NombrePromocion,
                        p.TipoDescuento,
                        p.ValorDescuento,
                        rp.DescuentoAplicado
                    FROM dbo.Factura f
                    INNER JOIN dbo.ReservaPromocion rp ON rp.IdReserva = f.IdReserva
                    INNER JOIN dbo.Promocion p ON p.IdPromocion = rp.IdPromocion
                    WHERE f.IdFactura = @IdFactura
                    ORDER BY p.NombrePromocion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private DataTable ObtenerPagosDetalleFactura(int idFactura)
        {
            return Database.ObtenerPagosPorFactura(idFactura);
        }

        private string ConstruirTextoResumenFactura(DataRow row)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Factura No.: " + row["IdFactura"]);
            sb.AppendLine("Reserva No.: " + row["IdReserva"]);
            sb.AppendLine("Cliente: " + row["Cliente"]);
            sb.AppendLine("Habitación: " + row["Habitacion"]);
            sb.AppendLine("Fecha factura: " + Convert.ToDateTime(row["FechaFactura"]).ToString("dd/MM/yyyy hh:mm tt"));
            sb.AppendLine("Entrada: " + Convert.ToDateTime(row["FechaEntrada"]).ToString("dd/MM/yyyy"));
            sb.AppendLine("Salida: " + Convert.ToDateTime(row["FechaSalida"]).ToString("dd/MM/yyyy"));
            sb.AppendLine("Noches: " + row["Noches"]);
            sb.AppendLine("Hospedaje: " + Convert.ToDecimal(row["Hospedaje"]).ToString("N2"));
            sb.AppendLine("Servicios: " + Convert.ToDecimal(row["Servicios"]).ToString("N2"));
            sb.AppendLine("Promociones: " + Convert.ToDecimal(row["Promociones"]).ToString("N2"));
            sb.AppendLine("Subtotal factura: " + Convert.ToDecimal(row["Subtotal"]).ToString("N2"));
            sb.AppendLine("Descuento factura: " + Convert.ToDecimal(row["Descuento"]).ToString("N2"));
            sb.AppendLine("Impuesto: " + Convert.ToDecimal(row["Impuesto"]).ToString("N2"));
            sb.AppendLine("Total: " + Convert.ToDecimal(row["Total"]).ToString("N2"));
            sb.AppendLine("Estado: " + row["Estado"]);

            return sb.ToString();
        }

        private void MostrarDetalleFactura(int idFactura)
        {
            DataTable dtResumen = ObtenerResumenDetalleFactura(idFactura);
            DataTable dtServicios = ObtenerServiciosDetalleFactura(idFactura);
            DataTable dtPromociones = ObtenerPromocionesDetalleFactura(idFactura);
            DataTable dtPagos = ObtenerPagosDetalleFactura(idFactura);

            if (dtResumen.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró el detalle de la factura.");
                return;
            }

            Form frm = new Form();
            frm.Text = "Detalle de factura";
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Size = new Size(1040, 760);
            frm.BackColor = Theme.FormBackground;

            Label lblDetalleTitulo = new Label();
            lblDetalleTitulo.Text = "Detalle completo de factura";
            lblDetalleTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDetalleTitulo.AutoSize = true;
            lblDetalleTitulo.Location = new Point(20, 15);

            TextBox txtResumen = new TextBox();
            txtResumen.Location = new Point(20, 50);
            txtResumen.Size = new Size(970, 170);
            txtResumen.Multiline = true;
            txtResumen.ReadOnly = true;
            txtResumen.ScrollBars = ScrollBars.Vertical;
            txtResumen.Text = ConstruirTextoResumenFactura(dtResumen.Rows[0]);

            Label lblServiciosDet = new Label();
            lblServiciosDet.Text = "Servicios incluidos";
            lblServiciosDet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblServiciosDet.AutoSize = true;
            lblServiciosDet.Location = new Point(20, 235);

            DataGridView dgvServiciosDet = new DataGridView();
            dgvServiciosDet.Location = new Point(20, 260);
            dgvServiciosDet.Size = new Size(470, 180);
            dgvServiciosDet.ReadOnly = true;
            dgvServiciosDet.AllowUserToAddRows = false;
            dgvServiciosDet.AllowUserToDeleteRows = false;
            dgvServiciosDet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvServiciosDet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServiciosDet.MultiSelect = false;
            dgvServiciosDet.RowHeadersWidth = 25;
            dgvServiciosDet.DataSource = dtServicios;

            if (dgvServiciosDet.Columns.Count > 0)
            {
                dgvServiciosDet.Columns["NombreServicio"].Width = 180;
                dgvServiciosDet.Columns["Cantidad"].Width = 70;
                dgvServiciosDet.Columns["PrecioUnitario"].Width = 90;
                dgvServiciosDet.Columns["Subtotal"].Width = 90;
            }

            Label lblPromosDet = new Label();
            lblPromosDet.Text = "Promociones aplicadas";
            lblPromosDet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPromosDet.AutoSize = true;
            lblPromosDet.Location = new Point(520, 235);

            DataGridView dgvPromosDet = new DataGridView();
            dgvPromosDet.Location = new Point(520, 260);
            dgvPromosDet.Size = new Size(470, 180);
            dgvPromosDet.ReadOnly = true;
            dgvPromosDet.AllowUserToAddRows = false;
            dgvPromosDet.AllowUserToDeleteRows = false;
            dgvPromosDet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPromosDet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromosDet.MultiSelect = false;
            dgvPromosDet.RowHeadersWidth = 25;
            dgvPromosDet.DataSource = dtPromociones;

            if (dgvPromosDet.Columns.Count > 0)
            {
                dgvPromosDet.Columns["NombrePromocion"].Width = 180;
                dgvPromosDet.Columns["TipoDescuento"].Width = 80;
                dgvPromosDet.Columns["ValorDescuento"].Width = 90;
                dgvPromosDet.Columns["DescuentoAplicado"].Width = 100;
            }

            Label lblPagosDet = new Label();
            lblPagosDet.Text = "Pagos registrados";
            lblPagosDet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPagosDet.AutoSize = true;
            lblPagosDet.Location = new Point(20, 460);

            DataGridView dgvPagosDet = new DataGridView();
            dgvPagosDet.Location = new Point(20, 485);
            dgvPagosDet.Size = new Size(970, 200);
            dgvPagosDet.ReadOnly = true;
            dgvPagosDet.AllowUserToAddRows = false;
            dgvPagosDet.AllowUserToDeleteRows = false;
            dgvPagosDet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPagosDet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPagosDet.MultiSelect = false;
            dgvPagosDet.RowHeadersWidth = 25;
            dgvPagosDet.DataSource = dtPagos;

            if (dgvPagosDet.Columns.Count > 0)
            {
                if (dgvPagosDet.Columns.Contains("IdFactura"))
                    dgvPagosDet.Columns["IdFactura"].Visible = false;

                dgvPagosDet.Columns["IdPago"].Width = 80;
                dgvPagosDet.Columns["MetodoPago"].Width = 140;
                dgvPagosDet.Columns["FechaPago"].Width = 140;
                dgvPagosDet.Columns["Monto"].Width = 100;
                dgvPagosDet.Columns["Referencia"].Width = 180;
                dgvPagosDet.Columns["Observacion"].Width = 260;
            }

            frm.Controls.Add(lblDetalleTitulo);
            frm.Controls.Add(txtResumen);
            frm.Controls.Add(lblServiciosDet);
            frm.Controls.Add(dgvServiciosDet);
            frm.Controls.Add(lblPromosDet);
            frm.Controls.Add(dgvPromosDet);
            frm.Controls.Add(lblPagosDet);
            frm.Controls.Add(dgvPagosDet);

            frm.ShowDialog(this);
        }

        private string ConstruirTextoImpresionFactura(int idFactura)
        {
            DataTable dtResumen = ObtenerResumenDetalleFactura(idFactura);
            DataTable dtServicios = ObtenerServiciosDetalleFactura(idFactura);
            DataTable dtPromociones = ObtenerPromocionesDetalleFactura(idFactura);
            DataTable dtPagos = ObtenerPagosDetalleFactura(idFactura);

            if (dtResumen.Rows.Count == 0)
                return "";

            DataRow row = dtResumen.Rows[0];
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=========================================");
            sb.AppendLine("             HOTEL RESERVAS");
            sb.AppendLine("=========================================");
            sb.AppendLine("FACTURA No.: " + row["IdFactura"]);
            sb.AppendLine("RESERVA No.: " + row["IdReserva"]);
            sb.AppendLine("CLIENTE: " + row["Cliente"]);
            sb.AppendLine("HABITACION: " + row["Habitacion"]);
            sb.AppendLine("FECHA FACTURA: " + Convert.ToDateTime(row["FechaFactura"]).ToString("dd/MM/yyyy hh:mm tt"));
            sb.AppendLine("ENTRADA: " + Convert.ToDateTime(row["FechaEntrada"]).ToString("dd/MM/yyyy"));
            sb.AppendLine("SALIDA: " + Convert.ToDateTime(row["FechaSalida"]).ToString("dd/MM/yyyy"));
            sb.AppendLine("NOCHES: " + row["Noches"]);
            sb.AppendLine("-----------------------------------------");
            sb.AppendLine("HOSPEDAJE: " + Convert.ToDecimal(row["Hospedaje"]).ToString("N2"));
            sb.AppendLine("SERVICIOS: " + Convert.ToDecimal(row["Servicios"]).ToString("N2"));
            sb.AppendLine("PROMOCIONES: " + Convert.ToDecimal(row["Promociones"]).ToString("N2"));
            sb.AppendLine("SUBTOTAL FACTURA: " + Convert.ToDecimal(row["Subtotal"]).ToString("N2"));
            sb.AppendLine("DESCUENTO FACTURA: " + Convert.ToDecimal(row["Descuento"]).ToString("N2"));
            sb.AppendLine("IMPUESTO: " + Convert.ToDecimal(row["Impuesto"]).ToString("N2"));
            sb.AppendLine("TOTAL: " + Convert.ToDecimal(row["Total"]).ToString("N2"));
            sb.AppendLine("ESTADO: " + row["Estado"]);
            sb.AppendLine("-----------------------------------------");

            sb.AppendLine("SERVICIOS INCLUIDOS:");
            if (dtServicios.Rows.Count == 0)
            {
                sb.AppendLine("  - Sin servicios adicionales");
            }
            else
            {
                foreach (DataRow s in dtServicios.Rows)
                {
                    sb.AppendLine("  - " + s["NombreServicio"] +
                                  " | Cant: " + s["Cantidad"] +
                                  " | PU: " + Convert.ToDecimal(s["PrecioUnitario"]).ToString("N2") +
                                  " | Subtotal: " + Convert.ToDecimal(s["Subtotal"]).ToString("N2"));
                }
            }

            sb.AppendLine("-----------------------------------------");
            sb.AppendLine("PROMOCIONES APLICADAS:");
            if (dtPromociones.Rows.Count == 0)
            {
                sb.AppendLine("  - Sin promociones");
            }
            else
            {
                foreach (DataRow p in dtPromociones.Rows)
                {
                    string tipo = p["TipoDescuento"].ToString() == "P" ? "PORCENTAJE" : "MONTO FIJO";
                    sb.AppendLine("  - " + p["NombrePromocion"] +
                                  " | Tipo: " + tipo +
                                  " | Valor: " + Convert.ToDecimal(p["ValorDescuento"]).ToString("N2") +
                                  " | Aplicado: " + Convert.ToDecimal(p["DescuentoAplicado"]).ToString("N2"));
                }
            }

            sb.AppendLine("-----------------------------------------");
            sb.AppendLine("PAGOS REGISTRADOS:");

            decimal totalPagado = 0;

            if (dtPagos.Rows.Count == 0)
            {
                sb.AppendLine("  - Sin pagos registrados");
            }
            else
            {
                foreach (DataRow pago in dtPagos.Rows)
                {
                    decimal montoPago = Convert.ToDecimal(pago["Monto"]);
                    totalPagado += montoPago;

                    sb.AppendLine("  - " + pago["MetodoPago"] +
                                  " | Fecha: " + Convert.ToDateTime(pago["FechaPago"]).ToString("dd/MM/yyyy hh:mm tt") +
                                  " | Monto: " + montoPago.ToString("N2"));
                }
            }

            decimal totalFactura = Convert.ToDecimal(row["Total"]);
            decimal balance = totalFactura - totalPagado;

            sb.AppendLine("-----------------------------------------");
            sb.AppendLine("TOTAL PAGADO: " + totalPagado.ToString("N2"));
            sb.AppendLine("BALANCE: " + balance.ToString("N2"));
            sb.AppendLine("=========================================");

            return sb.ToString();
        }

        private void PrintDocumentFactura_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Font fuente = new Font("Consolas", 10))
            {
                RectangleF area = new RectangleF(
                    e.MarginBounds.Left,
                    e.MarginBounds.Top,
                    e.MarginBounds.Width,
                    e.MarginBounds.Height);

                e.Graphics.DrawString(textoImpresionFactura, fuente, Brushes.Black, area);
                e.HasMorePages = false;
            }
        }

        private void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            if (idFacturaSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una factura del grid.");
                return;
            }

            try
            {
                MostrarDetalleFactura(idFacturaSeleccionada);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar detalle de factura: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImprimirPdf_Click(object sender, EventArgs e)
        {
            if (idFacturaSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una factura del grid.");
                return;
            }

            try
            {
                textoImpresionFactura = ConstruirTextoImpresionFactura(idFacturaSeleccionada);

                if (string.IsNullOrWhiteSpace(textoImpresionFactura))
                {
                    MessageBox.Show("No se pudo construir el documento de impresión.");
                    return;
                }

                using (PrintDialog dlg = new PrintDialog())
                {
                    dlg.Document = printDocumentFactura;
                    dlg.UseEXDialog = true;

                    MessageBox.Show(
                        "Para exportar a PDF, seleccione la impresora 'Microsoft Print to PDF'.",
                        "Imprimir / PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        printDocumentFactura.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir/exportar factura: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportarPdf_Click(object sender, EventArgs e)
        {
            if (idFacturaSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una factura del grid.");
                return;
            }

            try
            {
                string ruta = FacturaPdfExporter.ExportarPdf(idFacturaSeleccionada);
                MessageBox.Show("PDF generado correctamente:\n" + ruta,
                    "PDF exportado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the PDF
                System.Diagnostics.Process.Start(ruta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar PDF: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}