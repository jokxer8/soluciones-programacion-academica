# Calculadora con Menú Interactivo

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## Qué trabajé y cómo leer los ejemplos

Resuelvo cada operación a partir de dos números y una opción. En el ejemplo, 1 selecciona la suma y 10 y 5 son sus operandos; el resultado es 15. El 0 del menú significa salir, mientras que un divisor igual a 0 impide efectuar la división.

## Planteamiento

Este ejercicio consiste en crear una calculadora básica utilizando un menú interactivo.

El programa permite al usuario seleccionar una operación matemática, ingresar dos números y obtener el resultado correspondiente.

## Objetivo

Aplicar conceptos fundamentales de programación mediante el uso de ciclos, condicionales, métodos y validaciones básicas.

## Operaciones disponibles

El programa permite realizar las siguientes operaciones:

- Suma
- Resta
- Multiplicación
- División

## Entrada

El usuario selecciona una opción del menú e ingresa dos números.

Ejemplo:

```text
Seleccione una opción:
1. Sumar
2. Restar
3. Multiplicar
4. Dividir
0. Salir
Opción: 1

Ingrese el primer número: 10
Ingrese el segundo número: 5
```

## Salida

El programa muestra el resultado de la operación seleccionada.

Ejemplo:

```text
Resultado: 15
```

## Conceptos aplicados

- Entrada de datos
- Variables
- Menú interactivo
- Ciclos
- Condicionales
- Métodos
- Operaciones aritméticas
- Validación de división entre cero

## Análisis del algoritmo

El programa muestra un menú dentro de un ciclo que se repite hasta que el usuario decide salir.

Según la opción seleccionada, el programa solicita dos números y llama al método correspondiente para realizar la operación.

En el caso de la división, se valida que el segundo número no sea cero.

## Complejidad

- Tiempo: O(1) por operación
- Espacio: O(1)

## Lenguaje utilizado

C#

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
