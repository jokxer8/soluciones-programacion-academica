# Listas Perezosas en SML

## Descripción de la actividad

Esta actividad trabaja la construcción y manipulación de listas perezosas en Standard ML.

Aunque SML evalúa normalmente de forma estricta, es posible simular evaluación perezosa utilizando funciones de tipo `unit -> 'a seq`, permitiendo que el resto de una secuencia se calcule solamente cuando sea necesario.

---

## Objetivo

Construir secuencias infinitas mediante listas perezosas y utilizarlas para generar:

- Múltiplos de un número.
- Factoriales.
- Números de Fibonacci.

Además, se busca comprender el papel de la evaluación diferida y comparar este enfoque con soluciones recursivas tradicionales.

---

## Conceptos aplicados

- Standard ML.
- Programación funcional.
- Listas perezosas.
- Evaluación diferida.
- Secuencias infinitas.
- Tipos de datos recursivos.
- Funciones de orden superior.
- Recursividad.
- `mapSeq`.
- `filterSeq`.
- Factorial.
- Fibonacci.
- Razonamiento sobre eficiencia.

---

## Tipo de dato utilizado

La actividad parte del siguiente tipo de dato para representar una secuencia perezosa:

```sml
datatype 'a seq = Empty
                | Cons of 'a * (unit -> 'a seq);
```

Donde:

- `Empty` representa una secuencia vacía.
- `Cons` contiene un valor actual y una función que genera el resto de la secuencia cuando se necesite.

---

## Funciones base

La actividad utiliza funciones para trabajar con secuencias perezosas:

- `seqFrom`: genera una secuencia infinita de números naturales.
- `takeSeq`: obtiene los primeros elementos de una secuencia.
- `dropSeq`: descarta una cantidad de elementos.
- `mapSeq`: aplica una función a cada elemento conservando la pereza.
- `filterSeq`: filtra elementos conservando la evaluación diferida.

---

## Secuencias desarrolladas

### Secuencia de múltiplos

Genera los múltiplos de un número recibido como parámetro.

Ejemplo:

```sml
takeSeq (6, multSeq 5);
```

Resultado esperado:

```text
[5, 10, 15, 20, 25, 30]
```

---

### Secuencia de factoriales

Genera los factoriales de forma progresiva, reutilizando el resultado anterior.

Ejemplo:

```sml
takeSeq (6, factorialSeq);
```

Resultado esperado:

```text
[1, 2, 6, 24, 120, 720]
```

---

### Secuencia de Fibonacci

Genera los números de Fibonacci conservando los dos valores anteriores.

Ejemplo:

```sml
takeSeq (8, fibonacciSeq);
```

Resultado esperado:

```text
[1, 1, 2, 3, 5, 8, 13, 21]
```

---

## Archivos de esta carpeta

```text
listas-perezosas-sml/
├── README.md
├── desarrollo.md
├── solution.sml
└── actividad-listas-perezosas-sml.pdf
```

El archivo `README.md` presenta la actividad de forma general.

El archivo `desarrollo.md` contiene la explicación organizada de la solución.

El archivo `solution.sml` contiene la implementación limpia en Standard ML.

El archivo `actividad-listas-perezosas-sml.pdf` conserva la evidencia documental de la práctica original.

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
