-- MODIFICACIONES --

CREATE TABLE Nacionalidad (
    id_nacionalidad int identity(1,1) NOT NULL,
    nacionalidad varchar(200) NOT NULL,

    CONSTRAINT PK_Nacionalidad PRIMARY KEY (id_nacionalidad)
)

CREATE TABLE Provincia (
    id_provincia int identity(1,1) NOT NULL,
    provincia varchar(300) NOT NULL,

    CONSTRAINT PK_Provincia PRIMARY KEY (id_provincia)
)

-- Hay que separar el nombre de los apellidos ahora --
-- Las nuevas columnas not null tienen que tener un valor predefinido o poder ser nulos --

ALTER TABLE Cliente
    ADD apellido varchar(100) NOT NULL default '',
        fecha_nacimiento date NOT NULL default '',
        sexo char(1) NOT NULL default 'O',
        id_nacionalidad int NULL, --FK

    CONSTRAINT sexo CHECK (sexo IN ('M', 'F', 'O')), --M: masculino, F: femenino, O: otro

    CONSTRAINT FK_Cliente_Nacionalidad
        FOREIGN KEY (id_nacionalidad) REFERENCES Nacionalidad(id_nacionalidad);

ALTER TABLE Coche
    ADD anio_fabricacion int NULL;

-- Al ya nosotros haber puesto el atributo indicador con funcion de indicador de estados alias comentarios
-- Modificaremos la columna actual renombrandola comentario y crearemos una nueva columna de tipo entero
-- Indicador es reasignado como indicador_kilometraje cumpliendo la funcion de guardar el estado de kilometraje en el vehiculo al momento de la reserva

EXEC sp_rename 'Reserva.indicador', 'comentario', 'COLUMN'

ALTER TABLE Reserva
    ADD indicador_kilometraje INT NULL;

-- Id_provincia sera null temporalmente hasta anadirle la correcion a los datos

ALTER TABLE Agencia
    ADD calle varchar(100) NULL,
        edificio varchar(100) NULL,
        sector varchar(100) NULL,
        municipio varchar (100) NULL,
        id_provincia int NULL, --FK

    CONSTRAINT FK_Agencia_Provincia
        FOREIGN KEY (id_provincia) REFERENCES Provincia(id_provincia);