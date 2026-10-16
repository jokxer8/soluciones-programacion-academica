# Representación de datos y fundamentos

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md)

![Lenguaje](https://img.shields.io/badge/Lenguaje-Representaci%C3%B3n%20de%20datos-blue)
![Materia](https://img.shields.io/badge/Materia-Sistemas%20Computacionales-green)
![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

---

## Qué trabajé y cómo leer los ejemplos

Relaciono las bases numéricas con la forma en que se guardan enteros y números con decimales. En el desarrollo explico cada conversión, el ancho en bits y la diferencia entre interpretar un patrón como entero o como punto flotante.

## Descripción de la actividad

Sistemas de numeración, complemento a dos, IEEE 754 y tamaños de tipos.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

---

## Objetivo

Relacionar la representación binaria de la información con los valores que utiliza un programa, distinguiendo sistemas de numeración, enteros con signo, punto flotante y modelos de memoria.

---

## Conceptos aplicados

- Conversiones entre decimal, binario, octal y hexadecimal.
- Complemento a dos y ancho fijo de representación.
- Campos de signo, exponente y fracción de IEEE 754 binary32.
- Tamaños de tipos, punteros y diferencias entre LP64 y LLP64.

---

## Actividad desarrollada

Las notas de teoría, tablas visuales y respuestas manuscritas se presentan junto a una revisión independiente de conversiones. El desarrollo distingue el resultado entregado de las rectificaciones posteriores, y explica por qué un tamaño de tipo depende del modelo de datos del compilador.

## Archivos y soluciones

- [Desarrollo y rectificaciones](desarrollo.md).
- [Notas de teoría](../../archivos-originales/fundamentos-sistemas-computacionales/notas/teoria.txt).
- [Tabla visual](../../archivos-originales/fundamentos-sistemas-computacionales/referencias-visuales/tabla-de-binarios-octal-y-hexadecimal.png).
- [Tamaños de memoria](../../archivos-originales/fundamentos-sistemas-computacionales/referencias-visuales/espacio-en-memoria.png).
- [Asignación 1: enunciado](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/asignacion-1.pdf). No se encontraron respuestas a esta asignación; no se presenta como resuelta.

---

## Ejemplo de análisis y uso

| Caso de análisis | Resultado | Interpretación |
|---|---|---|
| Hexadecimal `0xB204` | `45572` decimal | Suma de los dígitos ponderados por potencias de 16. |
| Entero `-8017` en 32 bits | `0xFFFFE0AF` | Representación en complemento a dos. |
| Bits `0x43DF05A8` como binary32 | `446.044189453125` | Los mismos bits se interpretan según el formato elegido. |

La tabla completa y las observaciones sobre las respuestas del parcial están en [desarrollo.md](desarrollo.md).

---

## Organización de esta carpeta

```text
fundamentos-representacion-datos/
├── README.md
└── desarrollo.md

Evidencia enlazada en archivos-originales/:
  documentos/, notas/ y referencias-visuales/
```

---

## Cómo revisar la actividad

Comenzar por las conversiones de desarrollo.md, contrastarlas con el parcial y consultar las notas como respaldo. La asignación 1 se identifica expresamente como enunciado sin respuestas localizadas. La tabla visual de tamaños se interpreta como LP64; no se extrapola a todos los sistemas de 64 bits.

---

## Relación con los archivos originales y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/1er-parcial-kelvin.pdf). El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). Las correcciones se explican por separado de las entregas originales.

---

## Lenguaje y herramientas

Aritmética de bases, lectura de bits e inspección de tipos de datos. No requiere instalar una aplicación para revisar la evidencia.

## Estado de la actividad

Actividad organizada y documentada como parte del portafolio académico. Los resultados verificados y los límites se detallan en los informes enlazados; la documentación posterior se distingue de la evidencia de entrega.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
