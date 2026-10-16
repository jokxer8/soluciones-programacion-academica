# Cierre de permisos, reportes y configuración

Las tareas [#3](https://github.com/jokxer8/soluciones-programacion-academica/issues/3), [#5](https://github.com/jokxer8/soluciones-programacion-academica/issues/5) y [#7](https://github.com/jokxer8/soluciones-programacion-academica/issues/7) se completaron con una batería ampliada de **65 pruebas correctas, 0 fallidas**, ejecutada contra SQL Server LocalDB. La [ejecución final en Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38843402603) corresponde a `998db69`. El [registro de casos](evidencias/functional-results.tsv) incluye los resultados locales.

## #3 Autenticación, permisos e inactividad

| Módulo | ADMIN | RECEPCION | EMPLEADO | SOPORTE |
|---|---|---|---|---|
| Operaciones del hotel | Sí | Sí | Sí | Sí |
| Reportes y sus exportaciones | Sí | No | No | No |
| Configuración | Sí | No | No | No |
| Administración | Sí | No | No | Sí |
| Mantenimiento | Sí | No | No | Sí |

SesionUsuario centraliza la política del menú. Las operaciones restringidas comprueban además la sesión, el estado de la cuenta y su rol persistido en SQL, incluyendo las llamadas directas a las 24 consultas de reportes y a los exportadores. Se prueban cuentas sin sesión, inactivas y una alteración del rol en memoria; se rechaza el acceso y se invalida la sesión cuando corresponde.

El cierre explícito borra la sesión inmediatamente. Tras 15 minutos sin actividad aparece un aviso con 60 segundos para continuar. Las pruebas disparan el evento de inactividad y ejercitan el diálogo real: continuar reinicia el temporizador; expirar cierra la sesión e impide acceder a reportes. No se espera 15 minutos en CI. Se corrigió una recursión al cerrar el formulario propietario desde FormClosed del aviso; ahora el cierre ocurre después de terminar el diálogo. Se retiran filtros de mensajes y suscripciones de tema al liberar MainForm.

Esta política pertenece a la aplicación de escritorio. SQL Server sigue usando los permisos de la identidad de Windows configurada: no se presenta como aislamiento frente a un propietario que acceda directamente a la base o modifique el programa.

## #5 Reportes, filtros y exportaciones

Las 24 consultas aceptan una sucursal opcional sin estado global. El selector permite todas o una sucursal y se aplica a tablas, agregaciones y denominadores. Se contrastan sucursales 1, 2 e inexistente, fechas sin resultados, ocupación y facturación con consultas SQL de control. Un empleado puede atender una reserva de otra sucursal; el filtro conserva esa reserva y resuelve correctamente su nombre.

El detalle operativo descuenta tanto el descuento propio de la reserva como las promociones. Se corrigieron nombres de columnas de gráficos y conversiones de valores nulos. La exportación usa la tabla filtrada de la pestaña activa, con el nombre de la sucursal en el título; se incorporó PDF además de Excel.

Se generan, reabren y verifican tres XLSX y cinco PDF: ejemplo con acentos e importes, reporte sin datos, detalle operativo, PDF de varias páginas y factura. Se revisaron visualmente las nueve páginas PDF renderizadas y las tres hojas XLSX importadas y renderizadas. Se corrigió el ancho de fechas y textos del Excel; se conservan números como números, fechas formateadas, encabezados, filtros y filas inmovilizadas. El PDF repite encabezados al cambiar de página y admite las tablas operativas anchas en A3 horizontal.

La ejecución real detectó que faltaba BouncyCastle 1.8.9, dependencia de iTextSharp 5.5.13.3. Se añadió a packages.config y al proyecto, y se verifica su biblioteca en CI. Los paquetes y binarios permanecen excluidos de Git.

Ejemplos publicados con datos exclusivamente ficticios:

- [Reporte PDF](evidencias/reporte.pdf) y [reporte Excel](evidencias/reporte.xlsx).
- [Vista del PDF](evidencias/reporte-preview.png) y [vista del Excel](evidencias/excel-preview.png).
- [Reporte PDF sin datos](evidencias/reporte-vacio.pdf).

La inspección usa lectores y renderizadores; no certifica cada versión de Microsoft Excel ni todos los diálogos del sistema operativo.

## #7 Configuración de conexión

La aplicación lee HOTELRESERVAS_CONNECTION_STRING al crear la conexión. Ausente, vacía o con espacios utiliza el ejemplo local: Server=localhost;Database=HotelDB;Trusted_Connection=True;. Una configuración mal formada o sin servidor/base produce una indicación comprensible sin copiar la cadena ni incluir una excepción interna con datos privados. La apertura centralizada traduce un fallo SQL en instrucciones para comprobar instancia, base y permisos.

Se prueban configuración ausente/vacía, cadenas mal formadas e incompletas con una contraseña ficticia que no debe aparecer en el error y un servidor deliberadamente inaccesible con tiempo límite de un segundo. El fallo de conexión también elimina cualquier sesión previa. No se crean archivos privados de configuración en el repositorio.

Ejemplo para una instancia de práctica ya instalada, en PowerShell:

```powershell
$env:HOTELRESERVAS_CONNECTION_STRING = 'Server=(localdb)\MSSQLLocalDB;Database=HotelDB;Integrated Security=True;'
& './HotelReservas/bin/Release/HotelReservas.exe'
```

## Reproducción y límites

Compilar Release y ejecutar tests/Run-FunctionalTests.ps1 según la [guía funcional](PRUEBAS-FUNCIONALES.md). El ejecutor crea una instancia y archivos SQL exclusivos, trabaja en una ruta temporal corta compatible con las bibliotecas .NET Framework y copia los resultados al destino solicitado. Después elimina solo la base y la instancia de esa ejecución.

Las evidencias académicas de origen no se alteran. Los hashes publicados se actualizan para las correcciones y se conservan los hashes de origen. La advertencia CS0168 permanece sin afectar la compilación. La mejora de derivación de contraseñas [#8](https://github.com/jokxer8/soluciones-programacion-academica/issues/8) es una mejora separada y sigue pendiente; tampoco se certifican concurrencia de múltiples operadores o un despliegue de producción.

Referencia de la dependencia restaurada: [iTextSharp 5.5.13.3 en NuGet](https://www.nuget.org/packages/iTextSharp/5.5.13.3).
