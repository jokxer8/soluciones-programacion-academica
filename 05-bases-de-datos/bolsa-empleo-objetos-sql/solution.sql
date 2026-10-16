USE Bolsa_Empleo;
GO

/*
SUPUESTO NECESARIO:
Para poder calcular ofertas por semana y por mes se necesita una fecha de publicacion.
Si la copia local no la tiene, este bloque la crea.
*/
IF COL_LENGTH('dbo.Oferta_Empresa', 'fecha_publicacion') IS NULL
BEGIN
    ALTER TABLE dbo.Oferta_Empresa
    ADD fecha_publicacion DATE NULL;
END;
GO

UPDATE dbo.Oferta_Empresa
SET fecha_publicacion = ISNULL(fecha_publicacion, CAST(GETDATE() AS DATE));
GO

/* 1) Vista: cantidad de ofertas por semana, por empresa y por ciudad */
CREATE OR ALTER VIEW dbo.vw_OfertasPorSemanaEmpresaCiudad
AS
SELECT
    e.RNC,
    e.Nombre AS nombre_empresa,
    c.id_ciudad,
    c.nombre_ciudad,
    YEAR(oe.fecha_publicacion) AS anio,
    DATEPART(ISO_WEEK, oe.fecha_publicacion) AS semana,
    COUNT(*) AS cantidad_ofertas
FROM dbo.Oferta_Empresa AS oe
INNER JOIN dbo.Empresa AS e
    ON oe.RNC = e.RNC
INNER JOIN dbo.Ciudad AS c
    ON oe.id_ciudad = c.id_ciudad
WHERE oe.fecha_publicacion IS NOT NULL
GROUP BY
    e.RNC,
    e.Nombre,
    c.id_ciudad,
    c.nombre_ciudad,
    YEAR(oe.fecha_publicacion),
    DATEPART(ISO_WEEK, oe.fecha_publicacion);
GO

/* Consulta de prueba */
SELECT *
FROM dbo.vw_OfertasPorSemanaEmpresaCiudad
ORDER BY anio, semana, nombre_empresa, nombre_ciudad;
GO

/* 2) Procedimiento almacenado: total de ofertas por mes en un rango de fechas */
CREATE OR ALTER PROCEDURE dbo.sp_TotalOfertasPorMes
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SET NOCOUNT ON;

    IF @fecha_inicio IS NULL OR @fecha_fin IS NULL
    BEGIN
        RAISERROR('Debe indicar una fecha de inicio y una fecha fin.', 16, 1);
        RETURN;
    END;

    IF @fecha_inicio > @fecha_fin
    BEGIN
        RAISERROR('La fecha de inicio no puede ser mayor que la fecha fin.', 16, 1);
        RETURN;
    END;

    SELECT
        YEAR(fecha_publicacion) AS anio,
        MONTH(fecha_publicacion) AS numero_mes,
        DATENAME(MONTH, fecha_publicacion) AS nombre_mes,
        COUNT(*) AS total_ofertas
    FROM dbo.Oferta_Empresa
    WHERE fecha_publicacion >= @fecha_inicio
      AND fecha_publicacion < DATEADD(DAY, 1, @fecha_fin)
    GROUP BY
        YEAR(fecha_publicacion),
        MONTH(fecha_publicacion),
        DATENAME(MONTH, fecha_publicacion)
    ORDER BY
        anio,
        numero_mes;
END;
GO

/* Ejecucion de prueba */
EXEC dbo.sp_TotalOfertasPorMes '2025-01-01', '2025-12-31';
GO

