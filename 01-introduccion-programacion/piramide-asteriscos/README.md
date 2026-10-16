# Pirámide de asteriscos y revisión entre grupos

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

![Área](https://img.shields.io/badge/Área-Algoritmia-green)
![Lenguaje](https://img.shields.io/badge/Lenguaje-C%2B%2B-blue)

## Qué trabajé

Esta actividad corresponde al foro del grupo 2. Relaciono el análisis del problema, el pseudocódigo, el diagrama y el código de una pirámide centrada. Las rúbricas y respuestas permiten consultar también la revisión entre grupos.

---

## Objetivo

Aplicar validaciones y ciclos anidados para construir un patrón.

## Análisis y conceptos

n es la cantidad de pisos y está entre 1 y 60, ambos incluidos. En el piso p se imprimen n−p espacios y 2p−1 asteriscos. El 60 es un límite de la actividad, no una cantidad de caracteres por fila: la base puede llegar a 119 asteriscos.

## Entradas, proceso y salidas

| Elemento | Descripción |
|---|---|
| Entrada | Un número entero n de pisos, entre 1 y 60. |
| Proceso | Por cada piso p: n−p espacios y 2p−1 asteriscos. |
| Salida | n filas que forman una pirámide centrada. |
| Límites | Se incluyen 1 y 60; se rechazan 0, negativos y valores mayores que 60. |

## Ejemplo y lectura de resultados

Para n=3, las filas tienen 2, 1 y 0 espacios, seguidos de 1, 3 y 5 asteriscos. El código original usa n−p; algunas líneas del pseudocódigo y del DFD indican n−1, lo que mantendría constante la sangría. La explicación adopta n−p y conserva la evidencia de esa diferencia. El rango correcto incluye 60, aunque una frase del análisis lo describe de forma contradictoria.

---

## Ejemplo de consola

En este ejemplo incluyo la entrada junto al mensaje que la solicita:

```text
Ingrese el numero de pisos (1 a 60): 3

  *
 ***
*****
```

## Documentos y versiones

| Documento | Contenido |
|---|---|
| [Análisis](../../archivos-originales/introduccion-programacion/piramide/analisis.pdf) | Entrega y material de la actividad |
| [Algoritmo y DFD](../../archivos-originales/introduccion-programacion/piramide/algoritmo-dfd.pdf) | Entrega y material de la actividad |
| [Código original](../../archivos-originales/introduccion-programacion/piramide/codigo-original.txt) | Texto conservado |
| [Retroalimentación al grupo 1](../../archivos-originales/introduccion-programacion/piramide/retroalimentacion-a-grupo-1.pdf) | Entrega y material de la actividad |
| [Retroalimentación al grupo 3](../../archivos-originales/introduccion-programacion/piramide/retroalimentacion-a-grupo-3.pdf) | Entrega y material de la actividad |
| [Respuesta al grupo 1](../../archivos-originales/introduccion-programacion/piramide/respuesta-a-grupo-1.docx) | Entrega y material de la actividad |
| [Respuesta al grupo 3](../../archivos-originales/introduccion-programacion/piramide/respuesta-a-grupo-3.docx) | Entrega y material de la actividad |
| [Rúbrica grupo 1](../../archivos-originales/introduccion-programacion/piramide/rubrica-grupo-1.docx) | Entrega y material de la actividad |
| [Rúbrica grupo 3](../../archivos-originales/introduccion-programacion/piramide/rubrica-grupo-3.docx) | Entrega y material de la actividad |
| [Rúbrica foro 2, grupo 1](../../archivos-originales/introduccion-programacion/piramide/rubrica-foro-2-grupo-1.docx) | Entrega y material de la actividad |
| [Rúbrica foro 2, grupo 3](../../archivos-originales/introduccion-programacion/piramide/rubrica-foro-2-grupo-3.docx) | Entrega y material de la actividad |

La versión [solution.cpp](solution.cpp) se prepara para esta edición del portafolio. Los textos y documentos enlazados conservan la entrega previa; las diferencias se explican en el [desarrollo](desarrollo.md).

## Archivos de esta carpeta

```text
piramide-asteriscos/
├── README.md
├── desarrollo.md
└── solution.cpp
```

Los documentos de apoyo están enlazados en la tabla anterior; no se duplican dentro de la actividad. El [desarrollo](desarrollo.md) amplía el recorrido de los datos, las decisiones y los límites.

## Cómo revisar la actividad

Compilo el archivo independiente con un compilador C++17 y comparo sus resultados con los casos de [validación](../VALIDACION.md). Cada solución tiene su propio main; no se compilan juntas en un solo programa.

## Pruebas y estado

7 casos: alturas 1, 3 y 60; rechazo de −1, 0, 61 y texto. La versión compilable está verificada en Windows local y en [GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38982024711). Los datos y salidas de los casos se pueden consultar en el [registro de resultados](../tests/results-windows.json).

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
