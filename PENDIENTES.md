# Pendientes del repositorio

Estado revisado el 16 de octubre de 2026. Se distingue el trabajo pendiente de las limitaciones y ampliaciones posibles. Los estados de incorporación de las materias no equivalen a una certificación de todas las pruebas.

## Trabajo pendiente identificado

| Área | Pendiente | Estado y referencia |
|---|---|---|
| Fundamentos de Sistema Computacionales | Localizar las respuestas originales de la asignación 1, si se conservan en otra ubicación. | Solo se encontró el [enunciado](archivos-originales/fundamentos-sistemas-computacionales/documentos/asignacion-1.pdf). No se presenta como resuelta ni se requiere recrearla para publicar la evidencia disponible. |

## Límites documentados y ampliaciones posibles

- **C y memoria:** volver a ejecutar sanitizadores ASan/UBSan y ampliar casos de entrada inválida, fin de entrada, desbordamiento y falta de memoria. Las afirmaciones de la auditoría histórica no sustituyen estas comprobaciones actuales; véase [validación](00-fundamentos-sistemas-computacionales/VALIDACION.md).
- **CSV de países:** identificar la procedencia y licencia del conjunto histórico, que no estaban indicadas en los archivos recibidos; véase [proyecto](00-fundamentos-sistemas-computacionales/proyecto-final-paises).
- **Hotel:** realizar un recorrido visual completo de pantallas y, si se amplía su uso, pruebas de concurrencia entre operadores y despliegue. Las 79 pruebas verificadas no certifican esos alcances; véase [cierre de tareas](05-bases-de-datos/hotel-reservas-proyecto-final/docs/CIERRE-TAREAS.md).
- **Bolsa de empleo:** localizar el esquema completo previo para reproducir la práctica de forma autónoma. No estaba entre los entregables; véase [desarrollo](05-bases-de-datos/bolsa-empleo-objetos-sql/desarrollo.md).

Estas ampliaciones no se han convertido automáticamente en nuevas tareas del tablero. Las tareas del hotel **#1–#8 están completadas**; #3, #5 y #7 no deben volver a contarse como pendientes.

## Contraseñas del hotel completadas

La tarea #8 queda cerrada con PBKDF2-SHA256, sal individual, coste configurable y migración compatible de cuentas existentes. **79 pruebas correctas y 0 fallidas**, locales y en [GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38849154210). [Guía de instalación y seguridad](05-bases-de-datos/hotel-reservas-proyecto-final/docs/SEGURIDAD-CONTRASENAS.md).

## Comparación Linux completada

Los doce pares C/FASM pasan **132 comparaciones, 0 fallidas**, con pipe y terminal PTY en Ubuntu. Se corrigieron lectura, signos, rangos y alineación. [Resultados y ejecución](00-fundamentos-sistemas-computacionales/PRUEBAS-LINUX.md). Este punto deja de figurar como pendiente.

## Organización de las materias

La materia introductoria pasó del prefijo `06` a `00`, se ubicó antes de `01` en el índice y se actualizaron enlaces, rutas del inventario y referencias documentales. El código y los resultados de pruebas anteriores se conservan.

## Presentación y documentación revisadas

Revisé las seis materias (00–05) y sus 39 actividades: navegación, ejemplos, explicaciones y referencias documentales. Aclaré las bases numéricas, las unidades y las siglas, y retiré las ubicaciones personales de los inventarios publicados. La [guía de lectura](GUIA-DE-LECTURA.md) reúne la notación compartida. Esta revisión es documental; los resultados de ejecución siguen en los informes enlazados arriba.

[Volver al repositorio](README.md)
