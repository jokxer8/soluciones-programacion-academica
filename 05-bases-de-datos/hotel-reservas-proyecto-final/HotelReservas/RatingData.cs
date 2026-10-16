using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class RatingData
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
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Rating' AND schema_id = SCHEMA_ID('dbo'))
                    BEGIN
                        CREATE TABLE dbo.Rating (
                            IdRating    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                            IdReserva   INT NOT NULL,
                            Puntuacion  TINYINT NOT NULL,
                            Comentario  VARCHAR(500) NULL,
                            FechaRating DATETIME NOT NULL CONSTRAINT DF_Rating_Fecha DEFAULT (GETDATE()),
                            CONSTRAINT CK_Rating_Puntuacion CHECK (Puntuacion BETWEEN 1 AND 5),
                            CONSTRAINT UQ_Rating_Reserva UNIQUE (IdReserva),
                            CONSTRAINT FK_Rating_Reserva FOREIGN KEY (IdReserva)
                                REFERENCES dbo.Reserva(IdReserva)
                        );
                    END";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // CHECK IF RATING EXISTS
        // =========================
        public static bool ExisteRating(int idReserva)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM dbo.Rating WHERE IdReserva = @IdReserva;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value && Convert.ToInt32(result) > 0;
                }
            }
        }

        // =========================
        // INSERT RATING
        // =========================
        public static void Insertar(int idReserva, byte puntuacion, string comentario)
        {
            if (puntuacion < 1 || puntuacion > 5)
                throw new ArgumentOutOfRangeException("puntuacion", "La puntuación debe estar entre 1 y 5.");

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.Rating (IdReserva, Puntuacion, Comentario)
                    VALUES (@IdReserva, @Puntuacion, @Comentario);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    cmd.Parameters.AddWithValue("@Puntuacion", puntuacion);
                    cmd.Parameters.AddWithValue("@Comentario",
                        string.IsNullOrWhiteSpace(comentario) ? (object)DBNull.Value : comentario.Trim());

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // GET BY RESERVA
        // =========================
        public static DataRow ObtenerPorReserva(int idReserva)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT IdRating, IdReserva, Puntuacion, Comentario, FechaRating
                    FROM dbo.Rating
                    WHERE IdReserva = @IdReserva;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // =========================
        // GET ALL (for reports)
        // =========================
        public static DataTable ObtenerTodos(int pagina = 0, int tamPagina = 50)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        r.IdRating,
                        r.IdReserva,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        h.Numero AS Habitacion,
                        r.Puntuacion,
                        r.Comentario,
                        r.FechaRating
                    FROM dbo.Rating r
                    INNER JOIN dbo.Reserva res ON res.IdReserva = r.IdReserva
                    INNER JOIN dbo.Cliente c ON c.IdCliente = res.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = res.IdHabitacion
                    ORDER BY r.FechaRating DESC
                    OFFSET @Offset ROWS FETCH NEXT @TamPagina ROWS ONLY;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
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
    }
}
