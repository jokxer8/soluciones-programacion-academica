# Pruebas funcionales con SQL Server

## Resultado comprobado

La batería ejecutada el 15 de octubre de 2026 en Windows y SQL Server LocalDB **17.0.4025.3** terminó con **53 pruebas correctas y 0 fallidas** después de corregir los fallos reproducidos. Se cargan el esquema, la migración y el seed en ese orden. Las pruebas llaman al código C# compilado del hotel contra una base real; no sustituyen SQL Server por respuestas simuladas.

El [registro de casos](evidencias/functional-results.tsv) conserva el resultado local. La [ejecución de GitHub Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38840293702), sobre la revisión `1b72dc8`, ejecuta también esta batería desde un checkout limpio y muestra los casos en su resumen. La ejecución remota terminó correctamente con 53/53 y LocalDB 15.0.4382.1; la versión del ejecutor queda registrada en el resumen.

## Entorno aislado y reproducción

El servidor habitual localhost estaba detenido. Se utilizó LocalDB ya instalado, sin iniciar ese servicio ni utilizar bases personales. Cada ejecución del script crea una instancia HotelQA con un nombre aleatorio y archivos de datos exclusivos, instala los datos ficticios y al terminar elimina únicamente esa base y esa instancia. No acepta un servidor existente como destino. El seed sigue siendo de una sola ejecución sobre una base recién instalada.

Requisitos: Windows, .NET Framework 4.8, MSBuild, NuGet y SQL Server Express LocalDB. Desde la carpeta del hotel, restaurar paquetes y compilar Release como indica la [guía de compilación](COMPILACION-AUTOMATICA.md); después ejecutar en Windows PowerShell:

```powershell
powershell.exe -NoProfile -File tests/Run-FunctionalTests.ps1
```

Los resultados aparecen en tests/results, excluido de Git. El script devuelve error si falla la instalación, la compilación de las pruebas o cualquier caso. Usa la variable de entorno HOTELRESERVAS_CONNECTION_STRING solo durante la ejecución y restaura su valor anterior. La aplicación acepta esa variable para seleccionar una instancia de práctica; si no existe, mantiene localhost con autenticación de Windows. No se publican cadenas privadas ni credenciales de servidor.

## Cobertura y estados

| Tarea | Resultado | Estado |
|---|---|---|
| [#1 Instalación](https://github.com/jokxer8/soluciones-programacion-academica/issues/1) | Esquema, migración y seed; 21 tablas iniciales y 23 al inicializar módulos dos veces; claves y restricciones habilitadas y confiables; procedimientos, triggers e inicio técnico de formularios Login y Main | Completado |
| [#2 Reservas](https://github.com/jokxer8/soluciones-programacion-academica/issues/2) | Crear, consultar, modificar, cancelar y eliminar; rechazar fechas inválidas, importes negativos y referencias inexistentes; solapamientos al crear y modificar; coherencia al cancelar, trasladar y finalizar mantenimiento | Completado |
| [#3 Autenticación y roles](https://github.com/jokxer8/soluciones-programacion-academica/issues/3) | Acceso ADMIN, RECEPCION, EMPLEADO y SOPORTE; clave incorrecta, usuario inactivo y vacío; cierre de sesión y eliminación de sesión previa tras un acceso fallido | En curso: falta autorización más allá del menú y cierre por inactividad |
| [#4 Operación y pagos](https://github.com/jokxer8/soluciones-programacion-academica/issues/4) | Estados de check-in/out; empleado correcto aunque su identificador difiera del usuario; noches, servicios, promociones, descuentos, ITBIS configurable y redondeo; factura única, pagos parciales/completos y rollback ante fallos deliberados | Completado |
| [#5 Reportes](https://github.com/jokxer8/soluciones-programacion-academica/issues/5) | Las 24 consultas públicas se ejecutan; conteos e importes contrastados con SQL de control; filtros por fecha y ausencia de historial | En curso: faltan exportaciones visuales, sucursal y permisos |

Los estados completados corresponden a los criterios automatizados indicados. No acreditan una revisión visual completa ni concurrencia de varios operadores.

## Fallos reproducidos y correcciones

| Problema observado | Corrección |
|---|---|
| La instalación desde sqlcmd fallaba al crear un índice con QUOTED_IDENTIFIER desactivado | Declarar las opciones SET necesarias en el script inicial |
| El seed reutilizaba identificadores de reservas y facturas ya creados por la base inicial; colisión en ReservaServicio | Calcular desplazamientos antes de insertar reservas y aplicarlos a servicios, facturas, pagos y promociones |
| La selección de empleados del seed dependía de correos hotel.com que fueron sustituidos por ejemplos; sus nuevas claves no estaban hasheadas | Seleccionar empleados sin usuario y generar claves compatibles con el login después de la migración |
| Un login fallido conservaba la sesión anterior | Limpiar SesionUsuario antes de validar credenciales |
| Check-in/out escribía IdUsuario en una clave foránea de Empleado | Resolver IdEmpleado mediante Usuarios en ambas operaciones |
| El check-out generaba factura pero no finalizaba la reserva ni liberaba la habitación | Finalizar la reserva dentro de la misma transacción, después de facturar |
| Cancelar o trasladar una reserva y finalizar mantenimiento podían liberar una habitación con otra reserva confirmada | Recalcular el estado usando todas las reservas y mantenimientos de la habitación, incluidos los cambios de habitación y eliminaciones |
| Reportes sin historial lanzaba una excepción por una columna Anio duplicada | Reutilizar el esquema que devuelve la consulta y agregar solo la fila del año actual |

Los casos de regresión comprueban estas correcciones. Durante el desarrollo también se ajustaron dos errores de la batería: una cuenta de prueba infringía la unicidad de empleado y una aserción usaba un nombre de columna incorrecto. Esos errores del test no se atribuyen a la aplicación.

## Pendientes y límites

- El acceso a módulos se configura por visibilidad de botones en MainForm. Esta batería no certifica autorización en la capa de datos: las clases públicas de datos usan la misma conexión de Windows. La tarea #3 permanece abierta para definir y comprobar restricciones efectivas y el flujo de inactividad.
- No se han abierto exportaciones PDF/Excel ni revisado acentos, totales y legibilidad en los archivos exportados. Tampoco se certifica un filtro por sucursal; las firmas actuales de ReportesData reciben año o fechas. La tarea #5 permanece abierta.
- La prueba de inicio crea los formularios y sus controles nativos sin un recorrido interactivo completo. No valida todos los botones ni todos los formularios.
- No se probaron dos operadores simultáneos, despliegue, restauración de una base real ni migración de datos personales. Las correcciones SQL se verificaron en instalaciones nuevas; no se debe recrear una base con información que se necesite conservar.
- Se conserva la advertencia de compilación CS0168. La configuración externa ya se usa en las pruebas; su tratamiento de errores sigue en curso en #7. El almacenamiento de contraseñas sigue pendiente en #8.

Referencia técnica de la instalación: [opciones necesarias para índices filtrados de SQL Server](https://learn.microsoft.com/en-us/sql/t-sql/statements/create-index-transact-sql?view=sql-server-ver17).
