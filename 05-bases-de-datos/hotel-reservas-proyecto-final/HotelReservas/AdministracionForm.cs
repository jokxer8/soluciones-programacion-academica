using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class AdministracionForm : Form
    {
        private Label lblTitulo;
        private Label lblSucursal;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblCedula;
        private Label lblTelefono;
        private Label lblCorreo;
        private Label lblDireccion;
        private Label lblFechaNacimiento;
        private Label lblFechaIngreso;
        private Label lblCargo;
        private Label lblEstadoLaboral;
        private Label lblUsuario;
        private Label lblClave;
        private Label lblRol;
        private Label lblBuscar;
        private Label lblFoto;
        private Label lblCv;

        private ComboBox cboSucursal;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtCedula;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtDireccion;
        private DateTimePicker dtFechaNacimiento;
        private DateTimePicker dtFechaIngreso;
        private TextBox txtCargo;
        private ComboBox cboEstadoLaboral;
        private TextBox txtNombreUsuario;
        private TextBox txtClave;
        private ComboBox cboRol;
        private TextBox txtBuscar;
        private CheckBox chkUsuarioActivo;

        private PictureBox picFoto;
        private TextBox txtCvPath;

        private Button btnSeleccionarFoto;
        private Button btnSeleccionarCv;
        private Button btnVerCv;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnCancelarEmpleado;
        private Button btnBuscar;
        private Button btnLimpiar;

        private DataGridView dgvAdministracion;

        private int idEmpleadoSeleccionado = 0;
        private int idUsuarioSeleccionado = 0;
        private bool tieneEmpleadoSeleccionado = false;

        private string rutaFotoGuardada = "";
        private string rutaCvGuardada = "";
        private string rutaFotoTemporal = "";
        private string rutaCvTemporal = "";

        private EventHandler _themeHandler;

        public AdministracionForm()
        {
            SesionUsuario.ExigirAcceso("Administracion");
            InicializarComponentes();
            Load += AdministracionForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
            if (picFoto != null) picFoto.BackColor = Theme.ContentBackground;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Theme.OnThemeChanged -= _themeHandler;
            base.Dispose(disposing);
        }

        private void AdministracionForm_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarRegistros();
            LimpiarCampos();
        }

        private void InicializarComponentes()
        {
            Text = "Módulo administrativo";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            AutoScroll = true;

            lblTitulo = new Label();
            lblTitulo.Text = "Módulo administrativo";
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

            lblNombre = new Label();
            lblNombre.Text = "Nombre";
            lblNombre.Location = new Point(225, 70);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(225, 95);
            txtNombre.Width = 160;

            lblApellido = new Label();
            lblApellido.Text = "Apellido";
            lblApellido.Location = new Point(405, 70);

            txtApellido = new TextBox();
            txtApellido.Location = new Point(405, 95);
            txtApellido.Width = 160;

            lblCedula = new Label();
            lblCedula.Text = "Cédula";
            lblCedula.Location = new Point(585, 70);

            txtCedula = new TextBox();
            txtCedula.Location = new Point(585, 95);
            txtCedula.Width = 150;

            lblTelefono = new Label();
            lblTelefono.Text = "Teléfono";
            lblTelefono.Location = new Point(25, 140);

            txtTelefono = new TextBox();
            txtTelefono.Location = new Point(25, 165);
            txtTelefono.Width = 160;

            lblCorreo = new Label();
            lblCorreo.Text = "Correo";
            lblCorreo.Location = new Point(205, 140);

            txtCorreo = new TextBox();
            txtCorreo.Location = new Point(205, 165);
            txtCorreo.Width = 220;

            lblCargo = new Label();
            lblCargo.Text = "Cargo";
            lblCargo.Location = new Point(445, 140);

            txtCargo = new TextBox();
            txtCargo.Location = new Point(445, 165);
            txtCargo.Width = 170;

            lblEstadoLaboral = new Label();
            lblEstadoLaboral.Text = "Estado laboral";
            lblEstadoLaboral.Location = new Point(635, 140);

            cboEstadoLaboral = new ComboBox();
            cboEstadoLaboral.Location = new Point(635, 165);
            cboEstadoLaboral.Width = 150;
            cboEstadoLaboral.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstadoLaboral.SelectedIndexChanged += CboEstadoLaboral_SelectedIndexChanged;

            lblDireccion = new Label();
            lblDireccion.Text = "Dirección";
            lblDireccion.Location = new Point(25, 210);

            txtDireccion = new TextBox();
            txtDireccion.Location = new Point(25, 235);
            txtDireccion.Width = 760;

            lblFechaNacimiento = new Label();
            lblFechaNacimiento.Text = "Fecha nacimiento";
            lblFechaNacimiento.Location = new Point(25, 280);

            dtFechaNacimiento = new DateTimePicker();
            dtFechaNacimiento.Location = new Point(25, 305);
            dtFechaNacimiento.Width = 140;
            dtFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtFechaNacimiento.ShowCheckBox = true;

            lblFechaIngreso = new Label();
            lblFechaIngreso.Text = "Fecha ingreso";
            lblFechaIngreso.Location = new Point(185, 280);

            dtFechaIngreso = new DateTimePicker();
            dtFechaIngreso.Location = new Point(185, 305);
            dtFechaIngreso.Width = 140;
            dtFechaIngreso.Format = DateTimePickerFormat.Short;

            lblUsuario = new Label();
            lblUsuario.Text = "Usuario";
            lblUsuario.Location = new Point(345, 280);

            txtNombreUsuario = new TextBox();
            txtNombreUsuario.Location = new Point(345, 305);
            txtNombreUsuario.Width = 140;

            lblClave = new Label();
            lblClave.Text = "Clave";
            lblClave.Location = new Point(505, 280);

            txtClave = new TextBox();
            txtClave.Location = new Point(505, 305);
            txtClave.Width = 140;
            txtClave.PasswordChar = '*';

            lblRol = new Label();
            lblRol.Text = "Rol";
            lblRol.Location = new Point(665, 280);

            cboRol = new ComboBox();
            cboRol.Location = new Point(665, 305);
            cboRol.Width = 120;
            cboRol.DropDownStyle = ComboBoxStyle.DropDownList;

            chkUsuarioActivo = new CheckBox();
            chkUsuarioActivo.Text = "Usuario activo";
            chkUsuarioActivo.Location = new Point(25, 345);
            chkUsuarioActivo.AutoSize = true;
            chkUsuarioActivo.Checked = true;

            lblFoto = new Label();
            lblFoto.Text = "Foto 2x2";
            lblFoto.Location = new Point(825, 70);

            picFoto = new PictureBox();
            picFoto.Location = new Point(825, 95);
            picFoto.Size = new Size(140, 140);
            picFoto.BorderStyle = BorderStyle.FixedSingle;
            picFoto.SizeMode = PictureBoxSizeMode.StretchImage;
            picFoto.BackColor = Theme.ContentBackground;

            btnSeleccionarFoto = CrearBotonBlanco("Seleccionar foto", 825, 245, 140, 32);
            btnSeleccionarFoto.Click += BtnSeleccionarFoto_Click;

            lblCv = new Label();
            lblCv.Text = "Curriculum Vitae";
            lblCv.Location = new Point(825, 290);

            txtCvPath = new TextBox();
            txtCvPath.Location = new Point(825, 315);
            txtCvPath.Width = 220;
            txtCvPath.ReadOnly = true;

            btnSeleccionarCv = CrearBotonBlanco("Seleccionar CV", 825, 350, 105, 32);
            btnSeleccionarCv.Click += BtnSeleccionarCv_Click;

            btnVerCv = CrearBotonBlanco("Ver CV", 940, 350, 105, 32);
            btnVerCv.Click += BtnVerCv_Click;

            btnNuevo = CrearBotonBlanco("Nuevo", 25, 400, 90, 32);
            btnNuevo.Click += BtnNuevo_Click;

            btnGuardar = CrearBotonBlanco("Guardar", 125, 400, 90, 32);
            btnGuardar.Click += BtnGuardar_Click;

            btnEditar = CrearBotonBlanco("Editar", 225, 400, 90, 32);
            btnEditar.Click += BtnEditar_Click;

            btnCancelarEmpleado = CrearBotonBlanco("Cancelar", 325, 400, 90, 32);
            btnCancelarEmpleado.Click += BtnCancelarEmpleado_Click;

            lblBuscar = new Label();
            lblBuscar.Text = "Buscar";
            lblBuscar.Location = new Point(610, 380);

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(610, 405);
            txtBuscar.Width = 220;

            btnBuscar = CrearBotonBlanco("Buscar", 845, 400, 90, 32);
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBotonBlanco("Limpiar", 945, 400, 90, 32);
            btnLimpiar.Click += BtnLimpiar_Click;

            dgvAdministracion = new DataGridView();
            dgvAdministracion.Location = new Point(25, 455);
            dgvAdministracion.Size = new Size(1160, 300);
            dgvAdministracion.AllowUserToAddRows = false;
            dgvAdministracion.AllowUserToDeleteRows = false;
            dgvAdministracion.ReadOnly = true;
            dgvAdministracion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvAdministracion.ScrollBars = ScrollBars.Both;
            dgvAdministracion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdministracion.MultiSelect = false;
            dgvAdministracion.RowHeadersWidth = 25;
            dgvAdministracion.CellClick += DgvAdministracion_CellClick;

            Controls.Add(lblTitulo);
            Controls.Add(lblSucursal);
            Controls.Add(cboSucursal);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblCedula);
            Controls.Add(txtCedula);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Controls.Add(lblCargo);
            Controls.Add(txtCargo);
            Controls.Add(lblEstadoLaboral);
            Controls.Add(cboEstadoLaboral);
            Controls.Add(lblDireccion);
            Controls.Add(txtDireccion);
            Controls.Add(lblFechaNacimiento);
            Controls.Add(dtFechaNacimiento);
            Controls.Add(lblFechaIngreso);
            Controls.Add(dtFechaIngreso);
            Controls.Add(lblUsuario);
            Controls.Add(txtNombreUsuario);
            Controls.Add(lblClave);
            Controls.Add(txtClave);
            Controls.Add(lblRol);
            Controls.Add(cboRol);
            Controls.Add(chkUsuarioActivo);
            Controls.Add(lblFoto);
            Controls.Add(picFoto);
            Controls.Add(btnSeleccionarFoto);
            Controls.Add(lblCv);
            Controls.Add(txtCvPath);
            Controls.Add(btnSeleccionarCv);
            Controls.Add(btnVerCv);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEditar);
            Controls.Add(btnCancelarEmpleado);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvAdministracion);
            ApplyTheme();
        }

        private Button CrearBotonBlanco(string texto, int x, int y, int ancho, int alto)
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

        private void CargarCombos()
        {
            try
            {
                DataTable dtSucursales = Database.ObtenerSucursales();
                cboSucursal.DataSource = dtSucursales;
                cboSucursal.DisplayMember = "NombreSucursal";
                cboSucursal.ValueMember = "IdSucursal";
                cboSucursal.SelectedIndex = -1;

                DataTable dtRoles = AdministracionData.ObtenerRoles();
                cboRol.DataSource = dtRoles;
                cboRol.DisplayMember = "Rol";
                cboRol.ValueMember = "Rol";
                cboRol.SelectedIndex = -1;

                DataTable dtEstados = AdministracionData.ObtenerEstadosLaborales();
                cboEstadoLaboral.DataSource = dtEstados;
                cboEstadoLaboral.DisplayMember = "EstadoLaboral";
                cboEstadoLaboral.ValueMember = "EstadoLaboral";
                cboEstadoLaboral.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRegistros()
        {
            try
            {
                dgvAdministracion.DataSource = AdministracionData.ObtenerEmpleadosUsuarios();
                ConfigurarGrid();
                dgvAdministracion.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar registros administrativos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            if (dgvAdministracion.Columns.Count == 0)
                return;

            dgvAdministracion.Columns["IdEmpleado"].Width = 80;
            dgvAdministracion.Columns["IdUsuario"].Visible = false;
            dgvAdministracion.Columns["TieneEmpleado"].Visible = false;
            dgvAdministracion.Columns["TipoRegistro"].Width = 130;
            dgvAdministracion.Columns["IdSucursal"].Visible = false;
            dgvAdministracion.Columns["NombreSucursal"].Width = 130;
            dgvAdministracion.Columns["Nombre"].Width = 120;
            dgvAdministracion.Columns["Apellido"].Width = 120;
            dgvAdministracion.Columns["Cedula"].Width = 110;
            dgvAdministracion.Columns["Telefono"].Width = 110;
            dgvAdministracion.Columns["Correo"].Width = 160;
            dgvAdministracion.Columns["Direccion"].Width = 170;
            dgvAdministracion.Columns["FechaNacimiento"].Width = 95;
            dgvAdministracion.Columns["FechaIngreso"].Width = 95;
            dgvAdministracion.Columns["Cargo"].Width = 110;
            dgvAdministracion.Columns["EstadoLaboral"].Width = 100;
            dgvAdministracion.Columns["NombreUsuario"].Width = 110;
            dgvAdministracion.Columns["Rol"].Width = 90;
            dgvAdministracion.Columns["EstadoUsuarioTexto"].Width = 100;

            dgvAdministracion.Columns["FotoPath"].Visible = false;
            dgvAdministracion.Columns["CvPath"].Visible = false;
            dgvAdministracion.Columns["EmpleadoActivo"].Visible = false;
            dgvAdministracion.Columns["UsuarioActivo"].Visible = false;
            dgvAdministracion.Columns["Clave"].Visible = false;
        }

        private bool ValidarCamposEmpleadoUsuario()
        {
            if (cboSucursal.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar la sucursal.");
                cboSucursal.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe escribir el nombre.");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Debe escribir el apellido.");
                txtApellido.Focus();
                return false;
            }

            return ValidarCamposUsuario();
        }

        private bool ValidarCamposUsuario()
        {
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MessageBox.Show("Debe escribir el nombre de usuario.");
                txtNombreUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Debe escribir la clave.");
                txtClave.Focus();
                return false;
            }

            if (cboRol.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el rol.");
                cboRol.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            idEmpleadoSeleccionado = 0;
            idUsuarioSeleccionado = 0;
            tieneEmpleadoSeleccionado = false;

            cboSucursal.SelectedIndex = -1;
            txtNombre.Clear();
            txtApellido.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            dtFechaNacimiento.Checked = false;
            dtFechaIngreso.Value = DateTime.Today;
            txtCargo.Clear();
            cboEstadoLaboral.SelectedIndex = -1;
            txtNombreUsuario.Clear();
            txtClave.Clear();
            cboRol.SelectedIndex = -1;
            chkUsuarioActivo.Checked = true;

            rutaFotoGuardada = "";
            rutaCvGuardada = "";
            rutaFotoTemporal = "";
            rutaCvTemporal = "";
            txtCvPath.Clear();
            picFoto.Image = null;

            dgvAdministracion.ClearSelection();
        }

        private string GuardarArchivoEmpleado(string rutaOrigen, string subcarpeta)
        {
            if (string.IsNullOrWhiteSpace(rutaOrigen))
                return "";

            if (!File.Exists(rutaOrigen))
                return rutaOrigen;

            string carpetaDestino = Path.Combine(Application.StartupPath, "ArchivosEmpleados", subcarpeta);
            Directory.CreateDirectory(carpetaDestino);

            string extension = Path.GetExtension(rutaOrigen);
            string nombreArchivo = Guid.NewGuid().ToString("N") + extension;
            string rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);

            File.Copy(rutaOrigen, rutaDestino, true);

            return Path.Combine("ArchivosEmpleados", subcarpeta, nombreArchivo);
        }

        private string ResolverRutaArchivo(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                return "";

            if (Path.IsPathRooted(ruta))
                return ruta;

            return Path.Combine(Application.StartupPath, ruta);
        }

        private void MostrarFoto(string ruta)
        {
            try
            {
                picFoto.Image = null;

                string rutaReal = ResolverRutaArchivo(ruta);

                if (string.IsNullOrWhiteSpace(rutaReal) || !File.Exists(rutaReal))
                    return;

                using (Image img = Image.FromFile(rutaReal))
                {
                    picFoto.Image = new Bitmap(img);
                }
            }
            catch
            {
                picFoto.Image = null;
            }
        }

        private int ObtenerIntCelda(DataGridViewRow fila, string nombreColumna)
        {
            object valor = fila.Cells[nombreColumna].Value;
            if (valor == null || valor == DBNull.Value)
                return 0;

            int resultado;
            return int.TryParse(valor.ToString(), out resultado) ? resultado : 0;
        }

        private bool ObtenerBoolCelda(DataGridViewRow fila, string nombreColumna, bool valorPorDefecto = false)
        {
            object valor = fila.Cells[nombreColumna].Value;
            if (valor == null || valor == DBNull.Value)
                return valorPorDefecto;

            bool resultado;
            if (bool.TryParse(valor.ToString(), out resultado))
                return resultado;

            int numero;
            if (int.TryParse(valor.ToString(), out numero))
                return numero != 0;

            return valorPorDefecto;
        }

        private string ObtenerTextoCelda(DataGridViewRow fila, string nombreColumna)
        {
            object valor = fila.Cells[nombreColumna].Value;
            return valor == null || valor == DBNull.Value ? "" : valor.ToString();
        }

        private DateTime? ObtenerFechaCelda(DataGridViewRow fila, string nombreColumna)
        {
            object valor = fila.Cells[nombreColumna].Value;
            if (valor == null || valor == DBNull.Value)
                return null;

            DateTime fecha;
            if (DateTime.TryParse(valor.ToString(), out fecha))
                return fecha;

            return null;
        }

        private string ConstruirNombreCompletoUsuarioSolo()
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();

            string nombreCompleto = (nombre + " " + apellido).Trim();

            if (string.IsNullOrWhiteSpace(nombreCompleto))
                nombreCompleto = txtNombreUsuario.Text.Trim();

            return nombreCompleto;
        }

        private void CboEstadoLaboral_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstadoLaboral.Text == "CANCELADO")
                chkUsuarioActivo.Checked = false;
        }

        private void BtnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar foto 2x2";
                ofd.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaFotoTemporal = ofd.FileName;
                    MostrarFoto(rutaFotoTemporal);
                }
            }
        }

        private void BtnSeleccionarCv_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar curriculum vitae";
                ofd.Filter = "Documentos|*.pdf;*.doc;*.docx";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaCvTemporal = ofd.FileName;
                    txtCvPath.Text = Path.GetFileName(rutaCvTemporal);
                }
            }
        }

        private void BtnVerCv_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = "";

                if (!string.IsNullOrWhiteSpace(rutaCvTemporal) && File.Exists(rutaCvTemporal))
                    ruta = rutaCvTemporal;
                else
                    ruta = ResolverRutaArchivo(rutaCvGuardada);

                if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                {
                    MessageBox.Show("No hay un CV disponible para abrir.");
                    return;
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = ruta,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el CV: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado > 0 || idUsuarioSeleccionado > 0)
            {
                MessageBox.Show("Ya seleccionó un registro. Para modificarlo use Editar.");
                return;
            }

            if (!ValidarCamposEmpleadoUsuario())
                return;

            try
            {
                string fotoFinal = string.IsNullOrWhiteSpace(rutaFotoTemporal)
                    ? rutaFotoGuardada
                    : GuardarArchivoEmpleado(rutaFotoTemporal, "Fotos");

                string cvFinal = string.IsNullOrWhiteSpace(rutaCvTemporal)
                    ? rutaCvGuardada
                    : GuardarArchivoEmpleado(rutaCvTemporal, "CV");

                AdministracionData.InsertarEmpleadoUsuario(
                    Convert.ToInt32(cboSucursal.SelectedValue),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtCedula.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    dtFechaNacimiento.Checked ? (DateTime?)dtFechaNacimiento.Value.Date : null,
                    dtFechaIngreso.Value.Date,
                    txtCargo.Text.Trim(),
                    fotoFinal,
                    cvFinal,
                    cboEstadoLaboral.Text,
                    txtNombreUsuario.Text.Trim(),
                    txtClave.Text.Trim(),
                    cboRol.Text,
                    chkUsuarioActivo.Checked
                );

                MessageBox.Show("Empleado y usuario guardados correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarRegistros();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el registro administrativo: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0 && idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un registro para editar.");
                return;
            }

            try
            {
                if (tieneEmpleadoSeleccionado)
                {
                    if (!ValidarCamposEmpleadoUsuario())
                        return;

                    string fotoFinal = string.IsNullOrWhiteSpace(rutaFotoTemporal)
                        ? rutaFotoGuardada
                        : GuardarArchivoEmpleado(rutaFotoTemporal, "Fotos");

                    string cvFinal = string.IsNullOrWhiteSpace(rutaCvTemporal)
                        ? rutaCvGuardada
                        : GuardarArchivoEmpleado(rutaCvTemporal, "CV");

                    AdministracionData.ActualizarEmpleadoUsuario(
                        idEmpleadoSeleccionado,
                        idUsuarioSeleccionado,
                        Convert.ToInt32(cboSucursal.SelectedValue),
                        txtNombre.Text.Trim(),
                        txtApellido.Text.Trim(),
                        txtCedula.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtCorreo.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        dtFechaNacimiento.Checked ? (DateTime?)dtFechaNacimiento.Value.Date : null,
                        dtFechaIngreso.Value.Date,
                        txtCargo.Text.Trim(),
                        fotoFinal,
                        cvFinal,
                        cboEstadoLaboral.Text,
                        txtNombreUsuario.Text.Trim(),
                        txtClave.Text.Trim(),
                        cboRol.Text,
                        chkUsuarioActivo.Checked
                    );

                    MessageBox.Show(
                        idUsuarioSeleccionado > 0
                            ? "Registro administrativo actualizado correctamente."
                            : "Empleado actualizado y usuario creado correctamente.",
                        "Correcto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    if (!ValidarCamposUsuario())
                        return;

                    AdministracionData.ActualizarUsuarioSolo(
                        idUsuarioSeleccionado,
                        txtNombreUsuario.Text.Trim(),
                        txtClave.Text.Trim(),
                        ConstruirNombreCompletoUsuarioSolo(),
                        cboRol.Text,
                        chkUsuarioActivo.Checked
                    );

                    MessageBox.Show("Usuario actualizado correctamente.",
                        "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarRegistros();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el registro administrativo: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelarEmpleado_Click(object sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0 && idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un registro para cancelar.");
                return;
            }

            DialogResult r = MessageBox.Show(
                tieneEmpleadoSeleccionado
                    ? "¿Está seguro de marcar este empleado como CANCELADO e inactivar su usuario?"
                    : "¿Está seguro de inactivar este usuario?",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes)
                return;

            try
            {
                if (tieneEmpleadoSeleccionado)
                    AdministracionData.CancelarEmpleadoUsuario(idEmpleadoSeleccionado, idUsuarioSeleccionado);
                else
                    AdministracionData.CancelarUsuarioSolo(idUsuarioSeleccionado);

                MessageBox.Show("Registro inactivado correctamente.",
                    "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarRegistros();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar el registro: " + ex.Message,
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
                    CargarRegistros();
                    return;
                }

                dgvAdministracion.DataSource = AdministracionData.BuscarEmpleadosUsuarios(texto);
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar registros administrativos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            LimpiarCampos();
            CargarRegistros();
        }

        private void DgvAdministracion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvAdministracion.CurrentRow == null)
                return;

            try
            {
                DataGridViewRow fila = dgvAdministracion.CurrentRow;

                idEmpleadoSeleccionado = ObtenerIntCelda(fila, "IdEmpleado");
                idUsuarioSeleccionado = ObtenerIntCelda(fila, "IdUsuario");
                tieneEmpleadoSeleccionado = ObtenerBoolCelda(fila, "TieneEmpleado", false);

                int idSucursal = ObtenerIntCelda(fila, "IdSucursal");
                if (idSucursal > 0)
                    cboSucursal.SelectedValue = idSucursal;
                else
                    cboSucursal.SelectedIndex = -1;

                txtNombre.Text = ObtenerTextoCelda(fila, "Nombre");
                txtApellido.Text = ObtenerTextoCelda(fila, "Apellido");
                txtCedula.Text = ObtenerTextoCelda(fila, "Cedula");
                txtTelefono.Text = ObtenerTextoCelda(fila, "Telefono");
                txtCorreo.Text = ObtenerTextoCelda(fila, "Correo");
                txtDireccion.Text = ObtenerTextoCelda(fila, "Direccion");
                txtCargo.Text = ObtenerTextoCelda(fila, "Cargo");

                DateTime? fechaNacimiento = ObtenerFechaCelda(fila, "FechaNacimiento");
                if (fechaNacimiento.HasValue)
                {
                    dtFechaNacimiento.Checked = true;
                    dtFechaNacimiento.Value = fechaNacimiento.Value;
                }
                else
                {
                    dtFechaNacimiento.Checked = false;
                }

                DateTime? fechaIngreso = ObtenerFechaCelda(fila, "FechaIngreso");
                if (fechaIngreso.HasValue)
                    dtFechaIngreso.Value = fechaIngreso.Value;
                else
                    dtFechaIngreso.Value = DateTime.Today;

                string estadoLaboral = ObtenerTextoCelda(fila, "EstadoLaboral");
                if (string.IsNullOrWhiteSpace(estadoLaboral))
                    cboEstadoLaboral.SelectedIndex = -1;
                else
                    cboEstadoLaboral.Text = estadoLaboral;

                txtNombreUsuario.Text = ObtenerTextoCelda(fila, "NombreUsuario");
                txtClave.Text = ObtenerTextoCelda(fila, "Clave");

                string rol = ObtenerTextoCelda(fila, "Rol");
                if (string.IsNullOrWhiteSpace(rol))
                    cboRol.SelectedIndex = -1;
                else
                    cboRol.Text = rol;

                chkUsuarioActivo.Checked = ObtenerBoolCelda(fila, "UsuarioActivo", false);

                rutaFotoGuardada = ObtenerTextoCelda(fila, "FotoPath");
                rutaCvGuardada = ObtenerTextoCelda(fila, "CvPath");
                rutaFotoTemporal = "";
                rutaCvTemporal = "";

                txtCvPath.Text = string.IsNullOrWhiteSpace(rutaCvGuardada)
                    ? ""
                    : Path.GetFileName(rutaCvGuardada);

                MostrarFoto(rutaFotoGuardada);

                if (!tieneEmpleadoSeleccionado)
                {
                    MessageBox.Show(
                        "Este registro pertenece a un usuario sin empleado vinculado. Puede editar Usuario, Clave, Rol y Estado.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else if (idUsuarioSeleccionado == 0)
                {
                    MessageBox.Show(
                        "Este empleado no tiene usuario asignado todavía. Complete Usuario, Clave y Rol, marque Usuario activo si desea acceso, y luego pulse Editar.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el registro: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AdministracionForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "AdministracionForm";
            this.Load += new System.EventHandler(this.AdministracionForm_Load_1);
            this.ResumeLayout(false);

        }

        private void AdministracionForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}