# Reservas de Coches - Creación y Carga de Datos

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](.) · [Evidencia](modelo-y-diccionario.pdf)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SQL%20/%20C%23-lightgrey)

## Qué trabajé y cómo leer los ejemplos

Creo un esquema con un coche por reserva y cargo datos de práctica. Una clave primaria identifica cada registro y una clave foránea enlaza registros relacionados. Los identificadores numéricos no expresan importes ni cantidades de coches.

## Descripción de la actividad

Esquema de reservas con un coche por reserva, restricciones y datos de prueba.

---

## Objetivo

Implementar tablas, relaciones y restricciones de integridad a partir de un modelo de reservas.

---

## Conceptos aplicados

- SQL Server y T-SQL.
- DDL y DML.
- Claves y restricciones.
- Relación recursiva de aval.
- Atributos derivados.

---

## Actividad desarrollada

Agencia y Garaje funcionan como catálogos. Cliente incluye una referencia opcional a su avalador. Coche pertenece a un garaje, mientras Reserva relaciona cliente, agencia y vehículo. La vista vw_ReservaConPrecioTotal calcula el precio derivado.

---

## Ejemplo de análisis y uso

```sql
SELECT *
FROM dbo.vw_ReservaConPrecioTotal;
```

La consulta devuelve las columnas definidas por la vista del ejercicio. Se presenta como ejemplo de uso; no se atribuyen resultados a una ejecución realizada durante esta revisión.

---

## Archivos de esta carpeta

```text
reservas-coches-creacion-datos/
├── README.md
├── desarrollo.md
├── 01-creacion.sql
├── 02-datos.sql
└── modelo-y-diccionario.pdf
```

- [01-creacion.sql](01-creacion.sql)
- [02-datos.sql](02-datos.sql)
- [modelo-y-diccionario.pdf](modelo-y-diccionario.pdf)

El archivo [desarrollo.md](desarrollo.md) explica las decisiones y el alcance de la solución.

---

## Diagrama de apoyo

[DER del ejercicio de la unidad 4](diagrama-ejercicio-unidad-4.png). Se conserva como referencia del ejercicio; las variantes de coches deben contrastarse con su propio script.

---

## Cómo revisar la actividad

En SQL Server, crear una base vacía BD_ReservasCoches y seleccionarla en SSMS. Ejecutar 01-creacion.sql y luego 02-datos.sql. El primer script elimina tablas existentes: usar una base de práctica. Esta variante no se combina con el esquema de varios coches por reserva.

---

## Relación con los archivos originales

Los enunciados, modelos y documentos de apoyo están relacionados en el [inventario de documentos](../../archivos-originales/bases-de-datos/INVENTARIO.md). Allí se pueden consultar las versiones publicadas y su tratamiento.

## Lenguaje y herramientas

T-SQL para SQL Server.

## Estado

Actividad organizada y documentada como parte del portafolio académico de Bases de Datos. El [registro de validación](../VALIDACION.md) distingue la revisión realizada de las pruebas que requieren SQL Server.

[Volver a Bases de Datos](../README.md)
