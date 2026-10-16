/* =========================================================
   CONSULTAS (SQL SERVER) - POST MODIFICACIONES
   BD: ALQUILER_COCHES
   Requiere: Reserva_Coche para reservar 2 coches en una reserva
   ========================================================= */

USE ALQUILER_COCHES;
GO

/* =========================================================
  1) Obtener la información completa de todas las reservas
     de un cliente en una agencia dada.
     (Cambia el DNI y el nombre de la agencia según necesites)
   ========================================================= */
SELECT
    Cliente.codigo_cliente            AS CodigoCliente,
    Cliente.DNI                      AS DNICliente,
    Cliente.nombre                   AS NombreCliente,
    Cliente.apellido                 AS ApellidoCliente,

    Agencia.codigo_agencia           AS CodigoAgencia,
    Agencia.nombre_agencia           AS NombreAgencia,

    Reserva.codigo_reserva           AS CodigoReserva,
    Reserva.fecha_inicio             AS FechaInicio,
    Reserva.fecha_fin                AS FechaFin,
    Reserva.litros                   AS Litros,
    Reserva.comentario               AS ComentarioReserva,
    Reserva.indicador_kilometraje    AS IndicadorKilometraje,

    Coche.matricula                  AS MatriculaCoche,
    Coche.marca                      AS MarcaCoche,
    Coche.modelo                     AS ModeloCoche,
    Coche.color                      AS ColorCoche,
    Coche.anio_fabricacion           AS AnioFabricacion,
    Reserva_Coche.precio_alquiler    AS PrecioAlquiler
FROM Reserva AS Reserva
JOIN Cliente AS Cliente
    ON Cliente.codigo_cliente = Reserva.codigo_cliente
   AND Cliente.DNI = Reserva.DNI_cliente
JOIN Agencia AS Agencia
    ON Agencia.codigo_agencia = Reserva.codigo_agencia
JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
JOIN Coche AS Coche
    ON Coche.matricula = Reserva_Coche.matricula_coche
WHERE Cliente.DNI = '001-0000001-1'
  AND Agencia.nombre_agencia = 'Agencia Norte'
ORDER BY Reserva.codigo_reserva ASC, Coche.matricula ASC;


/* =========================================================
  2) Para cada coche indique la cantidad de reservas que ha tenido,
     ordenado por cantidad de reservas descendentemente.
   ========================================================= */
SELECT
    Coche.matricula AS MatriculaCoche,
    Coche.marca     AS MarcaCoche,
    Coche.modelo    AS ModeloCoche,
    Coche.color     AS ColorCoche,
    COUNT(DISTINCT Reserva_Coche.codigo_reserva) AS CantidadReservas
FROM Coche AS Coche
LEFT JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.matricula_coche = Coche.matricula
GROUP BY
    Coche.matricula, Coche.marca, Coche.modelo, Coche.color
ORDER BY
    CantidadReservas DESC, Coche.matricula ASC;


/* =========================================================
  3) Obtener el promedio del precio alquiler de los vehículos, por agencia.
   ========================================================= */
SELECT
    Agencia.codigo_agencia AS CodigoAgencia,
    Agencia.nombre_agencia AS NombreAgencia,
    AVG(Reserva_Coche.precio_alquiler) AS PromedioPrecioAlquiler
FROM Agencia AS Agencia
JOIN Reserva AS Reserva
    ON Reserva.codigo_agencia = Agencia.codigo_agencia
JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
GROUP BY
    Agencia.codigo_agencia, Agencia.nombre_agencia
ORDER BY
    Agencia.codigo_agencia ASC;


/* =========================================================
  4) Lo mismo que la anterior, pero solo los que superen 10,000 pesos.
   ========================================================= */
SELECT
    Agencia.codigo_agencia AS CodigoAgencia,
    Agencia.nombre_agencia AS NombreAgencia,
    AVG(Reserva_Coche.precio_alquiler) AS PromedioPrecioAlquiler
FROM Agencia AS Agencia
JOIN Reserva AS Reserva
    ON Reserva.codigo_agencia = Agencia.codigo_agencia
JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
GROUP BY
    Agencia.codigo_agencia, Agencia.nombre_agencia
HAVING
    AVG(Reserva_Coche.precio_alquiler) > 10000
ORDER BY
    PromedioPrecioAlquiler DESC;


/* =========================================================
  5) Obtener los clientes con más de 5 coches reservados,
     organizados por el año de la fecha inicio de la reserva (ASC).
     (Cuenta coches por cliente por año)
   ========================================================= */
