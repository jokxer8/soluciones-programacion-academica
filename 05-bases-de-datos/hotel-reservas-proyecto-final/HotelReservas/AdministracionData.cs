using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class AdministracionData
    {
        private static string Q(string nombre)
        {
            return "[" + nombre.Replace("]", "]]") + "]";
        }

        private static bool ExisteColumna(SqlConnection conn, SqlTransaction transaction, string tabla, string columna)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM sys.columns
                WHERE object_id = OBJECT_ID(@Tabla)
                  AND name = @Columna;", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@Tabla", tabla);
                cmd.Parameters.AddWithValue("@Columna", columna);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private static string ObtenerColumnaNombreRol(SqlConnection conn, SqlTransaction transaction = null)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT TOP 1 name
                FROM sys.columns
                WHERE object_id = OBJECT_ID('dbo.Roles')
                  AND name IN ('NombreRol', 'Rol', 'Nombre')
                ORDER BY CASE name
                    WHEN 'NombreRol' THEN 1
                    WHEN 'Rol' THEN 2
                    WHEN 'Nombre' THEN 3
                    ELSE 4
                END;", conn, transaction))
            {
                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    throw new Exception("No se encontró la columna del nombre del rol en dbo.Roles.");

                return result.ToString();
            }
        }

        private static int ObtenerIdRolPorNombre(SqlConnection conn, SqlTransaction transaction, string nombreRol)
        {
            string columnaRol = ObtenerColumnaNombreRol(conn, transaction);
            string sql = $"SELECT TOP 1 IdRol FROM dbo.Roles WHERE {Q(columnaRol)} = @NombreRol;";

            using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@NombreRol", nombreRol);

                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    throw new Exception("No existe el rol seleccionado en la tabla dbo.Roles: " + nombreRol);

                return Convert.ToInt32(result);
            }
        }

        private static string ConstruirConsultaBase(SqlConnection conn, SqlTransaction transaction = null)
        {
            string columnaRol = ObtenerColumnaNombreRol(conn, transaction);
            bool existeRolTexto = ExisteColumna(conn, transaction, "dbo.Usuarios", "Rol");

            string rolExprEmpleado = existeRolTexto
                ? $"ISNULL(r.{Q(columnaRol)}, ISNULL(u.Rol, ''))"
                : $"ISNULL(r.{Q(columnaRol)}, '')";

            string rolExprSoloUsuario = existeRolTexto
                ? $"ISNULL(r.{Q(columnaRol)}, ISNULL(u.Rol, ''))"
                : $"ISNULL(r.{Q(columnaRol)}, '')";

            string sql = $@"
                SELECT *
                FROM
                (
                    SELECT
                        e.IdEmpleado,
                        ISNULL(u.IdUsuario, 0) AS IdUsuario,
                        CAST(1 AS bit) AS TieneEmpleado,
                        'EMPLEADO + USUARIO' AS TipoRegistro,
                        e.IdSucursal,
                        ISNULL(s.NombreSucursal, '') AS NombreSucursal,
                        ISNULL(e.Nombre, '') AS Nombre,
                        ISNULL(e.Apellido, '') AS Apellido,
                        ISNULL(e.Cedula, '') AS Cedula,
                        ISNULL(e.Telefono, '') AS Telefono,
                        ISNULL(e.Correo, '') AS Correo,
                        ISNULL(e.Direccion, '') AS Direccion,
                        e.FechaNacimiento,
                        e.FechaIngreso,
                        ISNULL(e.Cargo, '') AS Cargo,
                        ISNULL(e.FotoPath, '') AS FotoPath,
                        ISNULL(e.CvPath, '') AS CvPath,
                        ISNULL(e.Estado, 0) AS EmpleadoActivo,
                        ISNULL(e.EstadoLaboral, '') AS EstadoLaboral,
                        ISNULL(u.NombreUsuario, '') AS NombreUsuario,
                        ISNULL(u.Clave, '') AS Clave,
                        {rolExprEmpleado} AS Rol,
                        ISNULL(u.Estado, 0) AS UsuarioActivo,
                        CASE WHEN ISNULL(u.Estado, 0) = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END AS EstadoUsuarioTexto
                    FROM dbo.Empleado e
                    LEFT JOIN dbo.Usuarios u ON u.IdEmpleado = e.IdEmpleado
                    LEFT JOIN dbo.Roles r ON r.IdRol = u.IdRol
                    LEFT JOIN dbo.Sucursal s ON s.IdSucursal = e.IdSucursal

                    UNION ALL

                    SELECT
                        0 AS IdEmpleado,
                        u.IdUsuario,
                        CAST(0 AS bit) AS TieneEmpleado,
                        'SOLO USUARIO' AS TipoRegistro,
                        0 AS IdSucursal,
                        '' AS NombreSucursal,
                        '' AS Nombre,
                        '' AS Apellido,
                        '' AS Cedula,
                        '' AS Telefono,
                        '' AS Correo,
                        '' AS Direccion,
                        CAST(NULL AS DATE) AS FechaNacimiento,
                        CAST(NULL AS DATE) AS FechaIngreso,
                        '' AS Cargo,
                        '' AS FotoPath,
                        '' AS CvPath,
                        CAST(0 AS bit) AS EmpleadoActivo,
                        '' AS EstadoLaboral,
                        ISNULL(u.NombreUsuario, '') AS NombreUsuario,
                        ISNULL(u.Clave, '') AS Clave,
                        {rolExprSoloUsuario} AS Rol,
                        ISNULL(u.Estado, 0) AS UsuarioActivo,
                        CASE WHEN ISNULL(u.Estado, 0) = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END AS EstadoUsuarioTexto
                    FROM dbo.Usuarios u
                    LEFT JOIN dbo.Roles r ON r.IdRol = u.IdRol
                    LEFT JOIN dbo.Empleado e ON e.IdEmpleado = u.IdEmpleado
                    WHERE u.IdEmpleado IS NULL OR e.IdEmpleado IS NULL
                ) X";

            return sql;
        }

        public static DataTable ObtenerRoles()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string columnaRol = ObtenerColumnaNombreRol(conn);
                string sql = $@"
                    SELECT
                        IdRol,
                        {Q(columnaRol)} AS Rol
                    FROM dbo.Roles
                    ORDER BY {Q(columnaRol)};";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerEstadosLaborales()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstadoLaboral", typeof(string));

            dt.Rows.Add("ACTIVO");
            dt.Rows.Add("VACACIONES");
            dt.Rows.Add("LICENCIA");
            dt.Rows.Add("SUSPENDIDO");
            dt.Rows.Add("CANCELADO");

            return dt;
        }

        public static DataTable ObtenerEmpleadosUsuarios()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = ConstruirConsultaBase(conn) + @"
                    ORDER BY
                        CASE WHEN TieneEmpleado = 1 THEN 0 ELSE 1 END,
                        IdEmpleado DESC,
                        IdUsuario DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarEmpleadosUsuarios(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = ConstruirConsultaBase(conn) + @"
                    WHERE
                        Nombre LIKE @texto OR
                        Apellido LIKE @texto OR
                        Cedula LIKE @texto OR
                        Cargo LIKE @texto OR
                        NombreUsuario LIKE @texto OR
                        Rol LIKE @texto OR
                        NombreSucursal LIKE @texto OR
                        TipoRegistro LIKE @texto OR
                        Correo LIKE @texto
                    ORDER BY
                        CASE WHEN TieneEmpleado = 1 THEN 0 ELSE 1 END,
                        IdEmpleado DESC,
                        IdUsuario DESC;";

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

        public static void InsertarEmpleadoUsuario(
            int idSucursal,
            string nombre,
            string apellido,
            string cedula,
            string telefono,
            string correo,
            string direccion,
            DateTime? fechaNacimiento,
            DateTime fechaIngreso,
            string cargo,
            string fotoPath,
            string cvPath,
            string estadoLaboral,
            string nombreUsuario,
            string clave,
            string rol,
            bool usuarioActivo)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    ValidarUsuarioUnico(conn, transaction, nombreUsuario, 0);

                    bool empleadoActivo = !estadoLaboral.Equals("CANCELADO", StringComparison.OrdinalIgnoreCase);
                    bool usuarioActivoFinal = usuarioActivo && empleadoActivo;
                    int idRol = ObtenerIdRolPorNombre(conn, transaction, rol);
                    bool existeRolTexto = ExisteColumna(conn, transaction, "dbo.Usuarios", "Rol");

                    int idEmpleado = 0;

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO dbo.Empleado
                        (
                            IdSucursal,
                            Nombre,
                            Apellido,
                            Cedula,
                            Telefono,
                            Correo,
                            Direccion,
                            FechaNacimiento,
                            FechaIngreso,
                            Cargo,
                            FotoPath,
                            CvPath,
                            Estado,
                            EstadoLaboral
                        )
                        VALUES
                        (
                            @IdSucursal,
                            @Nombre,
                            @Apellido,
                            @Cedula,
                            @Telefono,
                            @Correo,
                            @Direccion,
                            @FechaNacimiento,
                            @FechaIngreso,
                            @Cargo,
                            @FotoPath,
                            @CvPath,
                            @Estado,
                            @EstadoLaboral
                        );

                        SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdSucursal", idSucursal);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido", apellido);
                        cmd.Parameters.AddWithValue("@Cedula", string.IsNullOrWhiteSpace(cedula) ? (object)DBNull.Value : cedula);
                        cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono);
                        cmd.Parameters.AddWithValue("@Correo", string.IsNullOrWhiteSpace(correo) ? (object)DBNull.Value : correo);
                        cmd.Parameters.AddWithValue("@Direccion", string.IsNullOrWhiteSpace(direccion) ? (object)DBNull.Value : direccion);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento.HasValue ? (object)fechaNacimiento.Value.Date : DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaIngreso", fechaIngreso.Date);
                        cmd.Parameters.AddWithValue("@Cargo", string.IsNullOrWhiteSpace(cargo) ? (object)DBNull.Value : cargo);
                        cmd.Parameters.AddWithValue("@FotoPath", string.IsNullOrWhiteSpace(fotoPath) ? (object)DBNull.Value : fotoPath);
                        cmd.Parameters.AddWithValue("@CvPath", string.IsNullOrWhiteSpace(cvPath) ? (object)DBNull.Value : cvPath);
                        cmd.Parameters.AddWithValue("@Estado", empleadoActivo);
                        cmd.Parameters.AddWithValue("@EstadoLaboral", estadoLaboral);

                        idEmpleado = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    string sqlInsertUsuario = existeRolTexto
                        ? @"
                            INSERT INTO dbo.Usuarios
                            (
                                IdEmpleado,
                                NombreUsuario,
                                Clave,
                                NombreCompleto,
                                IdRol,
                                Rol,
                                Estado
                            )
                            VALUES
                            (
                                @IdEmpleado,
                                @NombreUsuario,
                                @Clave,
                                @NombreCompleto,
                                @IdRol,
                                @Rol,
                                @Estado
                            );"
                        : @"
                            INSERT INTO dbo.Usuarios
                            (
                                IdEmpleado,
                                NombreUsuario,
                                Clave,
                                NombreCompleto,
                                IdRol,
                                Estado
                            )
                            VALUES
                            (
                                @IdEmpleado,
                                @NombreUsuario,
                                @Clave,
                                @NombreCompleto,
                                @IdRol,
                                @Estado
                            );";

                    using (SqlCommand cmd = new SqlCommand(sqlInsertUsuario, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", Database.HashPassword(clave));
                        cmd.Parameters.AddWithValue("@NombreCompleto", nombre + " " + apellido);
                        cmd.Parameters.AddWithValue("@IdRol", idRol);
                        if (existeRolTexto)
                            cmd.Parameters.AddWithValue("@Rol", rol);
                        cmd.Parameters.AddWithValue("@Estado", usuarioActivoFinal);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void ActualizarEmpleadoUsuario(
            int idEmpleado,
            int idUsuario,
            int idSucursal,
            string nombre,
            string apellido,
            string cedula,
            string telefono,
            string correo,
            string direccion,
            DateTime? fechaNacimiento,
            DateTime fechaIngreso,
            string cargo,
            string fotoPath,
            string cvPath,
            string estadoLaboral,
            string nombreUsuario,
            string clave,
            string rol,
            bool usuarioActivo)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    ValidarUsuarioUnico(conn, transaction, nombreUsuario, idUsuario);

                    bool empleadoActivo = !estadoLaboral.Equals("CANCELADO", StringComparison.OrdinalIgnoreCase);
                    bool usuarioActivoFinal = usuarioActivo && empleadoActivo;
                    int idRol = ObtenerIdRolPorNombre(conn, transaction, rol);
                    bool existeRolTexto = ExisteColumna(conn, transaction, "dbo.Usuarios", "Rol");

                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE dbo.Empleado
                        SET
                            IdSucursal = @IdSucursal,
                            Nombre = @Nombre,
                            Apellido = @Apellido,
                            Cedula = @Cedula,
                            Telefono = @Telefono,
                            Correo = @Correo,
                            Direccion = @Direccion,
                            FechaNacimiento = @FechaNacimiento,
                            FechaIngreso = @FechaIngreso,
                            Cargo = @Cargo,
                            FotoPath = @FotoPath,
                            CvPath = @CvPath,
                            Estado = @Estado,
                            EstadoLaboral = @EstadoLaboral
                        WHERE IdEmpleado = @IdEmpleado;", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        cmd.Parameters.AddWithValue("@IdSucursal", idSucursal);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido", apellido);
                        cmd.Parameters.AddWithValue("@Cedula", string.IsNullOrWhiteSpace(cedula) ? (object)DBNull.Value : cedula);
                        cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono);
                        cmd.Parameters.AddWithValue("@Correo", string.IsNullOrWhiteSpace(correo) ? (object)DBNull.Value : correo);
                        cmd.Parameters.AddWithValue("@Direccion", string.IsNullOrWhiteSpace(direccion) ? (object)DBNull.Value : direccion);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento.HasValue ? (object)fechaNacimiento.Value.Date : DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaIngreso", fechaIngreso.Date);
                        cmd.Parameters.AddWithValue("@Cargo", string.IsNullOrWhiteSpace(cargo) ? (object)DBNull.Value : cargo);
                        cmd.Parameters.AddWithValue("@FotoPath", string.IsNullOrWhiteSpace(fotoPath) ? (object)DBNull.Value : fotoPath);
                        cmd.Parameters.AddWithValue("@CvPath", string.IsNullOrWhiteSpace(cvPath) ? (object)DBNull.Value : cvPath);
                        cmd.Parameters.AddWithValue("@Estado", empleadoActivo);
                        cmd.Parameters.AddWithValue("@EstadoLaboral", estadoLaboral);

                        cmd.ExecuteNonQuery();
                    }

                    if (idUsuario > 0)
                    {
                        string sqlUpdateUsuario = existeRolTexto
                            ? @"
                                UPDATE dbo.Usuarios
                                SET
                                    NombreUsuario = @NombreUsuario,
                                    Clave = @Clave,
                                    NombreCompleto = @NombreCompleto,
                                    IdRol = @IdRol,
                                    Rol = @Rol,
                                    Estado = @Estado
                                WHERE IdUsuario = @IdUsuario;"
                            : @"
                                UPDATE dbo.Usuarios
                                SET
                                    NombreUsuario = @NombreUsuario,
                                    Clave = @Clave,
                                    NombreCompleto = @NombreCompleto,
                                    IdRol = @IdRol,
                                    Estado = @Estado
                                WHERE IdUsuario = @IdUsuario;";

                        using (SqlCommand cmd = new SqlCommand(sqlUpdateUsuario, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                            cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                            cmd.Parameters.AddWithValue("@Clave", Database.HashPassword(clave));
                            cmd.Parameters.AddWithValue("@NombreCompleto", nombre + " " + apellido);
                            cmd.Parameters.AddWithValue("@IdRol", idRol);
                            if (existeRolTexto)
                                cmd.Parameters.AddWithValue("@Rol", rol);
                            cmd.Parameters.AddWithValue("@Estado", usuarioActivoFinal);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string sqlInsertUsuario = existeRolTexto
                            ? @"
                                INSERT INTO dbo.Usuarios
                                (
                                    IdEmpleado,
                                    NombreUsuario,
                                    Clave,
                                    NombreCompleto,
                                    IdRol,
                                    Rol,
                                    Estado
                                )
                                VALUES
                                (
                                    @IdEmpleado,
                                    @NombreUsuario,
                                    @Clave,
                                    @NombreCompleto,
                                    @IdRol,
                                    @Rol,
                                    @Estado
                                );"
                            : @"
                                INSERT INTO dbo.Usuarios
                                (
                                    IdEmpleado,
                                    NombreUsuario,
                                    Clave,
                                    NombreCompleto,
                                    IdRol,
                                    Estado
                                )
                                VALUES
                                (
                                    @IdEmpleado,
                                    @NombreUsuario,
                                    @Clave,
                                    @NombreCompleto,
                                    @IdRol,
                                    @Estado
                                );";

                        using (SqlCommand cmd = new SqlCommand(sqlInsertUsuario, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                            cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                            cmd.Parameters.AddWithValue("@Clave", Database.HashPassword(clave));
                            cmd.Parameters.AddWithValue("@NombreCompleto", nombre + " " + apellido);
                            cmd.Parameters.AddWithValue("@IdRol", idRol);
                            if (existeRolTexto)
                                cmd.Parameters.AddWithValue("@Rol", rol);
                            cmd.Parameters.AddWithValue("@Estado", usuarioActivoFinal);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void ActualizarUsuarioSolo(
            int idUsuario,
            string nombreUsuario,
            string clave,
            string nombreCompleto,
            string rol,
            bool usuarioActivo)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    ValidarUsuarioUnico(conn, transaction, nombreUsuario, idUsuario);

                    int idRol = ObtenerIdRolPorNombre(conn, transaction, rol);
                    bool existeRolTexto = ExisteColumna(conn, transaction, "dbo.Usuarios", "Rol");

                    string sql = existeRolTexto
                        ? @"
                            UPDATE dbo.Usuarios
                            SET
                                NombreUsuario = @NombreUsuario,
                                Clave = @Clave,
                                NombreCompleto = @NombreCompleto,
                                IdRol = @IdRol,
                                Rol = @Rol,
                                Estado = @Estado
                            WHERE IdUsuario = @IdUsuario;"
                        : @"
                            UPDATE dbo.Usuarios
                            SET
                                NombreUsuario = @NombreUsuario,
                                Clave = @Clave,
                                NombreCompleto = @NombreCompleto,
                                IdRol = @IdRol,
                                Estado = @Estado
                            WHERE IdUsuario = @IdUsuario;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", clave);
                        cmd.Parameters.AddWithValue("@NombreCompleto", string.IsNullOrWhiteSpace(nombreCompleto) ? nombreUsuario : nombreCompleto);
                        cmd.Parameters.AddWithValue("@IdRol", idRol);
                        if (existeRolTexto)
                            cmd.Parameters.AddWithValue("@Rol", rol);
                        cmd.Parameters.AddWithValue("@Estado", usuarioActivo);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void CancelarEmpleadoUsuario(int idEmpleado, int idUsuario)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE dbo.Empleado
                        SET
                            Estado = 0,
                            EstadoLaboral = 'CANCELADO'
                        WHERE IdEmpleado = @IdEmpleado;", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        cmd.ExecuteNonQuery();
                    }

                    if (idUsuario > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            UPDATE dbo.Usuarios
                            SET Estado = 0
                            WHERE IdUsuario = @IdUsuario;", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void CancelarUsuarioSolo(int idUsuario)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(@"
                    UPDATE dbo.Usuarios
                    SET Estado = 0
                    WHERE IdUsuario = @IdUsuario;", conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void ValidarUsuarioUnico(SqlConnection conn, SqlTransaction transaction, string nombreUsuario, int idUsuarioActual)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM dbo.Usuarios
                WHERE NombreUsuario = @NombreUsuario
                  AND IdUsuario <> @IdUsuarioActual;", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@IdUsuarioActual", idUsuarioActual);

                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                if (cantidad > 0)
                    throw new Exception("Ya existe un usuario con ese nombre. Debe elegir otro.");
            }
        }
    }
}