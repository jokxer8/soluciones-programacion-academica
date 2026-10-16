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
