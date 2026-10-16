# Desarrollo — Fibonacci: ciclos y corrida manual

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

## Punto de partida

Conservo un fragmento de práctica que genera Fibonacci y preparo una versión independiente para compilarlo. El fragmento no incluye las cabeceras y siempre imprime los dos primeros valores, incluso si se solicita menos de dos.

## Razonamiento

La secuencia empieza en F(0)=0 y F(1)=1. Cada término siguiente es la suma de los dos anteriores. La entrada indica cuántos términos imprimir, no el índice del último. La versión compilable admite de 1 a 93 términos y usa enteros sin signo de 64 bits para este rango.

## Comprobación manual

Con 7 términos la salida es 0 1 1 2 3 5 8. Antes de cada paso guardo el término anterior y el actual; calculo su suma y desplazo ambos valores. Para 1 término imprimo solo 0. El tiempo crece de forma lineal con n y el espacio auxiliar se mantiene constante.

## Alcance de la revisión

La versión compilable se contrasta con casos normales y límites de entrada. El informe de [validación](../VALIDACION.md) registra el entorno, las diferencias frente a la entrega y los resultados. No se atribuyen esas pruebas a los documentos históricos.

[Documentos de la actividad](README.md#documentos-y-versiones) · [Volver a la materia](../README.md)
