# Direccionamiento, subnetting y VLSM

[Fundamentos de Redes](../../README.md) · [Todas las actividades](../README.md)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../../assets/readme-banners/en-palabras-simples.svg)

Dividir una red es como repartir un bloque de direcciones en grupos más pequeños. Con VLSM puedes reservar grupos grandes donde hay más equipos y pequeños donde hay menos.

## Qué se trabajó

Ejemplo nuevo: divide 192.0.2.0/24 en /26. Son cuatro bloques de 64; el primero es 192.0.2.0–63, con hosts .1 a .62 y broadcast .63. Para VLSM asigna primero los bloques grandes y revisa que los rangos no se solapen.

## Evidencia y alcance

**Material localizado:** Cuaderno de Robb Jones, una entrega escaneada de 14 páginas y un solucionario de apoyo de 44 páginas.

El cuaderno usa una convención histórica que excluye las subredes de bits prestados todos 0 o todos 1 en ciertos ejercicios. No se aplica esa resta al conteo moderno de subredes. No se redistribuye el cuaderno ni un solucionario íntegro.

## Archivo de respaldo o estudio

[Cuaderno manuscrito de Kelvin (PDF)](../../tareas/09-subnetting/cuaderno-manuscrito.pdf), 14 páginas de trabajo original. El cálculo de subredes «utilizables» que resta dos al total se conserva como convención histórica del ejercicio: en CIDR no se descuentan la primera y la última subred. No confundirlo con las direcciones reservadas dentro de una subred. Para un ejemplo actualizado, abre la lámina 5 de la [guía](../../material-de-estudio/guia-de-estudio.pdf).

[Ver los archivos de esta actividad](../../tareas/09-subnetting/cuaderno-manuscrito.pdf).
