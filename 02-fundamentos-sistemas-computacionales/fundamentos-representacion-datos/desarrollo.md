# Desarrollo — Representación de datos y fundamentos

## Antes de convertir: valor, base y tamaño

Una conversión cambia cómo escribimos un valor. En base 2 se usan 0 y 1; en base 8, los dígitos 0–7; en base 16 se añaden A=10, B=11, C=12, D=13, E=14 y F=15. El prefijo `0x` indica hexadecimal: no es parte de los dígitos del número.

Por ejemplo, `0xB204` representa:

```text
11 × 16³ + 2 × 16² + 0 × 16¹ + 4 × 16⁰
= 45056 + 512 + 0 + 4
= 45572 en decimal
```

Un bit es una posición binaria y un byte contiene 8 bits. Cada dígito hexadecimal representa 4 bits; por eso ocho dígitos hexadecimales permiten escribir un patrón de 32 bits. Para interpretarlo hay que saber si corresponde a un entero sin signo, un entero con signo o un número de punto flotante.

## Revisión de conversiones

La entrega manuscrita se conserva como evidencia, con la matrícula retirada de la copia pública. Esta tabla rectifica resultados; no modifica retroactivamente las respuestas del examen.

| Dato y representación de entrada | Resultado y representación de salida |
|---|---|
| 57916 en decimal | 1110001000111100 en base 2 |
| `0xB204` en hexadecimal | 45572 en decimal |
| `0x7506` en hexadecimal | 72406 en base 8 (no decimal) |
| 110011001101.100100 en base 2 | 3277.5625 en decimal |
| -8017 en complemento a dos de 32 bits | 0xFFFFE0AF |
| -4707.357 en IEEE 754 binary32 | 0xC5931ADB |
| 0x43DF05A8 interpretado como binary32 | 446.044189453125 |

### Enteros negativos y punto flotante

Para representar −8017 en complemento a dos con 32 bits, calculo `2³² − 8017 = 4294959279`. Ese valor sin signo se escribe `0xFFFFE0AF`; interpretado con signo en 32 bits corresponde a −8017. Los dígitos F no son un error: forman parte del patrón que representa el número negativo.

IEEE 754 binary32 distribuye los 32 bits en un signo, 8 bits de exponente y 23 de fracción. `0xC5931ADB` es el patrón que resulta al redondear −4707.357 a este formato; no conserva necesariamente todos sus decimales. `0x43DF05A8`, leído con ese mismo formato, representa 446.044189453125. Convertir esos patrones como enteros produciría otros valores: primero hay que fijar el tipo.

En 110011001101.100100, la parte situada después del punto usa potencias negativas de 2: `.100100` equivale a `1/2 + 1/16 = 0.5625`. El punto separa la parte entera de la fraccionaria; no es un separador de miles.

### Procedimiento y tamaños de datos

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
