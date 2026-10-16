using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class ReportesData
    {
        // Un alcance parametrizado por consulta evita estado global y mantiene
        // el mismo filtro en numeradores, denominadores y subconsultas.
        private static string AplicarSucursal(string sql)
        {
            foreach (string table in new[] { "Habitacion", "Reserva", "Factura", "Pago", "Mantenimiento" })
                sql = System.Text.RegularExpressions.Regex.Replace(sql, @"\bdbo\." + table + @"\b", "Alcance" + table);
            // Un empleado puede gestionar reservas en otra sucursal: no eliminar
            // esas reservas al resolver el nombre del empleado en un JOIN.
            sql = System.Text.RegularExpressions.Regex.Replace(sql, @"\bFROM dbo\.Empleado\b", "FROM AlcanceEmpleado");
            string scopes = @"
                AlcanceHabitacion AS (SELECT * FROM dbo.Habitacion WHERE @IdSucursal IS NULL OR IdSucursal=@IdSucursal),
                AlcanceReserva AS (SELECT r.* FROM dbo.Reserva r JOIN AlcanceHabitacion h ON h.IdHabitacion=r.IdHabitacion),
                AlcanceFactura AS (SELECT f.* FROM dbo.Factura f JOIN AlcanceReserva r ON r.IdReserva=f.IdReserva),
                AlcancePago AS (SELECT p.* FROM dbo.Pago p JOIN AlcanceFactura f ON f.IdFactura=p.IdFactura),
                AlcanceMantenimiento AS (SELECT m.* FROM dbo.Mantenimiento m JOIN AlcanceHabitacion h ON h.IdHabitacion=m.IdHabitacion),
                AlcanceEmpleado AS (SELECT * FROM dbo.Empleado WHERE @IdSucursal IS NULL OR IdSucursal=@IdSucursal)";
            sql = sql.TrimStart();
            return sql.StartsWith("WITH ", StringComparison.OrdinalIgnoreCase)
                ? "WITH " + scopes + ", " + sql.Substring(5) : "WITH " + scopes + " " + sql;
        }

        public static DataTable ObtenerAniosDisponibles(int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT DISTINCT Anio
                    FROM
                    (
                        SELECT YEAR(FechaReserva) AS Anio FROM dbo.Reserva
                        UNION
                        SELECT YEAR(FechaFactura) AS Anio FROM dbo.Factura
                        UNION
                        SELECT YEAR(FechaPago) AS Anio FROM dbo.Pago
                        UNION
                        SELECT YEAR(FechaInicio) AS Anio FROM dbo.Mantenimiento
                    ) t
                    WHERE Anio IS NOT NULL
                    ORDER BY Anio DESC;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(DateTime.Today.Year);
            }

            return dt;
        }

        // =========================
        // OPERATIVOS
        // =========================
        public static DataTable ObtenerResumenOperativo(DateTime desde, DateTime hasta, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        COUNT(*) AS TotalReservas,
                        SUM(CASE WHEN er.NombreEstado = 'PENDIENTE' THEN 1 ELSE 0 END) AS Pendientes,
                        SUM(CASE WHEN er.NombreEstado = 'CONFIRMADA' THEN 1 ELSE 0 END) AS Confirmadas,
                        SUM(CASE WHEN er.NombreEstado = 'CANCELADA' THEN 1 ELSE 0 END) AS Canceladas,
                        SUM(CASE WHEN er.NombreEstado = 'FINALIZADA' THEN 1 ELSE 0 END) AS Finalizadas
                    FROM dbo.Reserva r
                    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                    WHERE r.FechaReserva >= @Desde
                      AND r.FechaReserva < DATEADD(DAY, 1, @Hasta);";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerReservasMensuales(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH meses AS
                    (
                        SELECT 1 AS MesNumero, 'Ene' AS MesNombre UNION ALL
                        SELECT 2, 'Feb' UNION ALL
                        SELECT 3, 'Mar' UNION ALL
                        SELECT 4, 'Abr' UNION ALL
                        SELECT 5, 'May' UNION ALL
                        SELECT 6, 'Jun' UNION ALL
                        SELECT 7, 'Jul' UNION ALL
                        SELECT 8, 'Ago' UNION ALL
                        SELECT 9, 'Sep' UNION ALL
                        SELECT 10, 'Oct' UNION ALL
                        SELECT 11, 'Nov' UNION ALL
                        SELECT 12, 'Dic'
                    )
                    SELECT
                        m.MesNumero,
                        m.MesNombre,
                        ISNULL(r.Cantidad, 0) AS Cantidad
                    FROM meses m
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(FechaReserva) AS MesNumero,
                            COUNT(*) AS Cantidad
                        FROM dbo.Reserva
                        WHERE YEAR(FechaReserva) = @Anio
                        GROUP BY MONTH(FechaReserva)
                    ) r ON r.MesNumero = m.MesNumero
                    ORDER BY m.MesNumero;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerDistribucionEstadosReserva(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH base AS
                    (
                        SELECT
                            er.NombreEstado AS Estado,
                            COUNT(*) AS Cantidad
                        FROM dbo.Reserva r
                        INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                        WHERE YEAR(r.FechaReserva) = @Anio
                        GROUP BY er.NombreEstado
                    )
                    SELECT
                        Estado,
                        Cantidad,
                        CAST(Cantidad * 100.0 / NULLIF((SELECT SUM(Cantidad) FROM base), 0) AS DECIMAL(10,2)) AS Porcentaje
                    FROM base
                    ORDER BY Cantidad DESC;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerDetalleOperativo(DateTime desde, DateTime hasta, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        r.IdReserva,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        h.Numero AS Habitacion,
                        e.Nombre + ' ' + e.Apellido AS Empleado,
                        er.NombreEstado AS EstadoReserva,
                        r.FechaReserva,
                        r.FechaEntrada,
                        r.FechaSalida,
                        r.CantidadHuespedes,
                        CAST(ISNULL(r.Subtotal, 0) AS DECIMAL(10,2)) AS Hospedaje,
                        CAST(ISNULL(srv.TotalServicios, 0) AS DECIMAL(10,2)) AS Servicios,
                        CAST(ISNULL(pro.TotalPromociones, 0) AS DECIMAL(10,2)) AS Promociones,
                        CAST(ISNULL(r.Subtotal, 0) + ISNULL(srv.TotalServicios, 0) - ISNULL(r.Descuento, 0) - ISNULL(pro.TotalPromociones, 0) AS DECIMAL(10,2)) AS TotalEstimado
                    FROM dbo.Reserva r
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    INNER JOIN dbo.Empleado e ON e.IdEmpleado = r.IdEmpleado
                    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                    OUTER APPLY
                    (
                        SELECT SUM(Subtotal) AS TotalServicios
                        FROM dbo.ReservaServicio rs
                        WHERE rs.IdReserva = r.IdReserva
                    ) srv
                    OUTER APPLY
                    (
                        SELECT SUM(DescuentoAplicado) AS TotalPromociones
                        FROM dbo.ReservaPromocion rp
                        WHERE rp.IdReserva = r.IdReserva
                    ) pro
                    WHERE r.FechaReserva >= @Desde
                      AND r.FechaReserva < DATEADD(DAY, 1, @Hasta)
                    ORDER BY r.IdReserva DESC;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // =========================
        // HABITACIONES
        // =========================
        public static DataTable ObtenerResumenHabitacionesActual(int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        COUNT(*) AS TotalHabitaciones,
                        SUM(CASE WHEN eh.NombreEstado = 'DISPONIBLE' THEN 1 ELSE 0 END) AS Disponibles,
                        SUM(CASE WHEN eh.NombreEstado = 'OCUPADA' THEN 1 ELSE 0 END) AS Ocupadas,
                        SUM(CASE WHEN eh.NombreEstado = 'MANTENIMIENTO' THEN 1 ELSE 0 END) AS Mantenimiento,
                        SUM(CASE WHEN eh.NombreEstado = 'LIMPIEZA' THEN 1 ELSE 0 END) AS Limpieza
                    FROM dbo.Habitacion h
                    INNER JOIN dbo.EstadoHabitacion eh ON eh.IdEstadoHabitacion = h.IdEstadoHabitacion;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerDistribucionEstadoHabitaciones(int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH base AS
                    (
                        SELECT
                            eh.NombreEstado AS Estado,
                            COUNT(*) AS Cantidad
                        FROM dbo.Habitacion h
                        INNER JOIN dbo.EstadoHabitacion eh ON eh.IdEstadoHabitacion = h.IdEstadoHabitacion
                        GROUP BY eh.NombreEstado
                    )
                    SELECT
                        Estado,
                        Cantidad,
                        CAST(Cantidad * 100.0 / NULLIF((SELECT SUM(Cantidad) FROM base), 0) AS DECIMAL(10,2)) AS Porcentaje
                    FROM base
                    ORDER BY Cantidad DESC;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerTopHabitacionesReservadas(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT TOP 10
                        h.Numero,
                        th.NombreTipo AS TipoHabitacion,
                        s.NombreSucursal,
                        COUNT(*) AS TotalReservas
                    FROM dbo.Reserva r
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    INNER JOIN dbo.TipoHabitacion th ON th.IdTipoHabitacion = h.IdTipoHabitacion
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    WHERE YEAR(r.FechaReserva) = @Anio
                    GROUP BY h.Numero, th.NombreTipo, s.NombreSucursal
                    ORDER BY TotalReservas DESC, h.Numero;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerOcupacionPorTipoHabitacion(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH base AS
                    (
                        SELECT
                            th.NombreTipo AS TipoHabitacion,
                            COUNT(*) AS Cantidad
                        FROM dbo.Reserva r
                        INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                        INNER JOIN dbo.TipoHabitacion th ON th.IdTipoHabitacion = h.IdTipoHabitacion
                        WHERE YEAR(r.FechaReserva) = @Anio
                        GROUP BY th.NombreTipo
                    )
                    SELECT
                        TipoHabitacion,
                        Cantidad,
                        CAST(Cantidad * 100.0 / NULLIF((SELECT SUM(Cantidad) FROM base), 0) AS DECIMAL(10,2)) AS Porcentaje
                    FROM base
                    ORDER BY Cantidad DESC;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerMantenimientosPeriodo(DateTime desde, DateTime hasta, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        m.IdMantenimiento,
                        h.Numero AS Habitacion,
                        e.Nombre + ' ' + e.Apellido AS Empleado,
                        m.FechaInicio,
                        m.FechaFin,
                        m.Motivo,
                        m.Costo,
                        m.Estado
                    FROM dbo.Mantenimiento m
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = m.IdHabitacion
                    LEFT JOIN dbo.Empleado e ON e.IdEmpleado = m.IdEmpleado
                    WHERE m.FechaInicio >= @Desde
                      AND m.FechaInicio < DATEADD(DAY, 1, @Hasta)
                    ORDER BY m.IdMantenimiento DESC;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // =========================
        // GERENCIALES
        // =========================
        public static DataTable ObtenerResumenGerencial(DateTime desde, DateTime hasta, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        ISNULL(fac.TotalFacturado, 0) AS TotalFacturado,
                        ISNULL(pag.TotalCobrado, 0) AS TotalCobrado,
                        ISNULL(fac.CantidadFacturas, 0) AS CantidadFacturas,
                        ISNULL(fac.TicketPromedio, 0) AS TicketPromedio,
                        ISNULL(fac.ClientesUnicos, 0) AS ClientesUnicos,
                        ISNULL(fac.ReservasFacturadas, 0) AS ReservasFacturadas
                    FROM
                    (
                        SELECT
                            SUM(CASE WHEN f.Estado <> 'ANULADA' THEN f.Total ELSE 0 END) AS TotalFacturado,
                            COUNT(CASE WHEN f.Estado <> 'ANULADA' THEN 1 END) AS CantidadFacturas,
                            AVG(CASE WHEN f.Estado <> 'ANULADA' THEN CAST(f.Total AS DECIMAL(18,2)) END) AS TicketPromedio,
                            COUNT(DISTINCT CASE WHEN f.Estado <> 'ANULADA' THEN r.IdCliente END) AS ClientesUnicos,
                            COUNT(DISTINCT CASE WHEN f.Estado <> 'ANULADA' THEN f.IdReserva END) AS ReservasFacturadas
                        FROM dbo.Factura f
                        INNER JOIN dbo.Reserva r ON r.IdReserva = f.IdReserva
                        WHERE f.FechaFactura >= @Desde
                          AND f.FechaFactura < DATEADD(DAY, 1, @Hasta)
                    ) fac
                    CROSS JOIN
                    (
                        SELECT
                            SUM(Monto) AS TotalCobrado
                        FROM dbo.Pago
                        WHERE FechaPago >= @Desde
                          AND FechaPago < DATEADD(DAY, 1, @Hasta)
                    ) pag;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerFacturacionMensual(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH meses AS
                    (
                        SELECT 1 AS MesNumero, 'Ene' AS MesNombre UNION ALL
                        SELECT 2, 'Feb' UNION ALL
                        SELECT 3, 'Mar' UNION ALL
                        SELECT 4, 'Abr' UNION ALL
                        SELECT 5, 'May' UNION ALL
                        SELECT 6, 'Jun' UNION ALL
                        SELECT 7, 'Jul' UNION ALL
                        SELECT 8, 'Ago' UNION ALL
                        SELECT 9, 'Sep' UNION ALL
                        SELECT 10, 'Oct' UNION ALL
                        SELECT 11, 'Nov' UNION ALL
                        SELECT 12, 'Dic'
                    )
                    SELECT
                        m.MesNumero,
                        m.MesNombre,
                        ISNULL(f.Total, 0) AS Total
                    FROM meses m
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(FechaFactura) AS MesNumero,
                            SUM(CASE WHEN Estado <> 'ANULADA' THEN Total ELSE 0 END) AS Total
                        FROM dbo.Factura
                        WHERE YEAR(FechaFactura) = @Anio
                        GROUP BY MONTH(FechaFactura)
                    ) f ON f.MesNumero = m.MesNumero
                    ORDER BY m.MesNumero;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerCobrosMensuales(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH meses AS
                    (
                        SELECT 1 AS MesNumero, 'Ene' AS MesNombre UNION ALL
                        SELECT 2, 'Feb' UNION ALL
                        SELECT 3, 'Mar' UNION ALL
                        SELECT 4, 'Abr' UNION ALL
                        SELECT 5, 'May' UNION ALL
                        SELECT 6, 'Jun' UNION ALL
                        SELECT 7, 'Jul' UNION ALL
                        SELECT 8, 'Ago' UNION ALL
                        SELECT 9, 'Sep' UNION ALL
                        SELECT 10, 'Oct' UNION ALL
                        SELECT 11, 'Nov' UNION ALL
                        SELECT 12, 'Dic'
                    )
                    SELECT
                        m.MesNumero,
                        m.MesNombre,
                        ISNULL(p.Total, 0) AS Total
                    FROM meses m
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(FechaPago) AS MesNumero,
                            SUM(Monto) AS Total
                        FROM dbo.Pago
                        WHERE YEAR(FechaPago) = @Anio
                        GROUP BY MONTH(FechaPago)
                    ) p ON p.MesNumero = m.MesNumero
                    ORDER BY m.MesNumero;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerRendimientoEmpleadosGerencial(DateTime desde, DateTime hasta, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH total_global AS
                    (
                        SELECT
                            ISNULL(SUM(CASE WHEN f.Estado <> 'ANULADA' THEN f.Total ELSE 0 END), 0) AS TotalGeneral
                        FROM dbo.Factura f
                        WHERE f.FechaFactura >= @Desde
                          AND f.FechaFactura < DATEADD(DAY, 1, @Hasta)
                    )
                    SELECT
                        e.IdEmpleado,
                        e.Nombre + ' ' + e.Apellido AS Empleado,
                        s.NombreSucursal,
                        COUNT(r.IdReserva) AS TotalReservas,
                        ISNULL(SUM(CASE WHEN f.Estado <> 'ANULADA' THEN f.Total ELSE 0 END), 0) AS TotalFacturado,
                        CAST(
                            ISNULL(SUM(CASE WHEN f.Estado <> 'ANULADA' THEN f.Total ELSE 0 END), 0) * 100.0
                            / NULLIF((SELECT TotalGeneral FROM total_global), 0)
                            AS DECIMAL(10,2)
                        ) AS PorcentajeParticipacion
                    FROM dbo.Empleado e
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = e.IdSucursal
                    LEFT JOIN dbo.Reserva r
                        ON r.IdEmpleado = e.IdEmpleado
                        AND r.FechaReserva >= @Desde
                        AND r.FechaReserva < DATEADD(DAY, 1, @Hasta)
                    LEFT JOIN dbo.Factura f ON f.IdReserva = r.IdReserva
                    GROUP BY e.IdEmpleado, e.Nombre, e.Apellido, s.NombreSucursal
                    ORDER BY TotalFacturado DESC, TotalReservas DESC;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerTopClientesGerencial(DateTime desde, DateTime hasta, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT TOP 10
                        c.IdCliente,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        COUNT(r.IdReserva) AS TotalReservas,
                        ISNULL(SUM(CASE WHEN f.Estado <> 'ANULADA' THEN f.Total ELSE 0 END), 0) AS TotalFacturado
                    FROM dbo.Cliente c
                    INNER JOIN dbo.Reserva r ON r.IdCliente = c.IdCliente
                    LEFT JOIN dbo.Factura f ON f.IdReserva = r.IdReserva
                    WHERE r.FechaReserva >= @Desde
                      AND r.FechaReserva < DATEADD(DAY, 1, @Hasta)
                    GROUP BY c.IdCliente, c.Nombres, c.Apellidos
                    ORDER BY TotalFacturado DESC, TotalReservas DESC;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // =========================
        // GLOBAL ANUAL
        // =========================
        public static DataTable ObtenerResumenGlobalAnual(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        ISNULL(r.TotalReservas, 0) AS TotalReservas,
                        ISNULL(f.TotalFacturado, 0) AS TotalFacturado,
                        ISNULL(p.TotalCobrado, 0) AS TotalCobrado,
                        ISNULL(sp.TotalServicios, 0) AS TotalServicios,
                        ISNULL(sp.TotalPromociones, 0) AS TotalPromociones,
                        ISNULL(fp.FacturasPagadas, 0) AS FacturasPagadas,
                        CAST(
                            ISNULL(p.TotalCobrado, 0) * 100.0 /
                            NULLIF(ISNULL(f.TotalFacturado, 0), 0)
                            AS DECIMAL(10,2)
                        ) AS PorcentajeCobro
                    FROM
                    (
                        SELECT COUNT(*) AS TotalReservas
                        FROM dbo.Reserva
                        WHERE YEAR(FechaReserva) = @Anio
                    ) r
                    CROSS JOIN
                    (
                        SELECT SUM(CASE WHEN Estado <> 'ANULADA' THEN Total ELSE 0 END) AS TotalFacturado
                        FROM dbo.Factura
                        WHERE YEAR(FechaFactura) = @Anio
                    ) f
                    CROSS JOIN
                    (
                        SELECT SUM(Monto) AS TotalCobrado
                        FROM dbo.Pago
                        WHERE YEAR(FechaPago) = @Anio
                    ) p
                    CROSS JOIN
                    (
                        SELECT
                            SUM(ISNULL(srv.TotalServicios, 0)) AS TotalServicios,
                            SUM(ISNULL(pro.TotalPromociones, 0)) AS TotalPromociones
                        FROM dbo.Factura fac
                        OUTER APPLY
                        (
                            SELECT SUM(Subtotal) AS TotalServicios
                            FROM dbo.ReservaServicio rs
                            WHERE rs.IdReserva = fac.IdReserva
                        ) srv
                        OUTER APPLY
                        (
                            SELECT SUM(DescuentoAplicado) AS TotalPromociones
                            FROM dbo.ReservaPromocion rp
                            WHERE rp.IdReserva = fac.IdReserva
                        ) pro
                        WHERE YEAR(fac.FechaFactura) = @Anio
                          AND fac.Estado <> 'ANULADA'
                    ) sp
                    CROSS JOIN
                    (
                        SELECT COUNT(*) AS FacturasPagadas
                        FROM dbo.Factura
                        WHERE YEAR(FechaFactura) = @Anio
                          AND Estado = 'PAGADA'
                    ) fp;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerComparativoIngresosMensual(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH meses AS
                    (
                        SELECT 1 AS MesNumero, 'Ene' AS MesNombre UNION ALL
                        SELECT 2, 'Feb' UNION ALL
                        SELECT 3, 'Mar' UNION ALL
                        SELECT 4, 'Abr' UNION ALL
                        SELECT 5, 'May' UNION ALL
                        SELECT 6, 'Jun' UNION ALL
                        SELECT 7, 'Jul' UNION ALL
                        SELECT 8, 'Ago' UNION ALL
                        SELECT 9, 'Sep' UNION ALL
                        SELECT 10, 'Oct' UNION ALL
                        SELECT 11, 'Nov' UNION ALL
                        SELECT 12, 'Dic'
                    )
                    SELECT
                        m.MesNumero,
                        m.MesNombre,
                        ISNULL(f.Facturado, 0) AS Facturado,
                        ISNULL(c.Cobrado, 0) AS Cobrado
                    FROM meses m
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(FechaFactura) AS MesNumero,
                            SUM(CASE WHEN Estado <> 'ANULADA' THEN Total ELSE 0 END) AS Facturado
                        FROM dbo.Factura
                        WHERE YEAR(FechaFactura) = @Anio
                        GROUP BY MONTH(FechaFactura)
                    ) f ON f.MesNumero = m.MesNumero
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(FechaPago) AS MesNumero,
                            SUM(Monto) AS Cobrado
                        FROM dbo.Pago
                        WHERE YEAR(FechaPago) = @Anio
                        GROUP BY MONTH(FechaPago)
                    ) c ON c.MesNumero = m.MesNumero
                    ORDER BY m.MesNumero;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerServiciosPromocionesMensual(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH meses AS
                    (
                        SELECT 1 AS MesNumero, 'Ene' AS MesNombre UNION ALL
                        SELECT 2, 'Feb' UNION ALL
                        SELECT 3, 'Mar' UNION ALL
                        SELECT 4, 'Abr' UNION ALL
                        SELECT 5, 'May' UNION ALL
                        SELECT 6, 'Jun' UNION ALL
                        SELECT 7, 'Jul' UNION ALL
                        SELECT 8, 'Ago' UNION ALL
                        SELECT 9, 'Sep' UNION ALL
                        SELECT 10, 'Oct' UNION ALL
                        SELECT 11, 'Nov' UNION ALL
                        SELECT 12, 'Dic'
                    )
                    SELECT
                        m.MesNumero,
                        m.MesNombre,
                        ISNULL(x.Servicios, 0) AS Servicios,
                        ISNULL(x.Promociones, 0) AS Promociones
                    FROM meses m
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(f.FechaFactura) AS MesNumero,
                            SUM(ISNULL(srv.TotalServicios, 0)) AS Servicios,
                            SUM(ISNULL(pro.TotalPromociones, 0)) AS Promociones
                        FROM dbo.Factura f
                        OUTER APPLY
                        (
                            SELECT SUM(Subtotal) AS TotalServicios
                            FROM dbo.ReservaServicio rs
                            WHERE rs.IdReserva = f.IdReserva
                        ) srv
                        OUTER APPLY
                        (
                            SELECT SUM(DescuentoAplicado) AS TotalPromociones
                            FROM dbo.ReservaPromocion rp
                            WHERE rp.IdReserva = f.IdReserva
                        ) pro
                        WHERE YEAR(f.FechaFactura) = @Anio
                          AND f.Estado <> 'ANULADA'
                        GROUP BY MONTH(f.FechaFactura)
                    ) x ON x.MesNumero = m.MesNumero
                    ORDER BY m.MesNumero;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerResumenMensualGlobal(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    WITH meses AS
                    (
                        SELECT 1 AS MesNumero, 'Ene' AS MesNombre UNION ALL
                        SELECT 2, 'Feb' UNION ALL
                        SELECT 3, 'Mar' UNION ALL
                        SELECT 4, 'Abr' UNION ALL
                        SELECT 5, 'May' UNION ALL
                        SELECT 6, 'Jun' UNION ALL
                        SELECT 7, 'Jul' UNION ALL
                        SELECT 8, 'Ago' UNION ALL
                        SELECT 9, 'Sep' UNION ALL
                        SELECT 10, 'Oct' UNION ALL
                        SELECT 11, 'Nov' UNION ALL
                        SELECT 12, 'Dic'
                    )
                    SELECT
                        m.MesNumero,
                        m.MesNombre,
                        ISNULL(r.Reservas, 0) AS Reservas,
                        ISNULL(f.Facturado, 0) AS Facturado,
                        ISNULL(c.Cobrado, 0) AS Cobrado,
                        ISNULL(sp.Servicios, 0) AS Servicios,
                        ISNULL(sp.Promociones, 0) AS Promociones,
                        CAST(
                            ISNULL(c.Cobrado, 0) * 100.0 /
                            NULLIF(ISNULL(f.Facturado, 0), 0)
                            AS DECIMAL(10,2)
                        ) AS PorcentajeCobro
                    FROM meses m
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(FechaReserva) AS MesNumero,
                            COUNT(*) AS Reservas
                        FROM dbo.Reserva
                        WHERE YEAR(FechaReserva) = @Anio
                        GROUP BY MONTH(FechaReserva)
                    ) r ON r.MesNumero = m.MesNumero
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(FechaFactura) AS MesNumero,
                            SUM(CASE WHEN Estado <> 'ANULADA' THEN Total ELSE 0 END) AS Facturado
                        FROM dbo.Factura
                        WHERE YEAR(FechaFactura) = @Anio
                        GROUP BY MONTH(FechaFactura)
                    ) f ON f.MesNumero = m.MesNumero
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(FechaPago) AS MesNumero,
                            SUM(Monto) AS Cobrado
                        FROM dbo.Pago
                        WHERE YEAR(FechaPago) = @Anio
                        GROUP BY MONTH(FechaPago)
                    ) c ON c.MesNumero = m.MesNumero
                    LEFT JOIN
                    (
                        SELECT
                            MONTH(fac.FechaFactura) AS MesNumero,
                            SUM(ISNULL(srv.TotalServicios, 0)) AS Servicios,
                            SUM(ISNULL(pro.TotalPromociones, 0)) AS Promociones
                        FROM dbo.Factura fac
                        OUTER APPLY
                        (
                            SELECT SUM(Subtotal) AS TotalServicios
                            FROM dbo.ReservaServicio rs
                            WHERE rs.IdReserva = fac.IdReserva
                        ) srv
                        OUTER APPLY
                        (
                            SELECT SUM(DescuentoAplicado) AS TotalPromociones
                            FROM dbo.ReservaPromocion rp
                            WHERE rp.IdReserva = fac.IdReserva
                        ) pro
                        WHERE YEAR(fac.FechaFactura) = @Anio
                          AND fac.Estado <> 'ANULADA'
                        GROUP BY MONTH(fac.FechaFactura)
                    ) sp ON sp.MesNumero = m.MesNumero
                    ORDER BY m.MesNumero;";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // =========================
        // MÉTRICAS HOTELERAS
        // =========================

        public static DataTable ObtenerRevPAR(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        s.NombreSucursal,
                        MONTH(f.FechaFactura) AS Mes,
                        SUM(CASE WHEN f.Estado <> 'ANULADA' THEN f.Total ELSE 0 END)
                            / NULLIF((SELECT COUNT(*) FROM dbo.Habitacion h2 WHERE h2.IdSucursal = s.IdSucursal), 0) AS RevPAR
                    FROM dbo.Factura f
                    INNER JOIN dbo.Reserva r ON r.IdReserva = f.IdReserva
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    WHERE YEAR(f.FechaFactura) = @Anio
                    GROUP BY s.NombreSucursal, s.IdSucursal, MONTH(f.FechaFactura)
                    ORDER BY s.NombreSucursal, MONTH(f.FechaFactura);";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerADR(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        MONTH(r.FechaEntrada) AS Mes,
                        SUM(r.Subtotal - ISNULL(r.Descuento, 0)) / NULLIF(COUNT(*), 0) AS ADR
                    FROM dbo.Reserva r
                    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                    WHERE er.NombreEstado IN ('CONFIRMADA', 'FINALIZADA')
                      AND YEAR(r.FechaEntrada) = @Anio
                    GROUP BY MONTH(r.FechaEntrada)
                    ORDER BY MONTH(r.FechaEntrada);";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerOccupancyRate(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        MONTH(r.FechaEntrada) AS Mes,
                        CAST(
                            SUM(DATEDIFF(DAY, r.FechaEntrada, r.FechaSalida)) * 100.0
                            / NULLIF(
                                (SELECT COUNT(*) FROM dbo.Habitacion)
                                * DAY(EOMONTH(DATEFROMPARTS(@Anio, MONTH(r.FechaEntrada), 1))),
                                0
                            ) AS DECIMAL(5,2)
                        ) AS OccupancyRate
                    FROM dbo.Reserva r
                    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                    WHERE er.NombreEstado IN ('CONFIRMADA', 'FINALIZADA')
                      AND YEAR(r.FechaEntrada) = @Anio
                    GROUP BY MONTH(r.FechaEntrada)
                    ORDER BY MONTH(r.FechaEntrada);";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable ObtenerGOPPAR(int anio, int? idSucursal = null)
        {
            SesionUsuario.ExigirAcceso("Reportes");
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);

                string sql = @"
                    SELECT
                        s.NombreSucursal,
                        MONTH(f.FechaFactura) AS Mes,
                        (SUM(CASE WHEN f.Estado <> 'ANULADA' THEN f.Total ELSE 0 END)
                         - ISNULL((
                             SELECT SUM(m.Costo)
                             FROM dbo.Mantenimiento m
                             INNER JOIN dbo.Habitacion hm ON hm.IdHabitacion = m.IdHabitacion
                             WHERE hm.IdSucursal = s.IdSucursal
                               AND YEAR(m.FechaInicio) = @Anio
                               AND MONTH(m.FechaInicio) = MONTH(f.FechaFactura)
                         ), 0))
                        / NULLIF((SELECT COUNT(*) FROM dbo.Habitacion WHERE IdSucursal = s.IdSucursal), 0) AS GOPPAR
                    FROM dbo.Factura f
                    INNER JOIN dbo.Reserva r ON r.IdReserva = f.IdReserva
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    WHERE YEAR(f.FechaFactura) = @Anio
                    GROUP BY s.IdSucursal, s.NombreSucursal, MONTH(f.FechaFactura)
                    ORDER BY s.NombreSucursal, MONTH(f.FechaFactura);";

                using (SqlCommand cmd = new SqlCommand(AplicarSucursal(sql), conn))
                {
                    cmd.Parameters.AddWithValue("@Anio", anio);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = (object)idSucursal ?? DBNull.Value;
                    da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
