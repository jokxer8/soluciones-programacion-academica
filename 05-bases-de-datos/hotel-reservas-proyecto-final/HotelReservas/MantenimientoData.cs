using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class MantenimientoData
    {
        // =========================
        // OBTENER TODOS
        // =========================
        public static DataTable ObtenerTodos()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        m.IdMantenimiento,
                        m.IdHabitacion,
                        h.Numero + ' - ' + s.NombreSucursal AS Habitacion,
                        m.IdEmpleado,
                        ISNULL(e.Nombre + ' ' + e.Apellido, 'Sin asignar') AS Empleado,
                        m.FechaInicio,
                        m.FechaFin,
                        m.Motivo,
                        m.Costo,
                        m.Estado
                    FROM dbo.Mantenimiento m
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = m.IdHabitacion
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    LEFT JOIN dbo.Empleado e ON e.IdEmpleado = m.IdEmpleado
                    ORDER BY m.IdMantenimiento DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // =========================
        // BUSCAR
        // =========================
        public static DataTable Buscar(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        m.IdMantenimiento,
                        m.IdHabitacion,
                        h.Numero + ' - ' + s.NombreSucursal AS Habitacion,
                        m.IdEmpleado,
                        ISNULL(e.Nombre + ' ' + e.Apellido, 'Sin asignar') AS Empleado,
                        m.FechaInicio,
                        m.FechaFin,
                        m.Motivo,
                        m.Costo,
                        m.Estado
                    FROM dbo.Mantenimiento m
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = m.IdHabitacion
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    LEFT JOIN dbo.Empleado e ON e.IdEmpleado = m.IdEmpleado
                    WHERE
                        h.Numero LIKE @texto OR
                        s.NombreSucursal LIKE @texto OR
                        e.Nombre LIKE @texto OR
                        e.Apellido LIKE @texto OR
                        m.Motivo LIKE @texto OR
                        m.Estado LIKE @texto
                    ORDER BY m.IdMantenimiento DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@texto", "%" + texto.Trim() + "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // =========================
        // INSERTAR
        // =========================
        public static void Insertar(
            int idHabitacion,
            int? idEmpleado,
            string motivo,
            decimal costo,
            DateTime fechaInicio,
            DateTime? fechaFin)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.Mantenimiento
                    (
                        IdHabitacion,
                        IdEmpleado,
                        Motivo,
                        Costo,
                        FechaInicio,
                        FechaFin,
                        Estado
                    )
                    VALUES
                    (
                        @IdHabitacion,
                        @IdEmpleado,
                        @Motivo,
                        @Costo,
                        @FechaInicio,
                        @FechaFin,
                        'PENDIENTE'
                    );";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                    cmd.Parameters.AddWithValue("@IdEmpleado",
                        idEmpleado.HasValue ? (object)idEmpleado.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Motivo", motivo);
                    cmd.Parameters.AddWithValue("@Costo", costo);
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin",
                        fechaFin.HasValue ? (object)fechaFin.Value : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // ACTUALIZAR
        // =========================
        public static void Actualizar(
            int idMantenimiento,
            string estado,
            decimal costo,
            DateTime? fechaFin)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Mantenimiento
                    SET
                        Estado = @Estado,
                        Costo = @Costo,
                        FechaFin = @FechaFin
                    WHERE IdMantenimiento = @IdMantenimiento;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@Costo", costo);
                    cmd.Parameters.AddWithValue("@FechaFin",
                        fechaFin.HasValue ? (object)fechaFin.Value : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // ELIMINAR
        // =========================
        public static void Eliminar(int idMantenimiento)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    DELETE FROM dbo.Mantenimiento
                    WHERE IdMantenimiento = @IdMantenimiento;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // COMBOS
        // =========================
        public static DataTable ObtenerHabitacionesCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        h.IdHabitacion,
                        h.Numero + ' - ' + th.NombreTipo + ' - ' + s.NombreSucursal AS Habitacion
                    FROM dbo.Habitacion h
                    INNER JOIN dbo.TipoHabitacion th ON th.IdTipoHabitacion = h.IdTipoHabitacion
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    ORDER BY h.Numero;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerEmpleadosCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdEmpleado,
                        Nombre + ' ' + Apellido AS NombreCompleto
                    FROM dbo.Empleado
                    WHERE Estado = 1
                    ORDER BY Nombre;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}
