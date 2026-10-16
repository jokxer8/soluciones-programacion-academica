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

        public static void Cerrar()
        {
            IdUsuario = 0;
            NombreUsuario = string.Empty;
            NombreCompleto = string.Empty;
            Rol = string.Empty;
        }
    }
}