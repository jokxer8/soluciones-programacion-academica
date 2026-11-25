
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/05-diseno-y-analisis-de-algoritmos-subsecuencia-creciente-suma-maxima.svg" alt="Subsecuencia Creciente con Suma Máxima (Increasing Subsequence with Maximum Sum)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Puedes saltarte números, pero no cambiar el orden original. Imagina que comparas dos grupos crecientes de una lista y eliges el que suma más, aunque tenga menos elementos. Ese es el objetivo: mayor suma, no mayor cantidad.

## Qué trabajé y cómo leer los ejemplos

Busco una subsecuencia creciente con la mayor suma. Puedo omitir elementos sin cambiar su orden: no tienen que ser consecutivos. La mejor suma tampoco exige la mayor cantidad de términos; dp[i] representa la mejor suma que termina en el elemento de índice i.

## Planteamiento

Este ejercicio consiste en encontrar una subsecuencia creciente dentro de un arreglo de números enteros, de forma que la suma de sus elementos sea la mayor posible.

Una subsecuencia mantiene el orden original de los elementos, pero no necesariamente utiliza todos los valores del arreglo.

## Objetivo

Aplicar programación dinámica para calcular la subsecuencia creciente con mayor suma posible.

## Entrada

El programa solicita la cantidad de elementos y luego los valores del arreglo separados por espacio.

Ejemplo:

```text
Cantidad de elementos: 7
Ingrese los elementos separados por espacio:
1 101 2 3 100 4 5
```

## Salida

El programa muestra la suma máxima encontrada y la subsecuencia correspondiente.

Ejemplo:

```text
Suma máxima: 106
Subsecuencia: 1 2 3 100
```

## Conceptos aplicados

- Arreglos
- Programación dinámica
- Subsecuencias
- Comparación de valores
- Reconstrucción de solución
- Análisis de complejidad

## Análisis del algoritmo

El algoritmo utiliza un arreglo auxiliar para guardar la mejor suma creciente que termina en cada posición.

También utiliza otro arreglo para guardar el índice anterior de cada elemento, permitiendo reconstruir la subsecuencia final.

Para cada elemento, se revisan los elementos anteriores y se verifica si pueden formar una subsecuencia creciente con mayor suma.

## Complejidad

- Tiempo: O(n²)
- Espacio: O(n)

## Lenguaje utilizado

C#

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
