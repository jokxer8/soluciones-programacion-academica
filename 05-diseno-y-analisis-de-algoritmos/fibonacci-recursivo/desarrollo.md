# Desarrollo — Fibonacci Recursivo

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Cada número se obtiene sumando los dos anteriores. Si pides Fibonacci de 5 mediante llamadas, aparecen cálculos repetidos, como Fibonacci de 3. Guardar un resultado ya obtenido evita volver a resolverlo cuando el ejercicio aplica esa mejora.

[Volver a la explicación y los ejemplos de la actividad](README.md#en-palabras-simples).

## Objetivo

Aplicar el concepto de recursividad para resolver un problema clásico de programación y analizar su comportamiento algorítmico.

## Conceptos aplicados

- Recursividad
- Validación de entrada
- Casos base
- Optimización mediante memoización
- Análisis de complejidad

## Análisis del algoritmo

La solución utiliza una función recursiva optimizada con memoización. Esto permite guardar resultados ya calculados y evitar repetir operaciones innecesarias.

## Complejidad

- Tiempo: O(n)
- Espacio: O(n)
