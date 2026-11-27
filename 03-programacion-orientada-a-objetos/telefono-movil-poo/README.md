
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/03-programacion-orientada-a-objetos-telefono-movil-poo.svg" alt="Teléfono Móvil - Programación Orientada a Objetos (Mobile Phone — Object-Oriented Programming)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cs)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-blue)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

Un teléfono tiene datos que describen su estado y acciones que pueden cambiarlo. Lee la clase preguntando qué recuerda el objeto, qué puede hacer y qué reglas impiden cambiar sus datos de cualquier manera.

- **¿Por qué?** Batería, pantalla y teléfono guardan datos y unidades diferentes.
- **¿Para qué?** Para modelar un teléfono con componentes que tienen sus propias propiedades.
- **¿Cómo?** Crea una batería y una pantalla; luego úsalas al construir el teléfono y revisa qué valor pertenece a cada objeto.
- **¿Cuándo usarlo?** Cuando un objeto se entiende mejor como un conjunto de piezas con responsabilidades propias.

## Qué trabajé y cómo leer los ejemplos

Agrupo las características de un teléfono y sus componentes mediante objetos. El precio, la capacidad de batería y las medidas de pantalla representan magnitudes diferentes; deben leerse con la unidad que acompañe al dato. mAh expresa capacidad de carga y no una duración fija en horas.

## Planteamiento

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

---

[Volver a la materia](../README.md) · [Notación y estados](../../GUIA-DE-LECTURA.md)
