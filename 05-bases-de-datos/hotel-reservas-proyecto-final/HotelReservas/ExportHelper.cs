using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace HotelReservas
{
    public static class ExportHelper
    {
        public static void ExportarDataTableAExcel(DataTable data, string titulo) { Guardar(data,titulo,false); }
        public static void ExportarDataTableAPdf(DataTable data, string titulo) { Guardar(data,titulo,true); }
        private static void Guardar(DataTable data,string titulo,bool pdf)
        {
            Validar(data);
            using(var dialog=new SaveFileDialog())
            {
                dialog.Title="Guardar reporte";
                dialog.Filter=pdf?"PDF (*.pdf)|*.pdf":"Excel (*.xlsx)|*.xlsx";
                dialog.DefaultExt=pdf?"pdf":"xlsx";
                dialog.FileName="Reporte_"+DateTime.Today.ToString("yyyyMMdd");
                if(dialog.ShowDialog()!=DialogResult.OK) return;
                try {
                    if(pdf) ExportarPdf(data,titulo,dialog.FileName); else ExportarExcel(data,titulo,dialog.FileName);
                    try { Process.Start(dialog.FileName); } catch { }
                    MessageBox.Show("Archivo exportado correctamente.","Exportación");
                } catch(Exception ex) { MessageBox.Show("No se pudo exportar: "+ex.Message,"Exportación"); }
            }
        }
        private static void Validar(DataTable data)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            if(data==null || data.Columns.Count==0) throw new ArgumentException("El reporte debe contener columnas.");
        }
        public static void ExportarExcel(DataTable data,string titulo,string path)
        {
            Validar(data);
            using(var package=new ExcelPackage())
            {
                var sheet=package.Workbook.Worksheets.Add("Reporte");
                sheet.Cells[1,1].Value=titulo;
                sheet.Cells[1,1,1,data.Columns.Count].Merge=true;
                sheet.Cells[1,1].Style.Font.Size=15;
                sheet.Cells[1,1].Style.Font.Bold=true;
                sheet.Cells[1,1].Style.WrapText=true;
                sheet.Row(1).Height=42;
                for(int c=0;c<data.Columns.Count;c++) {
                    sheet.Cells[2,c+1].Value=data.Columns[c].ColumnName;
                    for(int r=0;r<data.Rows.Count;r++) {
                        object value=data.Rows[r][c]; var cell=sheet.Cells[r+3,c+1];
                        cell.Value=value==DBNull.Value?null:value;
                        if(value is decimal || value is double) cell.Style.Numberformat.Format="#,##0.00";
                        if(value is DateTime) cell.Style.Numberformat.Format="dd/mm/yyyy";
                        cell.Style.WrapText=true;
                    }
                }
                var header=sheet.Cells[2,1,2,data.Columns.Count];
                header.Style.Font.Bold=true;
                header.Style.Fill.PatternType=ExcelFillStyle.Solid;
                header.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(37,99,235));
                header.Style.Font.Color.SetColor(Color.White);
                header.Style.WrapText=true; sheet.Row(2).Height=32;
                sheet.Cells[2,1,Math.Max(2,data.Rows.Count+2),data.Columns.Count].AutoFilter=true;
                sheet.View.FreezePanes(3,1);
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns(14,45);
                // AutoFit omite celdas con WrapText: fijar un ancho legible
                // también para fechas, encabezados y tablas vacías.
                for(int c=0;c<data.Columns.Count;c++) {
                    int length=data.Columns[c].ColumnName.Length;
                    foreach(DataRow row in data.Rows) length=Math.Max(length,Convert.ToString(row[c]).Length);
                    sheet.Column(c+1).Width=data.Columns[c].DataType==typeof(DateTime)?18:Math.Max(20,Math.Min(45,length+3));
                }
                if(data.Rows.Count==0) {
                    sheet.Cells[3,1,3,data.Columns.Count].Merge=true;
                    sheet.Cells[3,1].Value="Sin datos para los filtros seleccionados.";
                    sheet.Cells[3,1].Style.WrapText=true;
                    sheet.Row(3).Height=35;
                }
                sheet.PrinterSettings.Orientation=eOrientation.Landscape;
                sheet.PrinterSettings.FitToPage=true;
                sheet.PrinterSettings.FitToWidth=1; sheet.PrinterSettings.FitToHeight=0;
                sheet.PrinterSettings.RepeatRows=sheet.Cells["1:2"];
                package.SaveAs(new FileInfo(path));
            }
        }
        public static void ExportarPdf(DataTable data,string titulo,string path)
        {
            Validar(data);
            using(var stream=new FileStream(path,FileMode.Create,FileAccess.Write))
            using(var doc=new Document(data.Columns.Count>8?PageSize.A3.Rotate():PageSize.A4.Rotate(),30,30,30,30))
            {
                PdfWriter.GetInstance(doc,stream); doc.Open();
                var title=FontFactory.GetFont(FontFactory.HELVETICA_BOLD,15);
                var normal=FontFactory.GetFont(FontFactory.HELVETICA,9);
                var bold=FontFactory.GetFont(FontFactory.HELVETICA_BOLD,9);
                doc.Add(new Paragraph(titulo??"Reporte",title){SpacingAfter=14});
                var table=new PdfPTable(data.Columns.Count){WidthPercentage=100,HeaderRows=1,SplitLate=false};
                foreach(DataColumn column in data.Columns)
                    table.AddCell(new PdfPCell(new Phrase(column.ColumnName,bold)){Padding=6,BackgroundColor=new BaseColor(235,238,250)});
                foreach(DataRow row in data.Rows) foreach(object value in row.ItemArray) {
                    string text=value==DBNull.Value?"":value is DateTime?((DateTime)value).ToString("dd/MM/yyyy")
                        :value is decimal?((decimal)value).ToString("N2",System.Globalization.CultureInfo.GetCultureInfo("es-DO")):Convert.ToString(value);
                    table.AddCell(new PdfPCell(new Phrase(text,normal)){Padding=5});
                }
                doc.Add(table);
                if(data.Rows.Count==0) doc.Add(new Paragraph("Sin datos para los filtros seleccionados.",normal));
            }
        }
    }
}
