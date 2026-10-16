# Pruebas funcionales con SQL Server

## Resultado actual

**65 pruebas correctas y 0 fallidas**, verificadas localmente con LocalDB 17.0.4025.3 y en [GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38843402603), revisión 998db69. Los [casos y resultados](evidencias/functional-results.tsv) se conservan junto a la documentación. El [cierre de #3, #5 y #7](CIERRE-TAREAS.md) registra la matriz de roles, las exportaciones y los errores corregidos.

La batería llama al C# compilado contra SQL Server real. Cada ejecución instala esquema, migración y seed, en ese orden, en una instancia aleatoria HotelQA con archivos exclusivos. No usa bases personales ni inicia el servidor localhost. Al terminar elimina únicamente su base e instancia. Los archivos de prueba se generan en una ruta temporal corta y se copian al destino de evidencias solicitado.

## Reproducción

Requiere Windows, .NET Framework 4.8, MSBuild, NuGet y SQL Server Express LocalDB. Restaurar packages.config en packages, junto a HotelReservas, y compilar Release según la [guía de compilación](COMPILACION-AUTOMATICA.md). Desde la carpeta del hotel:

```powershell
powershell.exe -NoProfile -File tests/Run-FunctionalTests.ps1
```

Los resultados se guardan en tests/results, excluido de Git. El script falla si no se instala la base, no compilan las pruebas o falla cualquier caso. Configura HOTELRESERVAS_CONNECTION_STRING durante la prueba y restaura su valor anterior. El seed se ejecuta una sola vez sobre una base nueva.

## Cobertura y seguimiento

| Tarea | Cobertura | Estado |
|---|---|---|
| #1 Instalación | Esquema, migración, seed, restricciones y objetos; 21 tablas y 23 tras inicializar módulos dos veces; inicio técnico de Login/Main | Completado |
| #2 Reservas | CRUD, fechas/importes/referencias inválidos, solapamientos, cancelación, traslado y coherencia con mantenimiento | Completado |
| #3 Autenticación | Cuatro roles, matriz de módulos, llamadas directas denegadas, cuenta inactiva, rol alterado, cierre explícito, continuar/expirar aviso | Completado |
| #4 Operación y pagos | Check-in/out, relación usuario/empleado, noches, servicios, promociones, descuentos, ITBIS, redondeo, pagos y rollback deliberado | Completado |
| #5 Reportes | 24 consultas, fechas/sucursal, ocupación/importes de control, ausencia de datos, 3 XLSX y 5 PDF reabiertos y revisados | Completado |
| #6 Actions | Restauración, compilación, archivos requeridos y batería SQL automática | Completado |
| #7 Configuración | Variable externa, ejemplo local, cadenas vacías/inválidas, servidor inaccesible y mensajes sin datos privados | Completado |
| #8 Contraseñas | Mejorar derivación y migración del mecanismo SHA256 académico | Pendiente |

## Correcciones comprobadas

La primera fase corrigió opciones SET necesarias para índices, colisiones del seed con reservas y facturas iniciales, selección y claves de usuarios ampliados, sesión retenida tras login fallido, relación IdUsuario/IdEmpleado, check-out que no finalizaba la reserva, estados de habitación incorrectos y columna Anio duplicada en reportes sin historial.

La fase final añadió autorización efectiva en los módulos restringidos, saneó errores de conexión, corrigió la recursión de cierre por inactividad, filtros por sucursal y descuento del detalle operativo, agregó exportación PDF y la dependencia BouncyCastle faltante, y ajustó anchos/formatos del Excel. El [informe de cierre](CIERRE-TAREAS.md) permite revisar cada criterio y sus evidencias.

El primer conjunto de [53 casos](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38840293702) forma parte del historial; no sustituye la comprobación final de 65. Durante el desarrollo se corrigieron también errores del propio test, que no se atribuyen al código del hotel.

## Límites

La revisión visual corresponde a las exportaciones renderizadas, no a cada versión de Microsoft Excel. Las pruebas de inactividad adelantan el evento y ejercitan el diálogo real, sin esperar quince minutos en CI. El inicio y los controles de formularios se prueban técnicamente; no se afirma una revisión manual de todos los botones. No se probaron múltiples operadores simultáneos, despliegue ni migración de bases personales. La autorización pertenece al programa y no reemplaza los permisos de Windows/SQL. Se conserva la advertencia CS0168 no bloqueante. Las fuentes académicas y sus hashes de origen se mantienen.