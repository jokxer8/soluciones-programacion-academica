# Desarrollo - Cursos - Modelo Entidad-Relación

## Planteamiento

Representar entidades y cardinalidades de una oferta de formación mediante un diagrama entidad-relación.

## Análisis

El diagrama distingue el catálogo CURSO de OFERTA_CURSO, que incorpora el año y fechas de la edición. EMPRESA y EMPLEADO representan el contexto de participación; ACTIVIDAD, EVALUACION, NOTA_ACTIVIDAD y CERTIFICADO recogen la parte evaluativa. Se conserva el modelo académico encontrado, incluidos sus nombres y cardinalidades, sin afirmar que exista una implementación física.

## Revisión del material

Abrir el PDF para revisar el diseño y XMind para inspeccionar el archivo editable. El conjunto no contiene DDL de este caso.

## Alcance

Se conserva el trabajo académico y se distinguen los diseños, el material de estudio y las implementaciones. Consultar [VALIDACION.md](../VALIDACION.md) para conocer las comprobaciones realizadas.

## Observaciones del modelo conservado

El diagrama original utiliza la etiqueta INSTUCTORES y presenta en NOTA_ACTIVIDAD atributos repetidos del catálogo CURSO. Antes de transformarlo en DDL se debe aclarar la identificación de cada nota y su relación con empleado, oferta y actividad. Se documentan estas observaciones sin presentar el dibujo original como un esquema físico validado.
