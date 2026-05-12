# Combinador Y y Fibonacci en SML

## Descripción de la actividad

Esta actividad trabaja el uso del combinador Y para expresar funciones recursivas dentro del contexto de la programación funcional.

El ejercicio se centra en la función de Fibonacci, primero desde una perspectiva teórica mediante reducción lambda y luego mediante una implementación en Standard ML.

---

## Objetivo

Comprender cómo una función recursiva puede expresarse como un punto fijo utilizando el combinador Y.

Además, se busca implementar en SML una versión cuasi-recursiva de Fibonacci y comprobar su funcionamiento mediante diferentes pruebas.

---

## Conceptos aplicados

- Programación funcional.
- Cálculo lambda.
- Combinador Y.
- Punto fijo.
- Funciones de orden superior.
- Recursividad.
- Función Fibonacci.
- Reducción beta.
- Standard ML.
- Pruebas en SOSML.

---

## Definición teórica

El combinador Y permite expresar funciones recursivas sin declarar la recursividad directamente dentro de la función original.

Una forma del combinador Y es:

```text
Y ≜ λf.λn.f(Y f)n
```

La función cuasi-recursiva de Fibonacci se expresa como:

```text
Fib = λg.λm. if (m = 1 or m = 2) then 1 else (g(m - 1) + g(m - 2))
```

La actividad solicita reducir la expresión:

```text
(Y Fib)4
```

---

## Implementación en SML

La actividad también solicita expresar el combinador Y en Standard ML:

```sml
val rec Y = fn f => fn n => f (Y f) n
```

Luego se define la versión cuasi-recursiva de Fibonacci y se aplica el combinador Y para obtener una función recursiva funcional.

---

## Archivos de esta carpeta

```text
combinador-y-fibonacci-sml/
├── README.md
├── desarrollo.md
├── solution.sml
└── actividad-combinador-y-fibonacci.pdf
```

El archivo `README.md` presenta la actividad de forma general.

El archivo `desarrollo.md` contiene la explicación organizada de la reducción teórica.

El archivo `solution.sml` contiene la implementación limpia en Standard ML.

El archivo `actividad-combinador-y-fibonacci.pdf` conserva la evidencia documental de la práctica original.

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
