# Desarrollo — Subsecuencia Creciente con Suma Máxima

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Puedes saltarte números, pero no cambiar el orden original. Imagina que comparas dos grupos crecientes de una lista y eliges el que suma más, aunque tenga menos elementos. Ese es el objetivo: mayor suma, no mayor cantidad.

[Volver a la explicación y los ejemplos de la actividad](README.md#en-palabras-simples).

## Objetivo

Aplicar programación dinámica para calcular la subsecuencia creciente con mayor suma posible.

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
