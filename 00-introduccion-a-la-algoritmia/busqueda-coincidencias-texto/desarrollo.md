# Desarrollo — Búsqueda de coincidencias en un texto

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

## Punto de partida

Leo una cadena y un texto para contar sus coincidencias. Mantengo el criterio del código de la actividad: la búsqueda distingue mayúsculas y minúsculas, permite coincidencias dentro de otras palabras y cuenta las superpuestas.

## Razonamiento

Los índices empiezan en 0. std::string::npos indica que no se encontró otra aparición; no es una posición válida. Tras encontrar una coincidencia, continúo desde su índice más 1. La versión compilable usa getline para ambas entradas y rechaza una cadena de búsqueda vacía. En el original, cin.ignore descarta un carácter de la entrada; no cambia de biblioteca.

## Comprobación manual

Buscar «ana» en «banana» produce 2 coincidencias, en los índices 1 y 3. Buscar «sol» en «Sol sol solar» produce 2: «Sol» no coincide por la mayúscula, pero sí coinciden «sol» y el inicio de «solar». Por tanto, el resultado no equivale a contar únicamente palabras completas.

## Alcance de la revisión

La versión compilable se contrasta con casos normales y límites de entrada. El informe de [validación](../VALIDACION.md) registra el entorno, las diferencias frente a la entrega y los resultados. No se atribuyen esas pruebas a los documentos históricos.

[Documentos de la actividad](README.md#documentos-y-versiones) · [Volver a la materia](../README.md)

---

## Cómo avanza la búsqueda

`find` devuelve el índice de la primera coincidencia desde la posición indicada. Para evitar encontrar siempre la misma, la siguiente búsqueda empieza en `posicion+1`. Conservo ese avance porque permite contar apariciones superpuestas.

| Búsqueda de «ana» en «banana» | Desde qué índice busco | Resultado | Contador |
|---|---|---|---|
| Primera | 0 | Coincidencia en 1 | 1 |
| Segunda | 2 | Coincidencia en 3 | 2 |
| Tercera | 4 | No hay otra coincidencia (`npos`). | 2 |

En este ejemplo, los índices comienzan en 0 y las dos apariciones comparten una «a». Si avanzara la longitud completa de la cadena, contaría solo una. `std::string::npos` es el valor especial de «no encontrado», no una posición a la que deba acceder.

## Coincidencia de texto y palabra completa

| Búsqueda | Texto | Resultado | Motivo |
|---|---|---|---|
| `sol` | `Sol sol solar` | 2 | La mayúscula no coincide; el prefijo de «solar» sí. |
| `x` | `abc` | 0 | No aparece la cadena. |
| `x` | Línea vacía | 0 | El texto vacío es válido, pero no contiene «x». |

Aunque el mensaje de entrada dice «palabra», `getline` también permite introducir una cadena con espacios. El programa busca esa secuencia exacta. `std::string` trabaja con unidades `char`; con texto UTF-8, un carácter visible puede ocupar varios bytes. Esta versión no realiza búsqueda lingüística ni equivalencia entre distintas representaciones de un acento.

## Entrada y terminación

Leo las dos líneas con `getline`. Así no necesito mezclar una lectura mediante `>>` con un descarte posterior del salto de línea. Rechazo una búsqueda vacía para que el conteo tenga un significado definido y detecto si falta la línea del texto. La búsqueda termina cuando `find` devuelve `npos`.

## Recursos utilizados

Guardo la cadena de búsqueda y el texto, además de posición y contador. El espacio depende del tamaño de ambas entradas. El costo de tiempo depende de las búsquedas de subcadenas realizadas; no lo describo como O(1) ni supongo que `find` examina siempre un solo carácter.

[Volver a la actividad](README.md) · [Validación de la materia](../VALIDACION.md)
