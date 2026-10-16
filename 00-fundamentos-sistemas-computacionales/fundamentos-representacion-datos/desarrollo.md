# Desarrollo — Representación de datos y fundamentos

## Revisión de conversiones

La entrega manuscrita se conserva como evidencia, con la matrícula retirada de la copia pública. Esta tabla rectifica resultados; no modifica retroactivamente las respuestas del examen.

| Operación | Resultado verificado |
|---|---|
| 57916 a binario | 1110001000111100 |
| 0xB204 a decimal | 45572 |
| 0x7506 a octal | 72406 |
| 110011001101.100100 a decimal | 3277.5625 |
| -8017 en complemento a dos de 32 bits | 0xFFFFE0AF |
| -4707.357 en IEEE 754 binary32 | 0xC5931ADB |
| 0x43DF05A8 interpretado como binary32 | 446.044189453125 |

Para convertir un entero positivo por divisiones sucesivas, se repite hasta que el **cociente** sea cero y se leen los residuos al revés. Detenerse cuando el residuo sea cero sería incorrecto.

La imagen de tamaños corresponde al modelo LP64: `long` ocupa 8 bytes en muchos sistemas Unix de 64 bits. Windows x64 usa LLP64 y `long` ocupa 4; un puntero ocupa 8 en ambos modelos. Consultar `sizeof` en el compilador concreto. La tabla de numeración muestra decimal, hexadecimal y binario, aunque el nombre original también menciona octal.

---

## Procedimiento de análisis

1. Identificar la base de origen, la de destino y el ancho de representación.
2. Para un entero positivo, expandir potencias o dividir sucesivamente hasta que el cociente sea cero.
3. Para un entero negativo con ancho fijo, obtener el valor módulo `2^ancho` y comprobar su interpretación con signo.
4. Para binary32, separar signo, exponente y fracción; interpretar el patrón como número flotante, no como entero hexadecimal.

## Criterios de revisión

| Pregunta | Comprobación |
|---|---|
| ¿Se conserva el valor? | La conversión de vuelta a decimal coincide. |
| ¿Se fija el ancho? | El complemento a dos especifica 32 bits. |
| ¿Qué representa el patrón? | Se distingue un entero de IEEE 754. |
| ¿Depende de la plataforma? | Se identifica LP64 o LLP64 antes de asignar tamaños. |

---

[Actividad](README.md) · [Resultados comunes](../VALIDACION.md)
