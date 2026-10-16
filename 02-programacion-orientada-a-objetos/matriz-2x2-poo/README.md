# Matriz 2x2 - Programación Orientada a Objetos

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## Qué trabajé y cómo leer los ejemplos

Encapsulo una matriz de dos filas y dos columnas en una clase. Si sus filas son (a, b) y (c, d), el determinante es a·d − b·c. La matriz tiene inversa únicamente cuando ese determinante es distinto de 0; 2×2 describe sus dimensiones, no una multiplicación pendiente.

## Planteamiento

Este ejercicio consiste en crear una clase que represente una matriz de 2x2.

El programa permite calcular el determinante de la matriz, verificar si tiene inversa y, en caso afirmativo, mostrar la matriz inversa.

## Objetivo

Aplicar los principios de la programación orientada a objetos para representar una estructura matemática mediante una clase.

## Conceptos aplicados

- Clases
- Objetos
- Constructores
- Propiedades
- Métodos
- Encapsulación
- Validación
- Determinante
- Matriz inversa

## Estructura de la matriz

Una matriz de 2x2 tiene la siguiente forma:

```text
| a  b |
| c  d |
```

Su determinante se calcula con la fórmula:

```text
determinante = (a * d) - (b * c)
```

La matriz tiene inversa solamente si el determinante es diferente de cero.

## Ejemplo de salida

```text
Matriz original:
| 4  7 |
| 2  6 |

Determinante: 10

La matriz tiene inversa.

Matriz inversa:
| 0.6  -0.7 |
| -0.2  0.4 |
```

## Análisis

Este ejercicio muestra cómo una clase puede representar una entidad matemática y concentrar dentro de ella sus operaciones principales.

La clase `Matriz2x2` contiene los valores de la matriz y los métodos necesarios para calcular el determinante, verificar si existe inversa y generar la matriz inversa.

## Lenguaje utilizado

C#

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
