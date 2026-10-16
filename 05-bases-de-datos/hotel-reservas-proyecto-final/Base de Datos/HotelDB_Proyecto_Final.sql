/* =========================================================
   PROYECTO FINAL - SISTEMA DE RESERVAS DE HOTEL
   Script final limpio y consolidado
   DBMS: SQL Server
   Base de datos: HotelDB

   Contenido:
   - Creación de base de datos
   - Tablas indispensables del proyecto
   - Índices
   - Datos iniciales
   - Función
   - Procedimientos almacenados
   - Triggers
   - Vista para reportes

   Nota:
   - Este script está preparado para ejecutarse desde cero.
   - Se eliminaron consultas de diagnóstico, scripts de migración
     intermedia, duplicados y secciones no indispensables.
   ========================================================= */

-- =========================================================
-- 1) CREAR BASE DE DATOS
-- =========================================================
USE master;
GO

IF DB_ID('HotelDB') IS NOT NULL
BEGIN
    ALTER DATABASE HotelDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HotelDB;
END
GO

CREATE DATABASE HotelDB;
GO

USE HotelDB;
GO

-- =========================================================
-- 2) TABLAS
-- =========================================================

CREATE TABLE dbo.Roles
(
    IdRol INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreRol VARCHAR(20) NOT NULL UNIQUE,
    Descripcion VARCHAR(100) NULL
);
GO

CREATE TABLE dbo.Sucursal
(
    IdSucursal INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreSucursal VARCHAR(100) NOT NULL,
    Ciudad VARCHAR(100) NOT NULL,
    Direccion VARCHAR(200) NOT NULL,
    Telefono VARCHAR(20) NULL,
    Email VARCHAR(100) NULL,
    Estado BIT NOT NULL CONSTRAINT DF_Sucursal_Estado DEFAULT (1)
);
GO

CREATE TABLE dbo.Empleado
(
    IdEmpleado INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdSucursal INT NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Cedula VARCHAR(20) NOT NULL UNIQUE,
    Telefono VARCHAR(20) NULL,
    Correo VARCHAR(120) NULL,
    Direccion VARCHAR(250) NULL,
    FechaNacimiento DATE NULL,
    FechaIngreso DATE NOT NULL CONSTRAINT DF_Empleado_FechaIngreso DEFAULT (CAST(GETDATE() AS DATE)),
    Cargo VARCHAR(80) NOT NULL,
    FotoPath VARCHAR(500) NULL,
    CvPath VARCHAR(500) NULL,
    EstadoLaboral VARCHAR(30) NOT NULL CONSTRAINT DF_Empleado_EstadoLaboral DEFAULT ('ACTIVO'),
    Estado BIT NOT NULL CONSTRAINT DF_Empleado_Estado DEFAULT (1),

    CONSTRAINT CK_Empleado_EstadoLaboral
        CHECK (EstadoLaboral IN ('ACTIVO', 'VACACIONES', 'LICENCIA', 'SUSPENDIDO', 'CANCELADO')),

    CONSTRAINT FK_Empleado_Sucursal
        FOREIGN KEY (IdSucursal) REFERENCES dbo.Sucursal(IdSucursal)
);
GO

CREATE TABLE dbo.Usuarios
(
    IdUsuario INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdEmpleado INT NULL,
    NombreUsuario VARCHAR(50) NOT NULL,
    Clave VARCHAR(100) NOT NULL,
    NombreCompleto VARCHAR(100) NOT NULL,
    IdRol INT NOT NULL,
    Estado BIT NOT NULL CONSTRAINT DF_Usuarios_Estado DEFAULT (1),
    FechaCreacion DATETIME NOT NULL CONSTRAINT DF_Usuarios_FechaCreacion DEFAULT (GETDATE()),

    CONSTRAINT UQ_Usuarios_NombreUsuario UNIQUE (NombreUsuario),
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (IdRol) REFERENCES dbo.Roles(IdRol),
    CONSTRAINT FK_Usuarios_Empleado FOREIGN KEY (IdEmpleado) REFERENCES dbo.Empleado(IdEmpleado)
);
GO

