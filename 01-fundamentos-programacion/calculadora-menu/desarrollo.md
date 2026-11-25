# Desarrollo — Calculadora con Menú Interactivo

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

El menú pregunta qué operación quieres hacer. Esa opción decide qué cuenta aplicar a los números. Probar una división entre cero ayuda a entender por qué el programa debe revisar los datos antes de calcular.

[Volver a la explicación y los ejemplos de la actividad](README.md#en-palabras-simples).

## Objetivo

Aplicar conceptos fundamentales de programación mediante el uso de ciclos, condicionales, métodos y validaciones básicas.

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
