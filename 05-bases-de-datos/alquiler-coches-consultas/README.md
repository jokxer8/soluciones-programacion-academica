# Alquiler de Coches - Consultas SQL

## Descripción de la actividad

Consultas y evolución de un esquema con varios coches en una reserva.

---

## Objetivo

Aplicar relaciones muchos a muchos, consultas y ampliaciones de un esquema relacional.

---

## Conceptos aplicados

- Claves compuestas.
- Relación muchos a muchos.
- JOIN y subconsultas.
- GROUP BY y HAVING.
- ALTER TABLE.
- Funciones agregadas.

---

## Actividad desarrollada

La tabla Reserva_Coche separa la cabecera de la reserva de sus vehículos y precios. Los ejercicios consultan clientes, agencias, reservas, coches y garajes; después incorporan nacionalidad, provincia, apellidos, fecha de nacimiento y kilometraje.

---

## Ejemplo de análisis y uso

```sql
SELECT codigo_reserva, COUNT(*) AS CantidadCoches
FROM dbo.Reserva_Coche
GROUP BY codigo_reserva
HAVING COUNT(*) = 2;
```

Este ejemplo identifica reservas que contienen exactamente dos coches. El análisis completo explica el orden de las modificaciones y las consultas posteriores.

---

## Archivos de esta carpeta

```text
alquiler-coches-consultas/
├── README.md
├── desarrollo.md
├── 01-creacion.sql
├── 02-datos.sql
├── 03-consultas-basicas.sql
├── 04-modificaciones.sql
├── 05-actualizacion-datos.sql
├── 06-consultas-ampliadas.sql
├── 07-consultas-8-a-14.sql
└── modelo-relacional.pdf
```

- [01-creacion.sql](01-creacion.sql)
- [02-datos.sql](02-datos.sql)
- [03-consultas-basicas.sql](03-consultas-basicas.sql)
- [04-modificaciones.sql](04-modificaciones.sql)
- [05-actualizacion-datos.sql](05-actualizacion-datos.sql)
- [06-consultas-ampliadas.sql](06-consultas-ampliadas.sql)
- [07-consultas-8-a-14.sql](07-consultas-8-a-14.sql)
- [modelo-relacional.pdf](modelo-relacional.pdf)

El archivo [desarrollo.md](desarrollo.md) explica las decisiones y el alcance de la solución.

---

## Cómo revisar la actividad

Ejecutar los siete scripts numerados en orden, empezando por una base nueva. Las cargas dependen de identidades iniciales y los cambios de esquema no son idempotentes. Las variantes antiguas conservadas en originales no se ejecutan como migraciones adicionales.

---

## Relación con los archivos originales

Los archivos proceden de la carpeta académica Github indicada por el autor. El [inventario](../../archivos-originales/bases-de-datos/INVENTARIO.md) relaciona cada origen con su copia publicada y registra las versiones y duplicados.

## Lenguaje y herramientas

T-SQL para SQL Server.

## Estado

Actividad organizada y documentada como parte del portafolio académico de Bases de Datos. El [registro de validación](../VALIDACION.md) distingue la revisión realizada de las pruebas que requieren SQL Server.

[Volver a Bases de Datos](../README.md)