CREATE UNIQUE INDEX UX_Usuarios_IdEmpleado
ON dbo.Usuarios(IdEmpleado)
WHERE IdEmpleado IS NOT NULL;
GO

CREATE TABLE dbo.Cliente
(
    IdCliente INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombres VARCHAR(60) NOT NULL,
    Apellidos VARCHAR(60) NOT NULL,
    TipoDocumento VARCHAR(20) NOT NULL,
    Documento VARCHAR(25) NOT NULL UNIQUE,
    Telefono VARCHAR(20) NULL,
    Correo VARCHAR(100) NULL,
    Direccion VARCHAR(200) NULL,
    FechaRegistro DATETIME NOT NULL CONSTRAINT DF_Cliente_FechaRegistro DEFAULT (GETDATE())
);
GO

CREATE TABLE dbo.TipoHabitacion
(
    IdTipoHabitacion INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreTipo VARCHAR(50) NOT NULL UNIQUE,
    Capacidad INT NOT NULL,
    PrecioBase DECIMAL(10,2) NOT NULL,
    Descripcion VARCHAR(200) NULL,

    CONSTRAINT CK_TipoHabitacion_Capacidad CHECK (Capacidad > 0),
    CONSTRAINT CK_TipoHabitacion_PrecioBase CHECK (PrecioBase >= 0)
);
GO

CREATE TABLE dbo.EstadoHabitacion
(
    IdEstadoHabitacion INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreEstado VARCHAR(30) NOT NULL UNIQUE
);
GO

CREATE TABLE dbo.Habitacion
(
    IdHabitacion INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdSucursal INT NOT NULL,
    IdTipoHabitacion INT NOT NULL,
    IdEstadoHabitacion INT NOT NULL,
    Numero VARCHAR(10) NOT NULL,
    Piso INT NOT NULL,
    PrecioPorNoche DECIMAL(10,2) NOT NULL,
    Descripcion VARCHAR(200) NULL,

    CONSTRAINT UQ_Habitacion_Sucursal_Numero UNIQUE (IdSucursal, Numero),
    CONSTRAINT CK_Habitacion_Piso CHECK (Piso >= 1),
    CONSTRAINT CK_Habitacion_Precio CHECK (PrecioPorNoche >= 0),

    CONSTRAINT FK_Habitacion_Sucursal
        FOREIGN KEY (IdSucursal) REFERENCES dbo.Sucursal(IdSucursal),

    CONSTRAINT FK_Habitacion_Tipo
        FOREIGN KEY (IdTipoHabitacion) REFERENCES dbo.TipoHabitacion(IdTipoHabitacion),

    CONSTRAINT FK_Habitacion_Estado
        FOREIGN KEY (IdEstadoHabitacion) REFERENCES dbo.EstadoHabitacion(IdEstadoHabitacion)
);
GO

CREATE TABLE dbo.EstadoReserva
(
    IdEstadoReserva INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreEstado VARCHAR(30) NOT NULL UNIQUE
);
GO

CREATE TABLE dbo.Reserva
(
    IdReserva INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdCliente INT NOT NULL,
    IdHabitacion INT NOT NULL,
    IdEmpleado INT NOT NULL,
    IdEstadoReserva INT NOT NULL,
    FechaReserva DATETIME NOT NULL CONSTRAINT DF_Reserva_FechaReserva DEFAULT (GETDATE()),
    FechaEntrada DATE NOT NULL,
    FechaSalida DATE NOT NULL,
    CantidadHuespedes INT NOT NULL,
    Observacion VARCHAR(250) NULL,
    Subtotal DECIMAL(10,2) NOT NULL CONSTRAINT DF_Reserva_Subtotal DEFAULT (0),
    Descuento DECIMAL(10,2) NOT NULL CONSTRAINT DF_Reserva_Descuento DEFAULT (0),
    Total DECIMAL(10,2) NOT NULL CONSTRAINT DF_Reserva_Total DEFAULT (0),

    CONSTRAINT CK_Reserva_Fechas CHECK (FechaSalida > FechaEntrada),
    CONSTRAINT CK_Reserva_Huespedes CHECK (CantidadHuespedes > 0),

    CONSTRAINT FK_Reserva_Cliente
        FOREIGN KEY (IdCliente) REFERENCES dbo.Cliente(IdCliente),

    CONSTRAINT FK_Reserva_Habitacion
        FOREIGN KEY (IdHabitacion) REFERENCES dbo.Habitacion(IdHabitacion),

    CONSTRAINT FK_Reserva_Empleado
        FOREIGN KEY (IdEmpleado) REFERENCES dbo.Empleado(IdEmpleado),

    CONSTRAINT FK_Reserva_Estado
        FOREIGN KEY (IdEstadoReserva) REFERENCES dbo.EstadoReserva(IdEstadoReserva)
);
GO

