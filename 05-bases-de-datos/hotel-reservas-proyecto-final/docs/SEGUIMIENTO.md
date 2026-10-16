# Seguimiento de mejoras y pruebas del hotel

El [tablero HotelReservas - Mejoras y pruebas](https://github.com/users/jokxer8/projects/1) es la referencia de estados actualizados. Este documento registra el alcance y el resultado verificado de la compilación automática del 15 de octubre de 2026.

## Estados y prioridades

- **Pendiente:** trabajo planificado que todavía no se ha iniciado.
- **En curso:** trabajo iniciado; no implica que las pruebas hayan pasado.
- **Bloqueado:** requiere resolver una dependencia o disponer de un entorno.
- **Completado:** trabajo terminado con evidencia registrada.

P1 identifica las validaciones y automatización prioritarias. P2 agrupa mejoras y comprobaciones posteriores. Las pruebas que dependen de la base se ejecutarán cuando la instalación de SQL Server esté validada.

## Pruebas y mejoras registradas

| Tarea | Prioridad | Estado registrado | Dependencia |
|---|---|---|---|
| [Validar instalación completa en SQL Server](https://github.com/jokxer8/soluciones-programacion-academica/issues/1) | P1 | Bloqueado | Instancia de práctica accesible |
| [Reservas, disponibilidad e integridad](https://github.com/jokxer8/soluciones-programacion-academica/issues/2) | P1 | Pendiente | Instalación validada |
| [Autenticación y permisos por rol](https://github.com/jokxer8/soluciones-programacion-academica/issues/3) | P1 | Pendiente | Instalación validada |
| [Check-in, check-out, facturación y pagos](https://github.com/jokxer8/soluciones-programacion-academica/issues/4) | P1 | Pendiente | Instalación validada |
| [Reportes y exportaciones PDF y Excel](https://github.com/jokxer8/soluciones-programacion-academica/issues/5) | P2 | Pendiente | Instalación validada |
| [Compilación con GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/issues/6) | P1 | Completado | [Ejecución #1 correcta](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38838846909) |
| [Configuración externa de conexión](https://github.com/jokxer8/soluciones-programacion-academica/issues/7) | P2 | Pendiente | Definir mecanismo y comprobarlo |
| [Almacenamiento de contraseñas](https://github.com/jokxer8/soluciones-programacion-academica/issues/8) | P2 | Pendiente | Diseño de derivación y migración |

Cada tarea incluye criterios de aceptación verificables. Al cerrarla se deben registrar los pasos y resultados obtenidos, usando datos ficticios. No se debe marcar una prueba como aprobada a partir de la compilación solamente.

## Hitos ya completados

- Código, SQL, modelos, evidencia y documentación publicados con créditos del equipo. Véase el [proyecto](../README.md).
- Compilación Release de HotelReservas y revisión de sintaxis SQL realizadas. La advertencia CS0168 y el alcance de la revisión constan en [VALIDACION.md](../../VALIDACION.md).
- Workflow de Windows, MSBuild y NuGet publicado y ejecutado correctamente desde un checkout limpio. Véase la [evidencia de Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38838846909).

Estos hitos no acreditan ejecución de extremo a extremo contra SQL Server.

## Sugerencias de GitHub Actions

Las tarjetas **.NET** y **.NET Desktop** de la interfaz son plantillas sugeridas. No indican un error ni que exista ya una automatización activa.

HotelReservas utiliza Windows Forms con .NET Framework 4.8 y un proyecto csproj clásico. El workflow configurado utiliza un ejecutor Windows, restaura packages.config con NuGet en la ruta prevista y compila con MSBuild. Las opciones de empaquetado, firma y publicación de la plantilla Desktop no son requisitos de este proyecto.

La [tarea de Actions](https://github.com/jokxer8/soluciones-programacion-academica/issues/6) queda completada con la [ejecución correcta](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38838846909). La [guía de compilación automática](COMPILACION-AUTOMATICA.md) documenta la configuración y sus límites. No se han habilitado firma ni despliegue automático y todavía no existe un proyecto de pruebas automatizadas.

Referencias: [plantilla oficial .NET Desktop](https://github.com/actions/starter-workflows/blob/main/ci/dotnet-desktop.yml) y [configuración de MSBuild](https://github.com/microsoft/setup-msbuild).
