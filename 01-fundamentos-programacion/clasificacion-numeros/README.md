
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/01-fundamentos-programacion-clasificacion-numeros.svg" alt="Clasificación de Números (Number Classification)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Clasificar es decidir a qué grupo pertenece cada número. Para distinguir pares de impares, mira si sobra algo al dividir entre dos. Usa también el cero y un número negativo al revisar las condiciones: el signo y la paridad son preguntas diferentes.

- **¿Por qué?** Que un número sea negativo no determina si es par o impar.
- **¿Para qué?** Para separar dos criterios: signo y paridad.
- **¿Cómo?** Comprueba el signo comparando con cero y la paridad con el resto de dividir entre dos; `−4` es negativo y par.
- **¿Cuándo usarlo?** Cuando debes clasificar enteros por más de una propiedad o revisar casos como cero.

## Qué trabajé y cómo leer los ejemplos

Clasifico cada entrada según su signo y su paridad. Un mismo número puede pertenecer a ambas clasificaciones: −4 es negativo y par. El cero se cuenta por separado por su signo y también cumple la condición de paridad porque su residuo al dividir entre 2 es 0.

## Planteamiento

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

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
