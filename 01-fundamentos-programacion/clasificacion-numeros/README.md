# Clasificación de Números

## Descripción del problema

Este ejercicio consiste en leer varios números enteros ingresados por el usuario y clasificarlos según sus características.

El programa determina:

- Cuántos números son positivos.
- Cuántos números son negativos.
- Cuántos números son iguales a cero.
- Cuántos números son pares.
- Cuántos números son impares.
- La suma total de los números ingresados.

## Objetivo

Aplicar estructuras fundamentales de programación como ciclos, condicionales, contadores y acumuladores.

## Entrada

El programa solicita primero la cantidad de números que serán evaluados.

Luego solicita cada número de forma individual.

Ejemplo:

```text
Ingrese la cantidad de números: 6
Ingrese el número 1: 10
Ingrese el número 2: -4
Ingrese el número 3: 0
Ingrese el número 4: 7
Ingrese el número 5: -9
Ingrese el número 6: 2
```

## Salida

El programa muestra la clasificación general de los números ingresados.

Ejemplo:

```text
Resultados:
Positivos: 3
Negativos: 2
Ceros: 1
Pares: 4
Impares: 2
Suma total: 6
```

## Conceptos aplicados

- Entrada de datos
- Variables
- Ciclos
- Condicionales
- Contadores
- Acumuladores
- Operador módulo
- Validación básica

## Análisis del algoritmo

El programa recorre cada número ingresado por el usuario.

En cada iteración, evalúa si el número es positivo, negativo o cero. También verifica si es par o impar usando el operador módulo.

Además, acumula la suma total de todos los números ingresados.

## Complejidad

- Tiempo: O(n)
- Espacio: O(1)

## Lenguaje utilizado

C#
