
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/05-diseno-y-analisis-de-algoritmos-fibonacci-recursivo.svg" alt="Fibonacci Recursivo (Recursive Fibonacci)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Cada número se obtiene sumando los dos anteriores. Si pides Fibonacci de 5 mediante llamadas, aparecen cálculos repetidos, como Fibonacci de 3. Guardar un resultado ya obtenido evita volver a resolverlo cuando el ejercicio aplica esa mejora.

### Recursión directa

- **¿Por qué?** Cada número se define por los dos que lo preceden.
- **¿Para qué?** Para traducir la regla matemática en llamadas más pequeñas.
- **¿Cómo?** Define `F(0) = 0` y `F(1) = 1`; para `F(5)` suma `F(4)` y `F(3)`.
- **¿Cuándo usarlo?** Para entender la definición con valores cortos.

### Memorización como mejora

- **¿Por qué?** Distintas ramas pueden pedir de nuevo el mismo resultado.
- **¿Para qué?** Para evitar cálculos repetidos sin cambiar la serie.
- **¿Cómo?** Si ya calculaste `F(3)`, guarda su valor y consúltalo la próxima vez que lo necesites.
- **¿Cuándo usarlo?** Cuando la entrada crece y los subproblemas se repiten; revisa si el ejercicio o código incorpora la mejora.

## Qué trabajé y cómo leer los ejemplos

Calculo términos de Fibonacci a partir de F(0)=0 y F(1)=1. n indica la posición del término: F(5)=5 y F(6)=8. La fórmula F(n)=F(n−1)+F(n−2) explica la recursión; el costo depende de si se vuelven a calcular términos o se guardan resultados.

## Planteamiento

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

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
