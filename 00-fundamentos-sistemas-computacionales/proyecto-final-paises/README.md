# Gestión de países — Proyecto final

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.c)

![Lenguaje](https://img.shields.io/badge/Lenguaje-C-blue)
![Materia](https://img.shields.io/badge/Materia-Sistemas%20Computacionales-green)
![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

---

## Descripción de la actividad

Lista doblemente enlazada con nodo cabecera, carga CSV, consultas, inserciones, eliminación y cuatro criterios de ordenamiento.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

---

## Objetivo

Integrar archivos, estructuras y memoria dinámica en una aplicación de consola que cargue países, permita consultarlos y mantenga una lista doblemente enlazada coherente durante sus operaciones.

---

## Conceptos aplicados

- Lectura de CSV con campos entre comillas.
- Struct country y nodo cabecera de una lista doblemente enlazada.
- Copia profunda y propiedad de cadenas dinámicas.
- Búsqueda, inserción, eliminación y conversión a arreglo.
- Ordenamiento por nombre, población, área y densidad.

---

## Actividad desarrollada

El programa carga 227 registros de un CSV de 20 campos y construye una lista de países. El menú conecta consultas y modificaciones con funciones separadas; cada nodo conserva sus enlaces y sus datos. La guía original acompaña al código, y las comprobaciones posteriores ejercitan la implementación real.

## Archivos y soluciones

- [solution.c](solution.c): implementación y menú de consola.
- [countries_of_the_world.csv](countries_of_the_world.csv): 227 registros de países y 20 campos.
- [Guía de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/proyecto-final/guia-proyecto-final-paises-actualizada.docx).

Compilar como C17 y ejecutar desde esta carpeta para encontrar el CSV. Por ejemplo, en una consola de desarrollador de Visual Studio: `cl /std:c17 solution.c /Fe:paises.exe`, seguido de `paises.exe`. No requiere SQL Server.

El menú permite contar, buscar sin distinguir mayúsculas, calcular el PIB promedio, insertar al inicio/final, borrar, convertir a arreglo y ordenar por nombre, población, área o densidad. El CSV suministrado contiene datos históricos; no debe utilizarse como estadística actual. La carpeta original no especifica su licencia o procedencia editorial.

---

## Ejemplo de análisis y uso

```text
Cargar countries_of_the_world.csv
    → Cantidad inicial: 227 países

Buscar Afghanistan
    → Consultar el país por su nombre

Ordenar por población
    → Recorrer la misma colección en orden ascendente

Salir
    → Liberar la lista y sus datos
```

Un caso importante del lector es `Congo, Dem. Rep.`: la coma forma parte del nombre y no debe crear una columna adicional.

---

## Organización de esta carpeta

```text
proyecto-final-paises/
├── README.md
├── desarrollo.md
├── solution.c
└── countries_of_the_world.csv
```

---

## Cómo revisar la actividad

Compilar solution.c como C17 y ejecutar desde esta carpeta para que el nombre relativo del CSV se resuelva correctamente. Probar primero el conteo y una búsqueda; después revisar inserciones, eliminaciones y criterios de ordenamiento. Los controles automatizados de la lista y del CSV están en [tests/countries-check.c](../tests/countries-check.c), y su alcance en [VALIDACION.md](../VALIDACION.md).

---

## Relación con los archivos originales y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/proyecto-final-v2.pdf). El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). La documentación de revisión posterior se distingue del trabajo histórico.

---

## Lenguaje y herramientas

C17, biblioteca estándar, CSV y herramientas C de Visual Studio en la validación Windows. La aplicación no utiliza SQL Server ni un gestor de bases de datos.

## Estado de la actividad

Actividad organizada y documentada como parte del portafolio académico. Los resultados verificados y los límites se detallan en los informes enlazados; la documentación posterior se distingue de la evidencia de entrega.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
