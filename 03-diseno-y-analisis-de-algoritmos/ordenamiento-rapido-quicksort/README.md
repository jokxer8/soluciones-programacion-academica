# Ordenamiento Rápido - QuickSort

## Descripción del problema

Este ejercicio consiste en ordenar un arreglo de números enteros utilizando el algoritmo QuickSort.

QuickSort es un algoritmo de ordenamiento basado en la técnica de divide y vencerás. Su funcionamiento consiste en seleccionar un elemento como pivote, dividir el arreglo en dos partes y ordenar recursivamente cada una de ellas.

## Objetivo

Aplicar un algoritmo de ordenamiento eficiente utilizando recursividad y división del problema en partes más pequeñas.

## Entrada

El programa trabaja con un arreglo de números enteros desordenados.

Ejemplo:

```text
8, 3, 1, 7, 0, 10, 2
```

## Salida

El programa muestra el arreglo ordenado de menor a mayor.

Ejemplo:

```text
0, 1, 2, 3, 7, 8, 10
```

## Conceptos aplicados

- Arreglos
- Recursividad
- Divide y vencerás
- Pivote
- Intercambio de valores
- Ordenamiento
- Análisis de complejidad

## Análisis del algoritmo

El algoritmo QuickSort selecciona un pivote y reorganiza el arreglo colocando los valores menores a un lado y los mayores al otro. Luego repite el mismo proceso de forma recursiva en cada subarreglo.

Este enfoque permite ordenar los datos de manera eficiente en la mayoría de los casos.

## Complejidad

- Mejor caso: O(n log n)
- Caso promedio: O(n log n)
- Peor caso: O(n²)
- Espacio: O(log n), debido a las llamadas recursivas

## Lenguaje utilizado

C#
