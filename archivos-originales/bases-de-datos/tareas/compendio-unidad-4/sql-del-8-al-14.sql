/* =========================================================
   8) Cantidad de reservas que incluyen exactamente dos coches,
      agrupadas por litros de gasolina de la reserva.
   ========================================================= */

SELECT
    Reserva.litros,
    COUNT(*) AS CantidadReservas
FROM Reserva
JOIN (
    SELECT codigo_reserva
    FROM Reserva_Coche
    GROUP BY codigo_reserva
    HAVING COUNT(*) = 2
) AS ReservasDeDosCoches
    ON ReservasDeDosCoches.codigo_reserva = Reserva.codigo_reserva
GROUP BY Reserva.litros
ORDER BY Reserva.litros;

/* =========================================================
   9) Promedio de edad de los clientes, pero SOLO de los que:
      - han reservado más de un coche (en total)
      - y han reservado en más de una agencia
   ========================================================= */

SELECT AVG(ClientesValidos.Edad) AS PromedioEdad
FROM (
    SELECT
        Cliente.codigo_cliente,
        DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE()) AS Edad
    FROM Cliente
    JOIN Reserva
        ON Reserva.codigo_cliente = Cliente.codigo_cliente
       AND Reserva.DNI_cliente = Cliente.DNI
    JOIN Reserva_Coche
        ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
    GROUP BY Cliente.codigo_cliente, Cliente.fecha_nacimiento
    HAVING COUNT(Reserva_Coche.matricula_coche) > 1
       AND COUNT(DISTINCT Reserva.codigo_agencia) > 1
) AS ClientesValidos;

/* =========================================================
   10) Cantidad de reservas de clientes de más de 30 años,
       ordenados de más a menos reservas.
   ========================================================= */

SELECT
    Cliente.codigo_cliente,
    Cliente.DNI,
    Cliente.nombre,
    Cliente.apellido,
    DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE()) AS Edad,
    COUNT(DISTINCT Reserva.codigo_reserva) AS CantidadReservas
FROM Cliente
LEFT JOIN Reserva
    ON Reserva.codigo_cliente = Cliente.codigo_cliente
   AND Reserva.DNI_cliente = Cliente.DNI
WHERE DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE()) > 30
GROUP BY
    Cliente.codigo_cliente, Cliente.DNI, Cliente.nombre, Cliente.apellido, Cliente.fecha_nacimiento
ORDER BY CantidadReservas DESC;

/* =========================================================
   11) Información de coches + información de reservas.
       Si un coche nunca se reservó, la reserva sale NULL.
   ========================================================= */

SELECT
    Coche.matricula,
    Coche.marca,
    Coche.modelo,
    Coche.color,
    Coche.anio_fabricacion,
    Reserva.codigo_reserva,
    Reserva.fecha_inicio,
    Reserva.fecha_fin,
    Reserva.litros
FROM Coche
LEFT JOIN Reserva_Coche
    ON Reserva_Coche.matricula_coche = Coche.matricula
LEFT JOIN Reserva
    ON Reserva.codigo_reserva = Reserva_Coche.codigo_reserva
ORDER BY Coche.matricula, Reserva.codigo_reserva;

/* =========================================================
   12) DNI, nombre, apellido y edad de todos los clientes,
       y una columna mas_de_una_reserva:
         - 'SI' si tiene más de una reserva
         - NULL si no
   ========================================================= */

SELECT
    Cliente.DNI,
    Cliente.nombre,
    Cliente.apellido,
    DATEDIFF(YEAR, Cliente.fecha_nacimiento, GETDATE()) AS Edad,
    CASE
        WHEN COUNT(DISTINCT Reserva.codigo_reserva) > 1 THEN 'SI'
        ELSE NULL
    END AS mas_de_una_reserva
FROM Cliente
LEFT JOIN Reserva
    ON Reserva.codigo_cliente = Cliente.codigo_cliente
   AND Reserva.DNI_cliente = Cliente.DNI
GROUP BY
    Cliente.DNI, Cliente.nombre, Cliente.apellido, Cliente.fecha_nacimiento
ORDER BY Cliente.apellido, Cliente.nombre;

/* =========================================================
   13) Información de todos los clientes con su nacionalidad.
       Deben salir:
        - todas las nacionalidades
        - todos los clientes
       (FULL OUTER JOIN)
   ========================================================= */

SELECT
    Nacionalidad.nacionalidad,
    Cliente.codigo_cliente,
    Cliente.DNI,
    Cliente.nombre,
    Cliente.apellido
FROM Nacionalidad
FULL OUTER JOIN Cliente
    ON Cliente.id_nacionalidad = Nacionalidad.id_nacionalidad
ORDER BY Nacionalidad.nacionalidad, Cliente.apellido, Cliente.nombre;

/* =========================================================
   14) Cantidad de reservas por agencia, agrupadas por marca y modelo del coche.
       Dirección legible en un solo atributo (simple):
       Calle + Edificio + Sector + Municipio + Provincia
   ========================================================= */

SELECT
    Agencia.nombre_agencia,
    (ISNULL(Agencia.calle,'') + ', ' +
     ISNULL(Agencia.edificio,'') + ', ' +
     ISNULL(Agencia.sector,'') + ', ' +
     ISNULL(Agencia.municipio,'') + ', ' +
     ISNULL(Provincia.provincia,'')) AS DireccionAgencia,
    Coche.marca,
    Coche.modelo,
    COUNT(DISTINCT Reserva.codigo_reserva) AS CantidadReservas
FROM Agencia
LEFT JOIN Provincia
    ON Provincia.id_provincia = Agencia.id_provincia
JOIN Reserva
    ON Reserva.codigo_agencia = Agencia.codigo_agencia
JOIN Reserva_Coche
    ON Reserva_Coche.codigo_reserva = Reserva.codigo_reserva
JOIN Coche
    ON Coche.matricula = Reserva_Coche.matricula_coche
GROUP BY
    Agencia.nombre_agencia,
    ISNULL(Agencia.calle,''),
    ISNULL(Agencia.edificio,''),
    ISNULL(Agencia.sector,''),
    ISNULL(Agencia.municipio,''),
    ISNULL(Provincia.provincia,''),
    Coche.marca,
    Coche.modelo
ORDER BY Agencia.nombre_agencia, Coche.marca, Coche.modelo;