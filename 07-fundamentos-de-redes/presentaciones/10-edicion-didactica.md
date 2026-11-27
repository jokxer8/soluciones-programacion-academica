# Direccionamiento IPv4

[Fundamentos de Redes](../README.md) · [Archivo de la materia](../archivo/README.md)

## En palabras simples

![En palabras simples](../../assets/readme-banners/en-palabras-simples.svg)

### Prefijo, red y difusión

- **¿Por qué?** El prefijo determina el tamaño y los límites del bloque.
- **¿Para qué?** Para separar direcciones asignables de red y difusión en una LAN típica.
- **¿Cómo?** Un `/27` agrupa 32 direcciones; ubica el bloque que contiene `.77`.
- **¿Cuándo usarlo?** Al validar una máscara y su rango de hosts.

### Bloques según demanda

- **¿Por qué?** Dos LAN pueden necesitar números de hosts distintos.
- **¿Para qué?** Para asignar un prefijo a cada grupo sin usar un bloque demasiado grande.
- **¿Cómo?** Compara la capacidad de `/26` y `/27` con las demandas de 42 y 19 hosts.
- **¿Cuándo usarlo?** Cuando varias áreas comparten un bloque de direcciones.

## Ejercicio

Calcula red, rango y difusión de 192.0.2.77/27. Luego compara la demanda de dos LAN de 42 y 19 hosts.

## Comprobación

La red es .64, la difusión .95 y los hosts .65–.94. La demanda de 42 y 19 requiere /26 y /27 respectivamente.

[Volver a presentaciones](README.md). Material didáctico redactado para el portafolio. No contiene las diapositivas de Cisco.
