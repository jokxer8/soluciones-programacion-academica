using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;  // ✅ REQUERIDO para Chart
using HotelReservas.Assets;  // ✅ REQUERIDO para Theme

namespace HotelReservas
{
    public class ReportesForm : Form
    {
        private Panel panelFiltros;
        private Label lblDesde;
        private Label lblHasta;
        private Label lblAnio;
        private DateTimePicker dtDesde;
        private DateTimePicker dtHasta;
        private ComboBox cboAnio;
        private Button btnCargar;
        private Button btnExportarExcel;

        private TabControl tabControl;
        private TabPage tabOperativos;
        private TabPage tabHabitaciones;
        private TabPage tabGerenciales;
        private TabPage tabGlobalAnual;
        private TabPage tabMetricas;

        // Operativos
        private Label lblOpTotal;
        private Label lblOpPendientes;
        private Label lblOpConfirmadas;
        private Label lblOpCanceladas;
        private Label lblOpFinalizadas;
        private Chart chartOperMensual;
        private Chart chartOperEstados;
        private DataGridView dgvOperativo;

        // Habitaciones
        private Label lblHabTotal;
        private Label lblHabDisponibles;
        private Label lblHabOcupadas;
        private Label lblHabMantenimiento;
        private Label lblHabLimpieza;
        private Chart chartHabEstados;
        private Chart chartHabTop;
        private Chart chartHabTipo;
        private DataGridView dgvHabitaciones;

        // Gerenciales
        private Label lblGerFacturado;
        private Label lblGerCobrado;
        private Label lblGerFacturas;
        private Label lblGerTicket;
        private Label lblGerClientes;
        private Label lblGerReservas;
        private Chart chartGerFactMensual;
        private Chart chartGerEmpleados;
        private DataGridView dgvGerEmpleados;
        private DataGridView dgvGerClientes;

        // Métricas del Sector
        private Label lblMetricasAnio;
        private ComboBox cboMetricasAnio;
        private Button btnCargarMetricas;
        private Label lblMetricaSelector;
        private ComboBox cboMetricaSelector;
        private DataGridView dgvMetricas;
        private Label lblRevPAR;
        private Label lblADR;
        private Label lblOccupancy;
        private Label lblGOPPAR;

        // Global anual
        private Label lblAnTotalReservas;
        private Label lblAnFacturado;
        private Label lblAnCobrado;
        private Label lblAnServicios;
        private Label lblAnPromociones;
        private Label lblAnPctCobro;
        private Chart chartAnFactCobro;
        private Chart chartAnServiciosPromo;
        private DataGridView dgvAnualGlobal;

        private EventHandler _themeHandler;

        public ReportesForm()
        {
            InicializarComponentes();
            AnimationEngine.Register(this);

            Load += ReportesForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);

            panelFiltros.BackColor = Theme.PanelBackground;
            foreach (Control c in panelFiltros.Controls)
            {
                switch (c)
                {
                    case Label lbl:
                        lbl.ForeColor = Color.White;
                        lbl.BackColor = Color.Transparent;
                        break;
                    case DateTimePicker dtp:
                        dtp.CalendarForeColor = Theme.InputForeground;
                        dtp.CalendarMonthBackground = Theme.InputBackground;
                        dtp.CalendarTitleBackColor = Theme.GridHeaderBackground;
                        dtp.ForeColor = Theme.InputForeground;
                        break;
                    case ComboBox cbo:
                        cbo.BackColor = Theme.ComboBackground;
                        cbo.ForeColor = Theme.ComboForeground;
                        break;
                    case Button btn:
                        btn.BackColor = Theme.AccentBackground;
                        btn.ForeColor = Theme.AccentText;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.FlatAppearance.MouseOverBackColor = Theme.AccentHover;
                        break;
                }
            }

            tabControl.BackColor = Theme.ContentBackground;

            // ✅ Aplicar tema a charts directamente (sin usar Theme.ApplyToChart)
            AplicarTemaCharts();

            // ✅ Aplicar tema a grids directamente (sin usar Theme.ApplyToGrid que no existe)
            AplicarTemaGrids();
        }

