
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/15-programacion-funcional-sml-listas-perezosas-sml.svg" alt="Listas Perezosas en SML (Lazy Lists in SML)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.sml) · [Evidencia](actividad-listas-perezosas-sml.pdf)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SML-purple)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Una lista perezosa calcula el siguiente valor cuando se lo pides. Así puedes trabajar con una secuencia que, en teoría, no termina: solicitas los primeros cinco valores en vez de intentar construirlos todos.

### Construcción perezosa

- **¿Por qué?** No se puede construir toda una secuencia infinita antes de usarla.
- **¿Para qué?** Para pedir un número finito de elementos de una secuencia que puede continuar.
- **¿Cómo?** Guarda el valor actual y una función para calcular el resto; pide un prefijo con `takeSeq`.
- **¿Cuándo usarlo?** Cuando solo necesites un tramo de valores potencialmente numerosos.

### Múltiplos

- **¿Por qué?** No hace falta preparar una tabla completa para obtener los siguientes múltiplos.
- **¿Para qué?** Para generar avances de tamaño fijo bajo demanda.
- **¿Cómo?** Configura el paso en 5 y pide seis valores: empieza en 5 y termina en 30.
- **¿Cuándo usarlo?** Cuando quieras recorrer valores con incrementos iguales.

### Factoriales

- **¿Por qué?** El siguiente factorial reutiliza el producto anterior.
- **¿Para qué?** Para construir los resultados progresivamente.
- **¿Cómo?** Solicita seis valores y sigue el cambio de `24` a `120` al multiplicar por `5`.
- **¿Cuándo usarlo?** Cuando cada resultado dependa del producto acumulado.

### Fibonacci

- **¿Por qué?** Dos valores consecutivos bastan para calcular el siguiente.
- **¿Para qué?** Para generar la secuencia sin recalcular sus prefijos desde cero.
- **¿Cómo?** Parte de `1, 1`, suma ambos y conserva los dos últimos en cada paso.
- **¿Cuándo usarlo?** Cuando quieras estudiar secuencias que dependen de los dos resultados anteriores.

## Qué trabajé y cómo leer los ejemplos

Represento secuencias que calculan nuevos elementos cuando se solicitan. unit es el tipo cuyo valor es (), y una función unit -> valor aplaza un cálculo hasta recibir (). Solicitar n elementos significa observar un prefijo finito, incluso si la secuencia puede continuar indefinidamente.

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

El PDF complementa la explicación y permite consultar el planteamiento académico de la actividad.

En caso de que el documento original contenga datos personales, se debe subir una versión limpia o censurada.

---

## Estado

Actividad organizada y documentada como parte del portafolio académico de Programación Funcional-SML.

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
