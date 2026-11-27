
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/03-programacion-orientada-a-objetos-matriz-2x2-poo.svg" alt="Matriz 2x2 - Programación Orientada a Objetos (2×2 Matrix — Object-Oriented Programming)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Una matriz de 2 por 2 es una tabla con cuatro números. El ejercicio reúne esos valores en un objeto y le pide calcular operaciones como el determinante. Antes de buscar la inversa, comprueba si existe: si el determinante es cero, no se puede calcular.

- **¿Por qué?** No todas las matrices tienen inversa.
- **¿Para qué?** Para guardar sus cuatro valores y agrupar las operaciones que dependen de ellos.
- **¿Cómo?** En la matriz del ejemplo calcula `4×6−7×2 = 10`; como no es cero, puedes calcular la inversa.
- **¿Cuándo usarlo?** Cuando manejas matrices 2×2 y necesitas comprobar si sus operaciones están definidas.

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
