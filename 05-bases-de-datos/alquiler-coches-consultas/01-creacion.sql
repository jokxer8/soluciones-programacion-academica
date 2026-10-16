CREATE DATABASE ALQUILER_COCHES;
GO
USE ALQUILER_COCHES;
GO

/* =========================================================
   TABLA: AGENCIA
   ========================================================= */
CREATE TABLE Agencia (
    codigo_agencia INT IDENTITY(1,1) NOT NULL,
    nombre_agencia VARCHAR(100) NOT NULL,

    CONSTRAINT PK_Agencia PRIMARY KEY (codigo_agencia)
);

/* =========================================================
   TABLA: CLIENTE
   PK compuesta: (codigo_cliente, DNI)
   Relación recursiva: avala (0..1) => FK compuesta nullable
   ========================================================= */
CREATE TABLE Cliente (
    codigo_cliente INT IDENTITY(1,1) NOT NULL,
    DNI            VARCHAR(15) NOT NULL,
    nombre         VARCHAR(100) NOT NULL,
    telefono       VARCHAR(20) NULL,
    direccion      VARCHAR(200) NULL,

    codigo_cliente_avala INT NULL,
    DNI_cliente_avala    VARCHAR(15) NULL,

    CONSTRAINT PK_Cliente PRIMARY KEY (codigo_cliente, DNI),

    CONSTRAINT FK_Cliente_Avala
        FOREIGN KEY (codigo_cliente_avala, DNI_cliente_avala)
        REFERENCES Cliente(codigo_cliente, DNI),

    CONSTRAINT CK_Cliente_Avala_Pareja
        CHECK (
            (codigo_cliente_avala IS NULL AND DNI_cliente_avala IS NULL) OR
            (codigo_cliente_avala IS NOT NULL AND DNI_cliente_avala IS NOT NULL)
        )
);

/* =========================================================
   TABLA: GARAJE
   ========================================================= */
CREATE TABLE Garaje (
    codigo_garaje INT IDENTITY(1,1) NOT NULL,
    nombre        VARCHAR(100) NOT NULL,

    CONSTRAINT PK_Garaje PRIMARY KEY (codigo_garaje)
);

/* =========================================================
   TABLA: COCHE
   Cada coche pertenece a un garaje
   ========================================================= */
CREATE TABLE Coche (
    matricula     VARCHAR(10) NOT NULL,
    modelo        VARCHAR(50) NOT NULL,
    marca         VARCHAR(50) NOT NULL,
    color         VARCHAR(30) NOT NULL,
    codigo_garaje INT NOT NULL,

    CONSTRAINT PK_Coche PRIMARY KEY (matricula),

    CONSTRAINT FK_Coche_Garaje
        FOREIGN KEY (codigo_garaje) REFERENCES Garaje(codigo_garaje)
);

/* =========================================================
   TABLA: RESERVA (CABECERA)
   Una reserva pertenece a un cliente (FK compuesta)
   Una reserva la gestiona una agencia
   NOTA: ya NO guardamos matricula_coche aquí (porque puede haber N coches)
   ========================================================= */
CREATE TABLE Reserva (
    codigo_reserva INT IDENTITY(1,1) NOT NULL,
    litros         DECIMAL(6,2) NOT NULL,
    indicador      VARCHAR(50) NOT NULL,
    fecha_inicio   DATE NOT NULL,
    fecha_fin      DATE NOT NULL,

    codigo_cliente INT NOT NULL,
    DNI_cliente    VARCHAR(15) NOT NULL,

    codigo_agencia INT NOT NULL,

    CONSTRAINT PK_Reserva PRIMARY KEY (codigo_reserva),

    CONSTRAINT FK_Reserva_Cliente
        FOREIGN KEY (codigo_cliente, DNI_cliente)
        REFERENCES Cliente(codigo_cliente, DNI),

    CONSTRAINT FK_Reserva_Agencia
        FOREIGN KEY (codigo_agencia)
        REFERENCES Agencia(codigo_agencia),

    CONSTRAINT CK_Reserva_Fechas CHECK (fecha_fin >= fecha_inicio),
    CONSTRAINT CK_Reserva_Litros CHECK (litros >= 0)
);

/* =========================================================
   TABLA: RESERVA_COCHE (DETALLE)
   Permite: 1 reserva -> N coches
   precio_alquiler = atributo de la relación (Reserva - Coche)
   ========================================================= */
CREATE TABLE Reserva_Coche (
    codigo_reserva  INT NOT NULL,
    matricula_coche VARCHAR(10) NOT NULL,
    precio_alquiler DECIMAL(10,2) NOT NULL,

    CONSTRAINT PK_Reserva_Coche PRIMARY KEY (codigo_reserva, matricula_coche),

    CONSTRAINT FK_Reserva_Coche_Reserva
        FOREIGN KEY (codigo_reserva) REFERENCES Reserva(codigo_reserva),

    CONSTRAINT FK_Reserva_Coche_Coche
        FOREIGN KEY (matricula_coche) REFERENCES Coche(matricula),

    CONSTRAINT CK_Reserva_Coche_Precio CHECK (precio_alquiler >= 0)
);