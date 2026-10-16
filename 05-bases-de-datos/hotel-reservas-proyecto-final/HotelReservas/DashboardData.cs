using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class DashboardData
    {
        public static int ObtenerReservasHoy()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT COUNT(*)
                    FROM dbo.Reserva r
                    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                    WHERE CAST(r.FechaEntrada AS DATE) = CAST(GETDATE() AS DATE)
                      AND er.NombreEstado IN ('CONFIRMADA', 'PENDIENTE');";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
                }
            }
        }

        public static int ObtenerHabitacionesOcupadas()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT COUNT(*)
                    FROM dbo.Habitacion h
                    INNER JOIN dbo.EstadoHabitacion eh ON eh.IdEstadoHabitacion = h.IdEstadoHabitacion
                    WHERE eh.NombreEstado = 'OCUPADA';";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
                }
            }
        }

        public static int ObtenerTotalHabitaciones()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"SELECT COUNT(*) FROM dbo.Habitacion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
                }
            }
        }

        public static decimal ObtenerIngresosMes()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT ISNULL(SUM(Total), 0)
                    FROM dbo.Factura
                    WHERE MONTH(FechaFactura) = MONTH(GETDATE())
                      AND YEAR(FechaFactura) = YEAR(GETDATE())
                      AND Estado <> 'ANULADA';";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result == null || result == DBNull.Value ? 0m : Convert.ToDecimal(result);
                }
            }
        }

        public static int ObtenerMantenimientosPendientes()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT COUNT(*)
                    FROM dbo.Mantenimiento
                    WHERE Estado NOT IN ('FINALIZADO', 'COMPLETADO');";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
                }
            }
        }

        public static DataTable ObtenerOcupacionUltimos30Dias()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    WITH Dias AS
                    (
                        SELECT CAST(DATEADD(DAY, -n, CAST(GETDATE() AS DATE)) AS DATE) AS Fecha
                        FROM
                        (
                            SELECT TOP 30 ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS n
                            FROM sys.objects
                        ) t
                    )
                    SELECT
                        d.Fecha,
                        ISNULL(
                            (
                                SELECT COUNT(DISTINCT r.IdHabitacion)
                                FROM dbo.Reserva r
                                INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                                WHERE er.NombreEstado IN ('CONFIRMADA', 'FINALIZADA')
                                  AND r.FechaEntrada <= d.Fecha
                                  AND r.FechaSalida > d.Fecha
                            ), 0
                        ) AS HabitacionesOcupadas
                    FROM Dias d
                    ORDER BY d.Fecha ASC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}
