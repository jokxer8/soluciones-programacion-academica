# Desarrollo — Búsqueda Secuencial en un Arreglo

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

Esta guía reúne el análisis técnico ya documentado en la actividad. El README conserva el enunciado, los ejemplos y las explicaciones originales.

## Objetivo

Aplicar conceptos fundamentales de programación mediante el uso de arreglos, ciclos, condicionales, métodos y validación de datos.

## Conceptos aplicados

- Entrada de datos
- Variables
- Arreglos
- Ciclos
- Condicionales
- Métodos
- Búsqueda secuencial
- Validación de entrada

## Análisis del algoritmo

El programa recorre el arreglo desde la primera posición hasta la última.

En cada posición compara el valor almacenado con el número buscado. Si ambos son iguales, retorna la posición donde fue encontrado.

Si termina el recorrido sin encontrar el número, retorna -1 para indicar que el valor no existe en el arreglo.

## Complejidad

- Tiempo: O(n)
- Espacio: O(n)
