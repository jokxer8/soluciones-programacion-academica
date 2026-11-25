
<div align="center">

<h1 align="center"><img src="../../assets/readme-banners/00-introduccion-a-la-algoritmia-enunciados-primer-parcial.svg" alt="Primer parcial: comisiones y calificaciones (Midterm 1: Commissions and Grades)" width="100%"></h1>


</div>

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md)

![Área](https://img.shields.io/badge/Área-Algoritmia-green)
![Tipo](https://img.shields.io/badge/Tipo-Documentación-lightgrey)

## En palabras simples

![En palabras simples · Guía para estudiantes](../../assets/readme-banners/en-palabras-simples.svg)

**Comisiones:** separa el monto de venta de la regla que indica cuánto corresponde pagar; busca esa regla antes de multiplicar. **Calificaciones:** separa las notas que entran del criterio que decide qué mostrar al final. Las soluciones publicadas se añadieron como material didáctico y no sustituyen la entrega original.

## Qué trabajé

El enunciado original de los dos problemas se conserva. Como no se encontró una solución entregada, añadí dos elaboraciones nuevas en C++ y las identifico como tales; no se atribuyen a la entrega histórica.

---

## Objetivo

Identificar entradas, condiciones y límites a partir de un enunciado.

## Análisis y conceptos

El primer problema calcula una comisión según ciudad y volumen de ventas. Los códigos 1, 2 y 3 identifican Santiago, Salcedo y La Vega. El importe se multiplica por la tasa correspondiente al intervalo completo, no por una tarifa progresiva. El segundo problema convierte una calificación de 0 a 100 a una letra y mensaje.

## Entradas, proceso y salidas

| Elemento | Descripción |
|---|---|
| Problema 1 | Código de ciudad y volumen de ventas → importe de comisión. |
| Problema 2 | Datos descriptivos de asignatura y nota → letra y mensaje. |
| Material disponible | Enunciado original, análisis y dos soluciones nuevas no históricas. |
| Estado | Análisis documental; sin compilación ni pruebas de estos dos problemas. |

## Ejemplo y lectura de resultados

Para Santiago y ventas de 500, corresponde el 5 %: 500×0.05=25.00. Para 501, corresponde el 7 %: 35.07. Las ventas negativas y los códigos de ciudad desconocidos se rechazan. En calificaciones, el enunciado solapa extremos (70, 80 y 90); una interpretación sin solapamientos es F:[0,60), D:[60,70), C:[70,80), B:[80,90) y A:[90,100]. Esta precisión es una aclaración de lectura, no una solución entregada.

---

## Documentos y versiones

| Documento | Contenido |
|---|---|
| [Enunciado conservado](../../archivos-originales/introduccion-programacion/parcial-1/enunciado.txt) | Texto conservado |

Esta actividad incluye el análisis y dos programas nuevos. La solución de comisión utiliza la tasa del intervalo completo; la de calificaciones usa intervalos semiabiertos para que 70, 80 y 90 tengan una sola letra.

## Archivos de esta carpeta

```text
enunciados-primer-parcial/
├── README.md
└── desarrollo.md
```

Los documentos de apoyo están enlazados en la tabla anterior; no se duplican dentro de la actividad. El [desarrollo](desarrollo.md) amplía el recorrido de los datos, las decisiones y los límites.

## Cómo revisar la actividad

Leo primero el planteamiento, sigo el desarrollo y consulto los documentos enlazados para contrastar el análisis con la entrega.

## Estado de la actividad

Documentación, análisis y dos soluciones C++ nuevas disponibles. No forman parte de los 32 casos históricos y su verificación automatizada se documenta como pendiente.

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
