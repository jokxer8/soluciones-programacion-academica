using System;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HotelReservas
{
    /// <summary>
    /// Generates a structured PDF invoice using iTextSharp 5.5.x.
    /// Call ExportarPdf(idFactura) — returns the full path of the saved file.
    /// </summary>
    public static class FacturaPdfExporter
    {
        private static readonly BaseColor ColorAccent  = new BaseColor(37, 99, 235);
        private static readonly BaseColor ColorHeader  = new BaseColor(235, 238, 250);
        private static readonly BaseColor ColorBorder  = new BaseColor(198, 204, 228);
        private static readonly BaseColor ColorText    = new BaseColor(15, 20, 45);
        private static readonly BaseColor ColorSubText = new BaseColor(82, 94, 130);
        private static readonly BaseColor ColorWhite   = BaseColor.WHITE;

        public static string ExportarPdf(int idFactura)
        {
            // Fetch all data needed
            DataTable dtResumen     = ObtenerResumenFactura(idFactura);
            DataTable dtServicios   = ObtenerServiciosFactura(idFactura);
            DataTable dtPromociones = ObtenerPromocionesFactura(idFactura);
            DataTable dtPagos       = ObtenerPagosFactura(idFactura);

            if (dtResumen.Rows.Count == 0)
                throw new InvalidOperationException("No se encontró la factura #" + idFactura + ".");

            DataRow r = dtResumen.Rows[0];

            // Output path: Desktop
            string fileName = "Factura_" + idFactura + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                Document doc = new Document(PageSize.A4, 40f, 40f, 50f, 50f);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                Font fontTitulo    = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18f, ColorAccent);
                Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11f, ColorText);
                Font fontNormal    = FontFactory.GetFont(FontFactory.HELVETICA, 9f, ColorText);
                Font fontBold      = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, ColorText);
                Font fontSmall     = FontFactory.GetFont(FontFactory.HELVETICA, 8f, ColorSubText);
                Font fontTableHdr  = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, ColorWhite);
                Font fontTotal     = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11f, ColorAccent);

                // ── Header ────────────────────────────────────────────────────
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 60f, 40f });
                headerTable.SpacingAfter = 10f;

                // Hotel name cell
                PdfPCell cellHotel = new PdfPCell();
                cellHotel.Border = Rectangle.NO_BORDER;
                cellHotel.AddElement(new Paragraph("HOTEL RESERVAS", fontTitulo));
                cellHotel.AddElement(new Paragraph("Sistema de Gestion Hotelera", fontSmall));
                headerTable.AddCell(cellHotel);

                // Invoice info cell
                PdfPCell cellInvoice = new PdfPCell();
                cellInvoice.Border = Rectangle.NO_BORDER;
                cellInvoice.HorizontalAlignment = Element.ALIGN_RIGHT;
                string fechaFactura = r["FechaFactura"] == DBNull.Value
                    ? ""
                    : Convert.ToDateTime(r["FechaFactura"]).ToString("dd/MM/yyyy HH:mm");
                cellInvoice.AddElement(new Paragraph("FACTURA No. " + idFactura, fontSubtitulo)
                    { Alignment = Element.ALIGN_RIGHT });
                cellInvoice.AddElement(new Paragraph("Reserva No. " + (r["IdReserva"] == DBNull.Value ? "" : r["IdReserva"].ToString()), fontNormal)
                    { Alignment = Element.ALIGN_RIGHT });
                cellInvoice.AddElement(new Paragraph("Fecha: " + fechaFactura, fontNormal)
                    { Alignment = Element.ALIGN_RIGHT });
                string estadoFact = r["Estado"] == DBNull.Value ? "" : r["Estado"].ToString();
                cellInvoice.AddElement(new Paragraph("Estado: " + estadoFact, fontBold)
                    { Alignment = Element.ALIGN_RIGHT });
                headerTable.AddCell(cellInvoice);
                doc.Add(headerTable);

                // Horizontal rule
                PdfPTable ruleLine = new PdfPTable(1);
                ruleLine.WidthPercentage = 100;
                ruleLine.SpacingAfter = 12f;
                PdfPCell ruleCell = new PdfPCell(new Phrase(" "));
                ruleCell.Border = Rectangle.BOTTOM_BORDER;
                ruleCell.BorderColor = ColorAccent;
                ruleCell.BorderWidth = 2f;
                ruleLine.AddCell(ruleCell);
                doc.Add(ruleLine);

                // ── Client and stay data ──────────────────────────────────────
                PdfPTable infoTable = new PdfPTable(2);
                infoTable.WidthPercentage = 100;
                infoTable.SetWidths(new float[] { 50f, 50f });
                infoTable.SpacingAfter = 14f;

                string cliente    = r["Cliente"] == DBNull.Value ? "" : r["Cliente"].ToString();
                string habitacion = r["Habitacion"] == DBNull.Value ? "" : r["Habitacion"].ToString();
                string entrada    = r["FechaEntrada"] == DBNull.Value ? "" : Convert.ToDateTime(r["FechaEntrada"]).ToString("dd/MM/yyyy");
                string salida     = r["FechaSalida"] == DBNull.Value ? "" : Convert.ToDateTime(r["FechaSalida"]).ToString("dd/MM/yyyy");
                string noches     = r["Noches"] == DBNull.Value ? "0" : r["Noches"].ToString();

                PdfPCell cellClient = new PdfPCell();
                cellClient.Border = Rectangle.NO_BORDER;
                cellClient.AddElement(new Paragraph("FACTURADO A", fontBold));
                cellClient.AddElement(new Paragraph(cliente, fontNormal));
                infoTable.AddCell(cellClient);

                PdfPCell cellStay = new PdfPCell();
                cellStay.Border = Rectangle.NO_BORDER;
                cellStay.AddElement(new Paragraph("DATOS DE ESTADIA", fontBold));
                cellStay.AddElement(new Paragraph("Habitacion: " + habitacion, fontNormal));
                cellStay.AddElement(new Paragraph("Entrada: " + entrada + "   Salida: " + salida, fontNormal));
                cellStay.AddElement(new Paragraph("Noches: " + noches, fontNormal));
                infoTable.AddCell(cellStay);
                doc.Add(infoTable);

                // ── Line items ────────────────────────────────────────────────
                doc.Add(new Paragraph("DETALLE DE CARGOS", fontSubtitulo) { SpacingAfter = 6f });

                PdfPTable itemsTable = new PdfPTable(4);
                itemsTable.WidthPercentage = 100;
                itemsTable.SetWidths(new float[] { 45f, 15f, 20f, 20f });
                itemsTable.SpacingAfter = 10f;

                // Table header row
                string[] headers = { "Descripcion", "Cant.", "Precio Unit.", "Subtotal" };
                foreach (string h in headers)
                {
                    PdfPCell hCell = new PdfPCell(new Phrase(h, fontTableHdr));
                    hCell.BackgroundColor = ColorAccent;
                    hCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    hCell.Padding = 5f;
                    itemsTable.AddCell(hCell);
                }

                // Hospedaje row
                decimal hospedaje = r["Hospedaje"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Hospedaje"]);
                AddItemRow(itemsTable, "Hospedaje (" + noches + " noches)", noches, hospedaje == 0 ? 0 : hospedaje / Math.Max(1, Convert.ToDecimal(noches)), hospedaje, fontNormal, ColorHeader, ColorWhite);

                // Services rows
                bool alternate = false;
                foreach (DataRow svc in dtServicios.Rows)
                {
                    string nombreSvc = svc["NombreServicio"] == DBNull.Value ? "" : svc["NombreServicio"].ToString();
                    string cantSvc   = svc["Cantidad"] == DBNull.Value ? "1" : svc["Cantidad"].ToString();
                    decimal precSvc  = svc["PrecioUnitario"] == DBNull.Value ? 0 : Convert.ToDecimal(svc["PrecioUnitario"]);
                    decimal subSvc   = svc["Subtotal"] == DBNull.Value ? 0 : Convert.ToDecimal(svc["Subtotal"]);
                    AddItemRow(itemsTable, nombreSvc, cantSvc, precSvc, subSvc, fontNormal, alternate ? ColorHeader : ColorWhite, ColorWhite);
                    alternate = !alternate;
                }

                doc.Add(itemsTable);

                // ── Promotions ────────────────────────────────────────────────
                if (dtPromociones.Rows.Count > 0)
                {
                    doc.Add(new Paragraph("PROMOCIONES APLICADAS", fontSubtitulo) { SpacingAfter = 6f });

                    PdfPTable promoTable = new PdfPTable(3);
                    promoTable.WidthPercentage = 100;
                    promoTable.SetWidths(new float[] { 50f, 25f, 25f });
                    promoTable.SpacingAfter = 10f;

                    string[] promoHeaders = { "Promocion", "Tipo / Valor", "Descuento Aplicado" };
                    foreach (string ph in promoHeaders)
                    {
                        PdfPCell phCell = new PdfPCell(new Phrase(ph, fontTableHdr));
                        phCell.BackgroundColor = ColorAccent;
                        phCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        phCell.Padding = 5f;
                        promoTable.AddCell(phCell);
                    }

                    foreach (DataRow promo in dtPromociones.Rows)
                    {
                        string nombrePromo = promo["NombrePromocion"] == DBNull.Value ? "" : promo["NombrePromocion"].ToString();
                        string tipoDesc    = promo["TipoDescuento"] == DBNull.Value ? "" : promo["TipoDescuento"].ToString();
                        string valDesc     = promo["ValorDescuento"] == DBNull.Value ? "" : Convert.ToDecimal(promo["ValorDescuento"]).ToString("N2");
                        decimal descAplic  = promo["DescuentoAplicado"] == DBNull.Value ? 0 : Convert.ToDecimal(promo["DescuentoAplicado"]);

                        AddPromoRow(promoTable, nombrePromo, tipoDesc + " " + valDesc, descAplic, fontNormal);
                    }

                    doc.Add(promoTable);
                }

                // ── Totals ────────────────────────────────────────────────────
                decimal subtotal  = r["Subtotal"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Subtotal"]);
                decimal descuento = r["Descuento"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Descuento"]);
                decimal impuesto  = r["Impuesto"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Impuesto"]);
                decimal total     = r["Total"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Total"]);

                PdfPTable totalsTable = new PdfPTable(2);
                totalsTable.WidthPercentage = 50;
                totalsTable.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalsTable.SetWidths(new float[] { 55f, 45f });
                totalsTable.SpacingBefore = 5f;
                totalsTable.SpacingAfter = 14f;

                AddTotalRow(totalsTable, "Subtotal:", subtotal.ToString("N2"), fontNormal, fontBold, ColorWhite);
                AddTotalRow(totalsTable, "Descuento:", "-" + descuento.ToString("N2"), fontNormal, fontNormal, ColorWhite);
                AddTotalRow(totalsTable, "ITBIS (impuesto):", impuesto.ToString("N2"), fontNormal, fontNormal, ColorWhite);
                AddTotalRow(totalsTable, "TOTAL:", total.ToString("N2"), fontTotal, fontTotal, ColorHeader);
                doc.Add(totalsTable);

                // ── Payments ──────────────────────────────────────────────────
                if (dtPagos.Rows.Count > 0)
                {
                    doc.Add(new Paragraph("PAGOS REGISTRADOS", fontSubtitulo) { SpacingAfter = 6f });

                    PdfPTable pagosTable = new PdfPTable(4);
                    pagosTable.WidthPercentage = 100;
                    pagosTable.SetWidths(new float[] { 25f, 25f, 25f, 25f });
                    pagosTable.SpacingAfter = 10f;

                    string[] pagoHeaders = { "Metodo", "Fecha", "Monto", "Referencia" };
                    foreach (string ph in pagoHeaders)
                    {
                        PdfPCell phCell = new PdfPCell(new Phrase(ph, fontTableHdr));
                        phCell.BackgroundColor = ColorAccent;
                        phCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        phCell.Padding = 5f;
                        pagosTable.AddCell(phCell);
                    }

                    foreach (DataRow pago in dtPagos.Rows)
                    {
                        string metodo  = pago.Table.Columns.Contains("MetodoPago") && pago["MetodoPago"] != DBNull.Value ? pago["MetodoPago"].ToString() : "";
                        string fecha   = pago.Table.Columns.Contains("FechaPago") && pago["FechaPago"] != DBNull.Value ? Convert.ToDateTime(pago["FechaPago"]).ToString("dd/MM/yyyy") : "";
                        string monto   = pago.Table.Columns.Contains("Monto") && pago["Monto"] != DBNull.Value ? Convert.ToDecimal(pago["Monto"]).ToString("N2") : "";
                        string refPago = pago.Table.Columns.Contains("Referencia") && pago["Referencia"] != DBNull.Value ? pago["Referencia"].ToString() : "";

                        pagosTable.AddCell(NormalCell(metodo, fontNormal, ColorWhite));
                        pagosTable.AddCell(NormalCell(fecha, fontNormal, ColorWhite));
                        pagosTable.AddCell(NormalCell(monto, fontNormal, ColorWhite, Element.ALIGN_RIGHT));
                        pagosTable.AddCell(NormalCell(refPago, fontNormal, ColorWhite));
                    }

                    doc.Add(pagosTable);
                }

                // ── Footer ────────────────────────────────────────────────────
                PdfPTable footerRule = new PdfPTable(1);
                footerRule.WidthPercentage = 100;
                footerRule.SpacingBefore = 10f;
                PdfPCell footRuleCell = new PdfPCell(new Phrase(" "));
                footRuleCell.Border = Rectangle.TOP_BORDER;
                footRuleCell.BorderColor = ColorBorder;
                footerRule.AddCell(footRuleCell);
                doc.Add(footerRule);

                doc.Add(new Paragraph("Desarrollado por: Kelvin Del Castillo, Gabriel Galasso y Camila Vasquez", fontSmall)
                    { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Generado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fontSmall)
                    { Alignment = Element.ALIGN_CENTER });

                doc.Close();
            }

            return filePath;
        }

        // ── Private helpers ──────────────────────────────────────────────────

        private static void AddItemRow(PdfPTable table, string desc, string cant, decimal precUnit, decimal sub,
            Font font, BaseColor bg, BaseColor altBg)
        {
            PdfPCell c1 = new PdfPCell(new Phrase(desc, font));
            c1.BackgroundColor = bg; c1.Padding = 4f; c1.Border = Rectangle.BOX; c1.BorderColor = ColorBorder;
            table.AddCell(c1);

            PdfPCell c2 = new PdfPCell(new Phrase(cant, font));
            c2.BackgroundColor = bg; c2.Padding = 4f; c2.Border = Rectangle.BOX; c2.BorderColor = ColorBorder;
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(c2);

            PdfPCell c3 = new PdfPCell(new Phrase(precUnit.ToString("N2"), font));
            c3.BackgroundColor = bg; c3.Padding = 4f; c3.Border = Rectangle.BOX; c3.BorderColor = ColorBorder;
            c3.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(c3);

            PdfPCell c4 = new PdfPCell(new Phrase(sub.ToString("N2"), font));
            c4.BackgroundColor = bg; c4.Padding = 4f; c4.Border = Rectangle.BOX; c4.BorderColor = ColorBorder;
            c4.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(c4);
        }

        private static void AddPromoRow(PdfPTable table, string nombre, string tipo, decimal descuento, Font font)
        {
            table.AddCell(NormalCell(nombre, font, ColorWhite));
            table.AddCell(NormalCell(tipo, font, ColorWhite, Element.ALIGN_CENTER));
            table.AddCell(NormalCell("-" + descuento.ToString("N2"), font, ColorWhite, Element.ALIGN_RIGHT));
        }

        private static void AddTotalRow(PdfPTable table, string label, string value,
            Font labelFont, Font valueFont, BaseColor bg)
        {
            PdfPCell lc = new PdfPCell(new Phrase(label, labelFont));
            lc.BackgroundColor = bg; lc.Padding = 4f; lc.Border = Rectangle.BOX; lc.BorderColor = ColorBorder;
            lc.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(lc);

            PdfPCell vc = new PdfPCell(new Phrase(value, valueFont));
            vc.BackgroundColor = bg; vc.Padding = 4f; vc.Border = Rectangle.BOX; vc.BorderColor = ColorBorder;
            vc.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(vc);
        }

        private static PdfPCell NormalCell(string text, Font font, BaseColor bg,
            int align = Element.ALIGN_LEFT)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text ?? "", font));
            cell.BackgroundColor = bg;
            cell.Padding = 4f;
            cell.Border = Rectangle.BOX;
            cell.BorderColor = ColorBorder;
            cell.HorizontalAlignment = align;
            return cell;
        }

        // ── DB queries ───────────────────────────────────────────────────────

        private static DataTable ObtenerResumenFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (System.Data.SqlClient.SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        f.IdFactura,
                        f.IdReserva,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        h.Numero AS Habitacion,
                        r.FechaEntrada,
                        r.FechaSalida,
                        DATEDIFF(DAY, r.FechaEntrada, r.FechaSalida) AS Noches,
                        ISNULL(r.Subtotal, 0) AS Hospedaje,
                        f.Subtotal,
                        f.Descuento,
                        f.Impuesto,
                        f.Total,
                        f.Estado,
                        f.FechaFactura
                    FROM dbo.Factura f
                    INNER JOIN dbo.Reserva r ON r.IdReserva = f.IdReserva
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    WHERE f.IdFactura = @IdFactura;";

                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                    using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private static DataTable ObtenerServiciosFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (System.Data.SqlClient.SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        sa.NombreServicio,
                        rs.Cantidad,
                        rs.PrecioUnitario,
                        rs.Subtotal
                    FROM dbo.Factura f
                    INNER JOIN dbo.ReservaServicio rs ON rs.IdReserva = f.IdReserva
                    INNER JOIN dbo.ServicioAdicional sa ON sa.IdServicioAdicional = rs.IdServicioAdicional
                    WHERE f.IdFactura = @IdFactura
                    ORDER BY sa.NombreServicio;";

                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                    using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private static DataTable ObtenerPromocionesFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (System.Data.SqlClient.SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        p.NombrePromocion,
                        p.TipoDescuento,
                        p.ValorDescuento,
                        rp.DescuentoAplicado
                    FROM dbo.Factura f
                    INNER JOIN dbo.ReservaPromocion rp ON rp.IdReserva = f.IdReserva
                    INNER JOIN dbo.Promocion p ON p.IdPromocion = rp.IdPromocion
                    WHERE f.IdFactura = @IdFactura
                    ORDER BY p.NombrePromocion;";

                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                    using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private static DataTable ObtenerPagosFactura(int idFactura)
        {
            return Database.ObtenerPagosPorFactura(idFactura);
        }
    }
}
