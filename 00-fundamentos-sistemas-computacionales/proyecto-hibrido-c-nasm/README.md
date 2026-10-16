# Proyecto híbrido C y NASM

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](.)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%20%2F%20Ensamblador-blue)
![Materia](https://img.shields.io/badge/Materia-Sistemas%20Computacionales-green)
![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

---

## Qué trabajé y cómo leer los ejemplos

Integro funciones de ensamblador en un programa C. RCX, RDX, R8 y R9 son nombres de registros del procesador usados para pasar argumentos en Windows x64; no son valores hexadecimales. La convención de llamada determina cómo se comunican ambos lenguajes.

## Descripción de la actividad

Interoperabilidad C/ensamblador x64 para sumar, ordenar e intercambiar enteros.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

---

## Objetivo

Integrar una interfaz en C con funciones NASM, comprendiendo el contrato de parámetros, los valores de retorno y la convención de llamadas de Windows x64.

---

## Conceptos aplicados

- Interoperabilidad entre C y ensamblador.
- Punteros a enteros y arreglos.
- Registros de argumentos y valor de retorno.
- Suma, bubble sort e intercambio por dirección.
- Ensamblado de objetos COFF y enlazado con el programa principal.

---

## Actividad desarrollada

C valida la cantidad de elementos, obtiene los datos y presenta los resultados. Las operaciones se delegan en tres rutinas NASM: sum calcula el total, bubble_sort modifica el arreglo y swap intercambia dos variables mediante sus direcciones.

## Archivos y soluciones

- [main.c](main.c): interfaz, validación y llamadas a las rutinas.
- [sum.asm](sum.asm): suma del arreglo.
- [bubble_sort.asm](bubble_sort.asm): orden ascendente.
- [swap.asm](swap.asm): intercambio por dirección.

Requiere Windows x64, herramientas C de Visual Studio y NASM. Ensamblar con `nasm -f win64`, después enlazar los tres objetos con `main.c`. El script común automatiza ese proceso. No es un proyecto Linux: utiliza la convención de llamadas de Windows y funciones `scanf_s`/`sprintf_s`.

---

## Ejemplo de análisis y uso

```text
Arreglo inicial:   30, 50, 10, 40, 20
Arreglo ordenado:  10, 20, 30, 40, 50
Suma:             150

Antes:    x = 4,  y = -7
Después:  x = -7, y = 4
```

También se comprobó recuperación ante tamaño cero y una entrada no numérica antes de continuar con datos válidos. El tamaño aceptado es de 1 a 20.

---

## Organización de esta carpeta

```text
proyecto-hibrido-c-nasm/
├── README.md
├── desarrollo.md
├── main.c
├── sum.asm
├── bubble_sort.asm
└── swap.asm
```

---

## Cómo revisar la actividad

En una consola de desarrollador **x64** de Visual Studio, con NASM disponible, desde esta carpeta:

```text
nasm -f win64 sum.asm -o sum.obj
nasm -f win64 bubble_sort.asm -o bubble_sort.obj
nasm -f win64 swap.asm -o swap.obj
cl /std:c17 main.c sum.obj bubble_sort.obj swap.obj /Fe:hibrido.exe
hibrido.exe
```

Los objetos y el ejecutable son productos de compilación, no nuevas evidencias académicas. La batería común automatiza la compilación y los casos registrados.

---

## Relación con los archivos originales y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/resumen-del-proyecto.pdf). El resumen estaba guardado dentro de la asignación 2; su contenido describe este proyecto híbrido. El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). Las correcciones se explican por separado de las entregas originales.

---

## Lenguaje y herramientas

C17, MSVC, NASM y Windows x64. Este proyecto utiliza la ABI de Windows, diferente del formato ELF64 y las syscalls de los ejercicios FASM.

## Estado de la actividad

Actividad organizada y documentada como parte del portafolio académico. Los resultados verificados y los límites se detallan en los informes enlazados; la documentación posterior se distingue de la evidencia de entrega.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
