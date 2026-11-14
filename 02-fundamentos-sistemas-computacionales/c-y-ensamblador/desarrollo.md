# Desarrollo — Programación en C y ensamblador FASM

## Diseño

Los pares C/FASM resuelven problemas similares con distintos niveles de abstracción: `stdio` frente a syscalls, ciclos frente a saltos y variables frente a registros. Los patrones imprimen espacios y asteriscos mediante ciclos anidados. La primalidad prueba divisores; los tamaños y rangos deben respetar el enunciado.

En una consola de desarrollador de Visual Studio: `cl /std:c17 /D_CRT_SECURE_NO_WARNINGS c/06-numero-primo.c /Fe:primo.exe`. Introducir `13` debe producir `13 es primo.` y `1` debe producir `1 no es primo.`

FASM genera ejecutables ELF64. El ensamblado correcto verifica sintaxis y formato; no demuestra la lectura interactiva ni la equivalencia de salida. La revisión Linux corrigió la lectura por bloques que consumía varios valores: ahora se lee por token y se preserva el signo. También se corrigieron el salto inicial de los ejercicios 08/09 y la alineación del 12. La comparación por pipe y terminal PTY está registrada en [PRUEBAS-LINUX.md](../PRUEBAS-LINUX.md).

---

## Correspondencia entre las versiones

| Construcción en C | Idea equivalente en FASM |
|---|---|
| `scanf` y `printf` | Lectura de tokens, conversión numérica y syscalls de entrada/salida. |
| `if` y operadores relacionales | Comparación y salto condicional. |
| `for` y `while` | Inicialización, condición, cuerpo y salto de repetición. |
| Variables de control | Valores en registros, conservados entre las operaciones necesarias. |

## Coste de los ejercicios

- Operaciones y rango: tiempo constante para los datos de entrada.
- Cuenta regresiva: tiempo proporcional a la cantidad de valores impresos.
- Primalidad: prueba de divisores hasta la raíz cuadrada para los casos de la implementación.
- Cuadrado hueco: `n²` posiciones; escaleras y pirámides: cantidad cuadrática de caracteres en el tamaño máximo de la figura.

## Verificación funcional

La batería ejecuta cada caso con C y FASM y los contrasta con un resultado independiente. Incluye negativos, cero, tamaños 1 y 20, rechazo de -1/0/21 para las figuras y conservación de la sangría. No afirma equivalencia de todos los textos ni cobertura de desbordamientos o entrada incompleta. [Informe y correcciones](../PRUEBAS-LINUX.md).

---

[Actividad](README.md) · [Resultados comunes](../VALIDACION.md)

Corrección de publicación: en el ejercicio 04 se completó la expresión de longitud del separador como `coma_1 = $ - coma`; el original terminaba incompleto. Véase [auditoría](../AUDITORIA.md).
