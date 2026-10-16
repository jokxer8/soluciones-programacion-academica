# Desarrollo — Primer parcial: comisiones y calificaciones

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md)

## Punto de partida

Incluyo el enunciado de dos problemas del primer parcial. No se encontró una solución entregada para estos ejercicios; por eso presento el planteamiento y su análisis sin atribuirles una ejecución.

## Razonamiento

El primer problema calcula una comisión según ciudad y volumen de ventas. Los códigos 1, 2 y 3 identifican Santiago, Salcedo y La Vega. El importe se multiplica por la tasa correspondiente al intervalo completo, no por una tarifa progresiva. El segundo problema convierte una calificación de 0 a 100 a una letra y mensaje.

## Comprobación manual

Para Santiago y ventas de 500, corresponde el 5 %: 500×0.05=25.00. Para 501, corresponde el 7 %: 35.07. Las ventas negativas y los códigos de ciudad desconocidos se rechazan. En calificaciones, el enunciado solapa extremos (70, 80 y 90); una interpretación sin solapamientos es F:[0,60), D:[60,70), C:[70,80), B:[80,90) y A:[90,100]. Esta precisión es una aclaración de lectura, no una solución entregada.

## Alcance de la revisión

Se revisa la explicación y su correspondencia con los documentos. No se registra una ejecución de software para esta actividad.

[Documentos de la actividad](README.md#documentos-y-versiones) · [Volver a la materia](../README.md)

---

## Comisiones: datos y reglas

Las ventas s deben ser no negativas y la ciudad debe tener un código válido. Elijo una sola tasa según la fila y el intervalo del enunciado, y la multiplico por el volumen completo de ventas.

| Código y ciudad | 0≤s≤500 | 500<s≤1000 | 1000<s≤10000 | s>10000 |
|---|---|---|---|---|
| 1 · Santiago | 5 % | 7 % | 8 % | 12 % |
| 2 · Salcedo | 4.50 % | 7.50 % | 10 % | 13 % |
| 3 · La Vega | 5.50 % | 8 % | 12 % | 14.50 % |

Una tasa de 7.50 % se expresa como 0.075 al multiplicar. Los importes se muestran con dos decimales. El enunciado no fija aquí una moneda, por lo que trato las cifras como unidades monetarias del ejemplo.

| Ejemplo manual | Tasa | Comisión esperada |
|---|---|---|
| Santiago, s=500 | 5 % | 25.00 |
| Santiago, s=501 | 7 % | 35.07 |
| Salcedo, s=1000 | 7.50 % | 75.00 |
| La Vega, s=10001 | 14.50 % | 1450.145 antes de redondear |

En el último ejemplo se debe definir la regla de redondeo para los empates antes de exigir un resultado exacto a dos decimales. No se suma una comisión distinta por cada tramo: el texto plantea una tabla de tasas por intervalo, no una tarifa marginal progresiva.

## Calificaciones: evitar límites solapados

El enunciado usa expresiones como «entre 60 y 70» y «entre 70 y 80». Para que 70 no caiga en dos categorías, explico una interpretación con el extremo inferior incluido y el superior excluido, salvo 100.

| Intervalo propuesto | Letra | Mensaje |
|---|---|---|
| 0≤nota<60 | F | Reprobado |
| 60≤nota<70 | D | Aceptable |
| 70≤nota<80 | C | Bueno |
| 80≤nota<90 | B | Muy bueno |
| 90≤nota≤100 | A | Sobresaliente |

Los valores fuera de [0,100] son inválidos. La condición debe expresar «menor que 0 **o** mayor que 100»: una nota no puede ser ambas cosas a la vez. El nombre, la clave y la sección describen el registro que se muestra; no participan en el cálculo de la letra.

## Qué queda por recuperar

Se conserva el planteamiento y este análisis, sin archivo `solution.cpp` ni resultado de ejecución. Si aparece la solución entregada, se podrá comparar con las reglas y los casos límite. Crear una solución nueva sería una ampliación identificada como tal, no una recuperación del examen original.

[Volver a la actividad](README.md) · [Validación de la materia](../VALIDACION.md)
