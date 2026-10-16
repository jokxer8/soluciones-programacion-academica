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

---

## Asignar no significa conservar una ecuación

Una instrucción como `B=A+3` calcula el lado derecho con los valores de ese momento y guarda el resultado en B. No establece una relación que se actualice automáticamente cuando A cambie después.

### Ejercicio 3.9: conservar A y obtener B

| Instrucción | A | B |
|---|---|---|
| Leer A | 5 | Sin usar todavía |
| B=A | 5 | 5 |
| B=B+3 | 5 | 8 |

El código reúne las dos últimas asignaciones en `B=A+3`, con el mismo resultado. La versión compilable comprueba que sumar 3 no exceda el máximo del entero utilizado.

### Ejercicio 3.10: una asignación sobrescrita

| Instrucción | A | B |
|---|---|---|
| Leer A | 5 | Sin usar todavía |
| B=A+6 | 5 | 11 |
| A=A+1 | 6 | 11 |
| B=A−5 | 6 | 1 |

El 11 se pierde al asignar de nuevo B. La salida corresponde al último valor, no a todos los valores que tuvo la variable.

### Ejercicio 3.11: seguir las dependencias

| Instrucción | A | B | C |
|---|---|---|---|
| Leer A y B | 5 | 2 | Sin usar todavía |
| C=A+B | 5 | 2 | 7 |
| B=A+B | 5 | 7 | 7 |
| A=B−C | 0 | 7 | 7 |

### Ejercicio 3.12: por qué no intercambia valores

Con A=5 y B=2, `A=B` deja A=2 y B=2. Después `B=A` conserva ambos en 2: el 5 ya no está en ninguna variable. Un intercambio requiere conservar el A inicial, por ejemplo mediante una variable auxiliar; el diagrama publicado no lo hace.

## Representación y pruebas

3.9 usa `long long` en la versión compilable. La batería comprueba 5→8, −3→0 y un valor cercano al máximo representable, además de rechazar un desbordamiento y texto no numérico. El número grande del caso límite no es un identificador personal: explora el rango del tipo entero en el entorno probado.

Las cuatro trazas usan un número fijo de asignaciones: tiempo y espacio auxiliar O(1). Solo 3.9 se verifica mediante ejecución de C++; 3.10–3.12 se contrastan visualmente con los diagramas.

[Volver a la actividad](README.md) · [Validación de la materia](../VALIDACION.md)
