# Desarrollo: Cine: Modelado y Normalización

Analizar la estructura de un sistema de cine y reducir redundancias en un ejemplo de ventas de boletos.

## Modelado del cine

El DER y el modelo relacional del ejercicio 5.1 se acompañan de un diccionario de datos. La transformación identifica entidades y relaciones para establecer claves y referencias.

## Normalización de ventas

El ejercicio 5.3 parte de datos con película, formato y tipo de asiento en un mismo campo. La primera forma normal separa valores atómicos. Las siguientes etapas presentan tablas de clientes, películas, formatos, tipos de asiento, territorios, monedas, productos y ventas.

La evidencia describe el diseño propuesto en el trabajo. La validez de las dependencias depende de las reglas del caso; por ejemplo, la asignación de una moneda a un territorio debe revisarse antes de generalizarla a un sistema real.

## Alcance

Los ejercicios de cine son entregables relacionados pero distintos; el DER 5.1 y la normalización 5.3 no se presentan como un único esquema implementado.

## Relación con la actividad

El ejercicio 5.1 presenta el DER del cine, su modelo relacional y el diccionario. El ejercicio 5.3 recorre la separación de datos de ventas de boletos en clientes, películas, formatos, tipos de asiento, monedas, territorios y ventas. La transformación de hospital se conserva como parte del compendio.

El dato compuesto «Película: Dune Part Two, Formato: IMAX 2D, Tipo de asiento: Adulto» se descompone en atributos identificables. Los catálogos de la propuesta permiten referenciar esos conceptos desde el producto o la venta.

## Alcance de la revisión

Consultar [VALIDACION.md](../VALIDACION.md) para distinguir el análisis documental y sintáctico de los resultados de ejecución. Las copias públicas conservan las soluciones y los créditos; los identificadores institucionales detectados en texto fueron retirados.
