using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservas
{
    public static class Database
    {
        private static readonly string connectionString =
            @"Server=localhost;Database=HotelDB;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // =========================
        // HASH HELPER
        // =========================
        public static string HashPassword(string plainText)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainText));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        // =========================
        // LOGIN
        // =========================
        public static bool ValidarLogin(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                return false;

            string hashedClave = HashPassword(clave.Trim());

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.sp_ValidarLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario.Trim());
                    cmd.Parameters.AddWithValue("@Clave", hashedClave);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            SesionUsuario.IdUsuario = dr["IdUsuario"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["IdUsuario"]);

                            SesionUsuario.NombreUsuario = dr["NombreUsuario"] == DBNull.Value
                                ? ""
                                : dr["NombreUsuario"].ToString();

                            SesionUsuario.NombreCompleto = dr["NombreCompleto"] == DBNull.Value
                                ? SesionUsuario.NombreUsuario
                                : dr["NombreCompleto"].ToString();

                            SesionUsuario.Rol = dr["Rol"] == DBNull.Value
                                ? "EMPLEADO"
                                : dr["Rol"].ToString();

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        // =========================
        // CLIENTES
        // =========================
        public static DataTable ObtenerClientes()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdCliente,
                        Nombres,
                        Apellidos,
                        TipoDocumento,
                        Documento,
                        Telefono,
                        Correo,
                        Direccion,
                        FechaRegistro
                    FROM dbo.Cliente
                    ORDER BY IdCliente DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarClientes(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdCliente,
                        Nombres,
                        Apellidos,
                        TipoDocumento,
                        Documento,
                        Telefono,
                        Correo,
                        Direccion,
                        FechaRegistro
                    FROM dbo.Cliente
                    WHERE
                        Nombres LIKE @texto OR
                        Apellidos LIKE @texto OR
                        Documento LIKE @texto
                    ORDER BY IdCliente DESC;";

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

        public static void InsertarCliente(
            string nombres,
            string apellidos,
            string tipoDocumento,
            string documento,
            string telefono,
            string correo,
            string direccion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.Cliente
                    (
                        Nombres,
                        Apellidos,
                        TipoDocumento,
                        Documento,
                        Telefono,
                        Correo,
                        Direccion
                    )
                    VALUES
                    (
                        @Nombres,
                        @Apellidos,
                        @TipoDocumento,
                        @Documento,
                        @Telefono,
                        @Correo,
                        @Direccion
                    );";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombres", nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@TipoDocumento", tipoDocumento);
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono);
                    cmd.Parameters.AddWithValue("@Correo", string.IsNullOrWhiteSpace(correo) ? (object)DBNull.Value : correo);
                    cmd.Parameters.AddWithValue("@Direccion", string.IsNullOrWhiteSpace(direccion) ? (object)DBNull.Value : direccion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarCliente(
            int idCliente,
            string nombres,
            string apellidos,
            string tipoDocumento,
            string documento,
            string telefono,
            string correo,
            string direccion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Cliente
                    SET
                        Nombres = @Nombres,
                        Apellidos = @Apellidos,
                        TipoDocumento = @TipoDocumento,
                        Documento = @Documento,
                        Telefono = @Telefono,
                        Correo = @Correo,
                        Direccion = @Direccion
                    WHERE IdCliente = @IdCliente;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmd.Parameters.AddWithValue("@Nombres", nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@TipoDocumento", tipoDocumento);
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono);
                    cmd.Parameters.AddWithValue("@Correo", string.IsNullOrWhiteSpace(correo) ? (object)DBNull.Value : correo);
                    cmd.Parameters.AddWithValue("@Direccion", string.IsNullOrWhiteSpace(direccion) ? (object)DBNull.Value : direccion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void EliminarCliente(int idCliente)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM dbo.Cliente WHERE IdCliente = @IdCliente;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ObtenerClientesCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdCliente,
                        Nombres + ' ' + Apellidos AS Cliente
                    FROM dbo.Cliente
                    ORDER BY Cliente;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // =========================
        // SUCURSALES
        // =========================
        public static DataTable ObtenerSucursales()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdSucursal,
                        NombreSucursal
                    FROM dbo.Sucursal
                    WHERE Estado = 1
                    ORDER BY NombreSucursal;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerSucursalesAdmin()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdSucursal,
                        NombreSucursal,
                        Ciudad,
                        Direccion,
                        Telefono,
                        Email,
                        Estado,
                        CASE WHEN Estado = 1 THEN 'ACTIVA' ELSE 'INACTIVA' END AS EstadoTexto
                    FROM dbo.Sucursal
                    ORDER BY IdSucursal DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarSucursales(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdSucursal,
                        NombreSucursal,
                        Ciudad,
                        Direccion,
                        Telefono,
                        Email,
                        Estado,
                        CASE WHEN Estado = 1 THEN 'ACTIVA' ELSE 'INACTIVA' END AS EstadoTexto
                    FROM dbo.Sucursal
                    WHERE
                        NombreSucursal LIKE @texto OR
                        Ciudad LIKE @texto OR
                        Direccion LIKE @texto OR
                        Telefono LIKE @texto OR
                        Email LIKE @texto
                    ORDER BY IdSucursal DESC;";

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

        public static void InsertarSucursal(
            string nombreSucursal,
            string ciudad,
            string direccion,
            string telefono,
            string email,
            bool estado)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.Sucursal
                    (
                        NombreSucursal,
                        Ciudad,
                        Direccion,
                        Telefono,
                        Email,
                        Estado
                    )
                    VALUES
                    (
                        @NombreSucursal,
                        @Ciudad,
                        @Direccion,
                        @Telefono,
                        @Email,
                        @Estado
                    );";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NombreSucursal", nombreSucursal);
                    cmd.Parameters.AddWithValue("@Ciudad", ciudad);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarSucursal(
            int idSucursal,
            string nombreSucursal,
            string ciudad,
            string direccion,
            string telefono,
            string email,
            bool estado)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Sucursal
                    SET
                        NombreSucursal = @NombreSucursal,
                        Ciudad = @Ciudad,
                        Direccion = @Direccion,
                        Telefono = @Telefono,
                        Email = @Email,
                        Estado = @Estado
                    WHERE IdSucursal = @IdSucursal;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdSucursal", idSucursal);
                    cmd.Parameters.AddWithValue("@NombreSucursal", nombreSucursal);
                    cmd.Parameters.AddWithValue("@Ciudad", ciudad);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void EliminarSucursal(int idSucursal)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Sucursal
                    SET Estado = 0
                    WHERE IdSucursal = @IdSucursal;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdSucursal", idSucursal);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // TIPOS DE HABITACIÓN
        // =========================
        public static DataTable ObtenerTiposHabitacion()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdTipoHabitacion,
                        NombreTipo
                    FROM dbo.TipoHabitacion
                    ORDER BY NombreTipo;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerTiposHabitacionAdmin()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdTipoHabitacion,
                        NombreTipo,
                        Capacidad,
                        PrecioBase,
                        Descripcion
                    FROM dbo.TipoHabitacion
                    ORDER BY IdTipoHabitacion DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarTiposHabitacion(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdTipoHabitacion,
                        NombreTipo,
                        Capacidad,
                        PrecioBase,
                        Descripcion
                    FROM dbo.TipoHabitacion
                    WHERE
                        NombreTipo LIKE @texto OR
                        Descripcion LIKE @texto
                    ORDER BY IdTipoHabitacion DESC;";

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

        public static void InsertarTipoHabitacion(
            string nombreTipo,
            int capacidad,
            decimal precioBase,
            string descripcion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.TipoHabitacion
                    (
                        NombreTipo,
                        Capacidad,
                        PrecioBase,
                        Descripcion
                    )
                    VALUES
                    (
                        @NombreTipo,
                        @Capacidad,
                        @PrecioBase,
                        @Descripcion
                    );";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NombreTipo", nombreTipo);
                    cmd.Parameters.AddWithValue("@Capacidad", capacidad);
                    cmd.Parameters.AddWithValue("@PrecioBase", precioBase);
                    cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarTipoHabitacion(
            int idTipoHabitacion,
            string nombreTipo,
            int capacidad,
            decimal precioBase,
            string descripcion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.TipoHabitacion
                    SET
                        NombreTipo = @NombreTipo,
                        Capacidad = @Capacidad,
                        PrecioBase = @PrecioBase,
                        Descripcion = @Descripcion
                    WHERE IdTipoHabitacion = @IdTipoHabitacion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdTipoHabitacion", idTipoHabitacion);
                    cmd.Parameters.AddWithValue("@NombreTipo", nombreTipo);
                    cmd.Parameters.AddWithValue("@Capacidad", capacidad);
                    cmd.Parameters.AddWithValue("@PrecioBase", precioBase);
                    cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void EliminarTipoHabitacion(int idTipoHabitacion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM dbo.TipoHabitacion WHERE IdTipoHabitacion = @IdTipoHabitacion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdTipoHabitacion", idTipoHabitacion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // HABITACIONES
        // =========================
        public static DataTable ObtenerEstadosHabitacion()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdEstadoHabitacion,
                        NombreEstado
                    FROM dbo.EstadoHabitacion
                    ORDER BY NombreEstado;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerHabitaciones()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        h.IdHabitacion,
                        h.IdSucursal,
                        s.NombreSucursal,
                        h.IdTipoHabitacion,
                        th.NombreTipo,
                        h.IdEstadoHabitacion,
                        eh.NombreEstado,
                        h.Numero,
                        h.Piso,
                        h.PrecioPorNoche,
                        h.Descripcion
                    FROM dbo.Habitacion h
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    INNER JOIN dbo.TipoHabitacion th ON th.IdTipoHabitacion = h.IdTipoHabitacion
                    INNER JOIN dbo.EstadoHabitacion eh ON eh.IdEstadoHabitacion = h.IdEstadoHabitacion
                    ORDER BY h.IdHabitacion DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarHabitaciones(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        h.IdHabitacion,
                        h.IdSucursal,
                        s.NombreSucursal,
                        h.IdTipoHabitacion,
                        th.NombreTipo,
                        h.IdEstadoHabitacion,
                        eh.NombreEstado,
                        h.Numero,
                        h.Piso,
                        h.PrecioPorNoche,
                        h.Descripcion
                    FROM dbo.Habitacion h
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    INNER JOIN dbo.TipoHabitacion th ON th.IdTipoHabitacion = h.IdTipoHabitacion
                    INNER JOIN dbo.EstadoHabitacion eh ON eh.IdEstadoHabitacion = h.IdEstadoHabitacion
                    WHERE
                        h.Numero LIKE @texto OR
                        s.NombreSucursal LIKE @texto OR
                        th.NombreTipo LIKE @texto OR
                        eh.NombreEstado LIKE @texto
                    ORDER BY h.IdHabitacion DESC;";

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

        public static DataTable ObtenerHabitacionesCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
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

        public static void InsertarHabitacion(
            int idSucursal,
            int idTipoHabitacion,
            int idEstadoHabitacion,
            string numero,
            int piso,
            decimal precioPorNoche,
            string descripcion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO dbo.Habitacion
                    (
                        IdSucursal,
                        IdTipoHabitacion,
                        IdEstadoHabitacion,
                        Numero,
                        Piso,
                        PrecioPorNoche,
                        Descripcion
                    )
                    VALUES
                    (
                        @IdSucursal,
                        @IdTipoHabitacion,
                        @IdEstadoHabitacion,
                        @Numero,
                        @Piso,
                        @PrecioPorNoche,
                        @Descripcion
                    );";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdSucursal", idSucursal);
                    cmd.Parameters.AddWithValue("@IdTipoHabitacion", idTipoHabitacion);
                    cmd.Parameters.AddWithValue("@IdEstadoHabitacion", idEstadoHabitacion);
                    cmd.Parameters.AddWithValue("@Numero", numero);
                    cmd.Parameters.AddWithValue("@Piso", piso);
                    cmd.Parameters.AddWithValue("@PrecioPorNoche", precioPorNoche);
                    cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarHabitacion(
            int idHabitacion,
            int idSucursal,
            int idTipoHabitacion,
            int idEstadoHabitacion,
            string numero,
            int piso,
            decimal precioPorNoche,
            string descripcion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.Habitacion
                    SET
                        IdSucursal = @IdSucursal,
                        IdTipoHabitacion = @IdTipoHabitacion,
                        IdEstadoHabitacion = @IdEstadoHabitacion,
                        Numero = @Numero,
                        Piso = @Piso,
                        PrecioPorNoche = @PrecioPorNoche,
                        Descripcion = @Descripcion
                    WHERE IdHabitacion = @IdHabitacion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                    cmd.Parameters.AddWithValue("@IdSucursal", idSucursal);
                    cmd.Parameters.AddWithValue("@IdTipoHabitacion", idTipoHabitacion);
                    cmd.Parameters.AddWithValue("@IdEstadoHabitacion", idEstadoHabitacion);
                    cmd.Parameters.AddWithValue("@Numero", numero);
                    cmd.Parameters.AddWithValue("@Piso", piso);
                    cmd.Parameters.AddWithValue("@PrecioPorNoche", precioPorNoche);
                    cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void EliminarHabitacion(int idHabitacion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM dbo.Habitacion WHERE IdHabitacion = @IdHabitacion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // EMPLEADOS / ESTADOS RESERVA
        // =========================
        public static DataTable ObtenerEmpleadosCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdEmpleado,
                        Nombre + ' ' + Apellido AS Empleado
                    FROM dbo.Empleado
                    WHERE Estado = 1
                    ORDER BY Empleado;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerEstadosReservaCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdEstadoReserva,
                        NombreEstado
                    FROM dbo.EstadoReserva
                    ORDER BY NombreEstado;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // =========================
        // RESERVAS
        // =========================
        public static DataTable ObtenerReservas()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        r.IdReserva,
                        r.IdCliente,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        r.IdHabitacion,
                        h.Numero AS Habitacion,
                        r.IdEmpleado,
                        e.Nombre + ' ' + e.Apellido AS Empleado,
                        r.IdEstadoReserva,
                        er.NombreEstado,
                        r.FechaEntrada,
                        r.FechaSalida,
                        DATEDIFF(DAY, r.FechaEntrada, r.FechaSalida) AS Noches,
                        r.CantidadHuespedes,
                        r.Subtotal,
                        r.Descuento,
                        r.Total,
                        r.Observacion,
                        r.FechaReserva
                    FROM dbo.Reserva r
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    INNER JOIN dbo.Empleado e ON e.IdEmpleado = r.IdEmpleado
                    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                    ORDER BY r.IdReserva DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarReservas(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        r.IdReserva,
                        r.IdCliente,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        r.IdHabitacion,
                        h.Numero AS Habitacion,
                        r.IdEmpleado,
                        e.Nombre + ' ' + e.Apellido AS Empleado,
                        r.IdEstadoReserva,
                        er.NombreEstado,
                        r.FechaEntrada,
                        r.FechaSalida,
                        DATEDIFF(DAY, r.FechaEntrada, r.FechaSalida) AS Noches,
                        r.CantidadHuespedes,
                        r.Subtotal,
                        r.Descuento,
                        r.Total,
                        r.Observacion,
                        r.FechaReserva
                    FROM dbo.Reserva r
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    INNER JOIN dbo.Empleado e ON e.IdEmpleado = r.IdEmpleado
                    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                    WHERE
                        c.Nombres LIKE @texto OR
                        c.Apellidos LIKE @texto OR
                        h.Numero LIKE @texto OR
                        er.NombreEstado LIKE @texto
                    ORDER BY r.IdReserva DESC;";

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

        public static decimal CalcularTotalReserva(int idHabitacion, DateTime fechaEntrada, DateTime fechaSalida)
        {
            if (fechaSalida <= fechaEntrada)
                return 0;

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "SELECT PrecioPorNoche FROM dbo.Habitacion WHERE IdHabitacion = @IdHabitacion;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                    object precioObj = cmd.ExecuteScalar();

                    if (precioObj == null || precioObj == DBNull.Value)
                        return 0;

                    decimal precio = Convert.ToDecimal(precioObj);
                    int noches = (fechaSalida - fechaEntrada).Days;

                    return precio * noches;
                }
            }
        }

        public static void InsertarReserva(
            int idCliente,
            int idHabitacion,
            int idEmpleado,
            int idEstadoReserva,
            DateTime fechaEntrada,
            DateTime fechaSalida,
            int cantidadHuespedes,
            string observacion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertarReserva", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                    cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                    cmd.Parameters.AddWithValue("@FechaEntrada", fechaEntrada.Date);
                    cmd.Parameters.AddWithValue("@FechaSalida", fechaSalida.Date);
                    cmd.Parameters.AddWithValue("@CantidadHuespedes", cantidadHuespedes);
                    cmd.Parameters.AddWithValue("@Observacion", string.IsNullOrWhiteSpace(observacion) ? (object)DBNull.Value : observacion);
                    cmd.Parameters.AddWithValue("@IdEstadoReserva", idEstadoReserva);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarReserva(
            int idReserva,
            int idCliente,
            int idHabitacion,
            int idEmpleado,
            int idEstadoReserva,
            DateTime fechaEntrada,
            DateTime fechaSalida,
            int cantidadHuespedes,
            string observacion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int idHabitacionAnterior = 0;

                    using (SqlCommand cmdHabitacionAnterior = new SqlCommand(
                        "SELECT IdHabitacion FROM dbo.Reserva WHERE IdReserva = @IdReserva;", conn, transaction))
                    {
                        cmdHabitacionAnterior.Parameters.AddWithValue("@IdReserva", idReserva);
                        object result = cmdHabitacionAnterior.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                            idHabitacionAnterior = Convert.ToInt32(result);
                    }

                    using (SqlCommand cmdValidar = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM dbo.Reserva r
                        INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
                        WHERE r.IdReserva <> @IdReserva
                          AND r.IdHabitacion = @IdHabitacion
                          AND er.NombreEstado IN ('PENDIENTE', 'CONFIRMADA')
                          AND @FechaEntrada < r.FechaSalida
                          AND @FechaSalida > r.FechaEntrada;", conn, transaction))
                    {
                        cmdValidar.Parameters.AddWithValue("@IdReserva", idReserva);
                        cmdValidar.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                        cmdValidar.Parameters.AddWithValue("@FechaEntrada", fechaEntrada.Date);
                        cmdValidar.Parameters.AddWithValue("@FechaSalida", fechaSalida.Date);

                        int cantidad = Convert.ToInt32(cmdValidar.ExecuteScalar());

                        if (cantidad > 0)
                            throw new Exception("La habitación ya tiene una reserva en ese rango de fechas.");
                    }

                    decimal precioPorNoche = 0;

                    using (SqlCommand cmdPrecio = new SqlCommand(
                        "SELECT PrecioPorNoche FROM dbo.Habitacion WHERE IdHabitacion = @IdHabitacion;", conn, transaction))
                    {
                        cmdPrecio.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                        object result = cmdPrecio.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                            precioPorNoche = Convert.ToDecimal(result);
                    }

                    int noches = (fechaSalida.Date - fechaEntrada.Date).Days;
                    decimal subtotal = precioPorNoche * noches;
                    decimal descuento = 0;
                    decimal total = subtotal;

                    using (SqlCommand cmdUpdate = new SqlCommand(@"
                        UPDATE dbo.Reserva
                        SET
                            IdCliente = @IdCliente,
                            IdHabitacion = @IdHabitacion,
                            IdEmpleado = @IdEmpleado,
                            IdEstadoReserva = @IdEstadoReserva,
                            FechaEntrada = @FechaEntrada,
                            FechaSalida = @FechaSalida,
                            CantidadHuespedes = @CantidadHuespedes,
                            Observacion = @Observacion,
                            Subtotal = @Subtotal,
                            Descuento = @Descuento,
                            Total = @Total
                        WHERE IdReserva = @IdReserva;", conn, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@IdReserva", idReserva);
                        cmdUpdate.Parameters.AddWithValue("@IdCliente", idCliente);
                        cmdUpdate.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                        cmdUpdate.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        cmdUpdate.Parameters.AddWithValue("@IdEstadoReserva", idEstadoReserva);
                        cmdUpdate.Parameters.AddWithValue("@FechaEntrada", fechaEntrada.Date);
                        cmdUpdate.Parameters.AddWithValue("@FechaSalida", fechaSalida.Date);
                        cmdUpdate.Parameters.AddWithValue("@CantidadHuespedes", cantidadHuespedes);
                        cmdUpdate.Parameters.AddWithValue("@Observacion", string.IsNullOrWhiteSpace(observacion) ? (object)DBNull.Value : observacion);
                        cmdUpdate.Parameters.AddWithValue("@Subtotal", subtotal);
                        cmdUpdate.Parameters.AddWithValue("@Descuento", descuento);
                        cmdUpdate.Parameters.AddWithValue("@Total", total);

                        cmdUpdate.ExecuteNonQuery();
                    }

                    if (idHabitacionAnterior != 0 && idHabitacionAnterior != idHabitacion)
                    {
                        using (SqlCommand cmdLiberar = new SqlCommand(@"
                            UPDATE dbo.Habitacion
                            SET IdEstadoHabitacion = (
                                SELECT TOP 1 IdEstadoHabitacion
                                FROM dbo.EstadoHabitacion
                                WHERE NombreEstado = 'DISPONIBLE'
                            )
                            WHERE IdHabitacion = @IdHabitacionAnterior;", conn, transaction))
                        {
                            cmdLiberar.Parameters.AddWithValue("@IdHabitacionAnterior", idHabitacionAnterior);
                            cmdLiberar.ExecuteNonQuery();
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

        public static void EliminarReserva(int idReserva)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int idHabitacion = 0;

                    using (SqlCommand cmdGet = new SqlCommand(
                        "SELECT IdHabitacion FROM dbo.Reserva WHERE IdReserva = @IdReserva;", conn, transaction))
                    {
                        cmdGet.Parameters.AddWithValue("@IdReserva", idReserva);
                        object result = cmdGet.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                            idHabitacion = Convert.ToInt32(result);
                    }

                    using (SqlCommand cmdDelete = new SqlCommand(
                        "DELETE FROM dbo.Reserva WHERE IdReserva = @IdReserva;", conn, transaction))
                    {
                        cmdDelete.Parameters.AddWithValue("@IdReserva", idReserva);
                        cmdDelete.ExecuteNonQuery();
                    }

                    if (idHabitacion != 0)
                    {
                        using (SqlCommand cmdLiberar = new SqlCommand(@"
                            UPDATE dbo.Habitacion
                            SET IdEstadoHabitacion = (
                                SELECT TOP 1 IdEstadoHabitacion
                                FROM dbo.EstadoHabitacion
                                WHERE NombreEstado = 'DISPONIBLE'
                            )
                            WHERE IdHabitacion = @IdHabitacion;", conn, transaction))
                        {
                            cmdLiberar.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                            cmdLiberar.ExecuteNonQuery();
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

        // =========================
        // FACTURAS
        // =========================
        public static DataTable ObtenerReservasSinFacturaCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        r.IdReserva,
                        CAST(r.IdReserva AS VARCHAR(10)) + ' - ' +
                        c.Nombres + ' ' + c.Apellidos + ' - Hab. ' + h.Numero AS Reserva
                    FROM dbo.Reserva r
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    LEFT JOIN dbo.Factura f ON f.IdReserva = r.IdReserva
                    WHERE f.IdFactura IS NULL
                    ORDER BY r.IdReserva DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerDetalleFacturaPreview(int idReserva, decimal porcImpuesto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        r.IdReserva,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        h.Numero AS Habitacion,
                        CAST(ISNULL(r.Subtotal, 0) + ISNULL(srv.TotalServicios, 0) AS DECIMAL(10,2)) AS Subtotal,
                        CAST(ISNULL(r.Descuento, 0) + ISNULL(pro.DescuentoPromociones, 0) AS DECIMAL(10,2)) AS Descuento,
                        CAST(ROUND(((ISNULL(r.Subtotal, 0) + ISNULL(srv.TotalServicios, 0)) - (ISNULL(r.Descuento, 0) + ISNULL(pro.DescuentoPromociones, 0))) * (@PorcImpuesto / 100.0), 2) AS DECIMAL(10,2)) AS Impuesto,
                        CAST(((ISNULL(r.Subtotal, 0) + ISNULL(srv.TotalServicios, 0)) - (ISNULL(r.Descuento, 0) + ISNULL(pro.DescuentoPromociones, 0)))
                            + ROUND(((ISNULL(r.Subtotal, 0) + ISNULL(srv.TotalServicios, 0)) - (ISNULL(r.Descuento, 0) + ISNULL(pro.DescuentoPromociones, 0))) * (@PorcImpuesto / 100.0), 2)
                            AS DECIMAL(10,2)) AS Total
                    FROM dbo.Reserva r
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    OUTER APPLY
                    (
                        SELECT SUM(Subtotal) AS TotalServicios
                        FROM dbo.ReservaServicio rs
                        WHERE rs.IdReserva = r.IdReserva
                    ) srv
                    OUTER APPLY
                    (
                        SELECT SUM(DescuentoAplicado) AS DescuentoPromociones
                        FROM dbo.ReservaPromocion rp
                        WHERE rp.IdReserva = r.IdReserva
                    ) pro
                    WHERE r.IdReserva = @IdReserva;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    cmd.Parameters.AddWithValue("@PorcImpuesto", porcImpuesto);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static void GenerarFactura(int idReserva, decimal porcImpuesto)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.sp_GenerarFactura", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdReserva", idReserva);
                    cmd.Parameters.AddWithValue("@PorcImpuesto", porcImpuesto);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ObtenerFacturas()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        f.IdFactura,
                        f.IdReserva,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        h.Numero AS Habitacion,
                        f.FechaFactura,
                        f.Subtotal,
                        f.Descuento,
                        f.Impuesto,
                        f.Total,
                        f.Estado
                    FROM dbo.Factura f
                    INNER JOIN dbo.Reserva r ON r.IdReserva = f.IdReserva
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    ORDER BY f.IdFactura DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable BuscarFacturas(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        f.IdFactura,
                        f.IdReserva,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        h.Numero AS Habitacion,
                        f.FechaFactura,
                        f.Subtotal,
                        f.Descuento,
                        f.Impuesto,
                        f.Total,
                        f.Estado
                    FROM dbo.Factura f
                    INNER JOIN dbo.Reserva r ON r.IdReserva = f.IdReserva
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    WHERE
                        CAST(f.IdFactura AS VARCHAR(20)) LIKE @texto OR
                        CAST(f.IdReserva AS VARCHAR(20)) LIKE @texto OR
                        c.Nombres LIKE @texto OR
                        c.Apellidos LIKE @texto OR
                        h.Numero LIKE @texto OR
                        f.Estado LIKE @texto
                    ORDER BY f.IdFactura DESC;";

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
        // PAGOS
        // =========================
        public static DataTable ObtenerMetodosPago()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdMetodoPago,
                        NombreMetodo
                    FROM dbo.MetodoPago
                    ORDER BY NombreMetodo;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerPagosPorFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        p.IdPago,
                        p.IdFactura,
                        mp.NombreMetodo AS MetodoPago,
                        p.FechaPago,
                        p.Monto,
                        p.Referencia,
                        p.Observacion
                    FROM dbo.Pago p
                    INNER JOIN dbo.MetodoPago mp ON mp.IdMetodoPago = p.IdMetodoPago
                    WHERE p.IdFactura = @IdFactura
                    ORDER BY p.IdPago DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static decimal ObtenerTotalPagadoFactura(int idFactura)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT ISNULL(SUM(Monto), 0)
                    FROM dbo.Pago
                    WHERE IdFactura = @IdFactura;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return 0;

                    return Convert.ToDecimal(result);
                }
            }
        }

        public static void RegistrarPago(
            int idFactura,
            int idMetodoPago,
            decimal monto,
            string referencia,
            string observacion)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    decimal totalFactura = 0;
                    decimal totalPagado = 0;
                    string estadoFactura = "";

                    using (SqlCommand cmdFactura = new SqlCommand(@"
                        SELECT Total, Estado
                        FROM dbo.Factura
                        WHERE IdFactura = @IdFactura;", conn, transaction))
                    {
                        cmdFactura.Parameters.AddWithValue("@IdFactura", idFactura);

                        using (SqlDataReader dr = cmdFactura.ExecuteReader())
                        {
                            if (!dr.Read())
                                throw new Exception("La factura no existe.");

                            totalFactura = Convert.ToDecimal(dr["Total"]);
                            estadoFactura = dr["Estado"].ToString();
                        }
                    }

                    if (estadoFactura == "ANULADA")
                        throw new Exception("No se puede registrar pago a una factura anulada.");

                    if (estadoFactura == "PAGADA")
                        throw new Exception("La factura ya está pagada.");

                    using (SqlCommand cmdPagado = new SqlCommand(@"
                        SELECT ISNULL(SUM(Monto), 0)
                        FROM dbo.Pago
                        WHERE IdFactura = @IdFactura;", conn, transaction))
                    {
                        cmdPagado.Parameters.AddWithValue("@IdFactura", idFactura);
                        totalPagado = Convert.ToDecimal(cmdPagado.ExecuteScalar());
                    }

                    decimal balance = totalFactura - totalPagado;

                    if (monto <= 0)
                        throw new Exception("El monto del pago debe ser mayor que cero.");

                    if (monto > balance)
                        throw new Exception("El monto excede el balance pendiente de la factura.");

                    using (SqlCommand cmdInsert = new SqlCommand(@"
                        INSERT INTO dbo.Pago
                        (
                            IdFactura,
                            IdMetodoPago,
                            FechaPago,
                            Monto,
                            Referencia,
                            Observacion
                        )
                        VALUES
                        (
                            @IdFactura,
                            @IdMetodoPago,
                            GETDATE(),
                            @Monto,
                            @Referencia,
                            @Observacion
                        );", conn, transaction))
                    {
                        cmdInsert.Parameters.AddWithValue("@IdFactura", idFactura);
                        cmdInsert.Parameters.AddWithValue("@IdMetodoPago", idMetodoPago);
                        cmdInsert.Parameters.AddWithValue("@Monto", monto);
                        cmdInsert.Parameters.AddWithValue("@Referencia", string.IsNullOrWhiteSpace(referencia) ? (object)DBNull.Value : referencia);
                        cmdInsert.Parameters.AddWithValue("@Observacion", string.IsNullOrWhiteSpace(observacion) ? (object)DBNull.Value : observacion);

                        cmdInsert.ExecuteNonQuery();
                    }

                    decimal nuevoTotalPagado = totalPagado + monto;
                    string nuevoEstado = nuevoTotalPagado >= totalFactura ? "PAGADA" : "PENDIENTE";

                    using (SqlCommand cmdUpdateFactura = new SqlCommand(@"
                        UPDATE dbo.Factura
                        SET Estado = @Estado
                        WHERE IdFactura = @IdFactura;", conn, transaction))
                    {
                        cmdUpdateFactura.Parameters.AddWithValue("@Estado", nuevoEstado);
                        cmdUpdateFactura.Parameters.AddWithValue("@IdFactura", idFactura);
                        cmdUpdateFactura.ExecuteNonQuery();
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

        // =========================
        // REPORTES
        // =========================
        public static DataTable ObtenerRendimientoEmpleados()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"SELECT * FROM dbo.vw_RendimientoEmpleados;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // =========================
        // AUDIT LOG
        // =========================
        public static void RegistrarAccion(string accion, string entidad, int? idEntidad = null, string detalle = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO dbo.AuditLog
                            (IdUsuario, Accion, Entidad, IdEntidad, Detalle, FechaHora, IpMaquina)
                        VALUES
                            (@IdUsuario, @Accion, @Entidad, @IdEntidad, @Detalle, GETDATE(), @IpMaquina);";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario",
                            SesionUsuario.IdUsuario > 0 ? (object)SesionUsuario.IdUsuario : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Accion", accion ?? "");
                        cmd.Parameters.AddWithValue("@Entidad", entidad ?? "");
                        cmd.Parameters.AddWithValue("@IdEntidad",
                            idEntidad.HasValue ? (object)idEntidad.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Detalle",
                            string.IsNullOrWhiteSpace(detalle) ? (object)DBNull.Value : detalle);
                        cmd.Parameters.AddWithValue("@IpMaquina", Environment.MachineName ?? "");

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Audit failures must not interrupt business operations
            }
        }

        // =========================
        // CONFIGURACION SISTEMA
        // =========================
        public static string ObtenerConfiguracion(string clave, string valorDefault = "")
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT Valor FROM dbo.ConfiguracionSistema WHERE Clave = @Clave;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Clave", clave);
                        object result = cmd.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                            return valorDefault;

                        return result.ToString();
                    }
                }
            }
            catch
            {
                return valorDefault;
            }
        }

        public static void ActualizarConfiguracion(string clave, string valor)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE dbo.ConfiguracionSistema
                    SET Valor = @Valor, FechaModificacion = GETDATE()
                    WHERE Clave = @Clave;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Clave", clave);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ObtenerTodasConfiguraciones()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT Clave, Valor, Descripcion, FechaModificacion
                    FROM dbo.ConfiguracionSistema
                    ORDER BY Clave;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // =========================
        // PAGED OVERLOADS (F2.3)
        // Page size = 50 rows; pagina is 0-based
        // =========================

        public static DataTable ObtenerClientesPaginado(int pagina, int tamPagina = 50)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        IdCliente,
                        Nombres,
                        Apellidos,
                        TipoDocumento,
                        Documento,
                        Telefono,
                        Correo,
                        Direccion,
                        FechaRegistro
                    FROM dbo.Cliente
                    ORDER BY IdCliente DESC
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

        public static DataTable ObtenerHabitacionesPaginado(int pagina, int tamPagina = 50)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        h.IdHabitacion,
                        h.IdSucursal,
                        s.NombreSucursal,
                        h.IdTipoHabitacion,
                        th.NombreTipo,
                        h.IdEstadoHabitacion,
                        eh.NombreEstado,
                        h.Numero,
                        h.Piso,
                        h.PrecioPorNoche,
                        h.Descripcion
                    FROM dbo.Habitacion h
                    INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
                    INNER JOIN dbo.TipoHabitacion th ON th.IdTipoHabitacion = h.IdTipoHabitacion
                    INNER JOIN dbo.EstadoHabitacion eh ON eh.IdEstadoHabitacion = h.IdEstadoHabitacion
                    ORDER BY h.IdHabitacion DESC
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

        public static DataTable ObtenerFacturasPaginado(int pagina, int tamPagina = 50)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT
                        f.IdFactura,
                        f.IdReserva,
                        c.Nombres + ' ' + c.Apellidos AS Cliente,
                        h.Numero AS Habitacion,
                        f.FechaFactura,
                        f.Subtotal,
                        f.Descuento,
                        f.Impuesto,
                        f.Total,
                        f.Estado
                    FROM dbo.Factura f
                    INNER JOIN dbo.Reserva r ON r.IdReserva = f.IdReserva
                    INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
                    INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
                    ORDER BY f.IdFactura DESC
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