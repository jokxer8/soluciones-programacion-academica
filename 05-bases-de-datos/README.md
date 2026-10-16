# Bases de Datos

![Lenguaje](https://img.shields.io/badge/Lenguaje-SQL-blue)
![Materia](https://img.shields.io/badge/Materia-Bases%20de%20Datos-green)
![Estado](https://img.shields.io/badge/Estado-Completado-brightgreen)

Esta carpeta contiene actividades, ejercicios y soluciones de **ICC-223 Bases de Datos**, organizados a partir de la carpeta académica 30. Base de Datos, incluidas sus subcarpetas Github y Proyecto Hotel Final.

Los trabajos presentan la evolución desde fundamentos y diseño entidad-relación hasta normalización, implementación relacional, consultas y programación con SQL Server.

---

## Objetivo de la carpeta

Presentar de forma ordenada los trabajos realizados en la asignatura, acompañando las soluciones con análisis, diagramas, modelos, scripts y evidencia documental.

---

## Actividades incluidas

| Actividad | Descripción | Conceptos aplicados |
|---|---|---|
| [Fundamentos de Sistemas de Bases de Datos](fundamentos-sistemas-bases-datos) | Resúmenes y cuestionarios sobre gestión de datos, modelos y arquitectura de un DBMS. | Datos, información y metadatos, Esquema e instancia, DDL y DML |
| [Flotilla de Aeronaves - Modelo Entidad-Relación](flotilla-aeronaves-modelo-relacional) | Caso grupal de diseño de una flotilla con mantenimiento e historial de bases y estados. | Entidades, atributos y cardinalidades, Claves primarias y foráneas, Relaciones temporales |
| [Cine - Modelado y Normalización](cine-modelado-normalizacion) | Diseño de un cine y ejercicios de transformación relacional y normalización de ventas. | Modelo entidad-relación, Modelo relacional, Primera, segunda y tercera forma normal |
| [Reservas de Coches - Creación y Carga de Datos](reservas-coches-creacion-datos) | Esquema de reservas con un coche por reserva, restricciones y datos de prueba. | SQL Server y T-SQL, DDL y DML, Claves y restricciones |
| [Alquiler de Coches - Consultas SQL](alquiler-coches-consultas) | Consultas y evolución de un esquema con varios coches en una reserva. | Claves compuestas, Relación muchos a muchos, JOIN y subconsultas |
| [Bolsa de Empleo - Vistas, Procedimientos y Auditoría](bolsa-empleo-objetos-sql) | Objetos de base de datos para consultar ofertas y registrar cambios de empresas. | Vistas, Procedimientos almacenados, Funciones escalares |
| [Restaurante - Consultas del Segundo Parcial](restaurante-consultas-segundo-parcial) | Seis requerimientos SQL sobre órdenes, productos, clientes, meseros y propinas. | DISTINCT y JOIN, EXISTS, CTE |
| [Evaluaciones - Evidencia Académica](evaluaciones-evidencia-academica) | Respaldo del primer parcial y referencia al segundo parcial resuelto. | Diseño relacional, Razonamiento de bases de datos, SQL |
| [Cursos - Modelo Entidad-Relación](cursos-modelo-entidad-relacion) | Diseño conceptual de cursos, ofertas, instructores, empleados, actividades y certificados. | Entidades y atributos, Claves y cardinalidades, Relaciones entre ofertas, cursos e instructores |
| [Prácticas y Esquemas Complementarios](practicas-esquemas-complementarios) | Material de estudio y esquemas de librería, hospital y comida rápida conservados en la asignatura. | Lectura de esquemas relacionales, Claves primarias y foráneas, SQL Server |
| [Sistema de Reservas de Hotel - Proyecto Final](hotel-reservas-proyecto-final) | Proyecto grupal de C# Windows Forms y SQL Server que integra diseño relacional y gestión hotelera. | C# y .NET Framework 4.8, Windows Forms, SQL Server y T-SQL |

---

## Organización actual

```text
05-bases-de-datos/
├── fundamentos-sistemas-bases-datos/
├── flotilla-aeronaves-modelo-relacional/
├── cine-modelado-normalizacion/
├── reservas-coches-creacion-datos/
├── alquiler-coches-consultas/
├── bolsa-empleo-objetos-sql/
├── restaurante-consultas-segundo-parcial/
├── evaluaciones-evidencia-academica/
├── cursos-modelo-entidad-relacion/
├── practicas-esquemas-complementarios/
├── hotel-reservas-proyecto-final/
├── AUDITORIA.md
├── VALIDACION.md
└── README.md
```

---

## Habilidades evidenciadas

- Interpretación de requisitos de información.
- Identificación de entidades, atributos y cardinalidades.
- Transformación de modelos entidad-relación a modelos relacionales.
- Normalización y definición de diccionarios de datos.
- Creación de tablas, claves y restricciones de integridad.
- Consultas con JOIN, subconsultas, CTE y agrupaciones.
- Creación de vistas, procedimientos almacenados, funciones y triggers.
- Auditoría de cambios y documentación de soluciones académicas.
- Integración de SQL Server con C# y ADO.NET en una aplicación de escritorio.

---

## Enfoque académico

La sección complementa las áreas de programación del repositorio mostrando cómo se estructura, relaciona y consulta información persistente. Los casos de aeronaves y cine presentan decisiones de diseño; las actividades de coches, empleo y restaurante muestran su aplicación mediante SQL.

---

## Organización de cada actividad

Cada actividad mantiene el esquema utilizado en la sección de SML:

```text
nombre-de-la-actividad/
├── README.md
├── desarrollo.md
├── solution.sql / scripts numerados
└── evidencia y modelos
```

Las actividades teóricas y de modelado se documentan sin inventar código SQL. Las evidencias de evaluación se conservan como respaldo del trabajo realizado.

---

## Relación con archivos originales

Las versiones de trabajo, los enunciados y las evidencias están en [archivos-originales/bases-de-datos](../archivos-originales/bases-de-datos/README.md). El [inventario](../archivos-originales/bases-de-datos/INVENTARIO.md) permite contrastar su procedencia y consultar las variantes.

El [proyecto final del hotel](hotel-reservas-proyecto-final) incluye la aplicación, los scripts, el diseño y su documentación. El documento de requisitos se conserva en los originales y se diferencia de la implementación. La [auditoría de la carpeta](AUDITORIA.md) explica el tratamiento de los materiales adicionales.

---

## Lenguaje utilizado

SQL, principalmente T-SQL para Microsoft SQL Server; C# con Windows Forms y .NET Framework 4.8 en el proyecto del hotel. Los diagramas y modelos acompañan los ejercicios de diseño.

---

## Estado de la sección

Sección completada con nueve actividades de fundamentos, diseño, SQL y proyecto final, un apartado de esquemas de apoyo y un conjunto de evidencias de evaluación. El autor informa que aprobó la asignatura; la captura del primer parcial respalda esa evaluación concreta y no sustituye una certificación de nota final.

Los scripts se revisaron documentalmente y mediante análisis de sintaxis. El orden de ejecución, las dependencias y los scripts que recrean datos se explican en cada actividad y en [VALIDACION.md](VALIDACION.md).

[Volver al repositorio](../README.md)

El proyecto del hotel dispone de un [tablero de mejoras y pruebas](https://github.com/users/jokxer8/projects/1). La organización de la sección está completada. El hotel cuenta con [53 pruebas funcionales SQL correctas](hotel-reservas-proyecto-final/docs/PRUEBAS-FUNCIONALES.md), ejecutadas localmente y en Actions; permisos avanzados y exportaciones mantienen su seguimiento abierto.
