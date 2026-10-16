# Desarrollo: Alquiler de Coches: Consultas y Evolución del Esquema

Construir y consultar una base de alquiler donde una reserva puede incluir varios coches.

## Modelo

Reserva_Coche separa la cabecera de la reserva de sus vehículos y precios. Cliente utiliza una clave compuesta y un aval con referencia a esa misma clave. Las consultas recorren cliente, agencia, reserva, coche y garaje.

## Secuencia propuesta

1. 01-creacion.sql crea ALQUILER_COCHES y sus tablas. Se agregaron separadores GO tras CREATE DATABASE y USE para ejecutar por lotes.
2. 02-datos.sql carga el conjunto inicial; presupone identidades de una base nueva.
3. 03-consultas-basicas.sql contiene consultas sobre ese esquema.
4. 04-modificaciones.sql añade apellidos, nacimiento, nacionalidad, provincias y kilometraje, y renombra indicador a comentario. Se añadió USE y GO para fijar el contexto.
5. 05-actualizacion-datos.sql completa información del esquema ampliado.
6. 06-consultas-ampliadas.sql y 07-consultas-8-a-14.sql contienen consultas posteriores a las modificaciones.

## Límites

Las cargas y cambios no son idempotentes. No ejecutar las variantes antiguas del esquema sobre esta secuencia. Se conserva la lógica de los ejercicios; algunos valores predeterminados y supuestos de fechas requieren revisión para un uso real.

## Relación con la actividad

La tabla Reserva_Coche separa la cabecera de la reserva de sus vehículos y precios. Los ejercicios consultan clientes, agencias, reservas, coches y garajes; después incorporan nacionalidad, provincia, apellidos, fecha de nacimiento y kilometraje.

```sql
SELECT codigo_reserva, COUNT(*) AS CantidadCoches
FROM dbo.Reserva_Coche
GROUP BY codigo_reserva
HAVING COUNT(*) = 2;
```

Este ejemplo identifica reservas que contienen exactamente dos coches. El análisis completo explica el orden de las modificaciones y las consultas posteriores.

## Alcance de la revisión

Consultar [VALIDACION.md](../VALIDACION.md) para distinguir el análisis documental y sintáctico de los resultados de ejecución. Las copias públicas conservan las soluciones y los créditos; los identificadores institucionales detectados en texto fueron retirados.
