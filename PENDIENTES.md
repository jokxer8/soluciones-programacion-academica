# Pendientes del repositorio

[Inicio](README.md) · [Materias](README.md#áreas-principales) · [Tablero del hotel](https://github.com/users/jokxer8/projects/1)

Estado revisado el **14 de noviembre de 2026**. El portafolio tiene siete materias, de la 00 a la 06, y 46 actividades. Distingo las tareas realmente pendientes de las limitaciones cerradas mediante documentación. Las tareas del hotel #1–#8 están cerradas; no vuelven a contarse como pendientes.

En esta revisión se cierran las materias 00 y 01 dentro de la evidencia disponible: las soluciones nuevas del parcial de Introducción se incorporan al flujo de pruebas; la asignación 1 de Sistemas queda marcada como no realizada porque solo está disponible el enunciado.

## Actividades por resolver y documentar

| Materia | Qué falta | Qué está publicado |
|---|---|---|
| 06 · Bases de Datos | Preparar un esquema nuevo compatible con los objetos SQL de bolsa de empleo, documentar los supuestos, añadir datos de prueba y verificar la práctica. | [Objetos SQL y dependencias](06-bases-de-datos/bolsa-empleo-objetos-sql/desarrollo.md); falta una base completa que permita reproducirla de forma autónoma. |

La limitación de las materias 00–01 no crea una búsqueda de archivos pendiente. En el parcial de la materia 01, las soluciones añadidas son trabajos nuevos y se prueban con la batería automatizada. En la materia 00 no se infiere una respuesta a la asignación 1: el cierre declara esa actividad no realizada y deja enlazado el enunciado.

## Cierre de alcance de las materias 00–01

| Materia | Estado de cierre | Evidencia y límite |
|---|---|---|
| 00 · Sistemas Computacionales | Sección completada; asignación 1 no realizada. | Solo se conserva el [enunciado](archivos-originales/fundamentos-sistemas-computacionales/documentos/asignacion-1.pdf), sin respuestas localizadas. No se presenta como resuelta. |
| 01 · Introducción a la Programación | Sección completada; dos soluciones nuevas añadidas. | [Comisiones y calificaciones](01-introduccion-programacion/enunciados-primer-parcial/README.md). Se distinguen de la entrega histórica y se integran en la batería Windows. |

## Verificaciones pendientes

| Área | Siguiente comprobación | Situación actual |
|---|---|---|
| 00 · C y memoria dinámica | Ejecutar sanitizadores ASan/UBSan y ampliar casos de desbordamiento, fin de entrada y falta de memoria. | Hay compilaciones y casos comprobados; no equivalen a cubrir estos escenarios. [Alcance](00-fundamentos-sistemas-computacionales/VALIDACION.md). |
| 00 · Datos de países | Identificar la procedencia y licencia del CSV histórico. | La información no estaba indicada en el conjunto disponible. [Proyecto](00-fundamentos-sistemas-computacionales/proyecto-final-paises/README.md). |
| 06 · Hotel | Completar un recorrido visual de todas las pantallas. | Las 79 pruebas incluyen comprobaciones técnicas y funcionales, pero no ese recorrido interactivo completo. [Cierre y límites](06-bases-de-datos/hotel-reservas-proyecto-final/docs/CIERRE-TAREAS.md). |
| 06 · Prácticas SQL fuera del hotel | Registrar ejecuciones reproducibles de las prácticas que solo cuentan con revisión documental o sintáctica. | Los 18 scripts de actividades pasaron análisis de sintaxis; ese resultado no demuestra su ejecución contra cada esquema. [Informe](06-bases-de-datos/VALIDACION.md). |

En el hotel, el recorrido visual es una comprobación adicional al alcance ya completado de las tareas #1–#8. Esta lista no crea ni reabre tareas en el tablero.

## Ampliaciones opcionales

| Área | Posible ampliación |
|---|---|
| 01 · Entrada de los programas C++ | Ampliar la cobertura de entrada inválida en los programas originales. Los 32 casos de esos cinco programas cubren lo registrado en el [informe](01-introduccion-programacion/VALIDACION.md); el parcial ya tiene sus 21 casos nuevos. |
| 01 · Búsqueda de texto | Crear, como variante separada, búsqueda por palabras completas, reglas para mayúsculas y normalización de texto Unicode. La versión actual cuenta subcadenas exactas y superpuestas. |
| 01 · Diagramas secuenciales | Añadir implementaciones identificadas como nuevas de 3.10–3.12. Actualmente tienen diagrama y traza manual; solo 3.9 dispone de código entregado. |
| 02–05 · Programación, objetos, algoritmos y SML | Reunir en informes propios las compilaciones y ejecuciones reproducibles de los ejercicios; estas materias todavía no tienen un informe conjunto de validación como 00, 01 y 06. |
| 06 · Hotel | Añadir pruebas de concurrencia entre operadores y un procedimiento de despliegue si se amplía su uso. |

Estas ampliaciones no impiden consultar las entregas publicadas. Se mantendrían separadas de los trabajos originales y de los resultados ya comprobados.

## Trabajo completado

| Trabajo | Resultado y evidencia |
|---|---|
| Organización y presentación | Siete materias y 46 actividades con README, desarrollo y enlaces a las soluciones o documentos disponibles. La 01 incluye ahora entradas, salidas, ejemplos de consola y recorridos manuales más detallados. |
| Numeración | 00 Sistemas; 01 Introducción; 02 Fundamentos; 03 Objetos; 04 Algoritmos; 05 SML; 06 Bases de Datos. Índices, perfil, automatizaciones y enlaces de las ocho tareas del hotel actualizados. |
| Introducción: C++ | Siete programas: los cinco originales tienen **32 casos correctos y 0 fallidos**; se añadieron 21 casos para las dos soluciones nuevas en la [batería actualizada](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/introduccion-cpp.yml). |
| Introducción: triángulo en RAPTOR | Original ejecutado y error confirmado; diagrama y pseudocódigo corregidos. **Siete casos numéricos con el resultado esperado**, comparación con C++ y Newton, y capturas en el [informe](01-introduccion-programacion/triangulo-rectangulo/PRUEBAS-RAPTOR.md). |
| Nombre de la materia 05 | **Programación Funcional-SML**, corregido en carpeta, índices, navegación, documentación y perfil. |
| Sistemas: C/FASM en Linux | Doce pares de programas, **132 comparaciones correctas y 0 fallidas**. [Informe](00-fundamentos-sistemas-computacionales/PRUEBAS-LINUX.md). |
| Hotel: tareas #1–#8 | Instalación, operaciones, permisos, reportes, configuración, compilación y contraseñas completados, con **79 pruebas correctas y 0 fallidas**. [Validación tras renumerar](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38982024703). |
| Contraseñas del hotel | PBKDF2-SHA256 con sal individual, costo configurable y migración de cuentas existentes. [Guía](06-bases-de-datos/hotel-reservas-proyecto-final/docs/SEGURIDAD-CONTRASENAS.md). |

[Volver al repositorio](README.md)
