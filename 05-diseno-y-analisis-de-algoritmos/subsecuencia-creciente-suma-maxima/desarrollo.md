# Desarrollo — Subsecuencia Creciente con Suma Máxima

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

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
