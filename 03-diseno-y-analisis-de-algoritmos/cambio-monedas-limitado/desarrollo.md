# Desarrollo — Cambio de Monedas con Cantidad Limitada

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

Esta guía reúne el análisis técnico ya documentado en la actividad. El README conserva el enunciado, los ejemplos y las explicaciones originales.

## Objetivo

Aplicar programación dinámica para resolver un problema de optimización con restricciones de cantidad.

## Conceptos aplicados

- Programación dinámica
- Arreglos
- Listas
- Optimización
- Cantidad limitada de recursos
- Problema de cambio de monedas
- Análisis de complejidad

## Análisis del algoritmo

El algoritmo convierte las monedas disponibles en una lista individual, respetando la cantidad limitada de cada tipo de moneda.

Luego aplica programación dinámica para calcular el mínimo número de monedas necesario para alcanzar cada suma desde 0 hasta X.

El recorrido de las sumas se realiza de derecha a izquierda para evitar reutilizar una misma moneda más de una vez.

## Complejidad

Sea M la cantidad total de monedas disponibles después de expandir las cantidades.

- Tiempo: O(M × X)
- Espacio: O(X)
