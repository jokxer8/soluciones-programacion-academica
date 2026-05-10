# Subsequencia Creciente con Suma Máxima

## Descripción del problema

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
- Subsequencias
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
