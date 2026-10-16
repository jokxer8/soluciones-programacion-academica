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

---

## Estado de las variables

Antes de cada impresión, `anterior` contiene el término que voy a mostrar y `actual` el que le sigue. Calculo la suma en una variable separada para no perder un valor que todavía necesito.

| Iteración, desde 0 | anterior | actual | Valor impreso |
|---|---|---|---|
| 0 | 0 | 1 | 0 |
| 1 | 1 | 1 | 1 |
| 2 | 1 | 2 | 1 |
| 3 | 2 | 3 | 2 |
| 4 | 3 | 5 | 3 |
| 5 | 5 | 8 | 5 |
| 6 | 8 | 13 | 8 |

Para n=7, la última fila termina la impresión. El código evita hacer una actualización adicional cuando no queda otro término por mostrar.

## Cantidad e índice son cosas distintas

- Pedir 1 término produce solo F(0)=0.
- Pedir 2 términos produce F(0) y F(1): `0 1`.
- Pedir 7 términos llega hasta F(6)=8.
- Pedir 93 términos llega hasta F(92)=7540113804746346429.

El último número es un valor de Fibonacci, no un código ni una dirección de memoria. El límite de entrada permite trabajar dentro del rango elegido para `unsigned long long` en el entorno de prueba; no significa que la sucesión matemática termine allí.

## Diferencia con el fragmento de práctica

El fragmento conservado imprime primero 0 y 1 sin consultar cuántos términos se pidieron. La versión compilable imprime dentro del ciclo, de modo que respeta n=1. También incorpora las cabeceras y comprobaciones de entrada que necesita un programa independiente.

## Costo y comprobación

El ciclo realiza n impresiones: tiempo O(n) y espacio auxiliar O(1). No guardo un arreglo con toda la secuencia. La batería construye por separado la lista esperada y compara sus términos con la salida completa, incluidos el caso mínimo y el máximo admitido.

[Volver a la actividad](README.md) · [Validación de la materia](../VALIDACION.md)
