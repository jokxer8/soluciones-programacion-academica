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
