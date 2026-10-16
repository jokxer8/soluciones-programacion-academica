USE RESTAURANTE_ATENCION;

/* 1. Obtenga el nombre y apellido de todos los meseros que hayan atendido ordenes 
en las que la tasa de propina haya sido mayor a 10. No debe mostrarse registros repetidos */

SELECT DISTINCT
    m.Nombre,
    m.Apellido
FROM dbo.Meseros AS m
INNER JOIN dbo.Ordenes AS o
    ON m.MeseroID = o.MeseroID
WHERE o.TasaPropina > 10;

/* 2. Calcule la suma total pagada en las ordenes donde se haya consumido al menos un producto de la categoria
'Postre'. Para este calculo, considere como total pagado la suma de cantidad * PrecioUnitario de los detalles
de cada orden, añadiendo tambien la TasaImpuesto */

SELECT
    SUM(x.Subtotal + x.TasaImpuesto) AS SumaTotalPagada
FROM (
    SELECT
        o.OrdenID,
        SUM(d.Cantidad * d.PrecioUnitario) AS Subtotal,
        o.TasaImpuesto
    FROM dbo.Ordenes AS o
    INNER JOIN dbo.DetalleOrden AS d
        ON o.OrdenID = d.OrdenID
    WHERE EXISTS (
        SELECT 1
        FROM dbo.DetalleOrden AS d2
        INNER JOIN dbo.Menu AS m2
            ON d2.MenuID = m2.MenuID
        WHERE d2.OrdenID = o.OrdenID
          AND m2.Categoria = 'Postre'
    )
    GROUP BY o.OrdenID, o.TasaImpuesto
) AS x;

/* 3. Obtenga el nombre y apellido de los clientes junto con las fechas de sus ordenes. para aquellos clientes
que hayan realizado mas de una orden y cuyo subtotal por orden supere los 20. Ordene el resultado por nombre en forma
ascendente y fecha de orden en forma descendente. */

WITH Subtotales AS (
    SELECT
        o.OrdenID,
        o.ClienteID,
        o.FechaOrden,
        SUM(d.Cantidad * d.PrecioUnitario) AS Subtotal
    FROM dbo.Ordenes AS o
    INNER JOIN dbo.DetalleOrden AS d
        ON o.OrdenID = d.OrdenID
    GROUP BY o.OrdenID, o.ClienteID, o.FechaOrden
)
SELECT
    c.Nombre,
    c.Apellido,
    s.FechaOrden
FROM Subtotales AS s
INNER JOIN dbo.Clientes AS c
    ON s.ClienteID = c.ClienteID
WHERE s.Subtotal > 20
  AND s.ClienteID IN (
      SELECT ClienteID
      FROM dbo.Ordenes
      GROUP BY ClienteID
      HAVING COUNT(*) > 1
  )
ORDER BY c.Nombre ASC, s.FechaOrden DESC;

/* 4. Liste las categorias del menu, los nombres de los productos, la cantidad total de unidades vendidas por producto,
para la ordenes realizadas entre el 10 de febrero de 2026 y el 15 de febrero de 2026 */

SELECT
    m.Categoria,
    m.NombreItem,
    SUM(d.Cantidad) AS CantidadTotalVendida,
    SUM(d.Cantidad * d.PrecioUnitario) AS TotalVendido
FROM dbo.Ordenes AS o
INNER JOIN dbo.DetalleOrden AS d
    ON o.OrdenID = d.OrdenID
INNER JOIN dbo.Menu AS m
    ON d.MenuID = m.MenuID
WHERE o.FechaOrden >= '2026-02-10' AND o.FechaOrden < '2026-02-16'
GROUP BY
    m.Categoria,
    m.NombreItem
ORDER BY
    m.Categoria,
    m.NombreItem;

/* 5. Cree una vista que muestre el porcentaje promedio de propina aplicado en las ordenes, agrupado por tipo de documento.
Debe nombrar la vista como view_e_lb, sustituyendo lb por sus iniciales. */

GO
CREATE OR ALTER VIEW dbo.view_e_kdct
AS
SELECT
    TipoDocumento,
    AVG(CAST(TasaPropina AS DECIMAL(10,2))) AS PorcentajePromedioPropina
FROM dbo.Ordenes
GROUP BY TipoDocumento;

/* 6. Cree una funcion escala que reciba como parametro un tipo de documento y devuelva la tabla de propina promedio de todas
las ordenes emitidas con ese tipo. Debe nombrar la funcion como fn_f_lb, sustituyendo lb por sus iniciales. */

GO
CREATE OR ALTER FUNCTION dbo.fn_f_kdct (@TipoDocumento VARCHAR(20))
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Promedio DECIMAL(10,2);

    SELECT
        @Promedio = AVG(CAST(TasaPropina AS DECIMAL(10,2)))
    FROM dbo.Ordenes
    WHERE TipoDocumento = @TipoDocumento;

    RETURN ISNULL(@Promedio, 0);
END;
GO


    SELECT dbo.fn_f_kdct('Factura') AS PromedioFactura,
       dbo.fn_f_kdct('Ticket') AS PromedioTicket;