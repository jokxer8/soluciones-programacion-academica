# Búsqueda de coincidencias en un texto

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

![Área](https://img.shields.io/badge/Área-Algoritmia-green)
![Lenguaje](https://img.shields.io/badge/Lenguaje-C%2B%2B-blue)

## Qué trabajé

Leo una cadena y un texto para contar sus coincidencias. Mantengo el criterio del código de la actividad: la búsqueda distingue mayúsculas y minúsculas, permite coincidencias dentro de otras palabras y cuenta las superpuestas.

---

## Objetivo

Recorrer las apariciones de una cadena mediante std::string::find.

## Análisis y conceptos

Los índices empiezan en 0. std::string::npos indica que no se encontró otra aparición; no es una posición válida. Tras encontrar una coincidencia, continúo desde su índice más 1. La versión compilable usa getline para ambas entradas y rechaza una cadena de búsqueda vacía. En el original, cin.ignore descarta un carácter de la entrada; no cambia de biblioteca.

## Ejemplo y lectura de resultados

Buscar «ana» en «banana» produce 2 coincidencias, en los índices 1 y 3. Buscar «sol» en «Sol sol solar» produce 2: «Sol» no coincide por la mayúscula, pero sí coinciden «sol» y el inicio de «solar». Por tanto, el resultado no equivale a contar únicamente palabras completas.

---

## Documentos y versiones

| Documento | Contenido |
|---|---|
| [Enunciado](../../archivos-originales/introduccion-programacion/coincidencias/enunciado.txt) | Texto conservado |
| [Algoritmo](../../archivos-originales/introduccion-programacion/coincidencias/algoritmo.txt) | Texto conservado |
| [Código original](../../archivos-originales/introduccion-programacion/coincidencias/codigo-original.txt) | Texto conservado |

La versión [solution.cpp](solution.cpp) se prepara para esta edición del portafolio. Los textos y documentos enlazados conservan la entrega previa; las diferencias se explican en el [desarrollo](desarrollo.md).

## Cómo revisar la actividad

Compilo el archivo independiente con un compilador C++17 y comparo sus resultados con los casos de [validación](../VALIDACION.md). Cada solución tiene su propio main; no se compilan juntas en un solo programa.

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
