/* Ejecutar despues de esquema, Fase1 y SeedData, antes de abrir la nueva app.
   No convierte hashes SHA256 en PBKDF2: la app migra al verificar la clave.
   Se puede repetir sin modificar credenciales existentes. */
USE HotelDB;
GO
IF COL_LENGTH('dbo.Usuarios', 'Clave') < 256
    ALTER TABLE dbo.Usuarios ALTER COLUMN Clave VARCHAR(256) NOT NULL;
GO
-- SQL solo recupera la credencial de una cuenta activa; la app la verifica.
-- Nunca pasar el hash almacenado como si fuera una contrasena.
CREATE OR ALTER PROCEDURE dbo.sp_ValidarLogin
    @NombreUsuario VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.IdUsuario, u.NombreUsuario, u.Clave,
        ISNULL(NULLIF(LTRIM(RTRIM(u.NombreCompleto)), ''), u.NombreUsuario) AS NombreCompleto,
        r.NombreRol AS Rol
    FROM dbo.Usuarios u
    INNER JOIN dbo.Roles r ON r.IdRol = u.IdRol
    WHERE u.NombreUsuario = @NombreUsuario AND u.Estado = 1;
END;
GO
