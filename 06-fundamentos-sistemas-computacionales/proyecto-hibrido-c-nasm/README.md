# Proyecto híbrido C y NASM

![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

Interoperabilidad C/ensamblador x64 para sumar, ordenar e intercambiar enteros.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

## Contenido

- [main.c](main.c): interfaz, validación y llamadas a las rutinas.
- [sum.asm](sum.asm): suma del arreglo.
- [bubble_sort.asm](bubble_sort.asm): orden ascendente.
- [swap.asm](swap.asm): intercambio por dirección.

Requiere Windows x64, herramientas C de Visual Studio y NASM. Ensamblar con `nasm -f win64`, después enlazar los tres objetos con `main.c`. El script común automatiza ese proceso. No es un proyecto Linux: utiliza la convención de llamadas de Windows y funciones `scanf_s`/`sprintf_s`.

## Evidencia y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/resumen-del-proyecto.pdf). El resumen estaba guardado dentro de la asignación 2; su contenido describe este proyecto híbrido. El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). La documentación de revisión posterior se distingue del trabajo histórico.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
