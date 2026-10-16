/* =========================================================
   MANDATO 2 - CONSULTAS (ALQUILER_COCHES) - SQL SERVER
   SIN VARIABLES
   VALORES TOMADOS DE LOS DATOS INSERTADOS (Ana Perez, Garaje A, etc.)
   ========================================================= */

USE ALQUILER_COCHES;
GO

/* =========================================================
   1) Obtener el nombre de todos los clientes.
   ========================================================= */
SELECT
    Cliente.nombre AS NombreCliente
FROM Cliente AS Cliente;


/* =========================================================
   2) Lo mismo que #1, ordenado por nombre ascendente.
   ========================================================= */
SELECT
    Cliente.nombre AS NombreCliente
FROM Cliente AS Cliente
ORDER BY Cliente.nombre ASC;


/* =========================================================
   3) Datos de todos los coches que ha reservado un cliente en particular.
      (Usando un valor REAL de tu inserción: 'Ana Perez')
   ========================================================= */
SELECT DISTINCT
    Coche.matricula     AS Matricula,
    Coche.modelo        AS Modelo,
    Coche.marca         AS Marca,
    Coche.color         AS Color,
    Coche.codigo_garaje AS CodigoGaraje
FROM Cliente AS Cliente
JOIN Reserva AS Reserva
    ON Reserva.codigo_cliente = Cliente.codigo_cliente
   AND Reserva.DNI_cliente = Cliente.DNI
JOIN Reserva_Coche AS ReservaCoche
    ON ReservaCoche.codigo_reserva = Reserva.codigo_reserva
JOIN Coche AS Coche
    ON Coche.matricula = ReservaCoche.matricula_coche
WHERE Cliente.nombre = 'Ana Perez';


/* =========================================================
   4) Datos de todos los coches de un garaje en específico.
      (Usando un valor REAL: 'Garaje A')
   ========================================================= */
SELECT
    Coche.matricula AS Matricula,
    Coche.modelo    AS Modelo,
    Coche.marca     AS Marca,
    Coche.color     AS Color,
    Garaje.nombre   AS NombreGaraje
FROM Coche AS Coche
JOIN Garaje AS Garaje
    ON Garaje.codigo_garaje = Coche.codigo_garaje
WHERE Garaje.nombre = 'Garaje A';


/* =========================================================
   5) Datos de clientes que han reservado coches en una agencia específica
      en una fecha dada.
      (Usando valores reales: 'Agencia Norte' y fecha '2026-02-10')
      Regla: la fecha cae dentro del rango de la reserva.
   ========================================================= */
SELECT DISTINCT
    Cliente.codigo_cliente AS CodigoCliente,
    Cliente.DNI            AS DNI,
    Cliente.nombre         AS NombreCompleto,
    Cliente.telefono       AS Telefono,
    Cliente.direccion      AS Direccion
FROM Reserva AS Reserva
JOIN Agencia AS Agencia
    ON Agencia.codigo_agencia = Reserva.codigo_agencia
JOIN Cliente AS Cliente
    ON Cliente.codigo_cliente = Reserva.codigo_cliente
   AND Cliente.DNI = Reserva.DNI_cliente
WHERE Agencia.nombre_agencia = 'Agencia Norte'
  AND '2026-02-10' BETWEEN Reserva.fecha_inicio AND Reserva.fecha_fin;


/* =========================================================
   6) Fecha inicio, fin e importe total de reservas con litros > 15.
      ImporteTotal = SUM(precio_alquiler * dias)
      dias = DATEDIFF(DAY, inicio, fin) + 1
   ========================================================= */
SELECT
    Reserva.codigo_reserva AS CodigoReserva,
    Reserva.fecha_inicio   AS FechaInicio,
    Reserva.fecha_fin      AS FechaFin,
    SUM(
        ReservaCoche.precio_alquiler *
        (DATEDIFF(DAY, Reserva.fecha_inicio, Reserva.fecha_fin) + 1)
    ) AS ImporteTotal
FROM Reserva AS Reserva
JOIN Reserva_Coche AS ReservaCoche
    ON ReservaCoche.codigo_reserva = Reserva.codigo_reserva
WHERE Reserva.litros > 15
GROUP BY
    Reserva.codigo_reserva,
    Reserva.fecha_inicio,
    Reserva.fecha_fin;


/* =========================================================
   7) Igual que #6, pero solo para reservas avaladas por clientes
      cuyo AVALADOR tenga apellido terminado en 'ez'.
      (Apellido = última palabra del nombre del avalador)
   ========================================================= */
SELECT
    Reserva.codigo_reserva AS CodigoReserva,
    Reserva.fecha_inicio   AS FechaInicio,
    Reserva.fecha_fin      AS FechaFin,
    SUM(
        ReservaCoche.precio_alquiler *
        (DATEDIFF(DAY, Reserva.fecha_inicio, Reserva.fecha_fin) + 1)
    ) AS ImporteTotal
FROM Reserva AS Reserva
JOIN Reserva_Coche AS ReservaCoche
    ON ReservaCoche.codigo_reserva = Reserva.codigo_reserva
JOIN Cliente AS ClienteQueReserva
    ON ClienteQueReserva.codigo_cliente = Reserva.codigo_cliente
   AND ClienteQueReserva.DNI = Reserva.DNI_cliente
JOIN Cliente AS ClienteAvalador
    ON ClienteAvalador.codigo_cliente = ClienteQueReserva.codigo_cliente_avala
   AND ClienteAvalador.DNI = ClienteQueReserva.DNI_cliente_avala
