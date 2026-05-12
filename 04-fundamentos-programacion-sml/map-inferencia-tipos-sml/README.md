# Función Map e Inferencia de Tipos en SML

## Descripción de la actividad

Esta actividad trabaja la función `map` en Standard ML y el proceso de inferencia de tipos según Hindley-Milner.

El ejercicio consiste en analizar cómo se reduce una llamada a la función `map`, determinar su tipo más general y explicar el razonamiento utilizado para llegar a ese tipo.

---

## Objetivo

Comprender el funcionamiento de la función `map` en programación funcional y analizar cómo el sistema de tipos de SML puede inferir su tipo general.

La actividad busca reforzar conceptos relacionados con funciones de orden superior, listas, aplicación de funciones e inferencia de tipos.

---

## Conceptos aplicados

- Programación funcional.
- Standard ML.
- Funciones de orden superior.
- Función `map`.
- Listas.
- Recursividad.
- Concatenación de cadenas.
- Inferencia de tipos.
- Hindley-Milner.
- Tipos polimórficos.

---

## Función base

La función `map` aplica una función a cada elemento de una lista.

```sml
fun map f [] = []
  | map f (hd::tl) = (f hd) :: (map f tl);
```

Ejemplo:

```sml
map (fn n => 2 * n) [1,2,3,4,5,6,7,8];
```

Resultado esperado:

```text
[2,4,6,8,10,12,14,16]
```

---

## Actividad desarrollada

La llamada analizada es:

```sml
map (fn s => s ^ s) ["uno", "dos", "tres"];
```

La función anónima:

```sml
fn s => s ^ s
```

recibe una cadena y la concatena consigo misma.

Por tanto, el resultado esperado es:

```text
["unouno", "dosdos", "trestres"]
```

---

## Tipo más general de map

El tipo más general de la función `map` es:

```sml
('a -> 'b) -> 'a list -> 'b list
```

Esto significa que `map` recibe:

1. Una función que transforma valores de tipo `'a` en valores de tipo `'b`.
2. Una lista de elementos de tipo `'a`.

Y retorna:

1. Una lista de elementos de tipo `'b`.

---

## Archivos de esta carpeta

```text
map-inferencia-tipos-sml/
├── README.md
├── desarrollo.md
├── solution.sml
└── actividad-map-inferencia-tipos.pdf
```

El archivo `README.md` presenta la actividad de forma general.

El archivo `desarrollo.md` contiene la explicación organizada de la reducción y la inferencia de tipos.

El archivo `solution.sml` contiene la implementación limpia de la función `map` y pruebas en Standard ML.

El archivo `actividad-map-inferencia-tipos.pdf` conserva la evidencia documental de la práctica original.

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