CREATE TABLE dbo.ServicioAdicional
(
    IdServicioAdicional INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreServicio VARCHAR(50) NOT NULL UNIQUE,
    Precio DECIMAL(10,2) NOT NULL,
    Estado BIT NOT NULL CONSTRAINT DF_ServicioAdicional_Estado DEFAULT (1),

    CONSTRAINT CK_ServicioAdicional_Precio CHECK (Precio >= 0)
);
GO

CREATE TABLE dbo.ReservaServicio
(
    IdReserva INT NOT NULL,
    IdServicioAdicional INT NOT NULL,
    Cantidad INT NOT NULL CONSTRAINT DF_ReservaServicio_Cantidad DEFAULT (1),
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,

    CONSTRAINT PK_ReservaServicio PRIMARY KEY (IdReserva, IdServicioAdicional),
    CONSTRAINT CK_ReservaServicio_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_ReservaServicio_PrecioUnitario CHECK (PrecioUnitario >= 0),
    CONSTRAINT CK_ReservaServicio_Subtotal CHECK (Subtotal >= 0),

    CONSTRAINT FK_ReservaServicio_Reserva
        FOREIGN KEY (IdReserva) REFERENCES dbo.Reserva(IdReserva),

    CONSTRAINT FK_ReservaServicio_Servicio
        FOREIGN KEY (IdServicioAdicional) REFERENCES dbo.ServicioAdicional(IdServicioAdicional)
);
GO

CREATE TABLE dbo.Promocion
(
    IdPromocion INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombrePromocion VARCHAR(100) NOT NULL UNIQUE,
    TipoDescuento CHAR(1) NOT NULL,
    ValorDescuento DECIMAL(10,2) NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Estado BIT NOT NULL CONSTRAINT DF_Promocion_Estado DEFAULT (1),

    CONSTRAINT CK_Promocion_TipoDescuento CHECK (TipoDescuento IN ('P', 'M')),
    CONSTRAINT CK_Promocion_ValorDescuento CHECK (ValorDescuento >= 0),
    CONSTRAINT CK_Promocion_Fechas CHECK (FechaFin >= FechaInicio)
);
GO

CREATE TABLE dbo.ReservaPromocion
(
    IdReserva INT NOT NULL,
    IdPromocion INT NOT NULL,
    DescuentoAplicado DECIMAL(10,2) NOT NULL CONSTRAINT DF_ReservaPromocion_Descuento DEFAULT (0),

    CONSTRAINT PK_ReservaPromocion PRIMARY KEY (IdReserva, IdPromocion),
    CONSTRAINT CK_ReservaPromocion_Descuento CHECK (DescuentoAplicado >= 0),

    CONSTRAINT FK_ReservaPromocion_Reserva
        FOREIGN KEY (IdReserva) REFERENCES dbo.Reserva(IdReserva),

    CONSTRAINT FK_ReservaPromocion_Promocion
        FOREIGN KEY (IdPromocion) REFERENCES dbo.Promocion(IdPromocion)
);
GO

