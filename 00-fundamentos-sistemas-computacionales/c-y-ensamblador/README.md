# Programación en C y ensamblador FASM

![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

Doce ejercicios equivalentes de entrada/salida, decisiones, ciclos, primos y patrones.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

## Contenido

| Ejercicio | C | FASM Linux x86-64 |
|---|---|---|
| Saludo al mundo | [01-saludo-mundo.c](c/01-saludo-mundo.c) | [01-saludo-mundo.asm](fasm-linux/01-saludo-mundo.asm) |
| Suma, resta y producto de enteros | [02-operaciones-enteros.c](c/02-operaciones-enteros.c) | [02-operaciones-enteros.asm](fasm-linux/02-operaciones-enteros.asm) |
| Suma condicional | [03-suma-condicional.c](c/03-suma-condicional.c) | [03-suma-condicional.asm](fasm-linux/03-suma-condicional.asm) |
| Cuenta regresiva entre extremos | [04-cuenta-regresiva.c](c/04-cuenta-regresiva.c) | [04-cuenta-regresiva.asm](fasm-linux/04-cuenta-regresiva.asm) |
| Conteo dentro del rango 100–200 | [05-rango-100-200.c](c/05-rango-100-200.c) | [05-rango-100-200.asm](fasm-linux/05-rango-100-200.asm) |
| Primalidad | [06-numero-primo.c](c/06-numero-primo.c) | [06-numero-primo.asm](fasm-linux/06-numero-primo.asm) |
| Primos hasta N | [07-primos-hasta-n.c](c/07-primos-hasta-n.c) | [07-primos-hasta-n.asm](fasm-linux/07-primos-hasta-n.asm) |
| Cuadrado hueco | [08-cuadrado-hueco.c](c/08-cuadrado-hueco.c) | [08-cuadrado-hueco.asm](fasm-linux/08-cuadrado-hueco.asm) |
| Escalera ascendente | [09-escalera-ascendente.c](c/09-escalera-ascendente.c) | [09-escalera-ascendente.asm](fasm-linux/09-escalera-ascendente.asm) |
| Escalera descendente | [10-escalera-descendente.c](c/10-escalera-descendente.c) | [10-escalera-descendente.asm](fasm-linux/10-escalera-descendente.asm) |
| Pirámide vertical | [11-piramide-vertical.c](c/11-piramide-vertical.c) | [11-piramide-vertical.asm](fasm-linux/11-piramide-vertical.asm) |
| Escalera invertida | [12-escalera-invertida.c](c/12-escalera-invertida.c) | [12-escalera-invertida.asm](fasm-linux/12-escalera-invertida.asm) |

Los archivos `.asm` usan **FASM**, formato ELF64 y llamadas al sistema Linux. NASM no acepta directamente esta sintaxis. En Windows se puede verificar su ensamblado; para ejecutarlos se necesita Linux x86-64. La equivalencia funcional completa entre ambas versiones queda pendiente de pruebas en Linux.

## Evidencia y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/hw02-v2.pdf). El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). La documentación de revisión posterior se distingue del trabajo histórico.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
