
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/06-bases-de-datos-cursos-modelo-entidad-relacion.svg" alt="Cursos - Modelo Entidad-Relación (Courses — Entity-Relationship Model)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Evidencia](diagrama-cursos.pdf)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SQL%20/%20C%23-lightgrey)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Antes de crear tablas, dibuja las cosas que el sistema necesita recordar y sus relaciones. Aquí importa distinguir qué representa un curso y cómo se conecta con los demás elementos del modelo. Usa el diagrama como mapa antes de leer los nombres técnicos.

### Catálogo y oferta

- **¿Por qué?** El mismo curso puede ofrecerse en fechas diferentes.
- **¿Para qué?** Para separar la descripción permanente de cada edición.
- **¿Cómo?** En el diagrama sigue la relación entre `CURSO` y `OFERTA_CURSO`, donde aparecen el año y las fechas.
- **¿Cuándo usarlo?** Cuando una entidad general tiene varias ediciones o convocatorias.

### Participación y evaluación

- **¿Por qué?** Inscribirse, participar y obtener una calificación son hechos distintos.
- **¿Para qué?** Para saber qué relación conserva cada dato sin confundir empleados, actividades y certificados.
- **¿Cómo?** Sigue las claves y cardinalidades del diagrama desde la oferta hasta evaluación y certificado, sin asumir que ya existen tablas físicas.
- **¿Cuándo usarlo?** Al levantar requisitos de un sistema de formación antes de implementarlo.

## Qué trabajé y cómo leer los ejemplos

Modelo cursos y sus ofertas, instructores y certificados. Una relación 1:N permite asociar una instancia del primer tipo con varias del segundo. Esa proporción no indica cuántos registros existen actualmente; expresa una regla del diseño.

## Descripción de la actividad

Diseño conceptual de cursos, ofertas, instructores, empleados, actividades y certificados.

---

## Objetivo

Representar entidades y cardinalidades de una oferta de formación mediante un diagrama entidad-relación.

---

## Conceptos aplicados

- Entidades y atributos.
- Claves y cardinalidades.
- Relaciones entre ofertas, cursos e instructores.
- Inscripciones, evaluaciones y certificados.

---

## Actividad desarrollada

El diagrama distingue el catálogo CURSO de OFERTA_CURSO, que incorpora el año y fechas de la edición. EMPRESA y EMPLEADO representan el contexto de participación; ACTIVIDAD, EVALUACION, NOTA_ACTIVIDAD y CERTIFICADO recogen la parte evaluativa. Se conserva el modelo académico encontrado, incluidos sus nombres y cardinalidades, sin afirmar que exista una implementación física.

---

## Archivos de esta carpeta

- [diagrama-cursos.pdf](diagrama-cursos.pdf).
- [diagrama-cursos.xmind](diagrama-cursos.xmind).
- [Desarrollo y análisis](desarrollo.md).

---

## Cómo revisar la actividad

Abrir el PDF para revisar el diseño y XMind para inspeccionar el archivo editable. El conjunto no contiene DDL de este caso.

---

## Relación con los archivos originales

Los modelos y documentos de apoyo se encuentran en el [inventario de documentos](../../archivos-originales/bases-de-datos/INVENTARIO.md), junto con las versiones publicadas y sus adaptaciones.

## Estado

Diseño conceptual documentado.

[Volver a Bases de Datos](../README.md)
