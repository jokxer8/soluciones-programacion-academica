using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class TiposHabitacionForm : Form
    {
        private Label lblTitulo;
        private Label lblNombreTipo;
        private Label lblCapacidad;
        private Label lblPrecioBase;
        private Label lblDescripcion;
        private Label lblBuscar;

        private TextBox txtNombreTipo;
        private TextBox txtCapacidad;
        private TextBox txtPrecioBase;
        private TextBox txtDescripcion;
        private TextBox txtBuscar;

        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvTipos;

        private int idTipoSeleccionado = 0;

        private EventHandler _themeHandler;

        public TiposHabitacionForm()
        {
            // ✅ PRIMERO inicializar componentes
            InicializarComponentes();

            // ✅ DESPUÉS registrar animación
            AnimationEngine.Register(this);

            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void InicializarComponentes()
        {
            Text = "Módulo de tipos de habitación";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(980, 620);
            MaximizeBox = false;

            // ✅ Conectar Load event correctamente
            this.Load += TiposHabitacionForm_Load;

            lblTitulo = new Label();
            lblTitulo.Text = "Módulo de tipos de habitación";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblNombreTipo = new Label();
            lblNombreTipo.Text = "Nombre tipo";
            lblNombreTipo.Location = new Point(25, 70);

            txtNombreTipo = new TextBox();
            txtNombreTipo.Location = new Point(25, 95);
            txtNombreTipo.Width = 220;

            lblCapacidad = new Label();
            lblCapacidad.Text = "Capacidad";
            lblCapacidad.Location = new Point(270, 70);

            txtCapacidad = new TextBox();
            txtCapacidad.Location = new Point(270, 95);
            txtCapacidad.Width = 120;

            lblPrecioBase = new Label();
            lblPrecioBase.Text = "Precio base";
            lblPrecioBase.Location = new Point(415, 70);

            txtPrecioBase = new TextBox();
            txtPrecioBase.Location = new Point(415, 95);
            txtPrecioBase.Width = 140;

            lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción";
            lblDescripcion.Location = new Point(25, 140);

            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(25, 165);
            txtDescripcion.Width = 530;

            btnNuevo = CrearBotonBlanco("Nuevo", 25, 220);
            btnNuevo.Click += BtnNuevo_Click;

            btnGuardar = CrearBotonBlanco("Guardar", 125, 220);
            btnGuardar.Click += BtnGuardar_Click;

            btnEditar = CrearBotonBlanco("Editar", 225, 220);
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = CrearBotonBlanco("Eliminar", 325, 220);
            btnEliminar.Click += BtnEliminar_Click;

            lblBuscar = new Label();
            lblBuscar.Text = "Buscar";
            lblBuscar.Location = new Point(520, 200);

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(520, 225);
            txtBuscar.Width = 220;

            btnBuscar = CrearBotonBlanco("Buscar", 755, 220);
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBotonBlanco("Limpiar", 855, 220);
            btnLimpiar.Click += BtnLimpiar_Click;

            dgvTipos = new DataGridView();
            dgvTipos.Location = new Point(25, 280);
            dgvTipos.Size = new Size(900, 260);
            dgvTipos.AllowUserToAddRows = false;
            dgvTipos.AllowUserToDeleteRows = false;
            dgvTipos.ReadOnly = true;
            dgvTipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTipos.ScrollBars = ScrollBars.Both;
            dgvTipos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTipos.MultiSelect = false;
            dgvTipos.RowHeadersWidth = 25;
            dgvTipos.CellClick += DgvTipos_CellClick;

            Controls.Add(lblTitulo);
            Controls.Add(lblNombreTipo);
            Controls.Add(txtNombreTipo);
            Controls.Add(lblCapacidad);
            Controls.Add(txtCapacidad);
            Controls.Add(lblPrecioBase);
            Controls.Add(txtPrecioBase);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvTipos);

            ApplyTheme();
        }

        private void TiposHabitacionForm_Load(object sender, EventArgs e)
        {
            CargarTipos();
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
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

        private void CargarTipos()
        {
            try
            {
                dgvTipos.DataSource = Database.ObtenerTiposHabitacionAdmin();
                ConfigurarColumnasGrid();
                dgvTipos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de habitación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasGrid()
        {
            if (dgvTipos.Columns.Count == 0)
                return;

            dgvTipos.Columns["IdTipoHabitacion"].Width = 100;
            dgvTipos.Columns["NombreTipo"].Width = 180;
            dgvTipos.Columns["Capacidad"].Width = 100;
            dgvTipos.Columns["PrecioBase"].Width = 120;
            dgvTipos.Columns["Descripcion"].Width = 280;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreTipo.Text))
            {
                MessageBox.Show("Debe escribir el nombre del tipo.");
                txtNombreTipo.Focus();
                return false;
            }

            if (!int.TryParse(txtCapacidad.Text.Trim(), out int capacidad) || capacidad <= 0)
            {
                MessageBox.Show("Debe escribir una capacidad válida.");
                txtCapacidad.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecioBase.Text.Trim(), out decimal precioBase) || precioBase < 0)
            {
                MessageBox.Show("Debe escribir un precio base válido.");
                txtPrecioBase.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            idTipoSeleccionado = 0;
            txtNombreTipo.Clear();
            txtCapacidad.Clear();
            txtPrecioBase.Clear();
            txtDescripcion.Clear();
            txtBuscar.Clear();
            dgvTipos.ClearSelection();
            txtNombreTipo.Focus();
        }

        private void BtnNuevo_Click(object sender, EventArgs e) => LimpiarCampos();

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                Database.InsertarTipoHabitacion(
                    txtNombreTipo.Text.Trim(),
                    Convert.ToInt32(txtCapacidad.Text.Trim()),
                    Convert.ToDecimal(txtPrecioBase.Text.Trim()),
                    txtDescripcion.Text.Trim()
                );

                MessageBox.Show("Tipo de habitación guardado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarTipos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar tipo de habitación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idTipoSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un tipo para editar.");
                return;
            }

            if (!ValidarCampos()) return;

            try
            {
                Database.ActualizarTipoHabitacion(
                    idTipoSeleccionado,
                    txtNombreTipo.Text.Trim(),
                    Convert.ToInt32(txtCapacidad.Text.Trim()),
                    Convert.ToDecimal(txtPrecioBase.Text.Trim()),
                    txtDescripcion.Text.Trim()
                );

                MessageBox.Show("Tipo de habitación actualizado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarTipos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar tipo de habitación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idTipoSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un tipo para eliminar.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Está seguro de eliminar este tipo de habitación?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;

            try
            {
                Database.EliminarTipoHabitacion(idTipoSeleccionado);

                MessageBox.Show("Tipo de habitación eliminado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarTipos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar tipo de habitación: " + ex.Message,
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
                    CargarTipos();
                    return;
                }

                dgvTipos.DataSource = Database.BuscarTiposHabitacion(texto);
                ConfigurarColumnasGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar tipos de habitación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarTipos();
        }

        private void DgvTipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvTipos.CurrentRow == null)
                return;

            DataGridViewRow fila = dgvTipos.CurrentRow;

            idTipoSeleccionado = Convert.ToInt32(fila.Cells["IdTipoHabitacion"].Value);
            txtNombreTipo.Text = fila.Cells["NombreTipo"].Value?.ToString();
            txtCapacidad.Text = fila.Cells["Capacidad"].Value?.ToString();
            txtPrecioBase.Text = fila.Cells["PrecioBase"].Value?.ToString();
            txtDescripcion.Text = fila.Cells["Descripcion"].Value?.ToString();
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TiposHabitacionForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "TiposHabitacionForm";
            this.Load += new System.EventHandler(this.TiposHabitacionForm_Load_1);
            this.ResumeLayout(false);

        }

        private void TiposHabitacionForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}