# Desarrollo — Ordenamiento Rápido - QuickSort

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Se elige un número como referencia y se separan los valores según queden por debajo o por encima. Se repite en los grupos pequeños. Para seguirlo a mano, prueba con cinco números y marca dónde termina la referencia tras separar.

[Volver a la explicación y los ejemplos de la actividad](README.md#en-palabras-simples).

## Objetivo

Aplicar un algoritmo de ordenamiento eficiente utilizando recursividad y división del problema en partes más pequeñas.

## Conceptos aplicados

- Arreglos
- Recursividad
- Divide y vencerás
- Pivote
- Intercambio de valores
- Ordenamiento
- Análisis de complejidad

## Análisis del algoritmo

El algoritmo QuickSort selecciona un pivote y reorganiza el arreglo colocando los valores menores a un lado y los mayores al otro. Luego repite el mismo proceso de forma recursiva en cada subarreglo.

Este enfoque permite ordenar los datos de manera eficiente en la mayoría de los casos.

## Complejidad

- Mejor caso: O(n log n)
- Caso promedio: O(n log n)
- Peor caso: O(n²)
- Espacio: O(log n), debido a las llamadas recursivas
