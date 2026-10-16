# Fibonacci: ciclos y corrida manual

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

![Área](https://img.shields.io/badge/Área-Algoritmia-green)
![Lenguaje](https://img.shields.io/badge/Lenguaje-C%2B%2B-blue)

## Qué trabajé

Conservo un fragmento de práctica que genera Fibonacci y preparo una versión independiente para compilarlo. El fragmento no incluye las cabeceras y siempre imprime los dos primeros valores, incluso si se solicita menos de dos.

---

## Objetivo

Relacionar la actualización de variables con la secuencia generada.

## Análisis y conceptos

La secuencia empieza en F(0)=0 y F(1)=1. Cada término siguiente es la suma de los dos anteriores. La entrada indica cuántos términos imprimir, no el índice del último. La versión compilable admite de 1 a 93 términos y usa enteros sin signo de 64 bits para este rango.

## Ejemplo y lectura de resultados

Con 7 términos la salida es 0 1 1 2 3 5 8. Antes de cada paso guardo el término anterior y el actual; calculo su suma y desplazo ambos valores. Para 1 término imprimo solo 0. El tiempo crece de forma lineal con n y el espacio auxiliar se mantiene constante.

---

## Documentos y versiones

| Documento | Contenido |
|---|---|
| [Fragmento de práctica](../../archivos-originales/introduccion-programacion/fibonacci/fragmento-original.txt) | Texto conservado |

La versión [solution.cpp](solution.cpp) se prepara para esta edición del portafolio. Los textos y documentos enlazados conservan la entrega previa; las diferencias se explican en el [desarrollo](desarrollo.md).

## Cómo revisar la actividad

Compilo el archivo independiente con un compilador C++17 y comparo sus resultados con los casos de [validación](../VALIDACION.md). Cada solución tiene su propio main; no se compilan juntas en un solo programa.

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