CREATE TABLE dbo.Factura
(
    IdFactura INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdReserva INT NOT NULL UNIQUE,
    FechaFactura DATETIME NOT NULL CONSTRAINT DF_Factura_FechaFactura DEFAULT (GETDATE()),
    Subtotal DECIMAL(10,2) NOT NULL,
    Descuento DECIMAL(10,2) NOT NULL CONSTRAINT DF_Factura_Descuento DEFAULT (0),
    Impuesto DECIMAL(10,2) NOT NULL CONSTRAINT DF_Factura_Impuesto DEFAULT (0),
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(20) NOT NULL CONSTRAINT DF_Factura_Estado DEFAULT ('PENDIENTE'),

    CONSTRAINT CK_Factura_Subtotal CHECK (Subtotal >= 0),
    CONSTRAINT CK_Factura_Descuento CHECK (Descuento >= 0),
    CONSTRAINT CK_Factura_Impuesto CHECK (Impuesto >= 0),
    CONSTRAINT CK_Factura_Total CHECK (Total >= 0),
    CONSTRAINT CK_Factura_Estado CHECK (Estado IN ('PENDIENTE', 'PAGADA', 'ANULADA')),

    CONSTRAINT FK_Factura_Reserva
        FOREIGN KEY (IdReserva) REFERENCES dbo.Reserva(IdReserva)
);
GO

CREATE TABLE dbo.MetodoPago
(
    IdMetodoPago INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreMetodo VARCHAR(30) NOT NULL UNIQUE
);
GO

CREATE TABLE dbo.Pago
(
    IdPago INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdFactura INT NOT NULL,
    IdMetodoPago INT NOT NULL,
    FechaPago DATETIME NOT NULL CONSTRAINT DF_Pago_FechaPago DEFAULT (GETDATE()),
    Monto DECIMAL(10,2) NOT NULL,
    Referencia VARCHAR(100) NULL,
    Observacion VARCHAR(250) NULL,

    CONSTRAINT CK_Pago_Monto CHECK (Monto > 0),

    CONSTRAINT FK_Pago_Factura
        FOREIGN KEY (IdFactura) REFERENCES dbo.Factura(IdFactura),

    CONSTRAINT FK_Pago_Metodo
        FOREIGN KEY (IdMetodoPago) REFERENCES dbo.MetodoPago(IdMetodoPago)
);
GO

CREATE TABLE dbo.Mantenimiento
(
    IdMantenimiento INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdHabitacion INT NOT NULL,
    IdEmpleado INT NULL,
    FechaInicio DATETIME NOT NULL CONSTRAINT DF_Mantenimiento_FechaInicio DEFAULT (GETDATE()),
    FechaFin DATETIME NULL,
    Motivo VARCHAR(250) NOT NULL,
    Costo DECIMAL(10,2) NOT NULL CONSTRAINT DF_Mantenimiento_Costo DEFAULT (0),
    Estado VARCHAR(20) NOT NULL,

    CONSTRAINT CK_Mantenimiento_Costo CHECK (Costo >= 0),
    CONSTRAINT CK_Mantenimiento_Estado CHECK (Estado IN ('PENDIENTE', 'EN PROCESO', 'FINALIZADO')),

    CONSTRAINT FK_Mantenimiento_Habitacion
        FOREIGN KEY (IdHabitacion) REFERENCES dbo.Habitacion(IdHabitacion),

    CONSTRAINT FK_Mantenimiento_Empleado
        FOREIGN KEY (IdEmpleado) REFERENCES dbo.Empleado(IdEmpleado)
);
GO

-- =========================================================
-- 3) ÍNDICES
-- =========================================================
CREATE INDEX IX_Reserva_Fechas ON dbo.Reserva (FechaEntrada, FechaSalida);
GO

CREATE INDEX IX_Reserva_Empleado ON dbo.Reserva (IdEmpleado);
GO

CREATE INDEX IX_Habitacion_Estado ON dbo.Habitacion (IdEstadoHabitacion);
GO

CREATE INDEX IX_Pago_Factura ON dbo.Pago (IdFactura);
GO

-- =========================================================
-- 4) DATOS MAESTROS INICIALES
-- =========================================================

INSERT INTO dbo.Roles (NombreRol, Descripcion)
VALUES
('ADMIN', 'Administrador del sistema'),
('EMPLEADO', 'Usuario operativo del hotel'),
('SOPORTE', 'Soporte técnico');
GO

INSERT INTO dbo.Sucursal (NombreSucursal, Ciudad, Direccion, Telefono, Email, Estado)
VALUES
('Hotel Central', 'Santo Domingo', 'Av. Principal #10', '000-000-0001', 'demo001@example.com', 1),
('Hotel Playa', 'Punta Cana', 'Boulevard Arena Blanca', '000-000-0002', 'demo002@example.com', 1);
GO

