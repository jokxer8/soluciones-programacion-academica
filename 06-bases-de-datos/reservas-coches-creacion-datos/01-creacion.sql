/* ============================================================
   01_creacion_bd_reservas.sql
   Base de Datos: Reservas de Coches
   Motor: SQL Server (T-SQL)
   Contenido:
   - Creación de BD (opcional)
   - Creación de tablas con PK, FK y restricciones
   - Vista para atributo derivado precio_total
   ============================================================ */

-- (Opcional) Crear y usar base de datos
-- CREATE DATABASE BD_ReservasCoches;
-- GO
-- USE BD_ReservasCoches;
-- GO

/* =========================
   TABLAS MAESTRAS
   ========================= */

IF OBJECT_ID('dbo.Reserva', 'U') IS NOT NULL DROP TABLE dbo.Reserva;
IF OBJECT_ID('dbo.Coche', 'U')   IS NOT NULL DROP TABLE dbo.Coche;
IF OBJECT_ID('dbo.Cliente', 'U') IS NOT NULL DROP TABLE dbo.Cliente;
IF OBJECT_ID('dbo.Agencia', 'U') IS NOT NULL DROP TABLE dbo.Agencia;
IF OBJECT_ID('dbo.Garaje', 'U')  IS NOT NULL DROP TABLE dbo.Garaje;
GO

CREATE TABLE dbo.Agencia (
    codigo INT NOT NULL,
    nombre VARCHAR(80) NOT NULL,
    CONSTRAINT PK_Agencia PRIMARY KEY (codigo)
);
GO

CREATE TABLE dbo.Garaje (
    codigo INT NOT NULL,
    nombre VARCHAR(80) NOT NULL,
    CONSTRAINT PK_Garaje PRIMARY KEY (codigo)
);
GO

/* =========================
   CLIENTE (incluye relación recursiva "avala")
   - Un cliente puede ser avalado por 0..1 cliente (FK nullable)
   ========================= */

CREATE TABLE dbo.Cliente (
    codigo          INT NOT NULL,
    DNI             VARCHAR(20) NOT NULL,
    nombre          VARCHAR(80) NOT NULL,
    telefono        VARCHAR(30) NULL,
    direccion       VARCHAR(150) NULL,
    codigo_avalador INT NULL,

    CONSTRAINT PK_Cliente PRIMARY KEY (codigo),
    CONSTRAINT UQ_Cliente_DNI UNIQUE (DNI),
    CONSTRAINT FK_Cliente_Avalador
        FOREIGN KEY (codigo_avalador) REFERENCES dbo.Cliente(codigo)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);
GO

/* =========================
   COCHE
   - Cada coche pertenece a 1 garaje (FK NOT NULL)
   ========================= */

CREATE TABLE dbo.Coche (
    matricula     VARCHAR(15) NOT NULL,
    modelo        VARCHAR(60) NOT NULL,
    marca         VARCHAR(60) NOT NULL,
    color         VARCHAR(40) NULL,
    codigo_garaje INT NOT NULL,

    CONSTRAINT PK_Coche PRIMARY KEY (matricula),
    CONSTRAINT FK_Coche_Garaje
        FOREIGN KEY (codigo_garaje) REFERENCES dbo.Garaje(codigo)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);
GO

/* =========================
   RESERVA
   - Cliente 1:N Reserva  => FK codigo_cliente
   - Agencia 1:N Reserva  => FK codigo_agencia
   - Coche 1:N Reserva    => FK matricula_coche (1 coche por reserva)
   - precio_alquiler: atributo de la relación Reserva–Coche (se almacena aquí)
   - precio_total: derivado (NO se almacena)
   ========================= */

CREATE TABLE dbo.Reserva (
    codigo          INT NOT NULL,
    fecha_inicio    DATE NOT NULL,
    fecha_fin       DATE NOT NULL,
    litros          DECIMAL(10,2) NULL,
    indicador       VARCHAR(30) NULL,
    precio_alquiler DECIMAL(12,2) NOT NULL,

    codigo_cliente  INT NOT NULL,
    codigo_agencia  INT NOT NULL,
    matricula_coche VARCHAR(15) NOT NULL,

    CONSTRAINT PK_Reserva PRIMARY KEY (codigo),

    CONSTRAINT FK_Reserva_Cliente
        FOREIGN KEY (codigo_cliente) REFERENCES dbo.Cliente(codigo)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,

    CONSTRAINT FK_Reserva_Agencia
        FOREIGN KEY (codigo_agencia) REFERENCES dbo.Agencia(codigo)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,

    CONSTRAINT FK_Reserva_Coche
        FOREIGN KEY (matricula_coche) REFERENCES dbo.Coche(matricula)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,

    -- Restricciones recomendadas
    CONSTRAINT CK_Reserva_Fechas CHECK (fecha_fin >= fecha_inicio),
    CONSTRAINT CK_Reserva_Litros CHECK (litros IS NULL OR litros >= 0),
    CONSTRAINT CK_Reserva_Precio CHECK (precio_alquiler >= 0)
);
GO

/* =========================
   VISTA: precio_total (atributo derivado en el DER)
   Nota: fórmula propuesta = (días de reserva) * precio_alquiler
   Ajustar si la consigna define otra fórmula.
   ========================= */

IF OBJECT_ID('dbo.vw_ReservaConPrecioTotal', 'V') IS NOT NULL
    DROP VIEW dbo.vw_ReservaConPrecioTotal;
GO

CREATE VIEW dbo.vw_ReservaConPrecioTotal AS
SELECT
    r.codigo,
    r.fecha_inicio,
    r.fecha_fin,
    r.litros,
    r.indicador,
    r.precio_alquiler,
    r.codigo_cliente,
    r.codigo_agencia,
    r.matricula_coche,
    (DATEDIFF(DAY, r.fecha_inicio, r.fecha_fin) + 1) * r.precio_alquiler AS precio_total
FROM dbo.Reserva r;
GO
