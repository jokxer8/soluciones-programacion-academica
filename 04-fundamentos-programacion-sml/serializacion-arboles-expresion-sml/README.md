# Serialización de Árboles de Expresión en SML

## Descripción de la actividad

Esta actividad trabaja el uso de tipos de datos recursivos en Standard ML para representar árboles de expresión correspondientes a expresiones aritméticas.

A partir de un tipo de dato `tree`, se construye un árbol binario que representa una expresión matemática y luego se implementan funciones para serializarlo en diferentes recorridos.

---

## Objetivo

Construir funciones en SML que reciban un árbol de expresión como parámetro y produzcan una cadena que represente el recorrido del árbol.

Los recorridos solicitados son:

- Preorden.
- Enorden.
- Postorden.

---

## Conceptos aplicados

- Standard ML.
- Programación funcional.
- Tipos de datos algebraicos.
- Tipos de datos recursivos.
- Árboles binarios.
- Árboles de expresión.
- Pattern matching.
- Recursividad.
- Serialización de estructuras.
- Recorridos de árboles.

---

## Tipo de dato utilizado

El tipo de dato recursivo utilizado para representar el árbol es:

```sml
datatype tree = Leaf | Br of tree * string * tree;
```

Donde:

- `Leaf` representa una hoja vacía.
- `Br` representa una rama con subárbol izquierdo, dato y subárbol derecho.

---

## Expresión representada

La expresión aritmética trabajada es:

```text
(x * 45) + z / (y + 5050)
```

Esta expresión puede representarse mediante un árbol binario donde los operadores funcionan como nodos internos y los operandos como hojas.

---

## Recorridos implementados

### Preorden

Visita primero el nodo actual, luego el subárbol izquierdo y finalmente el subárbol derecho.

```text
raíz - izquierda - derecha
```

### Enorden

Visita primero el subárbol izquierdo, luego el nodo actual y finalmente el subárbol derecho.

```text
izquierda - raíz - derecha
```

### Postorden

Visita primero el subárbol izquierdo, luego el subárbol derecho y finalmente el nodo actual.

```text
izquierda - derecha - raíz
```

---

## Archivos de esta carpeta

```text
serializacion-arboles-expresion-sml/
├── README.md
├── desarrollo.md
├── solution.sml
└── actividad-serializacion-arboles-expresion.pdf
```

El archivo `README.md` presenta la actividad de forma general.

El archivo `desarrollo.md` contiene la explicación organizada del problema y los recorridos.

El archivo `solution.sml` contiene la implementación limpia en Standard ML.

El archivo `actividad-serializacion-arboles-expresion.pdf` conserva la evidencia documental de la práctica original.

---

## Relación con los archivos originales

El documento original de la actividad se conserva como respaldo académico en:

```text
archivos-originales/programacion-funcional-sml/
```

Esta carpeta contiene una versión organizada, limpia y profesional de la actividad.

---

## Nota sobre datos personales

Para mantener una presentación profesional y proteger información personal, se recomienda que el PDF utilizado como evidencia no contenga datos sensibles como ID, matrícula, correo, calificaciones o información privada.

En caso de que el documento original contenga datos personales, se debe subir una versión limpia o censurada.

---

## Estado

Actividad organizada y documentada como parte del portafolio académico de Programación Funcional con SML.
