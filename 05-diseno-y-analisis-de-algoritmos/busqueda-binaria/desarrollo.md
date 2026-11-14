# Desarrollo — Búsqueda Binaria

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## Objetivo

Aplicar un algoritmo de búsqueda eficiente sobre una colección ordenada de datos.

## Conceptos aplicados

- Arreglos
- Búsqueda binaria
- Ciclos
- Condicionales
- División del problema
- Análisis de complejidad

## Análisis del algoritmo

La búsqueda binaria solo funciona correctamente cuando los datos están ordenados.

En cada iteración se calcula la posición central del arreglo. Si el valor del centro es igual al número buscado, la búsqueda termina. Si el valor buscado es menor, se descarta la mitad derecha. Si es mayor, se descarta la mitad izquierda.

## Complejidad

- Tiempo: O(log n)
- Espacio: O(1)