INSERT INTO dbo.EstadoHabitacion (NombreEstado)
VALUES
('DISPONIBLE'),
('OCUPADA'),
('MANTENIMIENTO'),
('LIMPIEZA');
GO

INSERT INTO dbo.EstadoReserva (NombreEstado)
VALUES
('PENDIENTE'),
('CONFIRMADA'),
('CANCELADA'),
('FINALIZADA');
GO

INSERT INTO dbo.MetodoPago (NombreMetodo)
VALUES
('EFECTIVO'),
('TARJETA'),
('TRANSFERENCIA');
GO

INSERT INTO dbo.TipoHabitacion (NombreTipo, Capacidad, PrecioBase, Descripcion)
VALUES
('Simple', 1, 3500, 'Habitación para una persona'),
('Doble', 2, 5000, 'Habitación para dos personas'),
('Suite', 4, 8500, 'Habitación amplia y de lujo');
GO

INSERT INTO dbo.ServicioAdicional (NombreServicio, Precio, Estado)
VALUES
('Desayuno', 600, 1),
('Transporte', 1500, 1),
('Lavandería', 800, 1),
('Spa', 2500, 1);
GO

INSERT INTO dbo.Promocion (NombrePromocion, TipoDescuento, ValorDescuento, FechaInicio, FechaFin, Estado)
VALUES
('Promoción Corporativa', 'M', 1000, '2025-01-01', '2030-12-31', 1),
('Temporada Alta 10%', 'P', 10, '2025-01-01', '2030-12-31', 1);
GO

-- =========================================================
-- 5) DATOS OPERATIVOS INICIALES
-- =========================================================

INSERT INTO dbo.Empleado
(
    IdSucursal, Nombre, Apellido, Cedula, Telefono, Correo, Direccion,
    FechaNacimiento, FechaIngreso, Cargo, FotoPath, CvPath, EstadoLaboral, Estado
)
VALUES
(1, 'Administrador', 'Demo', 'DEMO-000001', '000-000-0003', 'demo003@example.com', 'Distrito Nacional', '1987-09-04', '2026-05-01', 'Administrador General', NULL, NULL, 'ACTIVO', 1),
(1, 'Maria', 'Lopez', 'DEMO-000002', '000-000-0004', 'demo004@example.com', 'Santo Domingo Norte', '1998-08-04', '2026-04-01', 'Recepcionista', NULL, NULL, 'ACTIVO', 1),
(2, 'Jose', 'Fernandez', 'DEMO-000003', '000-000-0005', 'demo005@example.com', 'Juan Dolio', '2000-03-12', '2026-04-01', 'Recepcionista', NULL, NULL, 'ACTIVO', 1),
(2, 'Colaborador', 'Demo', 'DEMO-000004', '000-000-0006', 'demo006@example.com', 'Santo Domingo, Av. Correa y Cidron', '1999-06-24', '2026-01-01', 'Administrador', NULL, NULL, 'ACTIVO', 1);
GO

INSERT INTO dbo.Usuarios
(
    IdEmpleado, NombreUsuario, Clave, NombreCompleto, IdRol, Estado
)
VALUES
(1, 'admin', '123456', 'Administrador General', 1, 1),
(2, 'empleado1', '1234', 'Maria Lopez', 2, 1),
(3, 'recepcion2', '4321', 'Jose Fernandez', 2, 1),
(4, 'gg.admin', '123456', 'Administrador Demo', 1, 1),
(NULL, 'soporte1', '123456', 'Usuario Soporte', 3, 1);
GO

INSERT INTO dbo.Cliente
(
    Nombres, Apellidos, TipoDocumento, Documento, Telefono, Correo, Direccion
)
VALUES
('Ana', 'Perez', 'CEDULA', 'DEMO-000005', '000-000-0007', 'demo007@example.com', 'Santiago'),
('Luis', 'Martinez', 'CEDULA', 'DEMO-000006', '000-000-0008', 'demo008@example.com', 'La Vega'),
('Carla', 'Gomez', 'PASAPORTE', 'P-00112233', '000-000-0009', 'demo009@example.com', 'Santo Domingo'),
('Cliente Demo', 'Demo', 'CEDULA', 'DEMO-000007', '000-000-0010', 'demo010@example.com', 'Santo Domingo');
GO

