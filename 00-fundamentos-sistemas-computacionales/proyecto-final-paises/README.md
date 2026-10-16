# Gestión de países — Proyecto final

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.c)

![Estado](https://img.shields.io/badge/Estado-Documentado-brightgreen)

Lista doblemente enlazada con nodo cabecera, carga CSV, consultas, inserciones, eliminación y cuatro criterios de ordenamiento.

Materia: [Fundamentos de Sistema Computacionales](../README.md). Autor de la entrega: Kelvin César Del Castillo Torres.

## Contenido

- [solution.c](solution.c): implementación y menú de consola.
- [countries_of_the_world.csv](countries_of_the_world.csv): 227 registros de países y 20 campos.
- [Guía de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/proyecto-final/guia-proyecto-final-paises-actualizada.docx).

Compilar como C17 y ejecutar desde esta carpeta para encontrar el CSV. Por ejemplo, en una consola de desarrollador de Visual Studio: `cl /std:c17 solution.c /Fe:paises.exe`, seguido de `paises.exe`. No requiere SQL Server.

El menú permite contar, buscar sin distinguir mayúsculas, calcular el PIB promedio, insertar al inicio/final, borrar, convertir a arreglo y ordenar por nombre, población, área o densidad. El CSV suministrado contiene datos históricos; no debe utilizarse como estadística actual. La carpeta original no especifica su licencia o procedencia editorial.

## Evidencia y validación

[Documento de la entrega](../../archivos-originales/fundamentos-sistemas-computacionales/documentos/proyecto-final-v2.pdf). El [inventario](../../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) permite rastrear las rutas y los hashes de origen.

Los resultados y sus límites están en [VALIDACION.md](../VALIDACION.md). La documentación de revisión posterior se distingue del trabajo histórico.

[Desarrollo](desarrollo.md) · [Volver a la materia](../README.md) · [Repositorio](../../README.md)
