using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class PromocionesForm : Form
    {
        private Label lblTitulo;
        private Label lblNombre;
        private Label lblTipo;
        private Label lblValor;
        private Label lblFechaInicio;
        private Label lblFechaFin;
        private Label lblBuscar;

        private TextBox txtNombre;
        private ComboBox cboTipoDescuento;
        private TextBox txtValor;
        private DateTimePicker dtFechaInicio;
        private DateTimePicker dtFechaFin;
        private TextBox txtBuscar;

        private CheckBox chkEstado;

        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvPromociones;

        private int idPromocionSeleccionada = 0;

        private EventHandler _themeHandler;

        public PromocionesForm()
        {
            // ✅ PRIMERO inicializar componentes
            InicializarComponentes();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            // ✅ DESPUÉS registrar animación
            AnimationEngine.Register(this);

            Load += PromocionesForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
        }

        private void PromocionesForm_Load(object sender, EventArgs e)
        {
            CargarPromociones();
        }

        private void InicializarComponentes()
        {
            Text = "Promociones";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1040, 620);
            MaximizeBox = false;

            lblTitulo = new Label();
            lblTitulo.Text = "Módulo de promociones";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblNombre = new Label();
            lblNombre.Text = "Nombre promoción";
            lblNombre.Location = new Point(25, 70);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(25, 95);
            txtNombre.Width = 220;

            lblTipo = new Label();
            lblTipo.Text = "Tipo descuento";
            lblTipo.Location = new Point(270, 70);

            cboTipoDescuento = new ComboBox();
            cboTipoDescuento.Location = new Point(270, 95);
            cboTipoDescuento.Width = 150;
            cboTipoDescuento.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoDescuento.Items.Add("PORCENTAJE");
            cboTipoDescuento.Items.Add("MONTO FIJO");

            lblValor = new Label();
            lblValor.Text = "Valor";
            lblValor.Location = new Point(445, 70);

            txtValor = new TextBox();
            txtValor.Location = new Point(445, 95);
            txtValor.Width = 100;

            lblFechaInicio = new Label();
            lblFechaInicio.Text = "Fecha inicio";
            lblFechaInicio.Location = new Point(570, 70);

            dtFechaInicio = new DateTimePicker();
            dtFechaInicio.Location = new Point(570, 95);
            dtFechaInicio.Width = 130;
            dtFechaInicio.Format = DateTimePickerFormat.Short;

            lblFechaFin = new Label();
            lblFechaFin.Text = "Fecha fin";
            lblFechaFin.Location = new Point(725, 70);

            dtFechaFin = new DateTimePicker();
            dtFechaFin.Location = new Point(725, 95);
            dtFechaFin.Width = 130;
            dtFechaFin.Format = DateTimePickerFormat.Short;

            chkEstado = new CheckBox();
            chkEstado.Text = "Promoción activa";
            chkEstado.Location = new Point(875, 97);
            chkEstado.AutoSize = true;
            chkEstado.Checked = true;

            btnNuevo = CrearBotonBlanco("Nuevo", 25, 145);
            btnNuevo.Click += BtnNuevo_Click;

            btnGuardar = CrearBotonBlanco("Guardar", 125, 145);
            btnGuardar.Click += BtnGuardar_Click;

            btnEditar = CrearBotonBlanco("Editar", 225, 145);
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = CrearBotonBlanco("Eliminar", 325, 145);
            btnEliminar.Click += BtnEliminar_Click;

            lblBuscar = new Label();
            lblBuscar.Text = "Buscar";
            lblBuscar.Width = 50;
            lblBuscar.Location = new Point(520, 125);

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(520, 150);
            txtBuscar.Width = 220;

            btnBuscar = CrearBotonBlanco("Buscar", 750, 145);
            btnBuscar.Width = 80;
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBotonBlanco("Limpiar", 845, 145);
            btnLimpiar.Width = 80;
            btnLimpiar.Click += BtnLimpiar_Click;

            dgvPromociones = new DataGridView();
            dgvPromociones.Location = new Point(25, 205);
            dgvPromociones.Size = new Size(970, 320);
            dgvPromociones.AllowUserToAddRows = false;
            dgvPromociones.AllowUserToDeleteRows = false;
            dgvPromociones.ReadOnly = true;
            dgvPromociones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPromociones.ScrollBars = ScrollBars.Both;
            dgvPromociones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromociones.MultiSelect = false;
            dgvPromociones.RowHeadersWidth = 25;
            dgvPromociones.CellClick += DgvPromociones_CellClick;

            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblTipo);
            Controls.Add(cboTipoDescuento);
            Controls.Add(lblValor);
            Controls.Add(txtValor);
            Controls.Add(lblFechaInicio);
            Controls.Add(dtFechaInicio);
            Controls.Add(lblFechaFin);
            Controls.Add(dtFechaFin);
            Controls.Add(chkEstado);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvPromociones);
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

        private void CargarPromociones()
        {
            try
            {
                dgvPromociones.DataSource = ReservasExtrasData.ObtenerPromocionesAdmin();
                ConfigurarGrid();
                dgvPromociones.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar promociones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            if (dgvPromociones.Columns.Count == 0)
                return;

            dgvPromociones.Columns["IdPromocion"].Width = 90;
            dgvPromociones.Columns["NombrePromocion"].Width = 190;
            dgvPromociones.Columns["TipoDescuento"].Width = 100;
            dgvPromociones.Columns["ValorDescuento"].Width = 110;
            dgvPromociones.Columns["FechaInicio"].Width = 95;
            dgvPromociones.Columns["FechaFin"].Width = 95;
            dgvPromociones.Columns["Estado"].Visible = false;
            dgvPromociones.Columns["EstadoTexto"].Width = 100;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe escribir el nombre de la promoción.");
                txtNombre.Focus();
                return false;
            }

            if (cboTipoDescuento.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el tipo de descuento.");
                cboTipoDescuento.Focus();
                return false;
            }

            if (!decimal.TryParse(txtValor.Text.Trim(), out decimal valor) || valor <= 0)
            {
                MessageBox.Show("Debe escribir un valor válido.");
                txtValor.Focus();
                return false;
            }

            if (dtFechaFin.Value.Date < dtFechaInicio.Value.Date)
            {
                MessageBox.Show("La fecha fin no puede ser menor que la fecha inicio.");
                dtFechaFin.Focus();
                return false;
            }

            return true;
        }

        private string ObtenerTipoDescuentoParaGuardar()
        {
            return cboTipoDescuento.Text == "PORCENTAJE" ? "P" : "F";
        }

        private void SeleccionarTipoDesdeValor(string tipo)
        {
            if (tipo == "P")
                cboTipoDescuento.SelectedItem = "PORCENTAJE";
            else
                cboTipoDescuento.SelectedItem = "MONTO FIJO";
        }

        private void LimpiarCampos()
        {
            idPromocionSeleccionada = 0;
            txtNombre.Clear();
            cboTipoDescuento.SelectedIndex = -1;
            txtValor.Clear();
            dtFechaInicio.Value = DateTime.Today;
            dtFechaFin.Value = DateTime.Today;
            txtBuscar.Clear();
            chkEstado.Checked = true;
            dgvPromociones.ClearSelection();
            txtNombre.Focus();
        }

        private void BtnNuevo_Click(object sender, EventArgs e) => LimpiarCampos();

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                ReservasExtrasData.InsertarPromocion(
                    txtNombre.Text.Trim(),
                    ObtenerTipoDescuentoParaGuardar(),
                    Convert.ToDecimal(txtValor.Text.Trim()),
                    dtFechaInicio.Value.Date,
                    dtFechaFin.Value.Date,
                    chkEstado.Checked
                );

                MessageBox.Show("Promoción guardada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPromociones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar promoción: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idPromocionSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una promoción para editar.");
                return;
            }

            if (!ValidarCampos()) return;

            try
            {
                ReservasExtrasData.ActualizarPromocion(
                    idPromocionSeleccionada,
                    txtNombre.Text.Trim(),
                    ObtenerTipoDescuentoParaGuardar(),
                    Convert.ToDecimal(txtValor.Text.Trim()),
                    dtFechaInicio.Value.Date,
                    dtFechaFin.Value.Date,
                    chkEstado.Checked
                );

                MessageBox.Show("Promoción actualizada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPromociones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar promoción: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idPromocionSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una promoción para inactivar.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Está seguro de inactivar esta promoción?",
                "Confirmar inactivación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;

            try
            {
                ReservasExtrasData.EliminarPromocion(idPromocionSeleccionada);

                MessageBox.Show("Promoción inactivada correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPromociones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inactivar promoción: " + ex.Message,
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
                    CargarPromociones();
                    return;
                }

                dgvPromociones.DataSource = ReservasExtrasData.BuscarPromociones(texto);
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar promociones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarPromociones();
        }

        private void DgvPromociones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvPromociones.CurrentRow == null)
                return;

            DataGridViewRow fila = dgvPromociones.CurrentRow;

            idPromocionSeleccionada = Convert.ToInt32(fila.Cells["IdPromocion"].Value);
            txtNombre.Text = fila.Cells["NombrePromocion"].Value?.ToString();
            SeleccionarTipoDesdeValor(fila.Cells["TipoDescuento"].Value?.ToString());
            txtValor.Text = fila.Cells["ValorDescuento"].Value?.ToString();
            dtFechaInicio.Value = Convert.ToDateTime(fila.Cells["FechaInicio"].Value);
            dtFechaFin.Value = Convert.ToDateTime(fila.Cells["FechaFin"].Value);
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