using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class MantenimientoForm : Form
    {
        private Label lblTitulo;
        private Label lblHabitacion;
        private Label lblEmpleado;
        private Label lblMotivo;
        private Label lblCosto;
        private Label lblFechaInicio;
        private Label lblFechaFin;
        private Label lblEstado;
        private Label lblBuscar;

        private ComboBox cboHabitacion;
        private ComboBox cboEmpleado;
        private ComboBox cboEstado;
        private TextBox txtMotivo;
        private TextBox txtCosto;
        private DateTimePicker dtFechaInicio;
        private DateTimePicker dtFechaFin;
        private CheckBox chkTieneFechaFin;
        private TextBox txtBuscar;

        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvMantenimiento;

        private int idMantenimientoSeleccionado = 0;

        private EventHandler _themeHandler;

        public MantenimientoForm()
        {
            InicializarComponentes();
            Load += MantenimientoForm_Load;
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

        private void MantenimientoForm_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarMantenimientos();
            ConfigurarPermisosPorRol();
        }

        private void InicializarComponentes()
        {
            AutoScroll = true;

            lblTitulo = new Label();
            lblTitulo.Text = "Módulo de mantenimiento";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            // ── Campos formulario ─────────────────────────────────────────────

            lblHabitacion = new Label();
            lblHabitacion.Text = "Habitación";
            lblHabitacion.Location = new Point(25, 70);

            cboHabitacion = new ComboBox();
            cboHabitacion.Location = new Point(25, 95);
            cboHabitacion.Width = 250;
            cboHabitacion.DropDownStyle = ComboBoxStyle.DropDownList;

            lblEmpleado = new Label();
            lblEmpleado.Text = "Empleado responsable";
            lblEmpleado.Location = new Point(295, 70);

            cboEmpleado = new ComboBox();
            cboEmpleado.Location = new Point(295, 95);
            cboEmpleado.Width = 250;
            cboEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;

            lblEstado = new Label();
            lblEstado.Text = "Estado";
            lblEstado.Location = new Point(565, 70);

            cboEstado = new ComboBox();
            cboEstado.Location = new Point(565, 95);
            cboEstado.Width = 150;
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.Items.AddRange(new object[] { "PENDIENTE", "EN PROCESO", "FINALIZADO" });
            cboEstado.SelectedIndex = 0;

            lblMotivo = new Label();
            lblMotivo.Text = "Motivo";
            lblMotivo.Location = new Point(25, 135);

            txtMotivo = new TextBox();
            txtMotivo.Location = new Point(25, 160);
            txtMotivo.Width = 690;

            lblCosto = new Label();
            lblCosto.Text = "Costo";
            lblCosto.Location = new Point(730, 135);

            txtCosto = new TextBox();
            txtCosto.Location = new Point(730, 160);
            txtCosto.Width = 120;
            txtCosto.Text = "0.00";

            lblFechaInicio = new Label();
            lblFechaInicio.Text = "Fecha inicio";
            lblFechaInicio.Location = new Point(25, 200);

            dtFechaInicio = new DateTimePicker();
            dtFechaInicio.Location = new Point(25, 225);
            dtFechaInicio.Width = 170;
            dtFechaInicio.Format = DateTimePickerFormat.Short;
            dtFechaInicio.Value = DateTime.Today;

            chkTieneFechaFin = new CheckBox();
            chkTieneFechaFin.Text = "Fecha fin";
            chkTieneFechaFin.Location = new Point(220, 200);
            chkTieneFechaFin.AutoSize = true;
            chkTieneFechaFin.CheckedChanged += (s, e) =>
            {
                dtFechaFin.Enabled = chkTieneFechaFin.Checked;
                lblFechaFin.Enabled = chkTieneFechaFin.Checked;
            };

            lblFechaFin = new Label();
            lblFechaFin.Text = "Fecha fin";
            lblFechaFin.Location = new Point(220, 200);
            lblFechaFin.Visible = false;

            dtFechaFin = new DateTimePicker();
            dtFechaFin.Location = new Point(220, 225);
            dtFechaFin.Width = 170;
            dtFechaFin.Format = DateTimePickerFormat.Short;
            dtFechaFin.Value = DateTime.Today;
            dtFechaFin.Enabled = false;

            // ── Botones CRUD ─────────────────────────────────────────────────

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

            // ── Grid ─────────────────────────────────────────────────────────

            dgvMantenimiento = new DataGridView();
            dgvMantenimiento.Location = new Point(25, 330);
            dgvMantenimiento.Size = new Size(1020, 350);
            dgvMantenimiento.AllowUserToAddRows = false;
            dgvMantenimiento.AllowUserToDeleteRows = false;
            dgvMantenimiento.ReadOnly = true;
            dgvMantenimiento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvMantenimiento.ScrollBars = ScrollBars.Both;
            dgvMantenimiento.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMantenimiento.MultiSelect = false;
            dgvMantenimiento.RowHeadersWidth = 25;
            dgvMantenimiento.CellClick += DgvMantenimiento_CellClick;

            // ── Agregar controles ────────────────────────────────────────────

            Controls.Add(lblTitulo);
            Controls.Add(lblHabitacion);
            Controls.Add(cboHabitacion);
            Controls.Add(lblEmpleado);
            Controls.Add(cboEmpleado);
            Controls.Add(lblEstado);
            Controls.Add(cboEstado);
            Controls.Add(lblMotivo);
            Controls.Add(txtMotivo);
            Controls.Add(lblCosto);
            Controls.Add(txtCosto);
            Controls.Add(lblFechaInicio);
            Controls.Add(dtFechaInicio);
            Controls.Add(chkTieneFechaFin);
            Controls.Add(dtFechaFin);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvMantenimiento);

            ApplyTheme();
        }

        private Button CrearBotonBlanco(string texto, int x, int y, int w = 90, int h = 30)
        {
            Button btn = new Button();
            btn.Text = texto;
            btn.Location = new Point(x, y);
            btn.Size = new Size(w, h);
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;
            btn.Font = Theme.AppFont;
            btn.BackColor = Theme.ButtonBackground;
            btn.ForeColor = Theme.ButtonText;
            btn.FlatAppearance.BorderColor = Theme.ButtonBorder;
            btn.FlatAppearance.MouseOverBackColor = Theme.ButtonHover;
            btn.FlatAppearance.MouseDownBackColor = Theme.ButtonDown;
            return btn;
        }

        private void CargarCombos()
        {
            try
            {
                DataTable dtHab = MantenimientoData.ObtenerHabitacionesCombo();
                cboHabitacion.DataSource = dtHab;
                cboHabitacion.DisplayMember = "Habitacion";
                cboHabitacion.ValueMember = "IdHabitacion";
                if (cboHabitacion.Items.Count > 0) cboHabitacion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando habitaciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                DataTable dtEmp = MantenimientoData.ObtenerEmpleadosCombo();
                DataRow drVacio = dtEmp.NewRow();
                drVacio["IdEmpleado"] = DBNull.Value;
                drVacio["NombreCompleto"] = "Sin asignar";
                dtEmp.Rows.InsertAt(drVacio, 0);

                cboEmpleado.DataSource = dtEmp;
                cboEmpleado.DisplayMember = "NombreCompleto";
                cboEmpleado.ValueMember = "IdEmpleado";
                cboEmpleado.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando empleados: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMantenimientos()
        {
            try
            {
                DataTable dt = MantenimientoData.ObtenerTodos();
                dgvMantenimiento.DataSource = dt;
                dgvMantenimiento.ApplyStandard();
                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando mantenimientos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarColumnas()
        {
            if (dgvMantenimiento.Columns.Count == 0) return;

            if (dgvMantenimiento.Columns.Contains("IdMantenimiento"))
                dgvMantenimiento.Columns["IdMantenimiento"].Width = 60;
            if (dgvMantenimiento.Columns.Contains("IdHabitacion"))
                dgvMantenimiento.Columns["IdHabitacion"].Visible = false;
            if (dgvMantenimiento.Columns.Contains("IdEmpleado"))
                dgvMantenimiento.Columns["IdEmpleado"].Visible = false;
            if (dgvMantenimiento.Columns.Contains("Habitacion"))
                dgvMantenimiento.Columns["Habitacion"].Width = 200;
            if (dgvMantenimiento.Columns.Contains("Empleado"))
                dgvMantenimiento.Columns["Empleado"].Width = 180;
            if (dgvMantenimiento.Columns.Contains("Motivo"))
                dgvMantenimiento.Columns["Motivo"].Width = 250;
            if (dgvMantenimiento.Columns.Contains("Costo"))
                dgvMantenimiento.Columns["Costo"].Width = 90;
            if (dgvMantenimiento.Columns.Contains("Estado"))
                dgvMantenimiento.Columns["Estado"].Width = 100;
            if (dgvMantenimiento.Columns.Contains("FechaInicio"))
                dgvMantenimiento.Columns["FechaInicio"].Width = 120;
            if (dgvMantenimiento.Columns.Contains("FechaFin"))
                dgvMantenimiento.Columns["FechaFin"].Width = 120;
        }

        private void ConfigurarPermisosPorRol()
        {
            bool esAdmin = SesionUsuario.EsAdmin();
            bool esSoporte = string.Equals(SesionUsuario.Rol, "SOPORTE", StringComparison.OrdinalIgnoreCase);

            bool puedeEditar = esAdmin || esSoporte;
            btnNuevo.Enabled = puedeEditar;
            btnGuardar.Enabled = puedeEditar;
            btnEditar.Enabled = puedeEditar;
            btnEliminar.Enabled = esAdmin;
        }

        private void LimpiarFormulario()
        {
            idMantenimientoSeleccionado = 0;
            if (cboHabitacion.Items.Count > 0) cboHabitacion.SelectedIndex = 0;
            if (cboEmpleado.Items.Count > 0) cboEmpleado.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            txtMotivo.Clear();
            txtCosto.Text = "0.00";
            dtFechaInicio.Value = DateTime.Today;
            chkTieneFechaFin.Checked = false;
            dtFechaFin.Value = DateTime.Today;
            dtFechaFin.Enabled = false;
        }

        private bool ValidarFormulario()
        {
            if (cboHabitacion.SelectedValue == null || cboHabitacion.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Seleccione una habitación.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Ingrese el motivo del mantenimiento.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtCosto.Text, out decimal costoVal) || costoVal < 0)
            {
                MessageBox.Show("El costo debe ser un número mayor o igual a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (chkTieneFechaFin.Checked && dtFechaFin.Value < dtFechaInicio.Value)
            {
                MessageBox.Show("La fecha de fin no puede ser anterior a la fecha de inicio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ── Eventos de botones ────────────────────────────────────────────────

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            try
            {
                int idHabitacion = Convert.ToInt32(cboHabitacion.SelectedValue);
                int? idEmpleado = null;
                if (cboEmpleado.SelectedValue != null && cboEmpleado.SelectedValue != DBNull.Value
                    && !string.IsNullOrEmpty(cboEmpleado.SelectedValue.ToString()))
                {
                    int empVal;
                    if (int.TryParse(cboEmpleado.SelectedValue.ToString(), out empVal))
                        idEmpleado = empVal;
                }
                decimal costo = decimal.Parse(txtCosto.Text);
                DateTime? fechaFin = chkTieneFechaFin.Checked ? (DateTime?)dtFechaFin.Value : null;

                if (idMantenimientoSeleccionado == 0)
                {
                    MantenimientoData.Insertar(
                        idHabitacion, idEmpleado, txtMotivo.Text.Trim(),
                        costo, dtFechaInicio.Value, fechaFin);
                    Database.RegistrarAccion("INSERT", "Mantenimiento", null, txtMotivo.Text.Trim());
                    MessageBox.Show("Mantenimiento registrado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MantenimientoData.Actualizar(
                        idMantenimientoSeleccionado,
                        cboEstado.SelectedItem?.ToString() ?? "PENDIENTE",
                        costo, fechaFin);
                    Database.RegistrarAccion("UPDATE", "Mantenimiento", idMantenimientoSeleccionado, txtMotivo.Text.Trim());
                    MessageBox.Show("Mantenimiento actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpiarFormulario();
                CargarMantenimientos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idMantenimientoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un mantenimiento de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idMantenimientoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un mantenimiento de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Eliminar el mantenimiento seleccionado?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;

            try
            {
                MantenimientoData.Eliminar(idMantenimientoSeleccionado);
                Database.RegistrarAccion("DELETE", "Mantenimiento", idMantenimientoSeleccionado);
                LimpiarFormulario();
                CargarMantenimientos();
                MessageBox.Show("Mantenimiento eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                DataTable dt = string.IsNullOrWhiteSpace(texto)
                    ? MantenimientoData.ObtenerTodos()
                    : MantenimientoData.Buscar(texto);

                dgvMantenimiento.DataSource = dt;
                dgvMantenimiento.ApplyStandard();
                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            LimpiarFormulario();
            CargarMantenimientos();
        }

        private void DgvMantenimiento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvMantenimiento.Rows[e.RowIndex];

            idMantenimientoSeleccionado = row.Cells["IdMantenimiento"].Value == DBNull.Value
                ? 0
                : Convert.ToInt32(row.Cells["IdMantenimiento"].Value);

            // Cargar habitación
            if (row.Cells["IdHabitacion"].Value != DBNull.Value)
            {
                int idHab = Convert.ToInt32(row.Cells["IdHabitacion"].Value);
                foreach (DataRowView item in cboHabitacion.Items)
                {
                    if (item["IdHabitacion"] != DBNull.Value && Convert.ToInt32(item["IdHabitacion"]) == idHab)
                    {
                        cboHabitacion.SelectedItem = item;
                        break;
                    }
                }
            }

            // Cargar empleado
            if (row.Cells["IdEmpleado"].Value != DBNull.Value)
            {
                object idEmpVal = row.Cells["IdEmpleado"].Value;
                foreach (DataRowView item in cboEmpleado.Items)
                {
                    if (item["IdEmpleado"] != DBNull.Value &&
                        item["IdEmpleado"].ToString() == idEmpVal.ToString())
                    {
                        cboEmpleado.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                cboEmpleado.SelectedIndex = 0;
            }

            // Cargar estado
            string estado = row.Cells["Estado"].Value == DBNull.Value
                ? "PENDIENTE"
                : row.Cells["Estado"].Value.ToString();
            int estadoIdx = cboEstado.Items.IndexOf(estado);
            if (estadoIdx >= 0) cboEstado.SelectedIndex = estadoIdx;

            // Cargar motivo
            txtMotivo.Text = row.Cells["Motivo"].Value == DBNull.Value
                ? ""
                : row.Cells["Motivo"].Value.ToString();

            // Cargar costo
            txtCosto.Text = row.Cells["Costo"].Value == DBNull.Value
                ? "0.00"
                : Convert.ToDecimal(row.Cells["Costo"].Value).ToString("F2");

            // Cargar fechas
            if (row.Cells["FechaInicio"].Value != DBNull.Value)
                dtFechaInicio.Value = Convert.ToDateTime(row.Cells["FechaInicio"].Value);

            if (row.Cells["FechaFin"].Value != DBNull.Value && row.Cells["FechaFin"].Value.ToString() != "")
            {
                chkTieneFechaFin.Checked = true;
                dtFechaFin.Value = Convert.ToDateTime(row.Cells["FechaFin"].Value);
            }
            else
            {
                chkTieneFechaFin.Checked = false;
            }
        }
    }
}
