# Primer parcial: comisiones y calificaciones

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md)

![Área](https://img.shields.io/badge/Área-Algoritmia-green)
![Tipo](https://img.shields.io/badge/Tipo-Documentación-lightgrey)

## Qué trabajé

Incluyo el enunciado de dos problemas del primer parcial. No se encontró una solución entregada para estos ejercicios; por eso presento el planteamiento y su análisis sin atribuirles una ejecución.

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
| Material disponible | Enunciado del parcial; no se localizó una solución entregada. |
| Estado | Análisis documental; sin compilación ni pruebas de estos dos problemas. |

## Ejemplo y lectura de resultados

Para Santiago y ventas de 500, corresponde el 5 %: 500×0.05=25.00. Para 501, corresponde el 7 %: 35.07. Las ventas negativas y los códigos de ciudad desconocidos se rechazan. En calificaciones, el enunciado solapa extremos (70, 80 y 90); una interpretación sin solapamientos es F:[0,60), D:[60,70), C:[70,80), B:[80,90) y A:[90,100]. Esta precisión es una aclaración de lectura, no una solución entregada.

---

## Documentos y versiones

| Documento | Contenido |
|---|---|
| [Enunciado conservado](../../archivos-originales/introduccion-programacion/parcial-1/enunciado.txt) | Texto conservado |

Esta actividad se consulta mediante sus documentos y el análisis; no se añade código para sustituir una entrega que no está disponible.

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

Documentación y análisis disponibles. Esta actividad no forma parte de los 32 casos ejecutados de C++: su alcance es documental.

[Materia](../README.md) · [Notación](../../GUIA-DE-LECTURA.md) · [Inventario](../../archivos-originales/introduccion-programacion/INVENTARIO.md)
