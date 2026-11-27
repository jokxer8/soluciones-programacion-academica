
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/06-bases-de-datos-alquiler-coches-consultas.svg" alt="Alquiler de Coches - Consultas SQL (Car Rental — SQL Queries)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](.) · [Evidencia](modelo-relacional.pdf)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SQL%20/%20C%23-lightgrey)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Las consultas cruzan datos del alquiler con los del auto o el cliente. Lee cada pregunta antes del SQL: ¿qué personas o autos necesito, cómo se relacionan y qué condición debe pasar cada fila?

- [Crear tablas](01-creacion.sql) y [cargar datos](02-datos.sql): prepara los espacios y añade los primeros registros.
- [Consultas básicas](03-consultas-basicas.sql): lee los datos antes de modificarlos.
- [Modificar la estructura](04-modificaciones.sql) y [actualizar datos](05-actualizacion-datos.sql): separa cambiar una tabla de corregir los datos que guarda.
- [Consultas ampliadas](06-consultas-ampliadas.sql) y [consultas 8 a 14](07-consultas-8-a-14.sql): compara qué información se cruza, filtra o resume en cada pregunta.

### Preparar tablas y datos

- **¿Por qué?** Una reserva puede incluir varios coches.
- **¿Para qué?** Para representar cada vehículo reservado sin repetir toda la cabecera.
- **¿Cómo?** Relaciona la reserva con sus coches mediante `Reserva_Coche`, y carga las filas referenciadas antes de las que dependen de ellas.
- **¿Cuándo usarlo?** Cuando una relación admite varios registros en ambos extremos.

### Consultas básicas y ampliadas

- **¿Por qué?** Los datos de una pregunta pueden estar repartidos entre varias tablas.
- **¿Para qué?** Para listar, cruzar o contar clientes, reservas y vehículos.
- **¿Cómo?** Une las tablas por sus claves; para contar coches por reserva, agrupa por la reserva y filtra el total con `HAVING`.
- **¿Cuándo usarlo?** Cuando una lista simple no alcanza para responder la pregunta.

### Cambios de estructura y datos

- **¿Por qué?** Añadir una columna y corregir lo que contiene son cambios distintos.
- **¿Para qué?** Para ampliar el esquema sin confundir la definición con sus valores.
- **¿Cómo?** Prueba primero el cambio de columna, después actualiza sus datos y comprueba con una consulta en una base de práctica.
- **¿Cuándo usarlo?** Cuando un requisito nuevo pide capturar información que no estaba en el modelo.

## Qué trabajé y cómo leer los ejemplos

Consulto un modelo que admite varios coches por reserva. Una clave compuesta identifica una fila mediante varias columnas juntas; no consiste en sumar sus valores. Los números de los scripts indican el orden de preparación y consulta explicado en esta actividad.

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

Los enunciados, modelos y documentos de apoyo están relacionados en el [inventario de documentos](../../archivos-originales/bases-de-datos/INVENTARIO.md). Allí se pueden consultar las versiones publicadas y su tratamiento.

## Lenguaje y herramientas

T-SQL para SQL Server.

## Estado

Actividad organizada y documentada como parte del portafolio académico de Bases de Datos. El [registro de validación](../VALIDACION.md) distingue la revisión realizada de las pruebas que requieren SQL Server.

[Volver a Bases de Datos](../README.md)