SELECT
    YEAR(Reserva.fecha_inicio) AS AnioReserva,
    Cliente.codigo_cliente     AS CodigoCliente,
    Cliente.DNI               AS DNICliente,
    Cliente.nombre            AS NombreCliente,
    Cliente.apellido          AS ApellidoCliente,
    COUNT(Reserva_Coche.matricula_coche) AS CantidadCochesReservados
FROM Cliente AS Cliente
JOIN Reserva AS Reserva
    ON Reserva.codigo_cliente = Cliente.codigo_cliente
   AND Reserva.DNI_cliente = Cliente.DNI
JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
GROUP BY
    YEAR(Reserva.fecha_inicio),
    Cliente.codigo_cliente, Cliente.DNI, Cliente.nombre, Cliente.apellido
HAVING
    COUNT(Reserva_Coche.matricula_coche) > 5
ORDER BY
    AnioReserva ASC, CantidadCochesReservados DESC;


/* =========================================================
  6) Obtener el total de reservas entre dos fechas de fin específicas.
     (Cambia las dos fechas)
   ========================================================= */
SELECT
    COUNT(*) AS TotalReservas
FROM Reserva AS Reserva
WHERE Reserva.fecha_fin BETWEEN '2026-01-01' AND '2026-02-28';


/* =========================================================
  7) Obtener la cantidad de reservas que incluyen exactamente dos coches.
   ========================================================= */
SELECT
    COUNT(*) AS CantidadReservasConDosCoches
FROM
(
    SELECT
        Reserva_Coche.codigo_reserva AS CodigoReserva
    FROM Reserva_Coche AS Reserva_Coche
    GROUP BY Reserva_Coche.codigo_reserva
    HAVING COUNT(*) = 2
) AS ReservasExactamenteDosCoches;


/* =========================================================
  8) Lo mismo que la anterior, pero agrupados por la cantidad de litros.
   ========================================================= */
SELECT
    Reserva.litros AS LitrosReserva,
    COUNT(*)       AS CantidadReservasConDosCoches
FROM Reserva AS Reserva
JOIN
(
    SELECT
        Reserva_Coche.codigo_reserva AS CodigoReserva
    FROM Reserva_Coche AS Reserva_Coche
    GROUP BY Reserva_Coche.codigo_reserva
    HAVING COUNT(*) = 2
) AS ReservasExactamenteDosCoches
    ON ReservasExactamenteDosCoches.CodigoReserva = Reserva.codigo_reserva
GROUP BY
    Reserva.litros
ORDER BY
    Reserva.litros ASC;


/* =========================================================
  9) Obtener el promedio de edad de los clientes,
     pero solo para clientes que:
       - hayan reservado más de un coche (en total)
       - y hayan hecho reservas en más de una agencia
   ========================================================= */
SELECT
    AVG(
        DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE())
        - CASE
            WHEN DATEADD(YEAR, DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE()), Cliente.fecha_nacimiento) > GETDATE()
                THEN 1
            ELSE 0
          END
    ) AS PromedioEdadClientes
FROM Cliente AS Cliente
WHERE
    (
        SELECT COUNT(*)
        FROM Reserva AS Reserva
        JOIN Reserva_Coche AS Reserva_Coche
            ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
        WHERE Reserva.codigo_cliente = Cliente.codigo_cliente
          AND Reserva.DNI_cliente = Cliente.DNI
    ) > 1
AND
    (
        SELECT COUNT(DISTINCT Reserva.codigo_agencia)
        FROM Reserva AS Reserva
        WHERE Reserva.codigo_cliente = Cliente.codigo_cliente
          AND Reserva.DNI_cliente = Cliente.DNI
    ) > 1;


/* =========================================================
  10) Cantidad de reservas que tienen los clientes de más de 30 años,
      ordenados por los que tienen más a los que tienen menos reservas.
   ========================================================= */
SELECT
    Cliente.codigo_cliente AS CodigoCliente,
    Cliente.DNI            AS DNICliente,
    Cliente.nombre         AS NombreCliente,
    Cliente.apellido       AS ApellidoCliente,
    (
        DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE())
        - CASE
            WHEN DATEADD(YEAR, DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE()), Cliente.fecha_nacimiento) > GETDATE()
                THEN 1
            ELSE 0
          END
    ) AS EdadCliente,
    COUNT(DISTINCT Reserva.codigo_reserva) AS CantidadReservas
FROM Cliente AS Cliente
LEFT JOIN Reserva AS Reserva
    ON Reserva.codigo_cliente = Cliente.codigo_cliente
   AND Reserva.DNI_cliente = Cliente.DNI
