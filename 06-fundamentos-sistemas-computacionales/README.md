# Fundamentos de Sistema Computacionales

![Lenguaje](https://img.shields.io/badge/Lenguajes-C%20%2F%20Ensamblador-blue)
![Estado](https://img.shields.io/badge/Estado-Incorporado%20y%20documentado-brightgreen)

Trabajos de representación de datos, programación C, ensamblador y estructuras dinámicas realizados durante mi formación en Ciencias de la Computación. Los enunciados identifican la asignatura como «Fundamentos de Sistemas Computacionales», ICC-133; aquí se utiliza el nombre indicado para el portafolio.

## Actividades

| Actividad | Contenido | Estado |
|---|---|---|
| [Representación de datos y fundamentos](fundamentos-representacion-datos) | Sistemas de numeración, complemento a dos, IEEE 754 y tamaños de tipos. | Documentado |
| [Programación en C y ensamblador FASM](c-y-ensamblador) | Doce ejercicios equivalentes de entrada/salida, decisiones, ciclos, primos y patrones. | Documentado |
| [Estructuras y memoria dinámica](estructuras-y-memoria) | Diez ejercicios de estructuras, punteros, rectángulos, complejos y listas. | Documentado |
| [Listas, colas y pilas](listas-colas-pilas) | Diecinueve ejercicios de búsqueda, agregación, eliminación, ordenamiento, FIFO y LIFO. | Documentado |
| [Proyecto híbrido C y NASM](proyecto-hibrido-c-nasm) | Interoperabilidad C/ensamblador x64 para sumar, ordenar e intercambiar enteros. | Documentado |
| [Gestión de países — Proyecto final](proyecto-final-paises) | Lista doblemente enlazada con nodo cabecera, carga CSV, consultas, inserciones, eliminación y cuatro criterios de ordenamiento. | Documentado |

## Organización y evidencia

Se revisaron los **121 archivos** de la carpeta académica. Se seleccionaron **88 archivos fuente/documentales** y se clasificaron 33 exclusiones: material editorial o de capacitación de terceros, comprimidos redundantes, un ejecutable y configuraciones locales. La documentación de revisión y las pruebas son adiciones posteriores y no se presentan como entregas originales.

Cada actividad sigue el patrón `README.md` + `desarrollo.md` + código o enlace a evidencia. Los documentos se conservan en [archivos originales](../archivos-originales/fundamentos-sistemas-computacionales), con [inventario completo](../archivos-originales/fundamentos-sistemas-computacionales/INVENTARIO.md) y hashes SHA-256.

## Ejecución y alcance

Los programas C independientes se compilan por separado. El proyecto híbrido necesita Windows x64 y NASM; las otras doce fuentes ASM usan FASM y generan ELF64 para Linux. El proyecto de países utiliza un CSV local y no requiere una base de datos SQL.

[Resultados reproducibles y límites](VALIDACION.md) · [Auditoría de publicación](AUDITORIA.md).

El estado de incorporación indica que la evidencia disponible está organizada. No afirma que se localizaron todas las respuestas ni equivale a una certificación de calificación: la asignación 1 solo contiene su enunciado y la ejecución FASM en Linux sigue sin verificarse.

## Autor

**Kelvin César Del Castillo Torres**. Se preservan los créditos y enunciados de origen. Libros y módulos de Huawei se describen en el inventario, sin redistribuirlos como trabajo propio o como certificados obtenidos.

[Volver al repositorio](../README.md)