WHERE Reserva.litros > 15
  AND (
        CASE
            WHEN CHARINDEX(' ', LTRIM(RTRIM(ClienteAvalador.nombre))) = 0
                THEN LTRIM(RTRIM(ClienteAvalador.nombre))
            ELSE RIGHT(
                LTRIM(RTRIM(ClienteAvalador.nombre)),
                CHARINDEX(' ', REVERSE(LTRIM(RTRIM(ClienteAvalador.nombre)) + ' ')) - 1
            )
        END
      ) LIKE '%ez'
GROUP BY
    Reserva.codigo_reserva,
    Reserva.fecha_inicio,
    Reserva.fecha_fin;


/* =========================================================
   8) Información de clientes que reservan en un mes dado del año
      y cuyo color NO es blanco NI negro.
      (Con tus datos: Febrero 2026)
   ========================================================= */
SELECT DISTINCT
    Cliente.codigo_cliente AS CodigoCliente,
    Cliente.DNI            AS DNI,
    Cliente.nombre         AS NombreCompleto,
    Cliente.telefono       AS Telefono,
    Cliente.direccion      AS Direccion
FROM Reserva AS Reserva
JOIN Cliente AS Cliente
    ON Cliente.codigo_cliente = Reserva.codigo_cliente
   AND Cliente.DNI = Reserva.DNI_cliente
JOIN Reserva_Coche AS ReservaCoche
    ON ReservaCoche.codigo_reserva = Reserva.codigo_reserva
JOIN Coche AS Coche
    ON Coche.matricula = ReservaCoche.matricula_coche
WHERE YEAR(Reserva.fecha_inicio) = 2026
  AND MONTH(Reserva.fecha_inicio) = 2
  AND UPPER(Coche.color) <> 'BLANCO'
  AND UPPER(Coche.color) <> 'NEGRO';


/* =========================================================
   9) Código y nombre completo de todos los clientes
      y de todos los clientes que avalan a otros (misma lista, sin repetición).
      OJO: si los avaladores ya son clientes, el resultado puede ser
      exactamente la lista completa de clientes (eso es normal).
   ========================================================= */
SELECT
    Cliente.codigo_cliente AS CodigoCliente,
    Cliente.nombre         AS NombreCompleto
FROM Cliente AS Cliente
UNION
SELECT
    ClienteAvalador.codigo_cliente AS CodigoCliente,
    ClienteAvalador.nombre         AS NombreCompleto
FROM Cliente AS ClienteAvalado
JOIN Cliente AS ClienteAvalador
  ON ClienteAvalador.codigo_cliente = ClienteAvalado.codigo_cliente_avala
 AND ClienteAvalador.DNI = ClienteAvalado.DNI_cliente_avala;


/* =========================================================
   9B) (EXTRA para que VEAS quien avala a quien)
   ========================================================= */
SELECT
    ClienteAvalado.codigo_cliente   AS CodigoClienteAvalado,
    ClienteAvalado.DNI              AS DNIAvalado,
    ClienteAvalado.nombre           AS NombreAvalado,
    ClienteAvalador.codigo_cliente  AS CodigoClienteAvalador,
    ClienteAvalador.DNI             AS DNIAvalador,
    ClienteAvalador.nombre          AS NombreAvalador
FROM Cliente AS ClienteAvalado
JOIN Cliente AS ClienteAvalador
  ON ClienteAvalador.codigo_cliente = ClienteAvalado.codigo_cliente_avala
 AND ClienteAvalador.DNI = ClienteAvalado.DNI_cliente_avala
ORDER BY
    ClienteAvalador.nombre ASC,
    ClienteAvalado.nombre ASC;


/* =========================================================
   10) Lo mismo que #9, pero ordenado por:
       - Apellido DESC
       - Nombre(s) ASC
       (Apellido = última palabra; Nombre(s) = resto)
   ========================================================= */
SELECT
    ListaFinal.CodigoCliente,
    ListaFinal.NombreCompleto
FROM
(
    SELECT
        Cliente.codigo_cliente AS CodigoCliente,
        Cliente.nombre         AS NombreCompleto
    FROM Cliente AS Cliente
    UNION
    SELECT
        ClienteAvalador.codigo_cliente AS CodigoCliente,
        ClienteAvalador.nombre         AS NombreCompleto
    FROM Cliente AS ClienteAvalado
    JOIN Cliente AS ClienteAvalador
      ON ClienteAvalador.codigo_cliente = ClienteAvalado.codigo_cliente_avala
     AND ClienteAvalador.DNI = ClienteAvalado.DNI_cliente_avala
) AS ListaFinal
ORDER BY
    /* Apellido DESC */
    (
        CASE
            WHEN CHARINDEX(' ', LTRIM(RTRIM(ListaFinal.NombreCompleto))) = 0
                THEN LTRIM(RTRIM(ListaFinal.NombreCompleto))
            ELSE RIGHT(
                LTRIM(RTRIM(ListaFinal.NombreCompleto)),
                CHARINDEX(' ', REVERSE(LTRIM(RTRIM(ListaFinal.NombreCompleto)) + ' ')) - 1
            )
        END
    ) DESC,

    /* Nombres ASC */
    (
        CASE
            WHEN CHARINDEX(' ', LTRIM(RTRIM(ListaFinal.NombreCompleto))) = 0
                THEN LTRIM(RTRIM(ListaFinal.NombreCompleto))
            ELSE LTRIM(RTRIM(
                LEFT(
                    LTRIM(RTRIM(ListaFinal.NombreCompleto)),
                    LEN(LTRIM(RTRIM(ListaFinal.NombreCompleto))) -
                    CHARINDEX(' ', REVERSE(LTRIM(RTRIM(ListaFinal.NombreCompleto)) + ' '))
                )
            ))
        END
    ) ASC;