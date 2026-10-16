using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace HotelReservas
{
    public static class ExportHelper
    {
        /// <summary>
        /// Exports a DataTable to an Excel file via EPPlus.
        /// Shows a SaveFileDialog and opens the file after saving.
        /// </summary>
        public static void ExportarDataTableAExcel(DataTable dt, string titulo)
        {
            if (dt == null || dt.Columns.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Guardar reporte Excel";
                dlg.Filter = "Excel (*.xlsx)|*.xlsx";
                dlg.FileName = titulo.Replace(" ", "_") + "_" + DateTime.Today.ToString("yyyyMMdd");
                dlg.DefaultExt = "xlsx";

                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add(
                            titulo.Length > 31 ? titulo.Substring(0, 31) : titulo);

                        // Título del reporte en fila 1
                        ws.Cells[1, 1].Value = titulo;
                        ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true;
                        ws.Cells[1, 1].Style.Font.Bold = true;
                        ws.Cells[1, 1].Style.Font.Size = 14;
                        ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(37, 99, 235));
                        ws.Cells[1, 1].Style.Font.Color.SetColor(Color.White);

                        // Encabezados en fila 2
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            ExcelRange cell = ws.Cells[2, col + 1];
                            cell.Value = dt.Columns[col].ColumnName;
                            cell.Style.Font.Bold = true;
                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(235, 238, 250));
                            cell.Style.Font.Color.SetColor(Color.FromArgb(15, 20, 45));
                            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        }

                        // Datos desde fila 3
                        for (int row = 0; row < dt.Rows.Count; row++)
                        {
                            for (int col = 0; col < dt.Columns.Count; col++)
                            {
                                object val = dt.Rows[row][col];
                                ws.Cells[row + 3, col + 1].Value =
                                    val == DBNull.Value ? null : val;
                            }
                        }

                        // Auto-ajustar columnas
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();

                        // Fecha de generación
                        int lastRow = dt.Rows.Count + 4;
                        ws.Cells[lastRow, 1].Value = "Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                        ws.Cells[lastRow, 1].Style.Font.Italic = true;
                        ws.Cells[lastRow, 1].Style.Font.Color.SetColor(Color.Gray);

                        pck.SaveAs(new FileInfo(dlg.FileName));
                    }

                    // Abrir el archivo
                    try { Process.Start(dlg.FileName); } catch { }

                    MessageBox.Show("Archivo exportado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
