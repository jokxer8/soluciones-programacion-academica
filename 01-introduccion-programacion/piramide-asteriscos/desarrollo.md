# Desarrollo — Pirámide de asteriscos y revisión entre grupos

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

## Punto de partida

Esta actividad corresponde al foro del grupo 2. Relaciono el análisis del problema, el pseudocódigo, el diagrama y el código de una pirámide centrada. Las rúbricas y respuestas permiten consultar también la revisión entre grupos.

## Razonamiento

n es la cantidad de pisos y está entre 1 y 60, ambos incluidos. En el piso p se imprimen n−p espacios y 2p−1 asteriscos. El 60 es un límite de la actividad, no una cantidad de caracteres por fila: la base puede llegar a 119 asteriscos.

## Comprobación manual

Para n=3, las filas tienen 2, 1 y 0 espacios, seguidos de 1, 3 y 5 asteriscos. El código original usa n−p; algunas líneas del pseudocódigo y del DFD indican n−1, lo que mantendría constante la sangría. La explicación adopta n−p y conserva la evidencia de esa diferencia. El rango correcto incluye 60, aunque una frase del análisis lo describe de forma contradictoria.

## Alcance de la revisión

La versión compilable se contrasta con casos normales y límites de entrada. El informe de [validación](../VALIDACION.md) registra el entorno, las diferencias frente a la entrega y los resultados. No se atribuyen esas pruebas a los documentos históricos.

[Documentos de la actividad](README.md#documentos-y-versiones) · [Volver a la materia](../README.md)

## Patrón esperado para tres pisos

```text
  *
 ***
*****
```

Los espacios iniciales son parte del resultado. Las rúbricas se identifican por el grupo al que corresponden y no se presentan como programas propios de otros equipos.
