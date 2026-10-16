# Desarrollo: Reservas de Coches: Creación y Carga de Datos

Transformar un modelo de reservas en tablas con restricciones y datos de ejemplo.

## Diseño

Agencia y Garaje son catálogos. Cliente incluye una referencia opcional a otro cliente como avalador. Coche pertenece a un garaje; Reserva referencia cliente, agencia y coche. La vista vw_ReservaConPrecioTotal calcula un atributo derivado.

## Orden de ejecución

1. Crear una base vacía llamada BD_ReservasCoches y seleccionarla en SSMS.
2. Ejecutar 01-creacion.sql.
3. Ejecutar 02-datos.sql.
4. Consultar las tablas y dbo.vw_ReservaConPrecioTotal.

Los scripts proceden de la tarea 1 de la unidad 4. El de creación elimina tablas si ya existen; debe utilizarse en una base de práctica. Esta variante admite un coche por reserva y no se combina con los scripts de la actividad de alquiler con varios coches.

## Relación con la actividad

Agencia y Garaje funcionan como catálogos. Cliente incluye una referencia opcional a su avalador. Coche pertenece a un garaje, mientras Reserva relaciona cliente, agencia y vehículo. La vista vw_ReservaConPrecioTotal calcula el precio derivado.

```sql
SELECT *
FROM dbo.vw_ReservaConPrecioTotal;
```

La consulta devuelve las columnas definidas por la vista del ejercicio. Se presenta como ejemplo de uso; no se atribuyen resultados a una ejecución realizada durante esta revisión.

## Alcance de la revisión

Consultar [VALIDACION.md](../VALIDACION.md) para distinguir el análisis documental y sintáctico de los resultados de ejecución. Las copias públicas conservan las soluciones y los créditos; los identificadores institucionales detectados en texto fueron retirados.
