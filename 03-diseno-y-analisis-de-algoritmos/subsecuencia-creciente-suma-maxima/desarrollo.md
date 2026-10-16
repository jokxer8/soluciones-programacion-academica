# Desarrollo — Subsequencia Creciente con Suma Máxima

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

Esta guía reúne el análisis técnico ya documentado en la actividad. El README conserva el enunciado, los ejemplos y las explicaciones originales.

## Objetivo

Aplicar programación dinámica para calcular la subsecuencia creciente con mayor suma posible.

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
