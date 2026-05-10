# Fibonacci Recursivo

## Descripción del problema

Este ejercicio consiste en calcular el número de Fibonacci correspondiente a una posición indicada por el usuario.

La sucesión de Fibonacci inicia con los valores 0 y 1. A partir de ahí, cada número se obtiene sumando los dos anteriores.

Ejemplo:

```text
0, 1, 1, 2, 3, 5, 8, 13, 21...
```

## Objetivo

Aplicar el concepto de recursividad para resolver un problema clásico de programación y analizar su comportamiento algorítmico.

## Entrada

El programa solicita al usuario un número entero positivo o igual a cero.

Ejemplo:

```text
Ingrese la posición de Fibonacci: 8
```

## Salida

El programa muestra el valor correspondiente dentro de la sucesión de Fibonacci.

Ejemplo:

```text
El número Fibonacci en la posición 8 es: 21
```

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

## Lenguaje utilizado

C#