/* 3) Funcion: validar cedula dominicana con Luhn */
CREATE OR ALTER FUNCTION dbo.fn_ValidarCedulaDominicana (@cedula VARCHAR(20))
RETURNS BIT
AS
BEGIN
    DECLARE @limpia VARCHAR(20);
    DECLARE @i INT;
    DECLARE @posicion INT;
    DECLARE @digito INT;
    DECLARE @valor INT;
    DECLARE @suma INT;
    DECLARE @digito_control INT;

    SET @limpia = REPLACE(REPLACE(REPLACE(ISNULL(@cedula, ''), '-', ''), ' ', ''), '.', '');

    IF LEN(@limpia) <> 11 OR @limpia LIKE '%[^0-9]%'
        RETURN 0;

    SET @i = 10;           -- se trabajan los primeros 10 digitos
    SET @posicion = 1;     -- posicion desde la derecha
    SET @suma = 0;

    WHILE @i >= 1
    BEGIN
        SET @digito = CONVERT(INT, SUBSTRING(@limpia, @i, 1));

        IF @posicion % 2 = 1
            SET @valor = @digito * 2;
        ELSE
            SET @valor = @digito;

        IF @valor > 9
            SET @valor = @valor - 9;

        SET @suma = @suma + @valor;
        SET @i = @i - 1;
        SET @posicion = @posicion + 1;
    END;

    SET @digito_control = (10 - (@suma % 10)) % 10;

    IF @digito_control = CONVERT(INT, RIGHT(@limpia, 1))
        RETURN 1;

    RETURN 0;
END;
GO

/* Pruebas de la funcion */
SELECT dbo.fn_ValidarCedulaDominicana('001-1391820-5') AS cedula_valida;
SELECT dbo.fn_ValidarCedulaDominicana('001-1391820-4') AS cedula_invalida;
GO

/* 4) Tabla de auditoria */
IF OBJECT_ID('dbo.auditoria_empresa', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.auditoria_empresa
    (
        id_auditoria INT IDENTITY(1,1) PRIMARY KEY,
        tipo_accion VARCHAR(10) NOT NULL,
        fecha_evento DATETIME NOT NULL DEFAULT GETDATE(),
        usuario_bd SYSNAME NOT NULL DEFAULT SUSER_SNAME(),
        RNC VARCHAR(20) NULL,
        nombre_anterior VARCHAR(150) NULL,
        nombre_nuevo VARCHAR(150) NULL,
        telefono_anterior VARCHAR(30) NULL,
        telefono_nuevo VARCHAR(30) NULL,
        direccion_anterior VARCHAR(200) NULL,
        direccion_nueva VARCHAR(200) NULL,
        id_ciudad_anterior INT NULL,
        id_ciudad_nueva INT NULL
    );
END;
GO

/* Trigger para INSERT */
CREATE OR ALTER TRIGGER dbo.trg_empresa_insert
ON dbo.Empresa
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.auditoria_empresa
    (
        tipo_accion,
        fecha_evento,
        usuario_bd,
        RNC,
        nombre_nuevo,
        telefono_nuevo,
        direccion_nueva,
        id_ciudad_nueva
    )
    SELECT
        'INSERT',
        GETDATE(),
        SUSER_SNAME(),
        i.RNC,
        i.Nombre,
        i.Telefono,
        i.Direccion,
        i.id_ciudad
    FROM inserted AS i;
END;
GO

/* Trigger para UPDATE */
CREATE OR ALTER TRIGGER dbo.trg_empresa_update
ON dbo.Empresa
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.auditoria_empresa
    (
        tipo_accion,
        fecha_evento,
        usuario_bd,
        RNC,
        nombre_anterior,
        nombre_nuevo,
        telefono_anterior,
        telefono_nuevo,
        direccion_anterior,
        direccion_nueva,
        id_ciudad_anterior,
        id_ciudad_nueva
    )
    SELECT
        'UPDATE',
        GETDATE(),
        SUSER_SNAME(),
        i.RNC,
        d.Nombre,
        i.Nombre,
        d.Telefono,
        i.Telefono,
        d.Direccion,
        i.Direccion,
        d.id_ciudad,
        i.id_ciudad
    FROM inserted AS i
    INNER JOIN deleted AS d
        ON i.RNC = d.RNC;
END;
GO

/* Trigger para DELETE */
CREATE OR ALTER TRIGGER dbo.trg_empresa_delete
ON dbo.Empresa
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.auditoria_empresa
    (
        tipo_accion,
        fecha_evento,
        usuario_bd,
        RNC,
        nombre_anterior,
        telefono_anterior,
        direccion_anterior,
        id_ciudad_anterior
    )
    SELECT
        'DELETE',
        GETDATE(),
        SUSER_SNAME(),
        d.RNC,
        d.Nombre,
        d.Telefono,
        d.Direccion,
        d.id_ciudad
    FROM deleted AS d;
END;
GO

/* Consulta de verificacion */
SELECT *
FROM dbo.auditoria_empresa
ORDER BY id_auditoria DESC;
GO
