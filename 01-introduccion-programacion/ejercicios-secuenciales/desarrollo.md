# Desarrollo — Asignaciones y diagramas secuenciales

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

## Punto de partida

Reúno los diagramas 3.9–3.12 y el código disponible del ejercicio 3.9. Los números identifican ejercicios del material de práctica, no versiones del programa ni calificaciones.

## Razonamiento

Una asignación reemplaza el valor anterior. En 3.9, B=A y B=B+3 equivalen a B=A+3 sin modificar A. En 3.10, B=A+6 queda sobrescrito: después se incrementa A y se asigna B=A−5. En 3.11, C=A+B y B=A+B dejan B=C; luego A=B−C vale 0. En 3.12, A=B seguido de B=A no intercambia los valores: ambos terminan con el B inicial.

## Comprobación manual

Con A inicial igual a 5, 3.9 termina en A=5 y B=8. Con A=5 en 3.10, termina en A=6 y B=1. Con A=5 y B=2 en 3.11, termina en A=0, B=7 y C=7; en 3.12, ambos quedan en 2. Solo 3.9 cuenta con código C++ entregado; los otros tres se explican como trazas de sus diagramas.

## Alcance de la revisión

La versión compilable se contrasta con casos normales y límites de entrada. El informe de [validación](../VALIDACION.md) registra el entorno, las diferencias frente a la entrega y los resultados. No se atribuyen esas pruebas a los documentos históricos.

[Documentos de la actividad](README.md#documentos-y-versiones) · [Volver a la materia](../README.md)
