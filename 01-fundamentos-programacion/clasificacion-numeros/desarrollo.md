# Desarrollo — Clasificación de Números

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Clasificar es decidir a qué grupo pertenece cada número. Para distinguir pares de impares, mira si sobra algo al dividir entre dos. Usa también el cero y un número negativo al revisar las condiciones: el signo y la paridad son preguntas diferentes.

[Volver a la explicación y los ejemplos de la actividad](README.md#en-palabras-simples).

## Objetivo

Aplicar estructuras fundamentales de programación como ciclos, condicionales, contadores y acumuladores.

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
