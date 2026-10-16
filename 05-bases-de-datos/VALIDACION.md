# Validación de Bases de Datos

Revisión del 15 de octubre de 2026 sobre la sección ampliada con la carpeta 30. Base de Datos y el proyecto final del hotel.

## Alcance

Se inventariaron 329 archivos del origen. El [registro de auditoría](AUDITORIA.md) clasifica todos los materiales y el [inventario](../archivos-originales/bases-de-datos/INVENTARIO.md) relaciona los entregables con sus fuentes. Las otras cuatro materias publicadas se conservan sin cambios. La sección mantiene el patrón README, desarrollo, código y evidencia.

## Comprobaciones realizadas

- Compilación Release de HotelReservas.csproj con MSBuild de Visual Studio: **correcta**, con una advertencia CS0168 por una variable ex sin uso en DashboardForm.cs. Se utilizaron los paquetes conservados con el proyecto; paquetes y binarios quedan excluidos de Git.
- Análisis de los **18 scripts T-SQL** de actividades mediante Microsoft.SqlServer.TransactSql.ScriptDom: **sin errores de sintaxis**.
- Verificación de 382 enlaces locales y de los hashes de origen y publicación de los 205 registros del inventario, correspondientes a 168 archivos de origen.
- Apertura de 37 copias PDF y comprobación de 42 contenedores Office y XMind y siete diagramas Draw.io, junto con sus XML.
- Revisión de la secuencia de instalación, cuentas de demostración, servidor local y tema inicial del hotel contra los scripts y el código.
- Sustitución de valores de documento, teléfono y correo de los datos de demostración del hotel por valores ficticios. Las credenciales privadas y los archivos de empleados quedan fuera.

## Compilación automática verificada

El [workflow de Windows, MSBuild y NuGet](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/hotel-build.yml) se ejecutó correctamente sobre la revisión `4128f7b`. La [ejecución #1](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38838846909) completó la restauración desde NuGet, la compilación Release y la comprobación del ejecutable, bibliotecas y logotipos. Conserva la advertencia CS0168. No se ejecutan pruebas funcionales ni SQL Server en este workflow.

## Rectificaciones documentadas

Restaurante incluye separadores GO, cierre de la función, consulta de ejemplo fuera de su cuerpo y un límite que incluye el día final completo. Alquiler de coches añade separadores y contexto de base donde faltaban. El hotel utiliza localhost como servidor de referencia e incluye la migración necesaria después del script inicial. Sus guías explican la recreación de HotelDB y la carga opcional de datos.

## Límites de la revisión

La conexión local a SQL Server no estaba disponible durante la revisión. No se ejecutaron los scripts sobre una instancia ni se probaron de extremo a extremo las pantallas y operaciones del hotel. La compilación y el análisis sintáctico no verifican resultados ni permisos en ejecución. NotificacionData y RatingData incluyen creación de tablas desde la aplicación, además del esquema preparado por los scripts.

Bolsa de empleo requiere un esquema previo no incluido y sus supuestos de fechas se explican en desarrollo.md. Las variantes de coches son independientes. Las opciones de los esquemas de práctica dependen de la versión de SQL Server.

Los DOCX y XLSX conservan su diseño académico; se comprobaron su integridad y XML, sin revisar paginación en Word o Excel. Las evidencias escaneadas se publican según la autorización del autor. Las evaluaciones parciales no sustituyen un acta oficial de nota final.
