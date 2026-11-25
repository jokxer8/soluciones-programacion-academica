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
