# Cursos - Modelo Entidad-Relación

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Evidencia](diagrama-cursos.pdf)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SQL%20/%20C%23-lightgrey)

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