INSERT INTO dbo.Habitacion
(
    IdSucursal, IdTipoHabitacion, IdEstadoHabitacion, Numero, Piso, PrecioPorNoche, Descripcion
)
VALUES
(1, 1, 1, '101', 1, 3500, 'Simple primer piso'),
(1, 2, 1, '102', 1, 5000, 'Doble primer piso'),
(1, 3, 1, '201', 2, 8500, 'Suite segundo piso'),
(2, 1, 1, '301', 3, 3800, 'Simple vista lateral'),
(2, 2, 1, '302', 3, 5200, 'Doble vista mar'),
(2, 3, 1, '401', 4, 8800, 'La mejor suite en un primer piso');
GO

-- =========================================================
-- 6) FUNCIÓN
-- =========================================================
CREATE FUNCTION dbo.fn_CalcularNoches
(
    @FechaEntrada DATE,
    @FechaSalida DATE
)
RETURNS INT
AS
BEGIN
    RETURN DATEDIFF(DAY, @FechaEntrada, @FechaSalida);
END;
GO

-- =========================================================
-- 7) PROCEDIMIENTOS ALMACENADOS
-- =========================================================

CREATE OR ALTER PROCEDURE dbo.sp_ValidarLogin
    @NombreUsuario VARCHAR(50),
    @Clave VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        u.IdUsuario,
        u.NombreUsuario,
        ISNULL(NULLIF(LTRIM(RTRIM(u.NombreCompleto)), ''), u.NombreUsuario) AS NombreCompleto,
        r.NombreRol AS Rol
    FROM dbo.Usuarios u
    INNER JOIN dbo.Roles r
        ON r.IdRol = u.IdRol
    WHERE u.NombreUsuario = @NombreUsuario
      AND u.Clave = @Clave
      AND u.Estado = 1;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_InsertarReserva
    @IdCliente INT,
    @IdHabitacion INT,
    @IdEmpleado INT,
    @FechaEntrada DATE,
    @FechaSalida DATE,
    @CantidadHuespedes INT,
    @Observacion VARCHAR(250) = NULL,
    @IdEstadoReserva INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PrecioPorNoche DECIMAL(10,2);
    DECLARE @Noches INT;
    DECLARE @Subtotal DECIMAL(10,2);
    DECLARE @Total DECIMAL(10,2);

    IF @FechaSalida <= @FechaEntrada
    BEGIN
        RAISERROR('La fecha de salida debe ser mayor que la fecha de entrada.', 16, 1);
        RETURN;
    END

    IF @IdEstadoReserva IS NULL
    BEGIN
        SELECT @IdEstadoReserva = IdEstadoReserva
        FROM dbo.EstadoReserva
        WHERE NombreEstado = 'PENDIENTE';
    END

    IF EXISTS
    (
        SELECT 1
        FROM dbo.Reserva r
        INNER JOIN dbo.EstadoReserva er
            ON er.IdEstadoReserva = r.IdEstadoReserva
        WHERE r.IdHabitacion = @IdHabitacion
          AND er.NombreEstado IN ('PENDIENTE', 'CONFIRMADA')
          AND @FechaEntrada < r.FechaSalida
          AND @FechaSalida > r.FechaEntrada
    )
    BEGIN
        RAISERROR('La habitación ya tiene una reserva en ese rango de fechas.', 16, 1);
        RETURN;
    END

    SELECT @PrecioPorNoche = PrecioPorNoche
    FROM dbo.Habitacion
    WHERE IdHabitacion = @IdHabitacion;

    IF @PrecioPorNoche IS NULL
    BEGIN
        RAISERROR('La habitación seleccionada no existe.', 16, 1);
        RETURN;
    END

    SET @Noches = dbo.fn_CalcularNoches(@FechaEntrada, @FechaSalida);
    SET @Subtotal = @PrecioPorNoche * @Noches;
    SET @Total = @Subtotal;

    INSERT INTO dbo.Reserva
    (
        IdCliente, IdHabitacion, IdEmpleado, IdEstadoReserva,
        FechaEntrada, FechaSalida, CantidadHuespedes, Observacion,
        Subtotal, Descuento, Total
    )
    VALUES
    (
        @IdCliente, @IdHabitacion, @IdEmpleado, @IdEstadoReserva,
        @FechaEntrada, @FechaSalida, @CantidadHuespedes, @Observacion,
        @Subtotal, 0, @Total
    );
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_GenerarFactura
    @IdReserva INT,
    @PorcImpuesto DECIMAL(5,2) = 18
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
    SET @Impuesto = ROUND((@Subtotal - @Descuento) * (@PorcImpuesto / 100.0), 2);
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
-- 8) TRIGGERS
-- =========================================================

