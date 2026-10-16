# Pendientes del repositorio

Estado revisado el 15 de octubre de 2026. Se distingue el trabajo pendiente de las limitaciones y ampliaciones posibles. Los estados de incorporación de las materias no equivalen a una certificación de todas las pruebas.

## Trabajo pendiente identificado

| Área | Pendiente | Estado y referencia |
|---|---|---|
| Hotel | Mejorar la derivación de contraseñas y definir la migración del mecanismo académico actual. | [Tarea #8 abierta](https://github.com/jokxer8/soluciones-programacion-academica/issues/8); única incidencia abierta al consultar GitHub. |
| Fundamentos de Sistema Computacionales | Ejecutar los doce programas FASM en Linux x86-64 y contrastar entrada interactiva y resultados con sus versiones C. | Ensamblan correctamente; [ejecución Linux sin verificar](00-fundamentos-sistemas-computacionales/VALIDACION.md). |
| Fundamentos de Sistema Computacionales | Localizar las respuestas originales de la asignación 1, si se conservan en otra ubicación. | Solo se encontró el [enunciado](archivos-originales/fundamentos-sistemas-computacionales/documentos/asignacion-1.pdf). No se presenta como resuelta ni se requiere recrearla para publicar la evidencia disponible. |

## Límites documentados y ampliaciones posibles

- **C y memoria:** volver a ejecutar sanitizadores ASan/UBSan y ampliar casos de entrada inválida, fin de entrada, desbordamiento y falta de memoria. Las afirmaciones de la auditoría histórica no sustituyen estas comprobaciones actuales; véase [validación](00-fundamentos-sistemas-computacionales/VALIDACION.md).
- **CSV de países:** identificar la procedencia y licencia del conjunto histórico, que no estaban indicadas en los archivos recibidos; véase [proyecto](00-fundamentos-sistemas-computacionales/proyecto-final-paises).
- **Hotel:** realizar un recorrido visual completo de pantallas y, si se amplía su uso, pruebas de concurrencia entre operadores y despliegue. Las 65 pruebas verificadas no certifican esos alcances; véase [cierre de tareas](05-bases-de-datos/hotel-reservas-proyecto-final/docs/CIERRE-TAREAS.md).
- **Bolsa de empleo:** localizar el esquema completo previo para reproducir la práctica de forma autónoma. No estaba entre los entregables; véase [desarrollo](05-bases-de-datos/bolsa-empleo-objetos-sql/desarrollo.md).

Estas ampliaciones no se han convertido automáticamente en nuevas tareas del tablero. Las tareas del hotel **#1–#7 están completadas**; #3, #5 y #7 no deben volver a contarse como pendientes.

## Último ajuste completado

La materia introductoria pasó del prefijo `06` a `00`, se ubicó antes de `01` en el índice y se actualizaron enlaces, rutas del inventario y referencias documentales. El código y los resultados de pruebas anteriores se conservan.

[Volver al repositorio](README.md)
