# Compilación automática de HotelReservas

El [workflow Hotel - Compilacion Windows](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/hotel-build.yml) compila el proyecto del hotel desde una copia limpia del repositorio. Su [configuración](../../../.github/workflows/hotel-build.yml) se conserva junto al código.

## Entorno y pasos

El trabajo utiliza Windows Server 2022, el entorno de Visual Studio disponible en el ejecutor, MSBuild y NuGet 6.x. Compila el csproj clásico para .NET Framework 4.8 y Windows Forms, con configuración Release y plataforma AnyCPU.

1. Obtener el código sin conservar credenciales de Git en la copia de trabajo.
2. Preparar MSBuild y NuGet.
3. Restaurar HotelReservas/packages.config desde el catálogo público de NuGet en la carpeta packages, junto a HotelReservas.
4. Ejecutar MSBuild con Rebuild sobre HotelReservas/HotelReservas.csproj.
5. Comprobar que existen HotelReservas.exe, EPPlus.dll, itextsharp.dll y los dos recursos de logotipo previstos por el proyecto.
6. Registrar un resumen de la compilación y la revisión de código en la página de la ejecución.

El acceso del token se limita a lectura del contenido. Las acciones se fijan a revisiones verificadas de sus repositorios oficiales. La ejecución tiene un máximo de 15 minutos y una nueva ejecución de la misma referencia sustituye a la anterior si aún está activa.

## Cuándo se ejecuta

- Al publicar en main cambios del código, paquetes, recursos o proyecto del hotel, de su solución o de este workflow.
- Al abrir o actualizar una solicitud de cambios hacia main que afecte a esas rutas.
- Manualmente, desde Actions, Hotel - Compilacion Windows, Run workflow, seleccionando la rama.

Los cambios de documentación por sí solos no vuelven a compilar el proyecto. Las otras materias mantienen su organización y no activan esta compilación.

## Primera ejecución y evidencia

La [ejecución #1](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38838846909) se inició por el push de la revisión `4128f7b18aa9476ff3c2066b1deba8d14c2ef419` y **finalizó correctamente** el 15 de octubre de 2026, hora de Santo Domingo. Restauración, compilación, verificación de archivos y cierre del trabajo terminaron con resultado success.

La compilación conserva la advertencia CS0168 en DashboardForm.cs por una variable ex sin uso. No hubo pasos fallidos ni se necesitaron correcciones para conseguir esta ejecución. La [tarea #6](https://github.com/jokxer8/soluciones-programacion-academica/issues/6) conserva los criterios de aceptación y la evidencia.

## Alcance

La restauración, la compilación y la existencia de los archivos de salida se comprueban en GitHub. No se inicia la interfaz, no se ejecutan los scripts de HotelDB y no se validan reservas, permisos, pagos ni reportes contra SQL Server. Estas verificaciones permanecen en las tareas correspondientes del tablero.

El workflow no firma, empaqueta ni despliega la aplicación y no necesita credenciales del servidor SQL. No existe todavía un proyecto de pruebas automatizadas.

## Consultar un fallo

Abrir la ejecución en Actions y revisar el primer paso fallido: restauración para problemas de paquetes, MSBuild para compilación y verificación de archivos para salidas ausentes. Corregir la causa y revisar una nueva ejecución antes de marcar la tarea como completada. Los enlaces del README muestran el estado del workflow y permiten acceder al historial.
