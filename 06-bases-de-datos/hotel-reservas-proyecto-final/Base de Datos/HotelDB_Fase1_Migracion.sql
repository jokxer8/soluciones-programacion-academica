/* =========================================================
   FASE 1 - MIGRACION HotelDB
   Fecha: 2026-04-06
   Descripcion: Agrega rol RECEPCION, hash de contrasenas,
                tabla CheckInOut, AuditLog, ConfiguracionSistema
                y ajusta sp_GenerarFactura para leer ITBIS de config.

   ORDEN DE EJECUCION: ejecutar este script completo en SSMS
   sobre la base de datos HotelDB.
   ========================================================= */

USE HotelDB;
GO

-- =========================================================
-- F1.1 — Rol RECEPCION
-- =========================================================

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE NombreRol = 'RECEPCION')
    INSERT INTO dbo.Roles (NombreRol, Descripcion)
    VALUES ('RECEPCION', 'Recepcionista - gestiona check-in/out y reservas');
GO

-- Migrar usuarios recepcion existentes (IdRol=2 = EMPLEADO -> RECEPCION)
-- NOTA: recepcion1 no existe en los datos base; recepcion2 si existe.
UPDATE dbo.Usuarios
SET IdRol = (SELECT IdRol FROM dbo.Roles WHERE NombreRol = 'RECEPCION')
WHERE NombreUsuario IN ('recepcion1', 'recepcion2');
GO

-- =========================================================
-- F1.2 — Hash SHA256 de contrasenas
-- =========================================================

-- Paso 1: Ampliar columna Clave a VARCHAR(128)
-- (puede ignorarse si ya tiene suficiente longitud)
ALTER TABLE dbo.Usuarios ALTER COLUMN Clave VARCHAR(128) NOT NULL;
GO

-- Paso 2: Hashear contrasenas existentes con SHA2_256
-- IMPORTANTE: este script se ejecuta UNA SOLA VEZ.
-- Si ya se ejecuto, las contrasenas ya son hashes y no se deben re-hashear.
-- Verificar antes de ejecutar: SELECT NombreUsuario, LEN(Clave) FROM dbo.Usuarios;
-- Si LEN(Clave) = 64 ya estan hasheadas. Si es menor, aun estan en texto plano.

-- Hashear solo si la clave tiene menos de 32 caracteres (texto plano):
UPDATE dbo.Usuarios
SET Clave = LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', Clave), 2))
WHERE LEN(Clave) < 32;
GO

-- =========================================================
-- F1.4 — Tabla CheckInOut
-- =========================================================

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('dbo.CheckInOut') AND type = 'U')
BEGIN
    CREATE TABLE dbo.CheckInOut
    (
        IdCheckInOut       INT IDENTITY(1,1) NOT NULL,
        IdReserva          INT NOT NULL,
        IdEmpleadoCheckIn  INT NULL,
        IdEmpleadoCheckOut INT NULL,
        FechaCheckIn       DATETIME NULL,
        FechaCheckOut      DATETIME NULL,
        ObservacionIn      VARCHAR(250) NULL,
        ObservacionOut     VARCHAR(250) NULL,
        Estado             VARCHAR(20) NOT NULL CONSTRAINT DF_CheckInOut_Estado DEFAULT ('PENDIENTE'),

        CONSTRAINT PK_CheckInOut PRIMARY KEY (IdCheckInOut),
        CONSTRAINT CK_CheckInOut_Estado CHECK (Estado IN ('PENDIENTE','CHECKED_IN','CHECKED_OUT')),
        CONSTRAINT UQ_CheckInOut_Reserva UNIQUE (IdReserva),
        CONSTRAINT FK_CheckInOut_Reserva FOREIGN KEY (IdReserva) REFERENCES dbo.Reserva(IdReserva),
        CONSTRAINT FK_CheckInOut_EmpleadoIn FOREIGN KEY (IdEmpleadoCheckIn) REFERENCES dbo.Empleado(IdEmpleado),
        CONSTRAINT FK_CheckInOut_EmpleadoOut FOREIGN KEY (IdEmpleadoCheckOut) REFERENCES dbo.Empleado(IdEmpleado)
    );
END
GO

-- =========================================================
-- F1.7 — Tabla AuditLog
-- =========================================================

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('dbo.AuditLog') AND type = 'U')
BEGIN
    CREATE TABLE dbo.AuditLog
    (
        IdLog      BIGINT IDENTITY(1,1) NOT NULL,
        IdUsuario  INT NULL,
        Accion     VARCHAR(50) NOT NULL,
        Entidad    VARCHAR(50) NOT NULL,
        IdEntidad  INT NULL,
        Detalle    NVARCHAR(500) NULL,
        FechaHora  DATETIME NOT NULL CONSTRAINT DF_AuditLog_Fecha DEFAULT (GETDATE()),
        IpMaquina  VARCHAR(50) NULL,

        CONSTRAINT PK_AuditLog PRIMARY KEY (IdLog),
        CONSTRAINT FK_AuditLog_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuarios(IdUsuario)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AuditLog_Fecha')
    CREATE INDEX IX_AuditLog_Fecha ON dbo.AuditLog (FechaHora DESC);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AuditLog_Entidad')
    CREATE INDEX IX_AuditLog_Entidad ON dbo.AuditLog (Entidad, IdEntidad);
GO

-- =========================================================
-- F1.9 — Tabla ConfiguracionSistema
-- =========================================================

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('dbo.ConfiguracionSistema') AND type = 'U')
BEGIN
    CREATE TABLE dbo.ConfiguracionSistema
    (
        Clave             VARCHAR(50) NOT NULL,
        Valor             VARCHAR(200) NOT NULL,
        Descripcion       VARCHAR(200) NULL,
        FechaModificacion DATETIME NOT NULL CONSTRAINT DF_Config_Fecha DEFAULT (GETDATE()),

        CONSTRAINT PK_ConfiguracionSistema PRIMARY KEY (Clave)
    );
