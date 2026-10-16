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
