/* =========================================================
   BASE DE DATOS: RESTAURANTE_ATENCION
   PROPÓSITO:
   Base de datos para gestionar la atención de mesas
   en un restaurante.
   ========================================================= */

-- ELIMINAR SI EXISTE
IF DB_ID('RESTAURANTE_ATENCION') IS NOT NULL
BEGIN
    ALTER DATABASE RESTAURANTE_ATENCION SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE RESTAURANTE_ATENCION;
END;
GO

-- CREAR BASE DE DATOS
CREATE DATABASE RESTAURANTE_ATENCION;
GO

USE RESTAURANTE_ATENCION;
GO

/* =========================================================
   TABLA: Meseros
   ========================================================= */
CREATE TABLE dbo.Meseros (
    MeseroID INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    FechaContratacion DATE NOT NULL,
    Turno VARCHAR(15) NOT NULL,
    Telefono VARCHAR(20) NULL,

    CONSTRAINT PK_Meseros PRIMARY KEY (MeseroID),
    CONSTRAINT CK_Meseros_Turno CHECK (Turno IN ('Mañana', 'Tarde', 'Noche')),
    CONSTRAINT CK_Meseros_FechaContratacion CHECK (FechaContratacion <= GETDATE())
);
GO

/* =========================================================
   TABLA: Mesas
   ========================================================= */
CREATE TABLE dbo.Mesas (
    MesaID INT IDENTITY(1,1) NOT NULL,
    NumeroMesa INT NOT NULL,
    Capacidad INT NOT NULL,
    Area VARCHAR(20) NOT NULL,
    Estado VARCHAR(15) NOT NULL,

    CONSTRAINT PK_Mesas PRIMARY KEY (MesaID),
    CONSTRAINT UQ_Mesas_NumeroMesa UNIQUE (NumeroMesa),
    CONSTRAINT CK_Mesas_Capacidad CHECK (Capacidad BETWEEN 2 AND 12),
    CONSTRAINT CK_Mesas_Area CHECK (Area IN ('Salón', 'Terraza', 'VIP')),
    CONSTRAINT CK_Mesas_Estado CHECK (Estado IN ('Disponible', 'Ocupada', 'Reservada'))
);
GO

/* =========================================================
   TABLA: Clientes
   ========================================================= */
CREATE TABLE dbo.Clientes (
    ClienteID INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Telefono VARCHAR(20) NULL,
    Email VARCHAR(100) NULL,

    CONSTRAINT PK_Clientes PRIMARY KEY (ClienteID),
    CONSTRAINT UQ_Clientes_Email UNIQUE (Email)
);
GO

/* =========================================================
   TABLA: Menu
   ========================================================= */
CREATE TABLE dbo.Menu (
    MenuID INT IDENTITY(1,1) NOT NULL,
    NombreItem VARCHAR(100) NOT NULL,
    Categoria VARCHAR(20) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Disponible BIT NOT NULL CONSTRAINT DF_Menu_Disponible DEFAULT (1),

    CONSTRAINT PK_Menu PRIMARY KEY (MenuID),
    CONSTRAINT UQ_Menu_NombreItem UNIQUE (NombreItem),
    CONSTRAINT CK_Menu_Categoria CHECK (Categoria IN ('Entrada', 'Plato Fuerte', 'Bebida', 'Postre')),
    CONSTRAINT CK_Menu_Precio CHECK (Precio > 0)
);
GO

/* =========================================================
   TABLA: Ordenes
   ========================================================= */
CREATE TABLE dbo.Ordenes (
    OrdenID INT IDENTITY(1,1) NOT NULL,
    ClienteID INT NOT NULL,
    MesaID INT NOT NULL,
    MeseroID INT NOT NULL,
    FechaOrden DATE NOT NULL,
    HoraOrden TIME NOT NULL,
    TipoDocumento VARCHAR(20) NOT NULL,
    MetodoPago VARCHAR(20) NOT NULL,
    TasaImpuesto DECIMAL(5,2) NOT NULL,
    TasaPropina DECIMAL(5,2) NOT NULL,

    CONSTRAINT PK_Ordenes PRIMARY KEY (OrdenID),

    CONSTRAINT FK_Ordenes_Clientes
        FOREIGN KEY (ClienteID) REFERENCES dbo.Clientes(ClienteID),

    CONSTRAINT FK_Ordenes_Mesas
        FOREIGN KEY (MesaID) REFERENCES dbo.Mesas(MesaID),

    CONSTRAINT FK_Ordenes_Meseros
        FOREIGN KEY (MeseroID) REFERENCES dbo.Meseros(MeseroID),

    CONSTRAINT CK_Ordenes_TipoDocumento CHECK (TipoDocumento IN ('Factura', 'Ticket')),
    CONSTRAINT CK_Ordenes_MetodoPago CHECK (MetodoPago IN ('Efectivo', 'Tarjeta', 'Transferencia')),
    CONSTRAINT CK_Ordenes_TasaImpuesto CHECK (TasaImpuesto BETWEEN 0 AND 25),
    CONSTRAINT CK_Ordenes_TasaPropina CHECK (TasaPropina BETWEEN 0 AND 20)
);
GO

/* =========================================================
   TABLA: DetalleOrden
   ========================================================= */
