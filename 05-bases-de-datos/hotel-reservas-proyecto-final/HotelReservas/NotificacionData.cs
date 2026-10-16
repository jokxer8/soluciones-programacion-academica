using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class NotificacionData
    {
        // =========================
        // ENSURE TABLE EXISTS
        // =========================
        public static void EnsureTableExists()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Notificacion' AND schema_id = SCHEMA_ID('dbo'))
                    BEGIN
                        CREATE TABLE dbo.Notificacion (
                            IdNotificacion  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                            IdUsuarioDestino INT NOT NULL,
                            Tipo            VARCHAR(30) NOT NULL,
                            Titulo          VARCHAR(100) NOT NULL,
                            Mensaje         VARCHAR(500) NOT NULL,
                            Leida           BIT NOT NULL CONSTRAINT DF_Notificacion_Leida DEFAULT (0),
                            FechaCreacion   DATETIME NOT NULL CONSTRAINT DF_Notificacion_Fecha DEFAULT (GETDATE()),
                            FechaLectura    DATETIME NULL,
                            CONSTRAINT FK_Notificacion_Usuario FOREIGN KEY (IdUsuarioDestino)
                                REFERENCES dbo.Usuarios(IdUsuario)
                        );
                        CREATE INDEX IX_Notificacion_Usuario_Leida
                            ON dbo.Notificacion (IdUsuarioDestino, Leida);
                    END";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // COUNT UNREAD
        // =========================
        public static int ContarNoLeidas(int idUsuario)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT COUNT(*)
                    FROM dbo.Notificacion
                    WHERE IdUsuarioDestino = @IdUsuario
                      AND Leida = 0;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    object result = cmd.ExecuteScalar();
                    return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
                }
            }
        }

        // =========================
        // LIST FOR USER (unread first, then recent)
        // =========================
        public static DataTable ObtenerPorUsuario(int idUsuario, int pagina = 0, int tamPagina = 50)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdNotificacion,
                        Tipo,
                        Titulo,
                        Mensaje,
                        Leida,
                        FechaCreacion,
                        FechaLectura
                    FROM dbo.Notificacion
                    WHERE IdUsuarioDestino = @IdUsuario
                    ORDER BY Leida ASC, FechaCreacion DESC
                    OFFSET @Offset ROWS FETCH NEXT @TamPagina ROWS ONLY;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@Offset", pagina * tamPagina);
                    cmd.Parameters.AddWithValue("@TamPagina", tamPagina);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // =========================
        // MARK AS READ
        // =========================
        public static void MarcarLeida(int idNotificacion)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Notificacion
                    SET Leida = 1, FechaLectura = GETDATE()
                    WHERE IdNotificacion = @IdNotificacion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdNotificacion", idNotificacion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // MARK ALL READ FOR USER
        // =========================
        public static void MarcarTodasLeidas(int idUsuario)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Notificacion
                    SET Leida = 1, FechaLectura = GETDATE()
                    WHERE IdUsuarioDestino = @IdUsuario AND Leida = 0;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // INSERT NOTIFICATION
        // =========================
        public static void Insertar(int idUsuarioDestino, string tipo, string titulo, string mensaje)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.Notificacion (IdUsuarioDestino, Tipo, Titulo, Mensaje)
                    VALUES (@IdUsuarioDestino, @Tipo, @Titulo, @Mensaje);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuarioDestino", idUsuarioDestino);
                    cmd.Parameters.AddWithValue("@Tipo", tipo.Length > 30 ? tipo.Substring(0, 30) : tipo);
                    cmd.Parameters.AddWithValue("@Titulo", titulo.Length > 100 ? titulo.Substring(0, 100) : titulo);
                    cmd.Parameters.AddWithValue("@Mensaje", mensaje.Length > 500 ? mensaje.Substring(0, 500) : mensaje);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
