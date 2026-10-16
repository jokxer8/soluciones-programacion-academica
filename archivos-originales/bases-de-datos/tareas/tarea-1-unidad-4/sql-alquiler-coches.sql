use master

create database ALQUILER_COCHES
use ALQUILER_COCHES

CREATE TABLE Agencia (
 codigo_agencia int identity(1,1) NOT NULL,
 nombre_agencia varchar(100) NOT NULL,

 CONSTRAINT PK_Agencia PRIMARY KEY (codigo_agencia)
)

CREATE TABLE Cliente (
 codigo_cliente int identity(1,1) NOT NULL,
 DNI varchar(15) NOT NULL,
 nombre varchar(100) NOT NULL,
 telefono varchar(20) NULL,
 direccion varchar(200) NULL,
 codigo_cliente_avala int NULL,
 DNI_cliente_avala varchar(15) NOT NULL
 
 CONSTRAINT PK_Cliente 
	PRIMARY KEY (codigo_cliente, DNI),
 CONSTRAINT FK_Cliente_Avala
	FOREIGN KEY (codigo_cliente_avala, DNI_cliente_avala) REFERENCES Cliente(codigo_cliente, DNI)
)

CREATE TABLE Reserva (
 codigo_reserva int identity(1,1) NOT NULL,
 litros decimal(6,2) NOT NULL,
 indicador varchar(50) NOT NULL,
 fecha_fin date NOT NULL,
 fecha_inicio date NOT NULL,
 codigo_cliente int NOT NULL,
 DNI_cliente varchar(15) NOT NULL,
 codigo_agencia int NOT NULL,

 CONSTRAINT PK_Reserva PRIMARY KEY (codigo_reserva),

 CONSTRAINT FK_Reserva_Cliente
	FOREIGN KEY (codigo_cliente, DNI_cliente) REFERENCES Cliente(codigo_cliente, DNI),

 CONSTRAINT FK_Reserva_Agencia
	FOREIGN KEY (codigo_agencia) REFERENCES Agencia(codigo_agencia)
)

CREATE TABLE Garaje (
 codigo_garaje int identity(1, 1) NOT NULL,
 nombre varchar(100) NOT NULL,

 CONSTRAINT PK_Garaje PRIMARY KEY (codigo_garaje)
)

CREATE TABLE Coche (
 matricula varchar(10) NOT NULL,
 modelo varchar(50) NOT NULL,
 marca varchar(50) NOT NULL,
 color varchar(30) NOT NULL,
 codigo_reserva int NULL,
 codigo_garaje int NOT NULL,

 CONSTRAINT PK_Coche PRIMARY KEY (matricula),

 CONSTRAINT FK_Coche_Reserva
	FOREIGN KEY (codigo_reserva) REFERENCES Reserva(codigo_reserva),

 CONSTRAINT FK_Coche_Garaje
	FOREIGN KEY (codigo_garaje) REFERENCES Garaje(codigo_garaje)
)