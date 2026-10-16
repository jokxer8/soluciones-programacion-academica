# Desarrollo: Bolsa de Empleo: Vistas, Procedimientos y Auditoría

Implementar objetos de base de datos para reportes y trazabilidad sobre el esquema Bolsa_Empleo.

## Objetos

- vw_OfertasPorSemanaEmpresaCiudad agrupa por empresa, ciudad y semana.
- sp_TotalOfertasPorMes recibe un rango y devuelve totales mensuales.
- fn_ValidarCedulaDominicana limpia el formato y comprueba un dígito de control con Luhn. Su resultado no acredita la existencia ni la asignación oficial de un documento.
- Tres triggers registran inserciones, cambios y eliminaciones de Empresa en auditoria_empresa.

## Dependencias y supuestos

El script requiere una base Bolsa_Empleo existente con las tablas y columnas del caso. No se encontró un script de creación completo de esa base entre los entregables. El trabajo añade fecha_publicacion si falta y sustituye fechas nulas por la fecha actual: ese relleno sirve como supuesto de práctica, pero no reconstruye fechas históricas reales.

La agrupación combina YEAR con ISO_WEEK; cerca del cambio de año puede no coincidir con el año ISO. Se documenta esta limitación del entregable conservado.

## Alcance de la publicación

Se mantiene el script académico sin presentar sus supuestos como datos reales. Las pruebas escritas en el script son ejemplos y no resultados de una ejecución realizada durante la organización.

## Relación con la actividad

La solución incluye una vista semanal por empresa y ciudad, un procedimiento de totales mensuales, una función de comprobación del dígito de control y tres triggers sobre Empresa. Los cambios quedan registrados en auditoria_empresa.

```sql
EXEC dbo.sp_TotalOfertasPorMes
    @fecha_inicio = '2025-01-01',
    @fecha_fin = '2025-12-31';
```

El procedimiento devuelve totales mensuales dentro del intervalo. La función de cédula comprueba formato y dígito de control; no confirma identidad ni asignación oficial.

## Alcance de la revisión

Consultar [VALIDACION.md](../VALIDACION.md) para distinguir el análisis documental y sintáctico de los resultados de ejecución. Las copias públicas conservan las soluciones y los créditos; los identificadores institucionales detectados en texto fueron retirados.
