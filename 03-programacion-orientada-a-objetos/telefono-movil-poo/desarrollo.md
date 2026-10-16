# Desarrollo — Teléfono Móvil - Programación Orientada a Objetos

[Actividad](README.md) · [Código](solution.cs) · [Materia](../README.md)

En este desarrollo explico la lógica de la solución y los conceptos que utilizo. Los datos de entrada y la salida de ejemplo están en el [README](README.md).

## Objetivo

Aplicar conceptos fundamentales de programación orientada a objetos mediante la creación de clases relacionadas entre sí.

## Conceptos aplicados

- Clases
- Objetos
- Propiedades
- Constructores
- Encapsulación
- Composición
- Métodos
- Separación de responsabilidades

## Estructura del programa

El programa contiene las siguientes clases:

### Clase `TelefonoMovil`

Representa el teléfono móvil principal.

Contiene información como:

- Modelo
- Fabricante
- Precio
- Propietario
- Batería
- Pantalla

### Clase `Bateria`

Representa las características de la batería del teléfono.

Contiene:

- Modelo
- Capacidad en mAh
- Tipo de batería

### Clase `Pantalla`

Representa las características de la pantalla.

Contiene:

- Tamaño en pulgadas
- Cantidad de colores

## Análisis

Este ejercicio permite comprender cómo un objeto principal puede estar compuesto por otros objetos.

En este caso, un teléfono móvil contiene una batería y una pantalla. Esto representa una relación de composición, porque esos elementos forman parte del teléfono.