CREATE TABLE dbo.DetalleOrden (
    DetalleID INT IDENTITY(1,1) NOT NULL,
    OrdenID INT NOT NULL,
    MenuID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,

    CONSTRAINT PK_DetalleOrden PRIMARY KEY (DetalleID),

    CONSTRAINT FK_DetalleOrden_Ordenes
        FOREIGN KEY (OrdenID) REFERENCES dbo.Ordenes(OrdenID),

    CONSTRAINT FK_DetalleOrden_Menu
        FOREIGN KEY (MenuID) REFERENCES dbo.Menu(MenuID),

    CONSTRAINT CK_DetalleOrden_Cantidad CHECK (Cantidad >= 1),
    CONSTRAINT CK_DetalleOrden_PrecioUnitario CHECK (PrecioUnitario > 0)
);
GO

/* =========================================================
   DATOS: Meseros
   ========================================================= */
INSERT INTO dbo.Meseros (Nombre, Apellido, FechaContratacion, Turno, Telefono)
VALUES
('Carlos', 'Gómez', '2022-01-15', 'Mañana', '809-555-1001'),
('María', 'Fernández', '2021-06-10', 'Tarde', '809-555-1002'),
('José', 'Ramírez', '2023-03-05', 'Noche', '809-555-1003'),
('Lucía', 'Herrera', '2022-11-20', 'Mañana', '809-555-1004');
GO

/* =========================================================
   DATOS: Mesas
   ========================================================= */
INSERT INTO dbo.Mesas (NumeroMesa, Capacidad, Area, Estado)
VALUES
(1, 2, 'Salón', 'Disponible'),
(2, 4, 'Salón', 'Ocupada'),
(3, 6, 'Terraza', 'Disponible'),
(4, 4, 'Terraza', 'Reservada'),
(5, 8, 'VIP', 'Disponible'),
(6, 2, 'Salón', 'Ocupada');
GO

/* =========================================================
   DATOS: Clientes
   ========================================================= */
INSERT INTO dbo.Clientes (Nombre, Apellido, Telefono, Email)
VALUES
('Laura', 'Pérez', '809-555-2001', 'laura.perez@email.com'),
('Andrés', 'Martínez', '809-555-2002', 'andres.martinez@email.com'),
('Sofía', 'López', '809-555-2003', 'sofia.lopez@email.com'),
('Miguel', 'Castro', '809-555-2004', 'miguel.castro@email.com'),
('Daniela', 'Reyes', '809-555-2005', 'daniela.reyes@email.com');
GO

/* =========================================================
   DATOS: Menu
   ========================================================= */
INSERT INTO dbo.Menu (NombreItem, Categoria, Precio, Disponible)
VALUES
('Ensalada César', 'Entrada', 6.50, 1),
('Croquetas de Queso', 'Entrada', 5.75, 1),
('Hamburguesa Clásica', 'Plato Fuerte', 9.50, 1),
('Pasta Alfredo', 'Plato Fuerte', 11.00, 1),
('Salmón a la Plancha', 'Plato Fuerte', 16.50, 1),
('Jugo Natural', 'Bebida', 3.00, 1),
('Refresco', 'Bebida', 2.50, 1),
('Café', 'Bebida', 2.00, 1),
('Flan de Caramelo', 'Postre', 4.25, 1),
('Brownie con Helado', 'Postre', 5.50, 0);
GO

/* =========================================================
   DATOS: Ordenes
   ========================================================= */
INSERT INTO dbo.Ordenes
(ClienteID, MesaID, MeseroID, FechaOrden, HoraOrden, TipoDocumento, MetodoPago, TasaImpuesto, TasaPropina)
VALUES
(1, 2, 1, '2026-02-10', '12:30:00', 'Factura', 'Tarjeta', 18.00, 10.00),
(2, 1, 2, '2026-02-10', '13:15:00', 'Ticket', 'Efectivo', 18.00, 8.00),
(3, 3, 2, '2026-02-11', '19:40:00', 'Factura', 'Tarjeta', 18.00, 12.00),
(4, 5, 3, '2026-02-12', '20:10:00', 'Factura', 'Transferencia', 18.00, 15.00),
(1, 4, 4, '2026-02-13', '18:45:00', 'Ticket', 'Tarjeta', 18.00, 10.00),
(5, 6, 1, '2026-02-14', '21:05:00', 'Factura', 'Efectivo', 18.00, 12.00),
(2, 3, 3, '2026-02-15', '14:20:00', 'Ticket', 'Tarjeta', 18.00, 8.00),
(3, 2, 4, '2026-02-16', '12:50:00', 'Factura', 'Efectivo', 18.00, 10.00);
GO

/* =========================================================
   DATOS: DetalleOrden
   ========================================================= */
INSERT INTO dbo.DetalleOrden (OrdenID, MenuID, Cantidad, PrecioUnitario)
VALUES
(1, 3, 2, 9.50),
(1, 6, 2, 3.00),

(2, 1, 1, 6.50),
(2, 7, 2, 2.50),
(2, 9, 1, 4.25),

(3, 4, 1, 11.00),
(3, 6, 2, 3.00),
(3, 8, 1, 2.00),

(4, 5, 2, 16.50),
(4, 7, 3, 2.50),
(4, 9, 2, 4.25),

(5, 2, 2, 5.75),
(5, 3, 1, 9.50),
(5, 6, 1, 3.00),

(6, 5, 1, 16.50),
(6, 8, 2, 2.00),
(6, 9, 1, 4.25),

(7, 3, 2, 9.50),
(7, 7, 2, 2.50),

(8, 4, 1, 11.00),
(8, 1, 1, 6.50),
(8, 6, 2, 3.00),
(8, 9, 2, 4.25);
GO
