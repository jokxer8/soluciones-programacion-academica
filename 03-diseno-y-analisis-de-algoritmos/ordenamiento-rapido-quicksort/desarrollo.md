# Desarrollo — Ordenamiento Rápido - QuickSort

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

Esta guía reúne el análisis técnico ya documentado en la actividad. El README conserva el enunciado, los ejemplos y las explicaciones originales.

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