CREATE OR ALTER TRIGGER dbo.trg_Reserva_ActualizarEstadoHabitacion
ON dbo.Reserva
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE h
    SET h.IdEstadoHabitacion = eh.IdEstadoHabitacion
    FROM dbo.Habitacion h
    INNER JOIN inserted i ON i.IdHabitacion = h.IdHabitacion
    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = i.IdEstadoReserva
    CROSS APPLY
    (
        SELECT TOP 1 IdEstadoHabitacion
        FROM dbo.EstadoHabitacion
        WHERE NombreEstado = 'OCUPADA'
    ) eh
    WHERE er.NombreEstado = 'CONFIRMADA';

    UPDATE h
    SET h.IdEstadoHabitacion = eh.IdEstadoHabitacion
    FROM dbo.Habitacion h
    INNER JOIN inserted i ON i.IdHabitacion = h.IdHabitacion
    INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = i.IdEstadoReserva
    CROSS APPLY
    (
        SELECT TOP 1 IdEstadoHabitacion
        FROM dbo.EstadoHabitacion
        WHERE NombreEstado = 'DISPONIBLE'
    ) eh
    WHERE er.NombreEstado IN ('CANCELADA', 'FINALIZADA');
END;
GO

CREATE OR ALTER TRIGGER dbo.trg_Mantenimiento_ActualizarEstadoHabitacion
ON dbo.Mantenimiento
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE h
    SET h.IdEstadoHabitacion = eh.IdEstadoHabitacion
    FROM dbo.Habitacion h
    INNER JOIN inserted i ON i.IdHabitacion = h.IdHabitacion
    CROSS APPLY
    (
        SELECT TOP 1 IdEstadoHabitacion
        FROM dbo.EstadoHabitacion
        WHERE NombreEstado =
            CASE
                WHEN i.Estado IN ('PENDIENTE', 'EN PROCESO') THEN 'MANTENIMIENTO'
                ELSE 'DISPONIBLE'
            END
    ) eh;
END;
GO

-- =========================================================
-- 9) VISTA PARA REPORTES
-- =========================================================

CREATE OR ALTER VIEW dbo.vw_RendimientoEmpleados
AS
SELECT
    e.IdEmpleado,
    e.Nombre + ' ' + e.Apellido AS Empleado,
    s.NombreSucursal,
    SUM(CASE WHEN er.NombreEstado <> 'CANCELADA' THEN 1 ELSE 0 END) AS TotalReservas,
    ISNULL(SUM(ISNULL(f.Total, 0)), 0) AS TotalFacturado
FROM dbo.Empleado e
INNER JOIN dbo.Sucursal s
    ON s.IdSucursal = e.IdSucursal
LEFT JOIN dbo.Reserva r
    ON r.IdEmpleado = e.IdEmpleado
LEFT JOIN dbo.EstadoReserva er
    ON er.IdEstadoReserva = r.IdEstadoReserva
LEFT JOIN dbo.Factura f
    ON f.IdReserva = r.IdReserva
GROUP BY
    e.IdEmpleado,
    e.Nombre,
    e.Apellido,
    s.NombreSucursal;
GO

-- =========================================================
-- 10) DATOS DE PRUEBA DEL PROCESO
-- =========================================================

EXEC dbo.sp_InsertarReserva
    @IdCliente = 1,
    @IdHabitacion = 1,
    @IdEmpleado = 2,
    @FechaEntrada = '2026-04-10',
    @FechaSalida = '2026-04-12',
    @CantidadHuespedes = 1,
    @Observacion = 'Reserva de prueba',
    @IdEstadoReserva = 2;
