using System.Windows.Forms;

namespace HotelReservas.Assets
{
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// Applies the current theme colors and standard layout to a DataGridView.
        /// </summary>
        public static void ApplyTheme(this DataGridView dgv)
        {
            if (dgv == null) return;
            Theme.ApplyToGrid(dgv);
        }

        /// <summary>
        /// Configures standard read-only display settings: no row headers, full-row select,
        /// no user resize/add/delete, double-buffered rendering.
        /// </summary>
        public static void ApplyReadOnly(this DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BorderStyle = BorderStyle.None;

            Theme.SetDoubleBuffered(dgv, true);
        }

        /// <summary>
        /// Convenience: applies both theme and read-only settings in one call.
        /// </summary>
        public static void ApplyStandard(this DataGridView dgv)
        {
            dgv.ApplyReadOnly();
            dgv.ApplyTheme();
        }
    }
}
