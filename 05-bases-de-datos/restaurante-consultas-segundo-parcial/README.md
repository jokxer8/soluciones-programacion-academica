# Restaurante - Consultas del Segundo Parcial

## Descripción de la actividad

Seis requerimientos SQL sobre órdenes, productos, clientes, meseros y propinas.

---

## Objetivo

Resolver consultas y definir objetos SQL a partir de un esquema de atención de restaurante.

---

## Conceptos aplicados

- DISTINCT y JOIN.
- EXISTS.
- CTE.
- Agregaciones.
- Vistas.
- Funciones escalares.

---

## Actividad desarrollada

Las respuestas identifican meseros, calculan importes de órdenes con postres, seleccionan clientes frecuentes, agrupan ventas por producto y calculan promedios de propina por tipo de documento.

---

## Ejemplo de análisis y uso

```sql
SELECT dbo.fn_f_kdct('Factura') AS PromedioFactura,
       dbo.fn_f_kdct('Ticket') AS PromedioTicket;
```

La consulta utiliza la función escalar del ejercicio para devolver el promedio de propina de cada tipo de documento. La versión organizada corrige el cierre de la función y separa la consulta de prueba de su definición.

---

## Archivos de esta carpeta

```text
restaurante-consultas-segundo-parcial/
├── README.md
├── desarrollo.md
├── 01-base.sql
├── diagrama.png
├── evidencia-segundo-parcial.pdf
└── solution.sql
```

- [01-base.sql](01-base.sql)
- [diagrama.png](diagrama.png)
- [evidencia-segundo-parcial.pdf](evidencia-segundo-parcial.pdf)
- [solution.sql](solution.sql)

El archivo [desarrollo.md](desarrollo.md) explica las decisiones y el alcance de la solución.

---

## Material de estudio complementario

[Guía con solución integrada](guia-estudio-solucion-integrada.pdf), conservada como material de repaso. Sus marcadores de iniciales no sustituyen las respuestas originales del parcial.

---

## Cómo revisar la actividad

Ejecutar 01-base.sql y después solution.sql en SSMS. El primer archivo elimina y recrea RESTAURANTE_ATENCION: utilizar un entorno de práctica. Las correcciones y el tratamiento del intervalo de fechas se detallan en desarrollo.md.

---

## Relación con los archivos originales

Los archivos proceden de la carpeta académica Github indicada por el autor. El [inventario](../../archivos-originales/bases-de-datos/INVENTARIO.md) relaciona cada origen con su copia publicada y registra las versiones y duplicados.

## Lenguaje y herramientas

T-SQL para SQL Server.

## Estado

Actividad organizada y documentada como parte del portafolio académico de Bases de Datos. El [registro de validación](../VALIDACION.md) distingue la revisión realizada de las pruebas que requieren SQL Server.

[Volver a Bases de Datos](../README.md)
