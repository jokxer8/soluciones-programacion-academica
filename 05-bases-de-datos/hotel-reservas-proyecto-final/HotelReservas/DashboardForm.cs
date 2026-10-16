using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using HotelReservas.Assets;

namespace HotelReservas
{
    public class DashboardForm : Form
    {
        // KPI Cards
        private Panel pnlKpiReservas;
        private Panel pnlKpiOcupacion;
        private Panel pnlKpiIngresos;
        private Panel pnlKpiMantenimiento;

        private Label lblKpiReservasTitle;
        private Label lblKpiReservasVal;
        private Label lblKpiOcupacionTitle;
        private Label lblKpiOcupacionVal;
        private Label lblKpiIngresosTitle;
        private Label lblKpiIngresosVal;
        private Label lblKpiMantTitle;
        private Label lblKpiMantVal;

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Button btnRefrescar;

        // Chart
        private Chart chartOcupacion;

        private EventHandler _themeHandler;

        public DashboardForm()
        {
            InicializarComponentes();
            Load += DashboardForm_Load;
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
        }

        private void ApplyTheme()
        {
            Theme.ApplyToForm(this);
            AplicarTemaKpiCards();
            AplicarTemaChart();
            if (lblTitulo != null) lblTitulo.ForeColor = Theme.TextPrimary;
            if (lblSubtitulo != null) lblSubtitulo.ForeColor = Theme.TextSecondary;
            if (btnRefrescar != null) Theme.ApplyAccentButton(btnRefrescar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Theme.OnThemeChanged -= _themeHandler;
            base.Dispose(disposing);
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void InicializarComponentes()
        {
            AutoScroll = true;

            lblTitulo = new Label();
            lblTitulo.Text = "Dashboard Operativo";
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblSubtitulo = new Label();
            lblSubtitulo.Text = "Resumen en tiempo real del hotel";
            lblSubtitulo.Font = Theme.AppFontSmall;
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(22, 55);

            btnRefrescar = new Button();
            btnRefrescar.Text = "Actualizar";
            btnRefrescar.Size = new Size(100, 30);
            btnRefrescar.Location = new Point(850, 25);
            btnRefrescar.FlatStyle = FlatStyle.Flat;
            btnRefrescar.Cursor = Cursors.Hand;
            btnRefrescar.Font = Theme.AppFont;
            btnRefrescar.BackColor = Theme.AccentBackground;
            btnRefrescar.ForeColor = Theme.AccentText;
            btnRefrescar.FlatAppearance.BorderSize = 0;
            btnRefrescar.Click += (s, e) => CargarDatos();

            // ── KPI Cards ─────────────────────────────────────────────────────
            int cardW = 220;
            int cardH = 100;
            int cardY = 90;
            int spacing = 240;

            pnlKpiReservas = CrearPanelKpi(20, cardY, cardW, cardH);
            lblKpiReservasTitle = CrearLabelKpiTitle("Reservas hoy");
            lblKpiReservasVal = CrearLabelKpiVal("0");
            pnlKpiReservas.Controls.Add(lblKpiReservasTitle);
            pnlKpiReservas.Controls.Add(lblKpiReservasVal);

            pnlKpiOcupacion = CrearPanelKpi(20 + spacing, cardY, cardW, cardH);
            lblKpiOcupacionTitle = CrearLabelKpiTitle("Ocupación actual");
            lblKpiOcupacionVal = CrearLabelKpiVal("0%");
            pnlKpiOcupacion.Controls.Add(lblKpiOcupacionTitle);
            pnlKpiOcupacion.Controls.Add(lblKpiOcupacionVal);

            pnlKpiIngresos = CrearPanelKpi(20 + spacing * 2, cardY, cardW, cardH);
            lblKpiIngresosTitle = CrearLabelKpiTitle("Ingresos del mes");
            lblKpiIngresosVal = CrearLabelKpiVal("$0");
            pnlKpiIngresos.Controls.Add(lblKpiIngresosTitle);
            pnlKpiIngresos.Controls.Add(lblKpiIngresosVal);

            pnlKpiMantenimiento = CrearPanelKpi(20 + spacing * 3, cardY, cardW, cardH);
            lblKpiMantTitle = CrearLabelKpiTitle("Mantenimientos pendientes");
            lblKpiMantVal = CrearLabelKpiVal("0");
            pnlKpiMantenimiento.Controls.Add(lblKpiMantTitle);
            pnlKpiMantenimiento.Controls.Add(lblKpiMantVal);

            // ── Chart ocupación últimos 30 días ───────────────────────────────
            chartOcupacion = new Chart();
            chartOcupacion.Location = new Point(20, 220);
            chartOcupacion.Size = new Size(1000, 300);

            ChartArea area = new ChartArea("Ocupacion");
            area.AxisX.Title = "Fecha";
            area.AxisY.Title = "Habitaciones ocupadas";
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartOcupacion.ChartAreas.Add(area);

            Series serie = new Series("Ocupacion");
            serie.ChartType = SeriesChartType.Line;
            serie.BorderWidth = 2;
            serie.Color = Theme.AccentBackground;
            serie.MarkerStyle = MarkerStyle.Circle;
            serie.MarkerSize = 5;
            chartOcupacion.Series.Add(serie);

            Legend leyenda = new Legend();
            leyenda.Enabled = false;
            chartOcupacion.Legends.Add(leyenda);

            // ── Controles ────────────────────────────────────────────────────
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(btnRefrescar);
            Controls.Add(pnlKpiReservas);
            Controls.Add(pnlKpiOcupacion);
            Controls.Add(pnlKpiIngresos);
            Controls.Add(pnlKpiMantenimiento);
            Controls.Add(chartOcupacion);

            ApplyTheme();
        }

        private Panel CrearPanelKpi(int x, int y, int w, int h)
        {
            Panel pnl = new Panel();
            pnl.Location = new Point(x, y);
            pnl.Size = new Size(w, h);
            pnl.BackColor = Theme.PanelBackground;
            pnl.Padding = new Padding(12);
            return pnl;
        }

        private Label CrearLabelKpiTitle(string texto)
        {
            Label lbl = new Label();
            lbl.Text = texto;
            lbl.Font = Theme.AppFontSmall;
            lbl.AutoSize = false;
            lbl.Size = new Size(196, 22);
            lbl.Location = new Point(12, 10);
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            return lbl;
        }

        private Label CrearLabelKpiVal(string valor)
        {
            Label lbl = new Label();
            lbl.Text = valor;
            lbl.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lbl.AutoSize = false;
            lbl.Size = new Size(196, 45);
            lbl.Location = new Point(12, 42);
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            return lbl;
        }

        private void AplicarTemaKpiCards()
        {
            Panel[] cards = { pnlKpiReservas, pnlKpiOcupacion, pnlKpiIngresos, pnlKpiMantenimiento };
            foreach (Panel p in cards)
            {
                if (p == null) continue;
                p.BackColor = Theme.PanelBackground;
                foreach (Control c in p.Controls)
                {
                    if (c is Label lbl)
                    {
                        lbl.ForeColor = Theme.TextPrimary;
                        lbl.BackColor = Color.Transparent;
                    }
                }
            }

            if (lblKpiReservasTitle != null) lblKpiReservasTitle.ForeColor = Theme.TextSecondary;
            if (lblKpiOcupacionTitle != null) lblKpiOcupacionTitle.ForeColor = Theme.TextSecondary;
            if (lblKpiIngresosTitle != null) lblKpiIngresosTitle.ForeColor = Theme.TextSecondary;
            if (lblKpiMantTitle != null) lblKpiMantTitle.ForeColor = Theme.TextSecondary;
        }

        private void AplicarTemaChart()
        {
            if (chartOcupacion == null) return;
            try
            {
                chartOcupacion.BackColor = Theme.FormBackground;
                if (chartOcupacion.ChartAreas.Count > 0)
                {
                    chartOcupacion.ChartAreas[0].BackColor = Theme.FormBackground;
                    chartOcupacion.ChartAreas[0].AxisX.LabelStyle.ForeColor = Theme.TextSecondary;
                    chartOcupacion.ChartAreas[0].AxisY.LabelStyle.ForeColor = Theme.TextSecondary;
                    chartOcupacion.ChartAreas[0].AxisX.MajorGrid.LineColor = Theme.GridLine;
                    chartOcupacion.ChartAreas[0].AxisY.MajorGrid.LineColor = Theme.GridLine;
                    chartOcupacion.ChartAreas[0].AxisX.TitleForeColor = Theme.TextSecondary;
                    chartOcupacion.ChartAreas[0].AxisY.TitleForeColor = Theme.TextSecondary;
                }
                if (chartOcupacion.Series.Count > 0)
                    chartOcupacion.Series[0].Color = Theme.AccentBackground;
            }
            catch { }
        }

        private void CargarDatos()
        {
            try
            {
                int reservasHoy = DashboardData.ObtenerReservasHoy();
                int ocupadas = DashboardData.ObtenerHabitacionesOcupadas();
                int total = DashboardData.ObtenerTotalHabitaciones();
                decimal ingresos = DashboardData.ObtenerIngresosMes();
                int mantPend = DashboardData.ObtenerMantenimientosPendientes();

                lblKpiReservasVal.Text = reservasHoy.ToString();
                string pct = total > 0
                    ? Math.Round(ocupadas * 100.0 / total, 1).ToString("F1") + "%"
                    : "0%";
                lblKpiOcupacionVal.Text = pct;
                lblKpiIngresosVal.Text = "$" + ingresos.ToString("N0");
                lblKpiMantVal.Text = mantPend.ToString();

                CargarGrafico();
            }
            catch (Exception ex)
            {
                // Si la DB no está disponible, mostrar valores en cero sin interrumpir la UI
                lblKpiReservasVal.Text = "--";
                lblKpiOcupacionVal.Text = "--";
                lblKpiIngresosVal.Text = "--";
                lblKpiMantVal.Text = "--";
            }
        }

        private void CargarGrafico()
        {
            try
            {
                DataTable dt = DashboardData.ObtenerOcupacionUltimos30Dias();
                chartOcupacion.Series[0].Points.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    DateTime fecha = Convert.ToDateTime(row["Fecha"]);
                    int ocupadas = Convert.ToInt32(row["HabitacionesOcupadas"]);
                    chartOcupacion.Series[0].Points.AddXY(fecha.ToString("dd/MM"), ocupadas);
                }
            }
            catch { }
        }
    }
}
