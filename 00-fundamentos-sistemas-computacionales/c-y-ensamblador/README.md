# Programación en C y ensamblador FASM

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [C](c) · [FASM](fasm-linux)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%20%2F%20Ensamblador-blue)
![Materia](https://img.shields.io/badge/Materia-Sistemas%20Computacionales-green)
![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

---

## Descripción de la actividad

Doce ejercicios equivalentes de entrada/salida, decisiones, ciclos, primos y patrones.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

---

## Objetivo

Resolver los mismos problemas con C y ensamblador, comparando cómo se expresan entrada/salida, operaciones, condiciones y ciclos en un lenguaje de alto nivel y en instrucciones x86-64.

---

## Conceptos aplicados

- Entrada/salida con stdio y llamadas al sistema Linux.
- Aritmética entera, condiciones, saltos y registros.
- Ciclos, divisibilidad y generación de números primos.
- Patrones de asteriscos, espacios y validación de rangos.
- Comparación reproducible de resultados por pipe y terminal PTY.

---

## Actividad desarrollada

La colección contiene doce pares de soluciones. Los primeros ejercicios trabajan decisiones y recorridos numéricos; los últimos construyen figuras mediante ciclos anidados. Las tablas siguientes permiten abrir cada versión por separado sin mezclar la sintaxis FASM para Linux con la del proyecto NASM para Windows.

## Archivos y soluciones

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

Los archivos `.asm` usan **FASM**, formato ELF64 y llamadas al sistema Linux. NASM no acepta directamente esta sintaxis. En Windows se puede verificar su ensamblado; para ejecutarlos se necesita Linux x86-64. La comparación en Linux de los doce pares está verificada para los casos documentados: **132 correctos y 0 fallidos**, con pipe y terminal PTY. [Informe de pruebas y correcciones](../PRUEBAS-LINUX.md).

---

## Ejemplo de análisis y uso

| Ejercicio | Entrada | Resultado esperado |
|---|---|---|
| Operaciones de enteros | `5`, `3` | Suma `8`, resta `2`, producto `15`. |
| Suma condicional | `4`, `4` | `16`: se duplica la suma al ser iguales. |
| Primalidad | `13` / `1` | Primo / no primo. |

Cuadrado hueco con tamaño `3`:

```text
***
* *
***
```

La comparación conserva los espacios de las figuras; los mensajes de entrada pueden variar. [132 comparaciones correctas en Linux](../PRUEBAS-LINUX.md).

---

## Organización de esta carpeta

```text
c-y-ensamblador/
├── README.md
├── desarrollo.md
├── c/
│   └── 12 programas .c independientes
└── fasm-linux/
    └── 12 programas .asm con los mismos nombres base
```

---

## Cómo revisar la actividad

En Linux x86-64, con GCC y FASM instalados, se puede revisar el caso de primalidad desde esta carpeta:

```sh
gcc -std=c17 c/06-numero-primo.c -o /tmp/primo-c
fasm fasm-linux/06-numero-primo.asm /tmp/primo-asm
chmod +x /tmp/primo-asm
printf '13\n' | /tmp/primo-c
printf '13\n' | /tmp/primo-asm
```

Para contrastar los doce pares, seguir la [reproducción de la batería Linux](../PRUEBAS-LINUX.md).

---

## Relación con los archivos originales y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/hw02-v2.pdf). El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). La documentación de revisión posterior se distingue del trabajo histórico.

---

## Lenguaje y herramientas

C17 y GCC en Linux para la comparación; MSVC en la revisión Windows de C. FASM 1.73.32 genera ejecutables ELF64 para Linux x86-64.

## Estado de la actividad

Actividad organizada y documentada como parte del portafolio académico. Los resultados verificados y los límites se detallan en los informes enlazados; la documentación posterior se distingue de la evidencia de entrega.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
