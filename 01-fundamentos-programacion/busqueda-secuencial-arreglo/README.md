# Búsqueda Secuencial en un Arreglo

## Descripción del problema

Este ejercicio consiste en buscar un número dentro de un arreglo de enteros utilizando el método de búsqueda secuencial.

La búsqueda secuencial revisa cada elemento del arreglo desde el inicio hasta encontrar el valor buscado o hasta terminar el recorrido.

## Objetivo

Aplicar conceptos fundamentales de programación mediante el uso de arreglos, ciclos, condicionales, métodos y validación de datos.

## Entrada

El programa solicita la cantidad de elementos del arreglo, luego permite ingresar cada número y finalmente solicita el valor que se desea buscar.

Ejemplo:

```text
Ingrese la cantidad de elementos: 5
Ingrese el número 1: 10
Ingrese el número 2: 4
Ingrese el número 3: 8
Ingrese el número 4: 20
Ingrese el número 5: 6

Ingrese el número que desea buscar: 20
```

## Salida

El programa muestra si el número fue encontrado y en qué posición se encuentra.

Ejemplo:

```text
El número 20 fue encontrado en la posición 3.
```

Si el número no existe en el arreglo, muestra:

```text
El número no fue encontrado.
```

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

## Lenguaje utilizado

C#
