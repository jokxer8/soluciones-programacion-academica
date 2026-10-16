# Reservas de Coches - Creación y Carga de Datos

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

## Cómo revisar la actividad

En SQL Server, crear una base vacía BD_ReservasCoches y seleccionarla en SSMS. Ejecutar 01-creacion.sql y luego 02-datos.sql. El primer script elimina tablas existentes: usar una base de práctica. Esta variante no se combina con el esquema de varios coches por reserva.

---

## Relación con los archivos originales

Los archivos proceden de la carpeta académica Github indicada por el autor. El [inventario](../../archivos-originales/bases-de-datos/INVENTARIO.md) relaciona cada origen con su copia publicada y registra las versiones y duplicados.

## Lenguaje y herramientas

T-SQL para SQL Server.

## Estado

Actividad organizada y documentada como parte del portafolio académico de Bases de Datos. El [registro de validación](../VALIDACION.md) distingue la revisión realizada de las pruebas que requieren SQL Server.

[Volver a Bases de Datos](../README.md)