END
GO

-- Insertar datos iniciales solo si no existen
IF NOT EXISTS (SELECT 1 FROM dbo.ConfiguracionSistema WHERE Clave = 'ITBIS_PORCENTAJE')
    INSERT INTO dbo.ConfiguracionSistema (Clave, Valor, Descripcion)
    VALUES ('ITBIS_PORCENTAJE', '18', 'Porcentaje de impuesto aplicado a facturas');

IF NOT EXISTS (SELECT 1 FROM dbo.ConfiguracionSistema WHERE Clave = 'MONEDA')
    INSERT INTO dbo.ConfiguracionSistema (Clave, Valor, Descripcion)
    VALUES ('MONEDA', 'DOP', 'Moneda del sistema');

IF NOT EXISTS (SELECT 1 FROM dbo.ConfiguracionSistema WHERE Clave = 'HOTEL_NOMBRE')
    INSERT INTO dbo.ConfiguracionSistema (Clave, Valor, Descripcion)
    VALUES ('HOTEL_NOMBRE', 'Hotel Reservas', 'Nombre comercial');

IF NOT EXISTS (SELECT 1 FROM dbo.ConfiguracionSistema WHERE Clave = 'CHECK_IN_HORA')
    INSERT INTO dbo.ConfiguracionSistema (Clave, Valor, Descripcion)
    VALUES ('CHECK_IN_HORA', '15:00', 'Hora estandar de check-in');

IF NOT EXISTS (SELECT 1 FROM dbo.ConfiguracionSistema WHERE Clave = 'CHECK_OUT_HORA')
    INSERT INTO dbo.ConfiguracionSistema (Clave, Valor, Descripcion)
    VALUES ('CHECK_OUT_HORA', '12:00', 'Hora estandar de check-out');
GO

-- =========================================================
-- F1.9 — Modificar sp_GenerarFactura para leer ITBIS de config
-- =========================================================

CREATE OR ALTER PROCEDURE dbo.sp_GenerarFactura
    @IdReserva INT,
    @PorcImpuesto DECIMAL(5,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SubtotalHospedaje DECIMAL(10,2) = 0;
    DECLARE @DescuentoReserva DECIMAL(10,2) = 0;
    DECLARE @TotalServicios DECIMAL(10,2) = 0;
    DECLARE @DescuentoPromociones DECIMAL(10,2) = 0;
    DECLARE @Subtotal DECIMAL(10,2) = 0;
    DECLARE @Descuento DECIMAL(10,2) = 0;
    DECLARE @Impuesto DECIMAL(10,2) = 0;
    DECLARE @Total DECIMAL(10,2) = 0;
    DECLARE @ItbisReal DECIMAL(5,2);

    -- Leer ITBIS de ConfiguracionSistema si no se paso como parametro
    IF @PorcImpuesto IS NULL
    BEGIN
        SELECT @ItbisReal = CAST(Valor AS DECIMAL(5,2))
        FROM dbo.ConfiguracionSistema
        WHERE Clave = 'ITBIS_PORCENTAJE';

        IF @ItbisReal IS NULL
            SET @ItbisReal = 18;
    END
    ELSE
    BEGIN
        SET @ItbisReal = @PorcImpuesto;
    END

    IF EXISTS (SELECT 1 FROM dbo.Factura WHERE IdReserva = @IdReserva)
    BEGIN
        RAISERROR('Ya existe una factura para esta reserva.', 16, 1);
        RETURN;
    END

    SELECT
        @SubtotalHospedaje = ISNULL(Subtotal, 0),
        @DescuentoReserva = ISNULL(Descuento, 0)
    FROM dbo.Reserva
    WHERE IdReserva = @IdReserva;

    SELECT
        @TotalServicios = ISNULL(SUM(Subtotal), 0)
    FROM dbo.ReservaServicio
    WHERE IdReserva = @IdReserva;

    SELECT
        @DescuentoPromociones = ISNULL(SUM(DescuentoAplicado), 0)
    FROM dbo.ReservaPromocion
    WHERE IdReserva = @IdReserva;

    SET @Subtotal = @SubtotalHospedaje + @TotalServicios;
    SET @Descuento = @DescuentoReserva + @DescuentoPromociones;
    SET @Impuesto = ROUND((@Subtotal - @Descuento) * (@ItbisReal / 100.0), 2);
    SET @Total = (@Subtotal - @Descuento) + @Impuesto;

    INSERT INTO dbo.Factura
    (
        IdReserva, Subtotal, Descuento, Impuesto, Total, Estado
    )
    VALUES
    (
        @IdReserva, @Subtotal, @Descuento, @Impuesto, @Total, 'PENDIENTE'
    );

    UPDATE dbo.Reserva
    SET Total = @Total
    WHERE IdReserva = @IdReserva;
END;
GO

-- =========================================================
-- FIN DE MIGRACION FASE 1
-- =========================================================
PRINT 'Migracion Fase 1 completada correctamente.';
GO