GO

EXEC dbo.sp_InsertarReserva
    @IdCliente = 2,
    @IdHabitacion = 2,
    @IdEmpleado = 2,
    @FechaEntrada = '2026-04-15',
    @FechaSalida = '2026-04-18',
    @CantidadHuespedes = 2,
    @Observacion = 'Cliente corporativo',
    @IdEstadoReserva = 4;
GO

EXEC dbo.sp_InsertarReserva
    @IdCliente = 4,
    @IdHabitacion = 6,
    @IdEmpleado = 2,
    @FechaEntrada = '2026-04-02',
    @FechaSalida = '2026-04-13',
    @CantidadHuespedes = 2,
    @Observacion = 'Reserva con servicios y promoción',
    @IdEstadoReserva = 1;
GO

INSERT INTO dbo.ReservaServicio (IdReserva, IdServicioAdicional, Cantidad, PrecioUnitario, Subtotal)
VALUES
(2, 1, 3, 600, 1800),
(2, 3, 1, 800, 800),
(3, 2, 1, 1500, 1500);
GO

INSERT INTO dbo.ReservaPromocion (IdReserva, IdPromocion, DescuentoAplicado)
VALUES
(2, 1, 1000),
(3, 1, 1000);
GO

EXEC dbo.sp_GenerarFactura
    @IdReserva = 2,
    @PorcImpuesto = 18;
GO

EXEC dbo.sp_GenerarFactura
    @IdReserva = 3,
    @PorcImpuesto = 18;
GO

INSERT INTO dbo.Pago (IdFactura, IdMetodoPago, Monto, Referencia, Observacion)
VALUES
(1, 2, 19588.00, 'TXN-1001', 'Pago con tarjeta'),
(2, 1, 30000.00, 'EF-0002', 'Abono inicial');
GO

UPDATE dbo.Factura
SET Estado = 'PAGADA'
WHERE IdFactura = 1;
GO

INSERT INTO dbo.Mantenimiento (IdHabitacion, IdEmpleado, Motivo, Costo, Estado)
VALUES
(5, 3, 'Revisión de aire acondicionado', 1200, 'EN PROCESO');
GO

-- =========================================================
-- 11) CONSULTAS BÁSICAS DE VERIFICACIÓN
-- =========================================================
SELECT u.IdUsuario, u.NombreUsuario, u.NombreCompleto, r.NombreRol, u.Estado
FROM dbo.Usuarios u
INNER JOIN dbo.Roles r ON r.IdRol = u.IdRol
ORDER BY u.IdUsuario;
GO

SELECT
    h.IdHabitacion,
    s.NombreSucursal,
    h.Numero,
    th.NombreTipo,
    eh.NombreEstado,
    h.PrecioPorNoche
FROM dbo.Habitacion h
INNER JOIN dbo.Sucursal s ON s.IdSucursal = h.IdSucursal
INNER JOIN dbo.TipoHabitacion th ON th.IdTipoHabitacion = h.IdTipoHabitacion
INNER JOIN dbo.EstadoHabitacion eh ON eh.IdEstadoHabitacion = h.IdEstadoHabitacion
ORDER BY s.NombreSucursal, h.Numero;
GO

SELECT
    r.IdReserva,
    c.Nombres + ' ' + c.Apellidos AS Cliente,
    h.Numero AS Habitacion,
    e.Nombre + ' ' + e.Apellido AS Empleado,
    er.NombreEstado,
    r.FechaEntrada,
    r.FechaSalida,
    r.Total
FROM dbo.Reserva r
INNER JOIN dbo.Cliente c ON c.IdCliente = r.IdCliente
INNER JOIN dbo.Habitacion h ON h.IdHabitacion = r.IdHabitacion
INNER JOIN dbo.Empleado e ON e.IdEmpleado = r.IdEmpleado
INNER JOIN dbo.EstadoReserva er ON er.IdEstadoReserva = r.IdEstadoReserva
ORDER BY r.IdReserva;
GO

SELECT * FROM dbo.vw_RendimientoEmpleados;
GO
