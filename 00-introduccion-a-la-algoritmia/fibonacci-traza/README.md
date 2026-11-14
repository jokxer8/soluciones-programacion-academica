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

## Entradas, proceso y salidas

| Elemento | Descripción |
|---|---|
| Entrada | Cantidad n de términos, de 1 a 93. |
| Proceso | Imprimir el término anterior y actualizar el par de valores. |
| Salida | Exactamente n términos, empezando por 0. |
| Rango | 93 términos abarcan F(0) hasta F(92); no significan calcular F(93). |

## Ejemplo y lectura de resultados

Con 7 términos la salida es 0 1 1 2 3 5 8. Antes de cada paso guardo el término anterior y el actual; calculo su suma y desplazo ambos valores. Para 1 término imprimo solo 0. El tiempo crece de forma lineal con n y el espacio auxiliar se mantiene constante.

---

## Ejemplo de consola

En este ejemplo incluyo la entrada junto al mensaje que la solicita:

```text
Ingrese la cantidad de terminos (1 a 93): 7

0 1 1 2 3 5 8
```

## Documentos y versiones

| Documento | Contenido |
|---|---|
| [Fragmento de práctica](../../archivos-originales/introduccion-programacion/fibonacci/fragmento-original.txt) | Texto conservado |

La versión [solution.cpp](solution.cpp) se prepara para esta edición del portafolio. Los textos y documentos enlazados conservan la entrega previa; las diferencias se explican en el [desarrollo](desarrollo.md).

## Archivos de esta carpeta

```text
fibonacci-traza/
├── README.md
├── desarrollo.md
└── solution.cpp
```

Los documentos de apoyo están enlazados en la tabla anterior; no se duplican dentro de la actividad. El [desarrollo](desarrollo.md) amplía el recorrido de los datos, las decisiones y los límites.

## Cómo revisar la actividad

Compilo el archivo independiente con un compilador C++17 y comparo sus resultados con los casos de [validación](../VALIDACION.md). Cada solución tiene su propio main; no se compilan juntas en un solo programa.

## Pruebas y estado

7 casos: 1, 2, 7 y 93 términos; rechazo de 0, 94 y texto. La versión compilable está verificada en Windows local y en [GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38982024711). Los datos y salidas de los casos se pueden consultar en el [registro de resultados](../tests/results-windows.json).

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
