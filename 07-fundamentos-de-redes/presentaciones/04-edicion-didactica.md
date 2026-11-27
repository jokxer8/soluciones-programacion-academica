# Sistemas de numeración

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Binario y decimal

- **¿Por qué?** Cada posición de un octeto pesa distinto.
- **¿Para qué?** Para leer direcciones y máscaras como valores numéricos.
- **¿Cómo?** Para `10110110`, suma solo los pesos de los bits en 1.
- **¿Cuándo usarlo?** Al interpretar un octeto IPv4.

### Hexadecimal

- **¿Por qué?** Agrupar bits reduce la longitud del texto.
- **¿Para qué?** Para leer valores expresados con dígitos `0–9` y `A–F`.
- **¿Cómo?** Divide el octeto en dos grupos de cuatro: `1011` y `0110` dan `B6`.
- **¿Cuándo usarlo?** Al leer direcciones MAC o convertir valores de una captura.

## Ejercicio

Convierte 10110110 a decimal y hexadecimal y vuelve al número binario para comprobar.

## Comprobación

128 + 32 + 16 + 4 + 2 = 182. Los grupos 1011 y 0110 se leen B y 6.

[Volver a presentaciones](README.md). Material didáctico redactado para el portafolio. No contiene las diapositivas de Cisco.
