using System;
using System.Drawing;
using System.Windows.Forms;
using HotelReservas.Assets;

namespace HotelReservas
{
    /// <summary>
    /// Post-checkout rating dialog. Shows as ShowDialog() from ReservasForm after
    /// a successful check-out. Non-modal blocking dialog — returns DialogResult.OK
    /// when rating is submitted, DialogResult.Cancel when skipped.
    /// </summary>
    public class RatingForm : Form
    {
        private readonly int _idReserva;

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblPuntuacion;
        private Label lblComentario;
        private Label[] lblEstrellas;
        private byte _puntuacionSeleccionada = 0;
        private TextBox txtComentario;
        private Button btnEnviar;
        private Button btnOmitir;

        private EventHandler _themeHandler;

        public RatingForm(int idReserva)
        {
            _idReserva = idReserva;
            InicializarComponentes();
            _themeHandler = (s, e) => ApplyTheme();
            Theme.OnThemeChanged += _themeHandler;
            ApplyTheme();
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

        private void InicializarComponentes()
        {
            Text = "Calificacion de estadia";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Size = new Size(480, 380);
            Font = Theme.AppFont;

            lblTitulo = new Label();
            lblTitulo.Text = "Calificacion de la estadia";
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            lblSubtitulo = new Label();
            lblSubtitulo.Text = "Reserva #" + _idReserva + " — Por favor califique la estadia del huesped.";
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = Theme.AppFontSmall;
            lblSubtitulo.Location = new Point(20, 55);

            lblPuntuacion = new Label();
            lblPuntuacion.Text = "Puntuacion (1-5 estrellas):";
            lblPuntuacion.AutoSize = true;
            lblPuntuacion.Font = Theme.AppFontBold;
            lblPuntuacion.Location = new Point(20, 90);

            // Star labels (clickable)
            lblEstrellas = new Label[5];
            for (int i = 0; i < 5; i++)
            {
                int idx = i + 1;
                lblEstrellas[i] = new Label();
                lblEstrellas[i].Text = "☆";
                lblEstrellas[i].Font = new Font("Segoe UI", 22F);
                lblEstrellas[i].AutoSize = true;
                lblEstrellas[i].Location = new Point(20 + i * 52, 115);
                lblEstrellas[i].Cursor = Cursors.Hand;
                lblEstrellas[i].Tag = (byte)idx;
                lblEstrellas[i].Click += EstrellaClick;
                lblEstrellas[i].MouseEnter += EstrellaMouseEnter;
                lblEstrellas[i].MouseLeave += EstrellaMouseLeave;
            }

            lblComentario = new Label();
            lblComentario.Text = "Comentario (opcional):";
            lblComentario.AutoSize = true;
            lblComentario.Font = Theme.AppFontBold;
            lblComentario.Location = new Point(20, 170);

            txtComentario = new TextBox();
            txtComentario.Location = new Point(20, 195);
            txtComentario.Size = new Size(420, 80);
            txtComentario.Multiline = true;
            txtComentario.ScrollBars = ScrollBars.Vertical;
            txtComentario.MaxLength = 500;

            btnEnviar = new Button();
            btnEnviar.Text = "Enviar calificacion";
            btnEnviar.Location = new Point(20, 295);
            btnEnviar.Size = new Size(180, 34);
            btnEnviar.FlatStyle = FlatStyle.Flat;
            btnEnviar.Font = Theme.AppFontBold;
            btnEnviar.Cursor = Cursors.Hand;
            btnEnviar.Click += BtnEnviar_Click;

            btnOmitir = new Button();
            btnOmitir.Text = "Omitir";
            btnOmitir.Location = new Point(215, 295);
            btnOmitir.Size = new Size(100, 34);
            btnOmitir.FlatStyle = FlatStyle.Flat;
            btnOmitir.Cursor = Cursors.Hand;
            btnOmitir.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblPuntuacion);
            foreach (Label lbl in lblEstrellas)
                Controls.Add(lbl);
            Controls.Add(lblComentario);
            Controls.Add(txtComentario);
            Controls.Add(btnEnviar);
            Controls.Add(btnOmitir);
        }

        private void ActualizarEstrellas(byte resaltar)
        {
            for (int i = 0; i < 5; i++)
            {
                lblEstrellas[i].Text = (i < resaltar) ? "★" : "☆";
                lblEstrellas[i].ForeColor = (i < resaltar) ? Color.Goldenrod : Theme.TextSecondary;
            }
        }

        private void EstrellaClick(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            _puntuacionSeleccionada = (byte)lbl.Tag;
            ActualizarEstrellas(_puntuacionSeleccionada);
        }

        private void EstrellaMouseEnter(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            byte hover = (byte)lbl.Tag;
            ActualizarEstrellas(hover);
        }

        private void EstrellaMouseLeave(object sender, EventArgs e)
        {
            ActualizarEstrellas(_puntuacionSeleccionada);
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (_puntuacionSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar al menos 1 estrella.",
                    "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                RatingData.Insertar(_idReserva, _puntuacionSeleccionada, txtComentario.Text.Trim());
                MessageBox.Show("Calificacion registrada. ¡Gracias!",
                    "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar calificacion: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
