
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/05-diseno-y-analisis-de-algoritmos-conteo-y-conexion-de-islas.svg" alt="Conteo y Conexión de Islas (Counting and Connecting Islands)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

El 1 es tierra y el 0 es agua. Primero reúne la tierra que se toca por lados para contar las islas. Después compara qué casillas de agua habría que convertir en tierra para conectarlas todas con el menor cambio posible.

## Qué trabajé y cómo leer los ejemplos

Interpreto 0 como agua y 1 como tierra; las conexiones son horizontales o verticales. F y C son filas y columnas, y K es la cantidad de islas. Una máscara de bits representa un subconjunto: con tres islas, 101 en base 2 selecciona la primera y la tercera, no ciento una islas.

## Planteamiento

Este ejercicio trabaja con una matriz formada por agua y tierra.

Cada celda puede tener uno de los siguientes valores:

```text
0 = agua
1 = tierra
```

El programa debe resolver dos tareas:

1. Contar cuántas islas existen en la matriz.
2. Calcular el mínimo número de celdas de agua que deben convertirse en tierra para conectar todas las islas en una sola.

Una isla está formada por una o más celdas de tierra conectadas vertical u horizontalmente.

## Objetivo

Aplicar recorridos sobre matrices, búsqueda en anchura y programación dinámica para resolver un problema de conexión entre componentes.

## Entrada

El programa recibe primero las dimensiones de la matriz: cantidad de filas y columnas.

Luego recibe la matriz con valores 0 y 1.

Ejemplo:

```text
3 4
1 0 0 1
0 0 0 0
1 0 0 0
```

## Salida

El programa muestra la cantidad de islas y el mínimo número de celdas de agua que deben convertirse para conectarlas.

Ejemplo:

```text
Cantidad de islas: 3
Mínimo de celdas de agua a convertir: 3
```

## Conceptos aplicados

- Matrices
- Búsqueda en anchura
- Recorrido por componentes
- Grafos implícitos
- Programación dinámica
- Máscaras de bits
- Optimización
- Análisis de complejidad

## Análisis del algoritmo

Primero se recorre la matriz para identificar y etiquetar cada isla.

Luego se utiliza una estrategia de programación dinámica con máscaras de bits para representar qué islas han sido conectadas.

El algoritmo calcula el menor costo para conectar subconjuntos de islas, considerando que convertir una celda de agua tiene costo 1 y moverse sobre tierra tiene costo 0.

Finalmente, se obtiene el costo mínimo necesario para conectar todas las islas.

## Complejidad

Sea:

- F = cantidad de filas
- C = cantidad de columnas
- K = cantidad de islas

La complejidad depende de la cantidad de islas, porque se utilizan máscaras de bits.

- Tiempo aproximado: O(3^K × F × C)
- Espacio: O(2^K × F × C)

Este enfoque es adecuado cuando la cantidad de islas no es demasiado grande.

## Lenguaje utilizado

C#

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
