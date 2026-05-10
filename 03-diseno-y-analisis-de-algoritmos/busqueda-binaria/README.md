# Búsqueda Binaria

## Descripción del problema

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