        // ✅ NUEVO: Aplicar tema a charts de forma directa
        private void AplicarTemaCharts()
        {
            Color chartBg = Theme.FormBackground;
            Color chartBorder = Theme.GridLine;
            Color chartText = Theme.TextSecondary;
            Color serieColor = Theme.Current == AppTheme.Dark
                ? Color.FromArgb(100, 180, 255)
                : Color.FromArgb(50, 100, 200);

            Color serieColor2 = Theme.Current == AppTheme.Dark
                ? Color.FromArgb(70, 130, 200)
                : Color.FromArgb(40, 80, 160);

            // Chart Operativos
            if (chartOperMensual != null)
            {
                try
                {
                    chartOperMensual.BackColor = chartBg;
                    chartOperMensual.BorderColor = chartBorder;
                    if (chartOperMensual.ChartAreas.Count > 0)
                    {
                        chartOperMensual.ChartAreas[0].BackColor = chartBg;
                        chartOperMensual.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartOperMensual.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartOperMensual.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartOperMensual.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }

            if (chartOperEstados != null)
            {
                try
                {
                    chartOperEstados.BackColor = chartBg;
                    chartOperEstados.BorderColor = chartBorder;
                    if (chartOperEstados.ChartAreas.Count > 0)
                    {
                        chartOperEstados.ChartAreas[0].BackColor = chartBg;
                        chartOperEstados.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartOperEstados.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartOperEstados.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartOperEstados.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }

            // Chart Habitaciones
            if (chartHabEstados != null)
            {
                try
                {
                    chartHabEstados.BackColor = chartBg;
                    chartHabEstados.BorderColor = chartBorder;
                    if (chartHabEstados.ChartAreas.Count > 0)
                    {
                        chartHabEstados.ChartAreas[0].BackColor = chartBg;
                        chartHabEstados.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartHabEstados.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartHabEstados.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartHabEstados.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }

            if (chartHabTop != null)
            {
                try
                {
                    chartHabTop.BackColor = chartBg;
                    chartHabTop.BorderColor = chartBorder;
                    if (chartHabTop.ChartAreas.Count > 0)
                    {
                        chartHabTop.ChartAreas[0].BackColor = chartBg;
                        chartHabTop.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartHabTop.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartHabTop.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartHabTop.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }

            if (chartHabTipo != null)
            {
                try
                {
                    chartHabTipo.BackColor = chartBg;
                    chartHabTipo.BorderColor = chartBorder;
                    if (chartHabTipo.ChartAreas.Count > 0)
                    {
                        chartHabTipo.ChartAreas[0].BackColor = chartBg;
                        chartHabTipo.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartHabTipo.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartHabTipo.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartHabTipo.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }

            // Chart Gerenciales
            if (chartGerFactMensual != null)
            {
                try
                {
                    chartGerFactMensual.BackColor = chartBg;
                    chartGerFactMensual.BorderColor = chartBorder;
                    if (chartGerFactMensual.ChartAreas.Count > 0)
                    {
                        chartGerFactMensual.ChartAreas[0].BackColor = chartBg;
                        chartGerFactMensual.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartGerFactMensual.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartGerFactMensual.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartGerFactMensual.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }

            if (chartGerEmpleados != null)
            {
                try
                {
                    chartGerEmpleados.BackColor = chartBg;
                    chartGerEmpleados.BorderColor = chartBorder;
                    if (chartGerEmpleados.ChartAreas.Count > 0)
                    {
                        chartGerEmpleados.ChartAreas[0].BackColor = chartBg;
                        chartGerEmpleados.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartGerEmpleados.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartGerEmpleados.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartGerEmpleados.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }

            // Chart Global Anual
            if (chartAnFactCobro != null)
            {
                try
                {
                    chartAnFactCobro.BackColor = chartBg;
                    chartAnFactCobro.BorderColor = chartBorder;
                    if (chartAnFactCobro.ChartAreas.Count > 0)
                    {
                        chartAnFactCobro.ChartAreas[0].BackColor = chartBg;
                        chartAnFactCobro.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartAnFactCobro.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartAnFactCobro.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartAnFactCobro.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }

            if (chartAnServiciosPromo != null)
            {
                try
                {
                    chartAnServiciosPromo.BackColor = chartBg;
                    chartAnServiciosPromo.BorderColor = chartBorder;
                    if (chartAnServiciosPromo.ChartAreas.Count > 0)
                    {
                        chartAnServiciosPromo.ChartAreas[0].BackColor = chartBg;
                        chartAnServiciosPromo.ChartAreas[0].AxisX.LabelStyle.ForeColor = chartText;
                        chartAnServiciosPromo.ChartAreas[0].AxisY.LabelStyle.ForeColor = chartText;
                        chartAnServiciosPromo.ChartAreas[0].AxisX.MajorGrid.LineColor = chartBorder;
                        chartAnServiciosPromo.ChartAreas[0].AxisY.MajorGrid.LineColor = chartBorder;
                    }
                }
                catch { }
            }
        }

        // ✅ NUEVO: Aplicar tema a grids de forma directa
        private void AplicarTemaGrids()
        {
            DataGridView[] grids = {
                dgvOperativo, dgvHabitaciones,
                dgvGerEmpleados, dgvGerClientes,
                dgvAnualGlobal
            };

            Color gridBg = Theme.FormBackground;
            Color gridLine = Theme.GridLine;
            Color textPrimary = Theme.TextSecondary;
            Color headerBg = Theme.GridHeaderBackground;
            Color selectionBg = Theme.GridSelectionBg;
            Color selectionText = Theme.GridSelectionText;

            foreach (DataGridView dgv in grids)
            {
                if (dgv == null || dgv.IsDisposed) continue;

                try
                {
                    dgv.BackgroundColor = gridBg;
                    dgv.GridColor = gridLine;
                    dgv.ForeColor = textPrimary;

                    if (dgv.DefaultCellStyle != null)
                    {
                        dgv.DefaultCellStyle.BackColor = Theme.ContentBackground;
                        dgv.DefaultCellStyle.ForeColor = textPrimary;
                        dgv.DefaultCellStyle.SelectionBackColor = selectionBg;
                        dgv.DefaultCellStyle.SelectionForeColor = selectionText;
                    }

                    if (dgv.RowHeadersDefaultCellStyle != null)
                    {
                        dgv.RowHeadersDefaultCellStyle.BackColor = headerBg;
                        dgv.RowHeadersDefaultCellStyle.ForeColor = textPrimary;
                    }

                    dgv.EnableHeadersVisualStyles = false;

                    if (dgv.Columns != null)
                    {
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (col == null) continue;

                            col.SortMode = DataGridViewColumnSortMode.NotSortable;

                            if (col.HeaderCell != null && col.HeaderCell.Style != null)
                            {
                                col.HeaderCell.Style.BackColor = headerBg;
                                col.HeaderCell.Style.ForeColor = textPrimary;
                            }

                            if (col.DefaultCellStyle != null)
                            {
                                col.DefaultCellStyle.BackColor = Theme.ContentBackground;
                                col.DefaultCellStyle.ForeColor = textPrimary;
                                col.DefaultCellStyle.SelectionBackColor = selectionBg;
                                col.DefaultCellStyle.SelectionForeColor = selectionText;
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void ReportesForm_Load(object sender, EventArgs e)
        {
            CargarAnios();
            dtDesde.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtHasta.Value = DateTime.Today;
            CargarTodo();
        }

        private void InicializarComponentes()
        {
            Text = "Reportes del sistema";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;

            panelFiltros = new Panel();
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Height = 70;
            panelFiltros.Tag = "skip";

            lblDesde = new Label();
            lblDesde.Text = "Desde";
            lblDesde.Location = new Point(20, 12);

            dtDesde = new DateTimePicker();
            dtDesde.Format = DateTimePickerFormat.Short;
            dtDesde.Location = new Point(20, 32);
            dtDesde.Width = 120;

            lblHasta = new Label();
            lblHasta.Text = "Hasta";
            lblHasta.Location = new Point(160, 12);

            dtHasta = new DateTimePicker();
            dtHasta.Format = DateTimePickerFormat.Short;
            dtHasta.Location = new Point(160, 32);
            dtHasta.Width = 120;

            lblAnio = new Label();
            lblAnio.Text = "Año gráfico";
            lblAnio.Location = new Point(300, 12);

            cboAnio = new ComboBox();
            cboAnio.Location = new Point(300, 32);
            cboAnio.Width = 100;
            cboAnio.DropDownStyle = ComboBoxStyle.DropDownList;

            btnCargar = new Button();
            btnCargar.Text = "Cargar reportes";
            btnCargar.Location = new Point(420, 28);
            btnCargar.Size = new Size(130, 30);
            btnCargar.Click += BtnCargar_Click;

            btnExportarExcel = new Button();
            btnExportarExcel.Text = "Exportar a Excel";
            btnExportarExcel.Location = new Point(570, 28);
            btnExportarExcel.Size = new Size(130, 30);
            btnExportarExcel.Click += BtnExportarExcel_Click;

            panelFiltros.Controls.Add(lblDesde);
            panelFiltros.Controls.Add(dtDesde);
            panelFiltros.Controls.Add(lblHasta);
            panelFiltros.Controls.Add(dtHasta);
            panelFiltros.Controls.Add(lblAnio);
            panelFiltros.Controls.Add(cboAnio);
            panelFiltros.Controls.Add(btnCargar);
            panelFiltros.Controls.Add(btnExportarExcel);

            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            tabOperativos = new TabPage("Operativos");
            tabHabitaciones = new TabPage("Habitaciones");
            tabGerenciales = new TabPage("Gerenciales");
            tabGlobalAnual = new TabPage("Global anual");

            tabOperativos.AutoScroll = true;
            tabHabitaciones.AutoScroll = true;
            tabGerenciales.AutoScroll = true;
            tabGlobalAnual.AutoScroll = true;

            tabMetricas = new TabPage("Metricas del Sector");
            tabMetricas.AutoScroll = true;

            InicializarTabOperativos();
            InicializarTabHabitaciones();
            InicializarTabGerenciales();
            InicializarTabGlobalAnual();
            InicializarTabMetricas();

            tabControl.TabPages.Add(tabOperativos);
            tabControl.TabPages.Add(tabHabitaciones);
            tabControl.TabPages.Add(tabGerenciales);
            tabControl.TabPages.Add(tabGlobalAnual);
            tabControl.TabPages.Add(tabMetricas);

            Controls.Add(tabControl);
            Controls.Add(panelFiltros);

            ApplyTheme();
        }

        private void InicializarTabOperativos()
        {
            lblOpTotal = CrearLabelKpi("Total reservas: 0", 20, 20);
            lblOpPendientes = CrearLabelKpi("Pendientes: 0", 250, 20);
            lblOpConfirmadas = CrearLabelKpi("Confirmadas: 0", 480, 20);
            lblOpCanceladas = CrearLabelKpi("Canceladas: 0", 710, 20);
            lblOpFinalizadas = CrearLabelKpi("Finalizadas: 0", 940, 20);

            chartOperMensual = CrearChart(20, 70, 560, 300, "Reservas mensuales");
            chartOperEstados = CrearChart(610, 70, 420, 300, "Distribución por estado");

            dgvOperativo = CrearGrid(20, 390, 1010, 320);

            tabOperativos.Controls.Add(lblOpTotal);
            tabOperativos.Controls.Add(lblOpPendientes);
            tabOperativos.Controls.Add(lblOpConfirmadas);
            tabOperativos.Controls.Add(lblOpCanceladas);
            tabOperativos.Controls.Add(lblOpFinalizadas);
            tabOperativos.Controls.Add(chartOperMensual);
            tabOperativos.Controls.Add(chartOperEstados);
            tabOperativos.Controls.Add(dgvOperativo);
        }

        private void InicializarTabHabitaciones()
        {
            lblHabTotal = CrearLabelKpi("Total habitaciones: 0", 20, 20);
            lblHabDisponibles = CrearLabelKpi("Disponibles: 0", 250, 20);
            lblHabOcupadas = CrearLabelKpi("Ocupadas: 0", 480, 20);
            lblHabMantenimiento = CrearLabelKpi("Mantenimiento: 0", 710, 20);
            lblHabLimpieza = CrearLabelKpi("Limpieza: 0", 940, 20);

            chartHabEstados = CrearChart(20, 70, 340, 280, "Estado actual habitaciones");
            chartHabTop = CrearChart(380, 70, 650, 280, "Top habitaciones más reservadas");
            chartHabTipo = CrearChart(20, 370, 1010, 250, "Ocupación por tipo de habitación");

            dgvHabitaciones = CrearGrid(20, 640, 1010, 250);

            tabHabitaciones.Controls.Add(lblHabTotal);
            tabHabitaciones.Controls.Add(lblHabDisponibles);
            tabHabitaciones.Controls.Add(lblHabOcupadas);
            tabHabitaciones.Controls.Add(lblHabMantenimiento);
            tabHabitaciones.Controls.Add(lblHabLimpieza);
            tabHabitaciones.Controls.Add(chartHabEstados);
            tabHabitaciones.Controls.Add(chartHabTop);
            tabHabitaciones.Controls.Add(chartHabTipo);
            tabHabitaciones.Controls.Add(dgvHabitaciones);
        }

        private void InicializarTabGerenciales()
        {
            lblGerFacturado = CrearLabelKpi("Total facturado: 0", 20, 20);
            lblGerCobrado = CrearLabelKpi("Total cobrado: 0", 250, 20);
            lblGerFacturas = CrearLabelKpi("Facturas: 0", 480, 20);
            lblGerTicket = CrearLabelKpi("Ticket promedio: 0", 710, 20);
            lblGerClientes = CrearLabelKpi("Clientes únicos: 0", 20, 55);
            lblGerReservas = CrearLabelKpi("Reservas facturadas: 0", 250, 55);

            chartGerFactMensual = CrearChart(20, 95, 500, 280, "Facturación mensual");
            chartGerEmpleados = CrearChart(540, 95, 490, 280, "Participación por empleado");

            dgvGerEmpleados = CrearGrid(20, 395, 500, 270);
            dgvGerClientes = CrearGrid(540, 395, 490, 270);

            tabGerenciales.Controls.Add(lblGerFacturado);
            tabGerenciales.Controls.Add(lblGerCobrado);
            tabGerenciales.Controls.Add(lblGerFacturas);
            tabGerenciales.Controls.Add(lblGerTicket);
            tabGerenciales.Controls.Add(lblGerClientes);
            tabGerenciales.Controls.Add(lblGerReservas);
            tabGerenciales.Controls.Add(chartGerFactMensual);
            tabGerenciales.Controls.Add(chartGerEmpleados);
            tabGerenciales.Controls.Add(dgvGerEmpleados);
            tabGerenciales.Controls.Add(dgvGerClientes);
        }

        private void InicializarTabGlobalAnual()
        {
            lblAnTotalReservas = CrearLabelKpi("Reservas año: 0", 20, 20);
            lblAnFacturado = CrearLabelKpi("Facturado año: 0", 240, 20);
            lblAnCobrado = CrearLabelKpi("Cobrado año: 0", 480, 20);
            lblAnServicios = CrearLabelKpi("Servicios año: 0", 720, 20);
            lblAnPromociones = CrearLabelKpi("Promociones año: 0", 20, 55);
            lblAnPctCobro = CrearLabelKpi("% Cobro anual: 0", 240, 55);

            chartAnFactCobro = CrearChart(20, 95, 500, 280, "Facturado vs cobrado por mes");
            chartAnServiciosPromo = CrearChart(540, 95, 490, 280, "Servicios vs promociones por mes");

            dgvAnualGlobal = CrearGrid(20, 395, 1010, 280);

            tabGlobalAnual.Controls.Add(lblAnTotalReservas);
            tabGlobalAnual.Controls.Add(lblAnFacturado);
            tabGlobalAnual.Controls.Add(lblAnCobrado);
            tabGlobalAnual.Controls.Add(lblAnServicios);
            tabGlobalAnual.Controls.Add(lblAnPromociones);
            tabGlobalAnual.Controls.Add(lblAnPctCobro);
            tabGlobalAnual.Controls.Add(chartAnFactCobro);
            tabGlobalAnual.Controls.Add(chartAnServiciosPromo);
            tabGlobalAnual.Controls.Add(dgvAnualGlobal);
        }

        private void InicializarTabMetricas()
        {
            lblMetricasAnio = new Label();
            lblMetricasAnio.Text = "Año:";
            lblMetricasAnio.Location = new Point(20, 20);
            lblMetricasAnio.AutoSize = true;

            cboMetricasAnio = new ComboBox();
            cboMetricasAnio.Location = new Point(60, 16);
            cboMetricasAnio.Width = 100;
            cboMetricasAnio.DropDownStyle = ComboBoxStyle.DropDownList;

            int currentYear = DateTime.Today.Year;
            for (int y = currentYear; y >= currentYear - 5; y--)
                cboMetricasAnio.Items.Add(y);
            cboMetricasAnio.SelectedIndex = 0;

            lblMetricaSelector = new Label();
            lblMetricaSelector.Text = "Métrica:";
            lblMetricaSelector.Location = new Point(185, 20);
            lblMetricaSelector.AutoSize = true;

            cboMetricaSelector = new ComboBox();
            cboMetricaSelector.Location = new Point(240, 16);
            cboMetricaSelector.Width = 180;
            cboMetricaSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMetricaSelector.Items.AddRange(new object[]
            {
                "RevPAR (Revenue per Available Room)",
                "ADR (Average Daily Rate)",
                "Occupancy Rate",
                "GOPPAR (Gross Operating Profit per Available Room)"
            });
            cboMetricaSelector.SelectedIndex = 0;

            btnCargarMetricas = new Button();
            btnCargarMetricas.Text = "Cargar";
            btnCargarMetricas.Location = new Point(440, 14);
            btnCargarMetricas.Size = new Size(80, 28);
            btnCargarMetricas.Click += BtnCargarMetricas_Click;

            lblRevPAR = CrearLabelKpi("RevPAR: --", 20, 70);
            lblADR = CrearLabelKpi("ADR: --", 250, 70);
            lblOccupancy = CrearLabelKpi("Occupancy: --", 480, 70);
            lblGOPPAR = CrearLabelKpi("GOPPAR: --", 710, 70);

            dgvMetricas = CrearGrid(20, 110, 1010, 550);

            tabMetricas.Controls.Add(lblMetricasAnio);
            tabMetricas.Controls.Add(cboMetricasAnio);
            tabMetricas.Controls.Add(lblMetricaSelector);
            tabMetricas.Controls.Add(cboMetricaSelector);
            tabMetricas.Controls.Add(btnCargarMetricas);
            tabMetricas.Controls.Add(lblRevPAR);
            tabMetricas.Controls.Add(lblADR);
            tabMetricas.Controls.Add(lblOccupancy);
            tabMetricas.Controls.Add(lblGOPPAR);
            tabMetricas.Controls.Add(dgvMetricas);
        }

        private void BtnCargarMetricas_Click(object sender, EventArgs e)
        {
            CargarMetricas();
        }

        private void CargarMetricas()
        {
            if (!SesionUsuario.EsAdmin()) return;

            int anio = DateTime.Today.Year;
            if (cboMetricasAnio.SelectedItem != null)
                int.TryParse(cboMetricasAnio.SelectedItem.ToString(), out anio);

            try
            {
                // Cargar KPI del mes actual para cada métrica
                DataTable dtRevPAR = ReportesData.ObtenerRevPAR(anio);
                DataTable dtADR = ReportesData.ObtenerADR(anio);
                DataTable dtOccupancy = ReportesData.ObtenerOccupancyRate(anio);
                DataTable dtGOPPAR = ReportesData.ObtenerGOPPAR(anio);

                int mesActual = DateTime.Today.Month;

                // KPI RevPAR mes actual (promedio de sucursales)
                decimal revparMes = 0;
                foreach (DataRow row in dtRevPAR.Rows)
                {
                    if (row["Mes"] != DBNull.Value && Convert.ToInt32(row["Mes"]) == mesActual)
                        revparMes += row["RevPAR"] == DBNull.Value ? 0m : Convert.ToDecimal(row["RevPAR"]);
                }
                lblRevPAR.Text = "RevPAR mes: " + revparMes.ToString("N2");

                // KPI ADR mes actual
                decimal adrMes = 0;
                foreach (DataRow row in dtADR.Rows)
                {
                    if (row["Mes"] != DBNull.Value && Convert.ToInt32(row["Mes"]) == mesActual)
                        adrMes = row["ADR"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ADR"]);
                }
                lblADR.Text = "ADR mes: " + adrMes.ToString("N2");

                // KPI Occupancy mes actual
                decimal occupancyMes = 0;
                foreach (DataRow row in dtOccupancy.Rows)
                {
                    if (row["Mes"] != DBNull.Value && Convert.ToInt32(row["Mes"]) == mesActual)
                        occupancyMes = row["OccupancyRate"] == DBNull.Value ? 0m : Convert.ToDecimal(row["OccupancyRate"]);
                }
                lblOccupancy.Text = "Occupancy mes: " + occupancyMes.ToString("N2") + "%";

                // KPI GOPPAR mes actual
                decimal gopparMes = 0;
                foreach (DataRow row in dtGOPPAR.Rows)
                {
                    if (row["Mes"] != DBNull.Value && Convert.ToInt32(row["Mes"]) == mesActual)
                        gopparMes += row["GOPPAR"] == DBNull.Value ? 0m : Convert.ToDecimal(row["GOPPAR"]);
                }
                lblGOPPAR.Text = "GOPPAR mes: " + gopparMes.ToString("N2");

                // Mostrar DataTable seleccionado en el grid
                int selIdx = cboMetricaSelector.SelectedIndex;
                DataTable dtActiva = selIdx == 0 ? dtRevPAR
                    : selIdx == 1 ? dtADR
                    : selIdx == 2 ? dtOccupancy
                    : dtGOPPAR;

                dgvMetricas.DataSource = dtActiva;
                AplicarTemaGridMetricas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar métricas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarTemaGridMetricas()
        {
            if (dgvMetricas == null) return;
            try
            {
                Color headerBg = Theme.GridHeaderBackground;
                Color textPrimary = Theme.TextPrimary;
                Color selectionBg = Theme.GridSelectionBg;
                Color selectionText = Theme.GridSelectionText;

                dgvMetricas.BackgroundColor = Theme.FormBackground;
                dgvMetricas.GridColor = Theme.GridLine;
                dgvMetricas.ForeColor = textPrimary;
                dgvMetricas.DefaultCellStyle.BackColor = Theme.ContentBackground;
                dgvMetricas.DefaultCellStyle.ForeColor = textPrimary;
                dgvMetricas.DefaultCellStyle.SelectionBackColor = selectionBg;
                dgvMetricas.DefaultCellStyle.SelectionForeColor = selectionText;
                dgvMetricas.EnableHeadersVisualStyles = false;

                if (dgvMetricas.Columns != null)
                {
                    foreach (DataGridViewColumn col in dgvMetricas.Columns)
                    {
                        if (col == null) continue;
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                        if (col.HeaderCell?.Style != null)
                        {
                            col.HeaderCell.Style.BackColor = headerBg;
                            col.HeaderCell.Style.ForeColor = textPrimary;
                        }
                    }
                }
            }
            catch { }
        }

        private Label CrearLabelKpi(string texto, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = texto;
            lbl.Font = Theme.AppFontBold;
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            return lbl;
        }

        private Chart CrearChart(int x, int y, int width, int height, string titulo)
        {
            Chart chart = new Chart();
            chart.Location = new Point(x, y);
            chart.Size = new Size(width, height);
            chart.BorderlineColor = Theme.GridLine;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BorderlineWidth = 1;

            ChartArea area = new ChartArea("Area1");
            chart.ChartAreas.Add(area);

            Legend legend = new Legend("Legend1");
            chart.Legends.Add(legend);

            chart.Titles.Add(titulo);

            return chart;
        }

        private DataGridView CrearGrid(int x, int y, int width, int height)
        {
            DataGridView dgv = new DataGridView();
            dgv.Location = new Point(x, y);
            dgv.Size = new Size(width, height);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.ScrollBars = ScrollBars.Both;
            dgv.RowHeadersWidth = 25;
            return dgv;
        }

        private void BtnCargar_Click(object sender, EventArgs e) => CargarTodo();

        private void BtnExportarExcel_Click(object sender, EventArgs e)
        {
            // Exportar el grid activo de la tab seleccionada
            DataGridView dgvActivo = null;
            string titulo = "Reporte";

            if (tabControl.SelectedTab == tabOperativos)
            {
                dgvActivo = dgvOperativo;
                titulo = "Operativos";
            }
            else if (tabControl.SelectedTab == tabHabitaciones)
            {
                dgvActivo = dgvHabitaciones;
                titulo = "Habitaciones";
            }
            else if (tabControl.SelectedTab == tabGerenciales)
            {
                dgvActivo = dgvGerEmpleados;
                titulo = "Gerenciales_Empleados";
            }
            else if (tabControl.SelectedTab == tabGlobalAnual)
            {
                dgvActivo = dgvAnualGlobal;
                titulo = "Global_Anual";
            }
            else if (tabControl.SelectedTab == tabMetricas)
            {
                dgvActivo = dgvMetricas;
                titulo = "Metricas_" + (cboMetricaSelector.SelectedItem?.ToString() ?? "");
            }

            if (dgvActivo == null || dgvActivo.DataSource == null)
            {
                MessageBox.Show("No hay datos en la pestaña activa para exportar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ExportHelper.ExportarDataTableAExcel(dgvActivo.DataSource as DataTable, titulo);
        }

        private void CargarAnios()
        {
            try
            {
                DataTable dt = ReportesData.ObtenerAniosDisponibles();
                cboAnio.DataSource = dt;
                cboAnio.DisplayMember = "Anio";
                cboAnio.ValueMember = "Anio";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar años: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTodo()
        {
            if (dtHasta.Value.Date < dtDesde.Value.Date)
            {
                MessageBox.Show("La fecha hasta no puede ser menor que la fecha desde.");
                return;
            }

            int anio = DateTime.Today.Year;
            if (cboAnio.SelectedValue != null)
                int.TryParse(cboAnio.SelectedValue.ToString(), out anio);

            CargarOperativos(dtDesde.Value.Date, dtHasta.Value.Date, anio);
            CargarHabitaciones(dtDesde.Value.Date, dtHasta.Value.Date, anio);
            CargarGerenciales(dtDesde.Value.Date, dtHasta.Value.Date, anio);
            CargarGlobalAnual(anio);
        }

        private void CargarOperativos(DateTime desde, DateTime hasta, int anio)
        {
            try
            {
                DataTable resumen = ReportesData.ObtenerResumenOperativo(desde, hasta);
                if (resumen.Rows.Count > 0)
                {
                    DataRow r = resumen.Rows[0];
                    lblOpTotal.Text = "Total reservas: " + Convert.ToInt32(r["TotalReservas"]);
                    lblOpPendientes.Text = "Pendientes: " + Convert.ToInt32(r["Pendientes"]);
                    lblOpConfirmadas.Text = "Confirmadas: " + Convert.ToInt32(r["Confirmadas"]);
                    lblOpCanceladas.Text = "Canceladas: " + Convert.ToInt32(r["Canceladas"]);
                    lblOpFinalizadas.Text = "Finalizadas: " + Convert.ToInt32(r["Finalizadas"]);
                }

                DataTable mensual = ReportesData.ObtenerReservasMensuales(anio);
                LlenarChartBarras(chartOperMensual, mensual, "Mes", "Cantidad", false);

                DataTable estados = ReportesData.ObtenerDistribucionEstadosReserva(anio);
                LlenarChartPie(chartOperEstados, estados, "Estado", "Cantidad", "Porcentaje");

                dgvOperativo.DataSource = ReportesData.ObtenerDetalleOperativo(desde, hasta);
                ConfigurarGridOperativo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reportes operativos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHabitaciones(DateTime desde, DateTime hasta, int anio)
        {
            try
            {
                DataTable resumen = ReportesData.ObtenerResumenHabitacionesActual();
                if (resumen.Rows.Count > 0)
                {
                    DataRow r = resumen.Rows[0];
                    lblHabTotal.Text = "Total habitaciones: " + Convert.ToInt32(r["TotalHabitaciones"]);
                    lblHabDisponibles.Text = "Disponibles: " + Convert.ToInt32(r["Disponibles"]);
                    lblHabOcupadas.Text = "Ocupadas: " + Convert.ToInt32(r["Ocupadas"]);
                    lblHabMantenimiento.Text = "Mantenimiento: " + Convert.ToInt32(r["Mantenimiento"]);
                    lblHabLimpieza.Text = "Limpieza: " + Convert.ToInt32(r["Limpieza"]);
                }

                DataTable estados = ReportesData.ObtenerDistribucionEstadoHabitaciones();
                LlenarChartPie(chartHabEstados, estados, "Estado", "Cantidad", "Porcentaje");

                DataTable top = ReportesData.ObtenerTopHabitacionesReservadas(anio);
                LlenarChartBarras(chartHabTop, top, "Numero", "TotalReservas", false);

                DataTable tipo = ReportesData.ObtenerOcupacionPorTipoHabitacion(anio);
                LlenarChartBarras(chartHabTipo, tipo, "TipoHabitacion", "Porcentaje", true);

                dgvHabitaciones.DataSource = ReportesData.ObtenerMantenimientosPeriodo(desde, hasta);
                ConfigurarGridHabitaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reportes de habitaciones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGerenciales(DateTime desde, DateTime hasta, int anio)
        {
            try
            {
                DataTable resumen = ReportesData.ObtenerResumenGerencial(desde, hasta);
                if (resumen.Rows.Count > 0)
                {
                    DataRow r = resumen.Rows[0];
                    lblGerFacturado.Text = "Total facturado: " + Convert.ToDecimal(r["TotalFacturado"]).ToString("N2");
                    lblGerCobrado.Text = "Total cobrado: " + Convert.ToDecimal(r["TotalCobrado"]).ToString("N2");
                    lblGerFacturas.Text = "Facturas: " + Convert.ToInt32(r["CantidadFacturas"]);
                    lblGerTicket.Text = "Ticket promedio: " + Convert.ToDecimal(r["TicketPromedio"]).ToString("N2");
                    lblGerClientes.Text = "Clientes únicos: " + Convert.ToInt32(r["ClientesUnicos"]);
                    lblGerReservas.Text = "Reservas facturadas: " + Convert.ToInt32(r["ReservasFacturadas"]);
                }

                DataTable factMensual = ReportesData.ObtenerFacturacionMensual(anio);
                LlenarChartBarras(chartGerFactMensual, factMensual, "Mes", "Total", false);

                DataTable empleados = ReportesData.ObtenerRendimientoEmpleadosGerencial(desde, hasta);
                LlenarChartBarras(chartGerEmpleados, empleados, "Empleado", "PorcentajeParticipacion", true);

                dgvGerEmpleados.DataSource = empleados;
                dgvGerClientes.DataSource = ReportesData.ObtenerTopClientesGerencial(desde, hasta);

                ConfigurarGridGerEmpleados();
                ConfigurarGridGerClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reportes gerenciales: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGlobalAnual(int anio)
        {
            try
            {
                DataTable resumen = ReportesData.ObtenerResumenGlobalAnual(anio);
                if (resumen.Rows.Count > 0)
                {
                    DataRow r = resumen.Rows[0];
                    lblAnTotalReservas.Text = "Reservas año: " + Convert.ToInt32(r["TotalReservas"]);
                    lblAnFacturado.Text = "Facturado año: " + Convert.ToDecimal(r["TotalFacturado"]).ToString("N2");
                    lblAnCobrado.Text = "Cobrado año: " + Convert.ToDecimal(r["TotalCobrado"]).ToString("N2");
                    lblAnServicios.Text = "Servicios año: " + Convert.ToDecimal(r["TotalServicios"]).ToString("N2");
                    lblAnPromociones.Text = "Promociones año: " + Convert.ToDecimal(r["TotalPromociones"]).ToString("N2");
                    lblAnPctCobro.Text = "% Cobro anual: " + Convert.ToDecimal(r["PorcentajeCobro"]).ToString("N2") + "%";
                }

                DataTable ingresos = ReportesData.ObtenerComparativoIngresosMensual(anio);
                LlenarChartDosSeries(chartAnFactCobro, ingresos, "Facturado", "Cobrado", "Facturado", "Cobrado");

                DataTable serviciosPromo = ReportesData.ObtenerServiciosPromocionesMensual(anio);
                LlenarChartDosSeries(chartAnServiciosPromo, serviciosPromo, "Servicios", "Promociones", "Servicios", "Promociones");

                dgvAnualGlobal.DataSource = ReportesData.ObtenerResumenMensualGlobal(anio);
                ConfigurarGridGlobalAnual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en informe global anual: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NUEVO: Llenar chart de barras (reemplaza a CargarGraficoMensual)
        private void LlenarChartBarras(Chart chart, DataTable dt, string labelField, string valueField, bool porcentaje)
        {
            if (chart == null || dt == null || dt.Rows.Count == 0) return;

            try
            {
                chart.Series.Clear();
                chart.ChartAreas[0].AxisX.Title = "Mes";
                chart.ChartAreas[0].AxisY.Title = porcentaje ? "%" : "Valor";

                Series serie = new Series("Serie");
                serie.ChartType = SeriesChartType.Column;
                serie.IsValueShownAsLabel = true;

                foreach (DataRow row in dt.Rows)
                {
                    string mes = row.Table.Columns.Contains("MesNombre")
                        ? row["MesNombre"].ToString()
                        : row["MesNumero"].ToString();

                    decimal valor = Convert.ToDecimal(row[valueField]);
                    int point = serie.Points.AddXY(mes, valor);
                    serie.Points[point].Label = porcentaje
                        ? valor.ToString("N2") + "%"
                        : valor.ToString("N2");
                }

                chart.Series.Add(serie);
            }
            catch { }
        }

        // ✅ NUEVO: Llenar chart de pie (reemplaza a CargarGraficoPie)
        private void LlenarChartPie(Chart chart, DataTable dt, string labelField, string valueField, string pctField)
        {
            if (chart == null || dt == null || dt.Rows.Count == 0) return;

            try
            {
                chart.Series.Clear();
                chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
                chart.ChartAreas[0].AxisY.LabelStyle.Enabled = false;

                Series serie = new Series("Serie");
                serie.ChartType = SeriesChartType.Pie;
                serie.IsValueShownAsLabel = true;

                foreach (DataRow row in dt.Rows)
                {
                    string nombre = row[labelField].ToString();
                    decimal valor = Convert.ToDecimal(row[valueField]);
                    decimal porcentaje = Convert.ToDecimal(row[pctField]);

                    int point = serie.Points.AddXY(nombre, valor);
                    serie.Points[point].Label = nombre + " (" + porcentaje.ToString("N2") + "%)";
                    serie.Points[point].LegendText = nombre;
                }

                chart.Series.Add(serie);
            }
            catch { }
        }

        // ✅ NUEVO: Llenar chart con dos series (reemplaza a CargarGraficoDosSeriesMensual)
        private void LlenarChartDosSeries(Chart chart, DataTable dt, string field1, string field2, string name1, string name2)
        {
            if (chart == null || dt == null || dt.Rows.Count == 0) return;

            try
            {
                chart.Series.Clear();
                chart.ChartAreas[0].AxisX.Title = "Mes";
                chart.ChartAreas[0].AxisY.Title = "Valor";

                Series serie1 = new Series(name1);
                serie1.ChartType = SeriesChartType.Column;
                serie1.IsValueShownAsLabel = true;

                Series serie2 = new Series(name2);
                serie2.ChartType = SeriesChartType.Column;
                serie2.IsValueShownAsLabel = true;

                foreach (DataRow row in dt.Rows)
                {
                    string mes = row["MesNombre"].ToString();
                    decimal v1 = Convert.ToDecimal(row[field1]);
                    decimal v2 = Convert.ToDecimal(row[field2]);

                    serie1.Points.AddXY(mes, v1);
                    serie1.Points[serie1.Points.Count - 1].Label = v1.ToString("N2");

                    serie2.Points.AddXY(mes, v2);
                    serie2.Points[serie2.Points.Count - 1].Label = v2.ToString("N2");
                }

                chart.Series.Add(serie1);
                chart.Series.Add(serie2);
            }
            catch { }
        }

        // ── Configurar grids ──────────────────────────────────────────────────────

        private void ConfigurarGridOperativo()
        {
            if (dgvOperativo.Columns.Count == 0) return;

            dgvOperativo.Columns["IdReserva"].Width = 80;
            dgvOperativo.Columns["Cliente"].Width = 180;
            dgvOperativo.Columns["Habitacion"].Width = 90;
            dgvOperativo.Columns["Empleado"].Width = 150;
            dgvOperativo.Columns["EstadoReserva"].Width = 110;
            dgvOperativo.Columns["FechaReserva"].Width = 110;
            dgvOperativo.Columns["FechaEntrada"].Width = 95;
            dgvOperativo.Columns["FechaSalida"].Width = 95;
            dgvOperativo.Columns["CantidadHuespedes"].Width = 105;
            dgvOperativo.Columns["Hospedaje"].Width = 90;
            dgvOperativo.Columns["Servicios"].Width = 90;
            dgvOperativo.Columns["Promociones"].Width = 95;
            dgvOperativo.Columns["TotalEstimado"].Width = 100;
        }

        private void ConfigurarGridHabitaciones()
        {
            if (dgvHabitaciones.Columns.Count == 0) return;

            dgvHabitaciones.Columns["IdMantenimiento"].Width = 90;
            dgvHabitaciones.Columns["Habitacion"].Width = 90;
            dgvHabitaciones.Columns["Empleado"].Width = 150;
            dgvHabitaciones.Columns["FechaInicio"].Width = 120;
            dgvHabitaciones.Columns["FechaFin"].Width = 120;
            dgvHabitaciones.Columns["Motivo"].Width = 240;
            dgvHabitaciones.Columns["Costo"].Width = 90;
            dgvHabitaciones.Columns["Estado"].Width = 110;
        }

        private void ConfigurarGridGerEmpleados()
        {
            if (dgvGerEmpleados.Columns.Count == 0) return;

            if (dgvGerEmpleados.Columns.Contains("IdEmpleado"))
                dgvGerEmpleados.Columns["IdEmpleado"].Visible = false;

            dgvGerEmpleados.Columns["Empleado"].Width = 150;
            dgvGerEmpleados.Columns["NombreSucursal"].Width = 120;
            dgvGerEmpleados.Columns["TotalReservas"].Width = 90;
            dgvGerEmpleados.Columns["TotalFacturado"].Width = 110;
            dgvGerEmpleados.Columns["PorcentajeParticipacion"].Width = 120;
        }

        private void ConfigurarGridGerClientes()
        {
            if (dgvGerClientes.Columns.Count == 0) return;

            if (dgvGerClientes.Columns.Contains("IdCliente"))
                dgvGerClientes.Columns["IdCliente"].Visible = false;

            dgvGerClientes.Columns["Cliente"].Width = 200;
            dgvGerClientes.Columns["TotalReservas"].Width = 100;
            dgvGerClientes.Columns["TotalFacturado"].Width = 120;
        }

        private void ConfigurarGridGlobalAnual()
        {
            if (dgvAnualGlobal.Columns.Count == 0) return;

            dgvAnualGlobal.Columns["MesNumero"].Visible = false;
            dgvAnualGlobal.Columns["MesNombre"].Width = 90;
            dgvAnualGlobal.Columns["Reservas"].Width = 90;
            dgvAnualGlobal.Columns["Facturado"].Width = 110;
            dgvAnualGlobal.Columns["Cobrado"].Width = 110;
            dgvAnualGlobal.Columns["Servicios"].Width = 110;
            dgvAnualGlobal.Columns["Promociones"].Width = 110;
            dgvAnualGlobal.Columns["PorcentajeCobro"].Width = 110;
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