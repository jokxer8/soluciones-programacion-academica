# Cine - Modelado y Normalización

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Evidencia](modelo-y-diccionario.pdf)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SQL%20/%20C%23-lightgrey)

## Descripción de la actividad

Diseño de un cine y ejercicios de transformación relacional y normalización de ventas.

---

## Objetivo

Transformar requisitos en relaciones y analizar la separación de datos para reducir redundancias.

---

## Conceptos aplicados

- Modelo entidad-relación.
- Modelo relacional.
- Primera, segunda y tercera forma normal.
- Dependencias funcionales.
- Diccionario de datos.

---

## Actividad desarrollada

El ejercicio 5.1 presenta el DER del cine, su modelo relacional y el diccionario. El ejercicio 5.3 recorre la separación de datos de ventas de boletos en clientes, películas, formatos, tipos de asiento, monedas, territorios y ventas. La transformación de hospital se conserva como parte del compendio.

---

## Ejemplo de análisis

El dato compuesto «Película: Dune Part Two, Formato: IMAX 2D, Tipo de asiento: Adulto» se descompone en atributos identificables. Los catálogos de la propuesta permiten referenciar esos conceptos desde el producto o la venta.

---

## Archivos de esta carpeta

```text
cine-modelado-normalizacion/
├── README.md
├── desarrollo.md
├── diagrama-cine.drawio
├── diagrama-cine.png
├── diccionario-cine.xlsx
├── modelo-relacional.xlsx
├── modelo-y-diccionario.pdf
├── normalizacion-ventas.pdf
├── normalizacion-ventas.xlsx
└── transformacion-hospital.xlsx
```

- [diagrama-cine.drawio](diagrama-cine.drawio)
- [diagrama-cine.png](diagrama-cine.png)
- [diccionario-cine.xlsx](diccionario-cine.xlsx)
- [modelo-relacional.xlsx](modelo-relacional.xlsx)
- [modelo-y-diccionario.pdf](modelo-y-diccionario.pdf)
- [normalizacion-ventas.pdf](normalizacion-ventas.pdf)
- [normalizacion-ventas.xlsx](normalizacion-ventas.xlsx)
- [transformacion-hospital.xlsx](transformacion-hospital.xlsx)

El archivo [desarrollo.md](desarrollo.md) explica las decisiones y el alcance de la solución.

---

## Cómo revisar la actividad

Consultar el diagrama y el modelo del ejercicio 5.1; después comparar las etapas de normalización del ejercicio 5.3. Son ejercicios relacionados, con supuestos propios, y no un único esquema implementado.

---

## Relación con los archivos originales

Los archivos proceden de la carpeta académica Github indicada por el autor. El [inventario](../../archivos-originales/bases-de-datos/INVENTARIO.md) relaciona cada origen con su copia publicada y registra las versiones y duplicados.

## Lenguaje y herramientas

Modelado relacional; documentos PDF, diagramas y hojas de cálculo.

## Estado

Actividad organizada y documentada como parte del portafolio académico de Bases de Datos. El [registro de validación](../VALIDACION.md) distingue la revisión realizada de las pruebas que requieren SQL Server.

[Volver a Bases de Datos](../README.md)
