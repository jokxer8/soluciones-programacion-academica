# Desarrollo — Triángulo rectángulo: hipotenusa, área y perímetro

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Código](solution.cpp)

## Punto de partida

Trabajo realizado en el grupo 5. A partir de dos catetos calculo la hipotenusa, el área y el perímetro. Conservo el algoritmo, el diagrama de RAPTOR, la investigación y las versiones de la entrega para poder comparar sus enfoques.

## Razonamiento

Si a y b son catetos positivos, c = √(a²+b²), área = a·b/2 y perímetro = a+b+c. Los catetos, la hipotenusa y el perímetro usan la misma unidad de longitud; el área usa esa unidad al cuadrado. La versión compilable usa std::hypot y valida los datos.

## Comprobación manual

Con a=3 y b=4, la hipotenusa es 5, el área 6 y el perímetro 12. La entrega C++ incrementa una raíz de 0.01 en 0.01 y conserva el último valor inferior; su resultado puede quedar por debajo del correcto. El pseudocódigo describe una aproximación de Newton y el diagrama utiliza incrementos distintos. Son versiones diferentes: no deben presentarse como si implementaran exactamente el mismo cálculo.

## Alcance de la revisión

La versión compilable se contrasta con casos normales y límites de entrada. El informe de [validación](../VALIDACION.md) registra el entorno, las diferencias frente a la entrega y los resultados. No se atribuyen esas pruebas a los documentos históricos.

[Documentos de la actividad](README.md#documentos-y-versiones) · [Volver a la materia](../README.md)

## Créditos del grupo 5

Kelvin Del Castillo, Jesús Almonte, Wesley Reyes, Roberto McKinney, Eduardo Volquez y Kellyn Santana. La investigación se conserva como trabajo en equipo.

---

## Del dato a cada resultado

Primero leo ambos catetos. Si falta alguno, la lectura falla o alguno no es positivo y finito, termino con un mensaje de error. Después calculo los tres resultados y compruebo que no excedan lo que permite representar el tipo `double`.

| Paso, con a=3 y b=4 | Operación | Resultado |
|---|---|---|
| Hipotenusa | √(3²+4²) = √25 | 5 |
| Área | (3/2)×4 | 6 |
| Perímetro | 3+4+5 | 12 |
| Presentación | Seis cifras decimales | 5.000000, 6.000000 y 12.000000 |

`std::hypot(a,b)` calcula la longitud de la hipotenusa. `std::fixed` y `std::setprecision(6)` controlan la forma de imprimir; no convierten el cálculo en aritmética decimal exacta. En la batería, las comparaciones numéricas admiten una tolerancia absoluta de 0.000001, además de la tolerancia relativa predeterminada de `math.isclose`.

## Tres versiones de una misma idea

| Material | Cómo obtiene la raíz | Cómo lo interpreto |
|---|---|---|
| Código entregado | Recorre aproximaciones separadas por 0.01. | Conserva el último valor que cumple la condición del ciclo; no garantiza obtener la raíz exacta. |
| Pseudocódigo | Describe la actualización de Newton. | Es una propuesta distinta del código entregado. |
| Diagrama RAPTOR | Utiliza otra progresión por incrementos. | Debe contrastarse con su propia condición de salida. |
| `solution.cpp` | Utiliza `std::hypot` y valida las entradas. | Es la versión compilable que respalda el informe de pruebas actual. |

La variable llamada `precision` en el código original contiene a²+b²: ese nombre no representa allí una tolerancia de error. En el pseudocódigo, en cambio, aparece una tolerancia de 0.00001. Por eso leo el uso de cada variable antes de comparar sus nombres.

## Casos y costo de la solución

Se comprueban 3–4, 5–12 y 0.3–0.4. Se rechazan un cateto cero, un negativo, texto no numérico y el par 1e308–1e308 cuando los resultados salen de rango. `1e308` significa 1×10³⁰⁸; se usa para explorar un límite de representación, no una longitud de ejemplo habitual.

La solución no recorre una colección: utiliza una cantidad fija de variables y llamadas matemáticas. Su espacio auxiliar es constante. Esto no significa que todos los procesadores tarden exactamente lo mismo en efectuar una operación matemática.

[Volver a la actividad](README.md) · [Validación de la materia](../VALIDACION.md)
