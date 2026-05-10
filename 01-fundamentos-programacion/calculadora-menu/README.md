# Calculadora con Menú Interactivo

## Descripción del problema

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
