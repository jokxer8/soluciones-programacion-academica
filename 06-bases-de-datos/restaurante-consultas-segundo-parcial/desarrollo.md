# Desarrollo: Restaurante: Consultas del Segundo Parcial

Resolver seis requerimientos de consulta y programación SQL sobre RESTAURANTE_ATENCION.

## Consultas

1. Meseros que atendieron órdenes con propina superior a 10, sin duplicados.
2. Total de órdenes que contienen al menos un postre: EXISTS selecciona las órdenes y la agregación suma todos sus detalles. Se mantiene la suma directa de TasaImpuesto indicada en el enunciado.
3. Clientes con más de una orden y subtotal por orden mayor a 20, mediante una CTE.
4. Unidades e importe por categoría y producto dentro del intervalo solicitado.
5. Vista del promedio de propina por tipo de documento.
6. Función escalar del promedio para un tipo recibido como parámetro.

## Correcciones de presentación ejecutable

El original SQLQueryKelvinCesar.sql se conserva en archivos-originales. En solution.sql se añaden GO antes de CREATE VIEW y CREATE FUNCTION, se cierra el bloque de la función con END y se mueve su consulta de ejemplo fuera de ella. Se usa CREATE OR ALTER y el filtro de fechas incluye todo el 15 de febrero mediante un límite superior exclusivo del día 16. No se alteran las respuestas originales conservadas.

## Preparación

01-base.sql recrea la base RESTAURANTE_ATENCION y carga los datos del ejercicio: elimina una base homónima si existe. Ejecutar únicamente en un entorno de práctica. Después ejecutar solution.sql.

## Relación con la actividad

Las respuestas identifican meseros, calculan importes de órdenes con postres, seleccionan clientes frecuentes, agrupan ventas por producto y calculan promedios de propina por tipo de documento.

```sql
SELECT dbo.fn_f_kdct('Factura') AS PromedioFactura,
       dbo.fn_f_kdct('Ticket') AS PromedioTicket;
```

La consulta utiliza la función escalar del ejercicio para devolver el promedio de propina de cada tipo de documento. La versión organizada corrige el cierre de la función y separa la consulta de prueba de su definición.

## Alcance de la revisión

Consultar [VALIDACION.md](../VALIDACION.md) para distinguir el análisis documental y sintáctico de los resultados de ejecución. Las copias públicas conservan las soluciones y los créditos; los identificadores institucionales detectados en texto fueron retirados.
