# Desarrollo — Programación en C y ensamblador FASM

## Diseño

Los pares C/FASM resuelven problemas similares con distintos niveles de abstracción: `stdio` frente a syscalls, ciclos frente a saltos y variables frente a registros. Los patrones imprimen espacios y asteriscos mediante ciclos anidados. La primalidad prueba divisores; los tamaños y rangos deben respetar el enunciado.

En una consola de desarrollador de Visual Studio: `cl /std:c17 /D_CRT_SECURE_NO_WARNINGS c/06-numero-primo.c /Fe:primo.exe`. Introducir `13` debe producir `13 es primo.` y `1` debe producir `1 no es primo.`

FASM genera ejecutables ELF64. El ensamblado correcto verifica sintaxis y formato; no demuestra la lectura interactiva ni la equivalencia de salida. La rutina de lectura consume bloques de entrada: no asumir que un flujo canalizado con varios números equivale a introducirlos por teclado.


[Actividad](README.md) · [Resultados comunes](../VALIDACION.md)

Corrección de publicación: en el ejercicio 04 se completó la expresión de longitud del separador como `coma_1 = $ - coma`; el original terminaba incompleto. Véase [auditoría](../AUDITORIA.md).
