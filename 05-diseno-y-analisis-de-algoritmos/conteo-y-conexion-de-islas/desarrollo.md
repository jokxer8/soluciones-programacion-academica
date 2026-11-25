# Desarrollo — Conteo y Conexión de Islas

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

El 1 es tierra y el 0 es agua. Primero reúne la tierra que se toca por lados para contar las islas. Después compara qué casillas de agua habría que convertir en tierra para conectarlas todas con el menor cambio posible.

[Volver a la explicación y los ejemplos de la actividad](README.md#en-palabras-simples).

## Objetivo

Aplicar recorridos sobre matrices, búsqueda en anchura y programación dinámica para resolver un problema de conexión entre componentes.

## Conceptos aplicados

- Matrices
- Búsqueda en anchura
- Recorrido por componentes
- Grafos implícitos
- Programación dinámica
- Máscaras de bits
- Optimización
- Análisis de complejidad

## Análisis del algoritmo

Primero se recorre la matriz para identificar y etiquetar cada isla.

Luego se utiliza una estrategia de programación dinámica con máscaras de bits para representar qué islas han sido conectadas.

El algoritmo calcula el menor costo para conectar subconjuntos de islas, considerando que convertir una celda de agua tiene costo 1 y moverse sobre tierra tiene costo 0.

Finalmente, se obtiene el costo mínimo necesario para conectar todas las islas.

## Complejidad

Sea:

- F = cantidad de filas
- C = cantidad de columnas
- K = cantidad de islas

La complejidad depende de la cantidad de islas, porque se utilizan máscaras de bits.

- Tiempo aproximado: O(3^K × F × C)
- Espacio: O(2^K × F × C)

Este enfoque es adecuado cuando la cantidad de islas no es demasiado grande.
