# Plan Maestro · Portafolio académico

[Inicio](README.md) · [Guía de lectura](GUIA-DE-LECTURA.md) · [Guía de estilo](GUIA-DE-ESTILO.md) · [Pendientes](PENDIENTES.md)

## Propósito y alcance
Documentar, evidenciar y enseñar lo realizado en la ruta 00–15. Cada materia orienta desde «En palabras simples» hacia el desarrollo, los archivos y las evidencias disponibles. Las fichas responden qué justifica cada tema, para qué sirve, cómo se aplica y cuándo conviene usarlo. En capítulos con varios temas, cada uno tiene sus propias cuatro respuestas. Las mejoras didácticas posteriores se identifican aparte de las entregas originales.

## Estado actual
Corte documental verificado: 27 de noviembre de 2026 UTC. 11 de 16 materias documentadas: 00–08, 10 y 15. Pendientes: 09 y 11–14. El estado describe la cobertura pública del portafolio, no la calificación de la materia ni que se haya vuelto a ejecutar cada ejercicio. En 04, la Práctica 3 no se presentó; el desarrollo complementario se identifica como tal.

## Historia y decisiones
1. Se organizó el material en una ruta numerada y se preservaron las entregas disponibles y los créditos.
2. Se incorporaron explicaciones breves y navegación por actividad.
3. Se registraron pruebas de ejercicios y proyectos en entornos acotados. La ruta externa [GitHub desde cero](https://github.com/jokxer8/guias-en-palabras-simples/tree/main/github) permite practicar el uso de GitHub desde un repositorio propio.

4. El 27 de noviembre de 2026 se aplicó el estándar de las cuatro preguntas en las 11 materias documentadas y sus guías de apoyo: se actualizaron 127 archivos Markdown en cuatro cambios, sin modificar código, entregas ni evidencias. La [guía de estilo](GUIA-DE-ESTILO.md#en-palabras-simples) define cómo mantenerlo.

## Tecnologías y resultados comprobados

| Tecnología y finalidad | Evidencia | Límite |
|---|---|---|
| C++ y MSVC en Windows, para ejercicios de algoritmia | [53 casos correctos: 32 de cinco programas originales y 21 de dos soluciones nuevas del parcial](00-introduccion-a-la-algoritmia/VALIDACION.md) | Batería del código publicado, no ejecución de todas las entregas históricas. |
| C y FASM en Linux, para comparar soluciones equivalentes | [132 comparaciones correctas en 12 pares](02-fundamentos-sistemas-computacionales/PRUEBAS-LINUX.md) | Conjuntos de entrada y entornos documentados. |
| C#, Windows Forms y SQL Server LocalDB, para el sistema de reservas | [79 pruebas funcionales correctas](06-bases-de-datos/hotel-reservas-proyecto-final/docs/PRUEBAS-FUNCIONALES.md) | Validación de laboratorio, no un despliegue real. |
| Cisco Packet Tracer, para simular la red hotelera de cuatro sedes | [Cinco escenarios documentados en la entrega](07-fundamentos-de-redes/proyecto-cadena-hotelera/validacion.md) | Informe de simulación; no verificación de una red en producción. |
| GitHub Actions, para repetir compilaciones y pruebas | Enlaces a ejecuciones en los informes de Windows, Linux y reservas. | No cubre todas las guías ni los simuladores. |

## Pendientes y próximos hitos
1. Añadir una materia solo cuando esté disponible el material y se autorice publicarlo. Mantener Inteligencia de Negocios pendiente hasta recibir y aprobar su carpeta.
2. Documentar cómo reproducir las prácticas SQL en sus esquemas, y completar el recorrido visual de Reservas de Hotel según [los pendientes](PENDIENTES.md).

## Sugerencias
- Elegir un caso de solapamiento de reservas y explicar los datos iniciales, la acción y el resultado esperado. Una segunda persona puede intentar reproducirlo y contribuir una corrección o un ejemplo útil. Atribuir coautoría solo si hubo aportación real.
- Cada nuevo resultado debería indicar el entorno, fecha, alcance y enlace a su evidencia.
