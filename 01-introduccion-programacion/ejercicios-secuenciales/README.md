# Asignaciones y diagramas secuenciales

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

![Área](https://img.shields.io/badge/Área-Algoritmia-green)
![Lenguaje](https://img.shields.io/badge/Lenguaje-C%2B%2B-blue)

## Qué trabajé

Reúno los diagramas 3.9–3.12 y el código disponible del ejercicio 3.9. Los números identifican ejercicios del material de práctica, no versiones del programa ni calificaciones.

---

## Objetivo

Seguir cómo cambia el valor de una variable después de cada instrucción.

## Análisis y conceptos

Una asignación reemplaza el valor anterior. En 3.9, B=A y B=B+3 equivalen a B=A+3 sin modificar A. En 3.10, B=A+6 queda sobrescrito: después se incrementa A y se asigna B=A−5. En 3.11, C=A+B y B=A+B dejan B=C; luego A=B−C vale 0. En 3.12, A=B seguido de B=A no intercambia los valores: ambos terminan con el B inicial.

## Ejemplo y lectura de resultados

Con A inicial igual a 5, 3.9 termina en A=5 y B=8. Con A=5 en 3.10, termina en A=6 y B=1. Con A=5 y B=2 en 3.11, termina en A=0, B=7 y C=7; en 3.12, ambos quedan en 2. Solo 3.9 cuenta con código C++ entregado; los otros tres se explican como trazas de sus diagramas.

---

## Documentos y versiones

| Documento | Contenido |
|---|---|
| [Algoritmo 3.9](../../archivos-originales/introduccion-programacion/secuenciales/algoritmo-3-9.txt) | Texto conservado |
| [Código original 3.9](../../archivos-originales/introduccion-programacion/secuenciales/codigo-3-9.txt) | Texto conservado |
| [Diagrama 3.9](../../archivos-originales/introduccion-programacion/secuenciales/3-9.drawio.png) | Entrega y material de la actividad |
| [Diagrama 3.10](../../archivos-originales/introduccion-programacion/secuenciales/3-10.drawio.png) | Entrega y material de la actividad |
| [Diagrama 3.11](../../archivos-originales/introduccion-programacion/secuenciales/3-11.drawio.png) | Entrega y material de la actividad |
| [Diagrama 3.12](../../archivos-originales/introduccion-programacion/secuenciales/3-12.drawio.png) | Entrega y material de la actividad |

La versión [solution.cpp](solution.cpp) se prepara para esta edición del portafolio. Los textos y documentos enlazados conservan la entrega previa; las diferencias se explican en el [desarrollo](desarrollo.md).

## Cómo revisar la actividad

Compilo el archivo independiente con un compilador C++17 y comparo sus resultados con los casos de [validación](../VALIDACION.md). Cada solución tiene su propio main; no se compilan juntas en un solo programa.

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