WHERE
    (
        DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE())
        - CASE
            WHEN DATEADD(YEAR, DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE()), Cliente.fecha_nacimiento) > GETDATE()
                THEN 1
            ELSE 0
          END
    ) > 30
GROUP BY
    Cliente.codigo_cliente, Cliente.DNI, Cliente.nombre, Cliente.apellido, Cliente.fecha_nacimiento
ORDER BY
    CantidadReservas DESC, Cliente.apellido ASC, Cliente.nombre ASC;


/* =========================================================
  11) Información de coches con info de reservas hechas de ellos.
      Si un coche nunca se ha reservado, info de reserva NULL.
   ========================================================= */
SELECT
    Coche.matricula           AS MatriculaCoche,
    Coche.marca               AS MarcaCoche,
    Coche.modelo              AS ModeloCoche,
    Coche.color               AS ColorCoche,
    Coche.anio_fabricacion    AS AnioFabricacion,

    Reserva.codigo_reserva    AS CodigoReserva,
    Reserva.fecha_inicio      AS FechaInicio,
    Reserva.fecha_fin         AS FechaFin,
    Reserva.litros            AS Litros,
    Reserva.comentario        AS ComentarioReserva,

    Agencia.nombre_agencia    AS NombreAgencia,
    Cliente.nombre            AS NombreCliente,
    Cliente.apellido          AS ApellidoCliente
FROM Coche AS Coche
LEFT JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.matricula_coche = Coche.matricula
LEFT JOIN Reserva AS Reserva
    ON Reserva.codigo_reserva = Reserva_Coche.codigo_reserva
LEFT JOIN Agencia AS Agencia
    ON Agencia.codigo_agencia = Reserva.codigo_agencia
LEFT JOIN Cliente AS Cliente
    ON Cliente.codigo_cliente = Reserva.codigo_cliente
   AND Cliente.DNI = Reserva.DNI_cliente
ORDER BY
    Coche.matricula ASC, Reserva.codigo_reserva ASC;


/* =========================================================
  12) DNI, nombre, apellido y edad de todos los clientes y una columna:
      mas_de_una_reserva = 'SI' si tiene >1 reserva, si no NULL.
   ========================================================= */
SELECT
    Cliente.DNI AS DNICliente,
    Cliente.nombre AS NombreCliente,
    Cliente.apellido AS ApellidoCliente,
    (
        DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE())
        - CASE
            WHEN DATEADD(YEAR, DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE()), Cliente.fecha_nacimiento) > GETDATE()
                THEN 1
            ELSE 0
          END
    ) AS EdadCliente,
    CASE
        WHEN COUNT(DISTINCT Reserva.codigo_reserva) > 1 THEN 'SI'
        ELSE NULL
    END AS MasDeUnaReserva
FROM Cliente AS Cliente
LEFT JOIN Reserva AS Reserva
    ON Reserva.codigo_cliente = Cliente.codigo_cliente
   AND Reserva.DNI_cliente = Cliente.DNI
GROUP BY
    Cliente.DNI, Cliente.nombre, Cliente.apellido, Cliente.fecha_nacimiento
ORDER BY
    Cliente.apellido ASC, Cliente.nombre ASC;


/* =========================================================
  13) Información de todos los clientes con su nacionalidad.
      Deben salir:
        - todas las nacionalidades
        - todos los clientes
      Clientes sin nacionalidad => nacionalidad NULL
      Nacionalidades sin clientes => cliente NULL
   ========================================================= */
SELECT
    Nacionalidad.id_nacionalidad AS IdNacionalidad,
    Nacionalidad.nacionalidad    AS Nacionalidad,
    Cliente.codigo_cliente       AS CodigoCliente,
    Cliente.DNI                  AS DNICliente,
    Cliente.nombre               AS NombreCliente,
    Cliente.apellido             AS ApellidoCliente
FROM Nacionalidad AS Nacionalidad
FULL OUTER JOIN Cliente AS Cliente
    ON Cliente.id_nacionalidad = Nacionalidad.id_nacionalidad
ORDER BY
    Nacionalidad.nacionalidad ASC,
    Cliente.apellido ASC,
    Cliente.nombre ASC;


/* =========================================================
  14) Cantidad de reservas de cada agencia agrupadas por marca y modelo del coche.
      Sacar en un solo atributo la dirección legible de la agencia.
   ========================================================= */
