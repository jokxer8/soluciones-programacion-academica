using System;

namespace HotelReservas
{
    public static class SesionUsuario
    {
        public static int IdUsuario { get; set; }
        public static string NombreUsuario { get; set; } = string.Empty;
        public static string NombreCompleto { get; set; } = string.Empty;
        public static string Rol { get; set; } = string.Empty;

        public static bool EsAdmin()
        {
            return Rol.Equals("ADMIN", StringComparison.OrdinalIgnoreCase);
        }

        public static bool EsRecepcion()
        {
            return Rol.Equals("RECEPCION", StringComparison.OrdinalIgnoreCase);
        }

        public static bool PuedeAcceder(string modulo)
        {
            if (IdUsuario <= 0) return false;
            switch (modulo)
            {
                case "Reportes": case "Configuracion": return EsAdmin();
                case "Administracion": case "Mantenimiento":
                    return EsAdmin() || string.Equals(Rol, "SOPORTE", StringComparison.OrdinalIgnoreCase);
                case "Operaciones":
                    return EsAdmin() || EsRecepcion() || string.Equals(Rol, "EMPLEADO", StringComparison.OrdinalIgnoreCase)
                        || string.Equals(Rol, "SOPORTE", StringComparison.OrdinalIgnoreCase);
                default: return false;
            }
        }

        public static void ExigirAcceso(string modulo)
        {
            if (!PuedeAcceder(modulo)) throw new UnauthorizedAccessException("No tiene permiso para acceder a " + modulo + ".");
            // Comprobar también el rol persistido y el estado de la cuenta, no solo el menú.
            using (var conn = Database.GetConnection())
            {
                Database.AbrirConexion(conn);
                using (var cmd = new System.Data.SqlClient.SqlCommand(@"
                    SELECT r.NombreRol FROM dbo.Usuarios u JOIN dbo.Roles r ON r.IdRol=u.IdRol
                    WHERE u.IdUsuario=@Usuario AND u.Estado=1", conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", IdUsuario);
                    string rolActual = Convert.ToString(cmd.ExecuteScalar());
                    if (!string.Equals(rolActual, Rol, StringComparison.OrdinalIgnoreCase))
                    {
                        Cerrar();
                        throw new UnauthorizedAccessException("La sesión dejó de ser válida. Inicie sesión nuevamente.");
                    }
                }
            }
        }

        public static void Cerrar()
        {
            IdUsuario = 0;
            NombreUsuario = string.Empty;
            NombreCompleto = string.Empty;
            Rol = string.Empty;
        }
    }
}
