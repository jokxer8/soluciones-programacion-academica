# Comparación C / FASM en Linux

[![C y FASM en Linux](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/fundamentos-linux.yml/badge.svg?branch=main)](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/fundamentos-linux.yml)

**Resultado: 132 comparaciones correctas, 0 fallidas, 12 pares de programas.** Ejecución en Ubuntu 24.04 x86-64 mediante GitHub Actions, 16 de octubre de 2026 UTC (15 de octubre en Santo Domingo).

[Ver ejecución correcta](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38847529558) · [Registro JSON](tests/results-linux.json) · [Código de las pruebas](tests/compare-linux.py)

## Entorno y método

- Commit probado: `a81dcad179ccd8765dee920802ca23f5b5636b0d`.
- GCC 13.3.0, C17, `-Wall -Wextra -Werror`; FASM 1.73.32 del paquete Ubuntu.
- Se compilan los 12 archivos C y se ensamblan los 12 ejecutables ELF64 a partir de las fuentes del repositorio.
- Cada conjunto de datos se ejecuta con **pipe** y con **terminal PTY** en modo canónico, sin eco de entrada. Cada comparación ejecuta C y FASM: 132 comparaciones representan 264 ejecuciones.
- Ambos resultados se contrastan con una expectativa independiente. No basta con que coincidan entre sí. Cada proceso tiene un límite de tres segundos.
- Se retiran las preguntas de entrada y saltos vacíos de presentación. El texto de primalidad se compara por su significado; se conservan los espacios de alineación de las figuras. No se exige igualdad byte por byte de todos los mensajes.

## Casos ejecutados

| Programa | Datos | Comparaciones |
|---|---|---:|
| 01 Saludo | Sin entrada | 2 |
| 02 Operaciones | Positivos, negativos, cero y producto 50000 × 50000 | 12 |
| 03 Suma condicional | Iguales, distintos, negativos y cero | 8 |
| 04 Cuenta regresiva | Extremos en ambos órdenes y extremos iguales | 6 |
| 05 Rango 100–200 | Límites 100/200, valores exteriores y combinación negativa | 10 |
| 06 Primo | -1, 0, 1, 2, 13, 25, 97 | 14 |
| 07 Primos hasta N | 0, 1, 2, 20, 50 | 10 |
| 08–12 Figuras | -1, 0, 1, 2, 3, 20, 21 en cada programa; espacios y filas contrastados | 70 |
| **Total** | **66 conjuntos × 2 formas de entrada** | **132** |

## Fallos detectados y correcciones

La [primera ejecución](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38847310634), sobre `f9de1e9`, produjo 93 comparaciones correctas y 37 fallidas entre 130 casos. La batería final incorpora además el producto superior a 32 bits.

| Problema observado | Corrección aplicada |
|---|---|
| Una lectura de 64 bytes consumía datos de varios números; algunas rutinas concatenaban dígitos de líneas distintas. | Lectura por token, un byte por syscall, sin consumir los tokens siguientes. Se conservan separadores y signo. |
| La suma condicional perdía el signo negativo al leer o imprimir. | Lectura con signo y conversión de salida con signo en el programa 03. |
| Cuadrado y escalera ascendente llamaban al final de fila como si fuera una subrutina; se saltaba la validación del tamaño. | Salto de línea inicial independiente; se validan 1–20 antes de entrar al bucle. |
| Las figuras interpretaban -1 como 1. | Se conserva el signo antes de comprobar el rango. |
| La mitad creciente de la escalera invertida no imprimía su sangría. | Se añaden los espacios `n - fila` antes de las estrellas. |

Los originales locales siguen intactos. Los hashes de origen y los de las versiones públicas corregidas se distinguen en el [inventario](../archivos-originales/fundamentos-sistemas-computacionales/inventario.json).

## Reproducción

En Linux x86-64 con Python 3, GCC y FASM instalados, desde la raíz del repositorio:

```sh
python3 00-fundamentos-sistemas-computacionales/tests/compare-linux.py
```

El script usa una carpeta temporal y genera `linux-results.json` en el directorio de ejecución. El [workflow](../.github/workflows/fundamentos-linux.yml) se ejecuta al cambiar las fuentes comparadas o la batería, y también permite ejecución manual.

## Límites

Se comprueban los datos indicados, no todo el dominio de enteros. No se certifican desbordamientos, texto arbitrario, fallos de dispositivos o entrada incompleta. La lectura de un byte privilegia claridad y corrección para estos ejercicios, no rendimiento de grandes flujos. ASan/UBSan y el endurecimiento general de entradas siguen siendo ampliaciones independientes.

Las pruebas Windows previas corresponden a su revisión inicial; las nuevas correcciones FASM se comprobaron con ejecución real en Linux. Los programas C y las rutinas NASM Windows no cambiaron.

[Materia](README.md) · [Actividad C/FASM](c-y-ensamblador/README.md) · [Pendientes](../PENDIENTES.md)
