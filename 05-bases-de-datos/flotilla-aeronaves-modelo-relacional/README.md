# Flotilla de Aeronaves - Modelo Entidad-Relación

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Evidencia](informe-final.pdf)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SQL%20/%20C%23-lightgrey)

## Qué trabajé y cómo leer los ejemplos

Documento el diseño de la flotilla realizado en equipo. Distingo la identidad de una aeronave de sus cambios de base o estado a lo largo del tiempo. Las claves identifican registros; las fechas permiten interpretar su historial.

## Descripción de la actividad

Caso grupal de diseño de una flotilla con mantenimiento e historial de bases y estados.

---

## Objetivo

Diseñar un modelo que represente la operación de las aeronaves y conserve sus cambios a lo largo del tiempo.

---

## Conceptos aplicados

- Entidades, atributos y cardinalidades.
- Claves primarias y foráneas.
- Relaciones temporales.
- Transformación al modelo relacional.
- Normalización hasta 3FN.
- Diccionario de datos.

---

## Actividad desarrollada

El modelo organiza aeronaves, modelos, fabricantes, talleres, mantenimientos y aeropuertos. Las entidades BASE y REGISTRO_ESTADO_AERONAVE guardan periodos de vigencia para consultar tanto la situación actual como la histórica.

---

## Ejemplo de análisis

Si una aeronave cambia de aeropuerto base, se registra un nuevo periodo de asignación. El historial permite responder dónde estaba basada antes del cambio.

---

## Archivos de esta carpeta

```text
flotilla-aeronaves-modelo-relacional/
├── README.md
├── desarrollo.md
├── diagrama.drawio
├── diagrama.png
├── diccionario-datos.xlsx
├── informe-final.pdf
└── modelo-relacional-3fn.xlsx
```

- [diagrama.drawio](diagrama.drawio)
- [diagrama.png](diagrama.png)
- [diccionario-datos.xlsx](diccionario-datos.xlsx)
- [informe-final.pdf](informe-final.pdf)
- [modelo-relacional-3fn.xlsx](modelo-relacional-3fn.xlsx)

El archivo [desarrollo.md](desarrollo.md) explica las decisiones y el alcance de la solución.

---

## Cómo revisar la actividad

Revisar el informe final, abrir el DER en diagrams.net y comparar las claves con el modelo relacional y el diccionario. Esta actividad conserva un diseño; no incluye un esquema SQL completo.

---

## Relación con los archivos originales

Los enunciados, modelos y documentos de apoyo están relacionados en el [inventario de documentos](../../archivos-originales/bases-de-datos/INVENTARIO.md). Allí se pueden consultar las versiones publicadas y su tratamiento.

## Autoría

Trabajo del Grupo 3: Kelvin Del Castillo Torres, Gabriel Galasso González y Camila Michelle Vásquez.

---

## Lenguaje y herramientas

Modelado entidad-relación y relacional.

## Estado

Actividad organizada y documentada como parte del portafolio académico de Bases de Datos. El [registro de validación](../VALIDACION.md) distingue la revisión realizada de las pruebas que requieren SQL Server.

[Volver a Bases de Datos](../README.md)
