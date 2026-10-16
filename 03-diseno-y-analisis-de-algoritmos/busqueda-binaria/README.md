# Búsqueda Binaria

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## Qué trabajé y cómo leer los ejemplos

Busco sobre datos ordenados y reduzco a la mitad el intervalo en cada paso. n es la cantidad de elementos; log₂(n) describe cuántas divisiones sucesivas entre dos hacen falta. La condición de orden es necesaria para descartar correctamente una mitad.

## Planteamiento

Este ejercicio consiste en buscar un número dentro de un arreglo ordenado utilizando el algoritmo de búsqueda binaria.

La búsqueda binaria divide el conjunto de datos en dos partes en cada paso, descartando la mitad donde el valor buscado no puede encontrarse.

## Objetivo

Aplicar un algoritmo de búsqueda eficiente sobre una colección ordenada de datos.

## Entrada

El programa utiliza un arreglo de números enteros previamente ordenado y solicita al usuario el número que desea buscar.

Ejemplo:

```text
Ingrese el número que desea buscar: 15
```

## Salida

El programa indica si el número fue encontrado y muestra su posición dentro del arreglo.

Ejemplo:

```text
El número 15 fue encontrado en la posición 4.
```

Si el número no existe en el arreglo, muestra:

```text
El número no fue encontrado.
```

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

## Lenguaje utilizado

C#

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