SELECT
    Agencia.codigo_agencia AS CodigoAgencia,
    Agencia.nombre_agencia AS NombreAgencia,

    /* Dirección legible */
    LTRIM(RTRIM(
        COALESCE(Agencia.calle, '') +
        CASE WHEN Agencia.calle IS NOT NULL AND Agencia.calle <> '' THEN ' ' ELSE '' END +
        COALESCE(Agencia.edificio, '') +
        CASE WHEN Agencia.sector IS NOT NULL AND Agencia.sector <> '' THEN ', ' ELSE '' END +
        COALESCE(Agencia.sector, '') +
        CASE WHEN Agencia.municipio IS NOT NULL AND Agencia.municipio <> '' THEN ', ' ELSE '' END +
        COALESCE(Agencia.municipio, '') +
        CASE WHEN Provincia.provincia IS NOT NULL AND Provincia.provincia <> '' THEN ', ' ELSE '' END +
        COALESCE(Provincia.provincia, '')
    )) AS DireccionAgencia,

    Coche.marca  AS MarcaCoche,
    Coche.modelo AS ModeloCoche,
    COUNT(DISTINCT Reserva.codigo_reserva) AS CantidadReservas
FROM Agencia AS Agencia
LEFT JOIN Provincia AS Provincia
    ON Provincia.id_provincia = Agencia.id_provincia
JOIN Reserva AS Reserva
    ON Reserva.codigo_agencia = Agencia.codigo_agencia
JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
JOIN Coche AS Coche
    ON Coche.matricula = Reserva_Coche.matricula_coche
GROUP BY
    Agencia.codigo_agencia, Agencia.nombre_agencia,
    Agencia.calle, Agencia.edificio, Agencia.sector, Agencia.municipio,
    Provincia.provincia,
    Coche.marca, Coche.modelo
ORDER BY
    Agencia.codigo_agencia ASC, Coche.marca ASC, Coche.modelo ASC;


/* =========================================================
  15) Cantidad de coches por año de fabricación y color,
      ordenando por año DESC y color ASC.
   ========================================================= */
SELECT
    Coche.anio_fabricacion AS AnioFabricacion,
    Coche.color            AS ColorCoche,
    COUNT(*)               AS CantidadCoches
FROM Coche AS Coche
GROUP BY
    Coche.anio_fabricacion, Coche.color
ORDER BY
    Coche.anio_fabricacion DESC,
    Coche.color ASC;


/* =========================================================
  16) Obtener los meses que han transcurrido desde la última reserva de cada cliente.
      (Si nunca ha reservado, devolver NULL)
   ========================================================= */
SELECT
    Cliente.codigo_cliente AS CodigoCliente,
    Cliente.DNI            AS DNICliente,
    Cliente.nombre         AS NombreCliente,
    Cliente.apellido       AS ApellidoCliente,
    DATEDIFF(MONTH, MAX(Reserva.fecha_fin), GETDATE()) AS MesesDesdeUltimaReserva
FROM Cliente AS Cliente
LEFT JOIN Reserva AS Reserva
    ON Reserva.codigo_cliente = Cliente.codigo_cliente
   AND Reserva.DNI_cliente = Cliente.DNI
GROUP BY
    Cliente.codigo_cliente, Cliente.DNI, Cliente.nombre, Cliente.apellido
ORDER BY
    Cliente.codigo_cliente ASC;


/* =========================================================
  17) Datos de todos los coches con nombres de clientes dominicanos que los han reservado,
      reemplazando en el apellido del cliente la vocal 'a' por 'x'.
   ========================================================= */
SELECT DISTINCT
    Coche.matricula AS MatriculaCoche,
    Coche.marca     AS MarcaCoche,
    Coche.modelo    AS ModeloCoche,
    Coche.color     AS ColorCoche,

    Cliente.DNI     AS DNICliente,
    Cliente.nombre  AS NombreCliente,
    REPLACE(REPLACE(Cliente.apellido, 'a', 'x'), 'A', 'x') AS ApellidoModificado
FROM Coche AS Coche
JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.matricula_coche = Coche.matricula
JOIN Reserva AS Reserva
    ON Reserva.codigo_reserva = Reserva_Coche.codigo_reserva
JOIN Cliente AS Cliente
    ON Cliente.codigo_cliente = Reserva.codigo_cliente
   AND Cliente.DNI = Reserva.DNI_cliente
JOIN Nacionalidad AS Nacionalidad
    ON Nacionalidad.id_nacionalidad = Cliente.id_nacionalidad
WHERE Nacionalidad.nacionalidad = 'Dominicano'
ORDER BY
    Coche.matricula ASC, Cliente.apellido ASC, Cliente.nombre ASC;


