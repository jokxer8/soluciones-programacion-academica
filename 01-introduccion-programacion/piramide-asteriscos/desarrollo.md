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

---

## Recorrido por pisos

El ciclo exterior controla el número de fila. Dentro de cada fila completo primero los espacios y después los asteriscos; el salto de línea se escribe al terminar ambos ciclos interiores.

| Piso p para n=3 | Espacios n−p | Asteriscos 2p−1 | Fila, con puntos para visualizar los espacios |
|---|---|---|---|
| 1 | 2 | 1 | `..*` |
| 2 | 1 | 3 | `.***` |
| 3 | 0 | 5 | `*****` |

Los puntos son únicamente una ayuda de lectura: el programa imprime espacios. Si utilizara n−1 en todas las filas, la sangría quedaría constante y el dibujo no tendría el centrado previsto. Esta es la diferencia que aclaro frente al pseudocódigo y al DFD conservados.

## Cuánto se imprime

La fila p contiene el p-ésimo número impar de asteriscos. Al sumar 1+3+5+…+(2n−1), el total de asteriscos es n². Los espacios iniciales suman n(n−1)/2. El trabajo de impresión crece, por tanto, como O(n²), y solo se necesitan contadores: espacio auxiliar O(1).

Con 60 pisos, la última fila contiene 119 asteriscos. El límite de 60 se refiere a filas, no a columnas. Si la consola es estrecha, una línea puede ajustarse visualmente aunque la secuencia de caracteres sea correcta.

## Cómo compruebo el resultado

La batería compara la salida con el patrón esperado sin eliminar los espacios iniciales. Prueba la altura mínima, una pirámide pequeña que puedo revisar a mano y la altura máxima. También comprueba que las entradas inválidas incluidas produzcan un error.

## Análisis, revisión y autoría

El foro corresponde al grupo 2; el número no identifica una versión del programa. Las rúbricas y respuestas registran observaciones sobre enumeración, sintaxis y representación del final del diagrama. Las conservo como parte del proceso de revisión, sin convertir las soluciones de los otros grupos en entregas propias.

[Volver a la actividad](README.md) · [Validación de la materia](../VALIDACION.md)
