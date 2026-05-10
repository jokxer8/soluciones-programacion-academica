# Teléfono Móvil - Programación Orientada a Objetos

## Descripción del problema

Este ejercicio consiste en modelar un teléfono móvil utilizando los principios de la programación orientada a objetos.

El programa permite representar un celular con información como modelo, fabricante, precio, propietario, batería y pantalla.

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

## Ejemplo de salida

```text
Información del teléfono móvil

Modelo: Galaxy A54
Fabricante: Samsung
Precio: 35000
Propietario: Kelvin César Del Castillo

Batería:
Modelo: EB-BA546ABY
Capacidad: 5000 mAh
Tipo: Li-Ion

Pantalla:
Tamaño: 6.4 pulgadas
Colores: 16000000
```

## Análisis

Este ejercicio permite comprender cómo un objeto principal puede estar compuesto por otros objetos.

En este caso, un teléfono móvil contiene una batería y una pantalla. Esto representa una relación de composición, porque esos elementos forman parte del teléfono.

## Lenguaje utilizado

C#