/* =========================================================
  18) Comentarios (reserva.comentario) en mayúscula, solo los que existen,
      para todas las reservas de un cliente dado.
      (Cambia el DNI)
   ========================================================= */
SELECT
    Reserva.codigo_reserva AS CodigoReserva,
    Reserva.fecha_inicio   AS FechaInicio,
    Reserva.fecha_fin      AS FechaFin,
    UPPER(Reserva.comentario) AS ComentarioEnMayuscula
FROM Reserva AS Reserva
WHERE Reserva.DNI_cliente = '001-0000001-1'
  AND Reserva.comentario IS NOT NULL
  AND LTRIM(RTRIM(Reserva.comentario)) <> ''
ORDER BY
    Reserva.codigo_reserva ASC;


/* =========================================================
  19) Devuelve SI/NO (IGUALES) si el mes de nacimiento del cliente
      es igual al mes de alguna de sus reservas (inicio o fin).
   ========================================================= */
SELECT
    Cliente.codigo_cliente AS CodigoCliente,
    Cliente.DNI            AS DNICliente,
    Cliente.nombre         AS NombreCliente,
    Cliente.apellido       AS ApellidoCliente,
    Cliente.fecha_nacimiento AS FechaNacimiento,
    CASE
        WHEN EXISTS
        (
            SELECT 1
            FROM Reserva AS Reserva
            WHERE Reserva.codigo_cliente = Cliente.codigo_cliente
              AND Reserva.DNI_cliente = Cliente.DNI
              AND
              (
                  MONTH(Reserva.fecha_inicio) = MONTH(Cliente.fecha_nacimiento)
                  OR MONTH(Reserva.fecha_fin) = MONTH(Cliente.fecha_nacimiento)
              )
        )
        THEN 'SI'
        ELSE 'NO'
    END AS IGUALES
FROM Cliente AS Cliente
ORDER BY
    Cliente.codigo_cliente ASC;


/* =========================================================
  20) Historial completo (reservas, carros, agencias y garajes)
      para las reservas que NO tienen comentarios.
      (comentario NULL o vacío)
   ========================================================= */
SELECT
    Cliente.codigo_cliente AS CodigoCliente,
    Cliente.DNI            AS DNICliente,
    Cliente.nombre         AS NombreCliente,
    Cliente.apellido       AS ApellidoCliente,

    Reserva.codigo_reserva AS CodigoReserva,
    Reserva.fecha_inicio   AS FechaInicio,
    Reserva.fecha_fin      AS FechaFin,
    Reserva.litros         AS Litros,
    Reserva.indicador_kilometraje AS IndicadorKilometraje,
    Reserva.comentario     AS ComentarioReserva,

    Agencia.codigo_agencia AS CodigoAgencia,
    Agencia.nombre_agencia AS NombreAgencia,
    Agencia.calle          AS CalleAgencia,
    Agencia.edificio       AS EdificioAgencia,
    Agencia.sector         AS SectorAgencia,
    Agencia.municipio      AS MunicipioAgencia,

    Provincia.provincia    AS ProvinciaAgencia,

    Coche.matricula        AS MatriculaCoche,
    Coche.marca            AS MarcaCoche,
    Coche.modelo           AS ModeloCoche,
    Coche.color            AS ColorCoche,
    Coche.anio_fabricacion AS AnioFabricacion,

    Garaje.codigo_garaje   AS CodigoGaraje,
    Garaje.nombre          AS NombreGaraje,

    Reserva_Coche.precio_alquiler AS PrecioAlquiler
FROM Reserva AS Reserva
JOIN Cliente AS Cliente
    ON Cliente.codigo_cliente = Reserva.codigo_cliente
   AND Cliente.DNI = Reserva.DNI_cliente
JOIN Agencia AS Agencia
    ON Agencia.codigo_agencia = Reserva.codigo_agencia
LEFT JOIN Provincia AS Provincia
    ON Provincia.id_provincia = Agencia.id_provincia
JOIN Reserva_Coche AS Reserva_Coche
    ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
JOIN Coche AS Coche
    ON Coche.matricula = Reserva_Coche.matricula_coche
JOIN Garaje AS Garaje
    ON Garaje.codigo_garaje = Coche.codigo_garaje
WHERE Reserva.comentario IS NULL
   OR LTRIM(RTRIM(Reserva.comentario)) = ''
ORDER BY
    Cliente.codigo_cliente ASC,
    Reserva.codigo_reserva ASC,
    Coche.matricula ASC;