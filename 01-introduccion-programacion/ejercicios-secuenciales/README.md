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

## Entradas, proceso y salidas

| Elemento | Descripción |
|---|---|
| Entrada | 3.9 y 3.10 reciben A; 3.11 y 3.12 reciben A y B. |
| Proceso | Ejecutar las asignaciones en el orden del diagrama. |
| Salida | Valores finales de las variables indicadas. |
| Código disponible | `solution.cpp` corresponde únicamente al ejercicio 3.9. |

## Ejemplo y lectura de resultados

Con A inicial igual a 5, 3.9 termina en A=5 y B=8. Con A=5 en 3.10, termina en A=6 y B=1. Con A=5 y B=2 en 3.11, termina en A=0, B=7 y C=7; en 3.12, ambos quedan en 2. Solo 3.9 cuenta con código C++ entregado; los otros tres se explican como trazas de sus diagramas.

---

## Ejemplo de consola

En este ejemplo incluyo la entrada junto al mensaje que la solicita:

```text
Ingrese A: 5

A: 5
B: 8
```

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

## Archivos de esta carpeta

```text
ejercicios-secuenciales/
├── README.md
├── desarrollo.md
└── solution.cpp
```

Los documentos de apoyo están enlazados en la tabla anterior; no se duplican dentro de la actividad. El [desarrollo](desarrollo.md) amplía el recorrido de los datos, las decisiones y los límites.

## Cómo revisar la actividad

Compilo el archivo independiente con un compilador C++17 y comparo sus resultados con los casos de [validación](../VALIDACION.md). Cada solución tiene su propio main; no se compilan juntas en un solo programa.

## Pruebas y estado

5 casos ejecutados para 3.9. Las trazas 3.10–3.12 son análisis manuales. La versión compilable está verificada en Windows local y en [GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38982024711). Los datos y salidas de los casos se pueden consultar en el [registro de resultados](../tests/results-windows.json).

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
