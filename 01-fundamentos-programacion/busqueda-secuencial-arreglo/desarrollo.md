# Desarrollo — Búsqueda Secuencial en un Arreglo

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Si tienes la lista `10, 4, 8` y buscas el `8`, el programa revisa `10`, luego `4` y por último `8`. Muestra la posición `2` porque C# empieza a contar en `0`. Si buscas `7`, revisará toda la lista y te dirá que no aparece.

[Volver a la explicación y los ejemplos de la actividad](README.md#en-palabras-simples).

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
