# Desarrollo - Sistema de Reservas de Hotel

## 1. Problema y alcance

El proyecto conecta una aplicación de escritorio con SQL Server para gestionar reservas y operaciones hoteleras. Integra clientes, habitaciones, reservas, servicios adicionales, promociones, facturas, pagos, empleados y mantenimiento. Es un trabajo grupal de Kelvin Del Castillo Torres, Gabriel Galasso y Camila Vasquez.

## 2. Diseño de la base

El DER y el modelo relacional acompañan a la implementación. El script base define 18 tablas; la migración añade CheckInOut, AuditLog y ConfiguracionSistema. NotificacionData.cs y RatingData.cs incluyen creación de tablas adicionales cuando se inicializan sus módulos. Las claves, relaciones y objetos ejecutables se consultan en los scripts y el código, mientras que docs/DICCIONARIO_DATOS.md sirve como referencia de campos. La documentación original puede describir comportamientos de distintas fases y debe contrastarse con el código actual.

## 3. Organización del código

Program.cs abre LoginForm. Database.cs centraliza la conexión y operaciones generales; las clases *Data.cs agrupan acceso a datos por módulo. SesionUsuario conserva el usuario y rol. MainForm carga los formularios y configura la visibilidad según rol: informes y configuración para ADMIN, administración y mantenimiento para ADMIN o SOPORTE. El tema inicial verificado en Theme.cs es claro.

## 4. Instalación de la base de práctica

1. Abrir SSMS y conectarse a una instancia SQL Server destinada a pruebas.
2. Ejecutar [HotelDB_Proyecto_Final.sql](Base%20de%20Datos/HotelDB_Proyecto_Final.sql). **Elimina y recrea HotelDB**: no utilizar sobre una base que contenga trabajo que se deba conservar.
3. Ejecutar [HotelDB_Fase1_Migracion.sql](Base%20de%20Datos/HotelDB_Fase1_Migracion.sql) una vez sobre la base recién creada. Esta fase prepara el esquema y la autenticación para el código publicado.
4. Opcionalmente ejecutar [HotelDB_SeedData.sql](Base%20de%20Datos/HotelDB_SeedData.sql) una sola vez para ampliar los datos de demostración.
5. Definir HOTELRESERVAS_CONNECTION_STRING en el entorno de la aplicación para elegir una instancia de práctica, por ejemplo Server=(localdb)\MSSQLLocalDB;Database=HotelDB;Integrated Security=True;. Sin esa variable se utiliza localhost con autenticación de Windows.

Los datos de identidad y contacto de los scripts publicados se han sustituido por valores ficticios, manteniendo referencias y unicidad. No se presenta ese conjunto como una base de clientes real.

## 5. Compilación y ejecución

Se requiere Windows, Visual Studio con desarrollo de escritorio .NET y .NET Framework 4.8 Developer Pack. Abrir HotelReservas/HotelReservas.csproj y restaurar packages.config con NuGet en packages, junto a HotelReservas. Las dependencias son EPPlus 4.5.3.3 e iTextSharp 5.5.13.3. El archivo slnx se conserva para ediciones de Visual Studio compatibles con ese formato; el csproj permite abrir directamente el proyecto.

Compilar Release o Debug y ejecutar después de instalar la base. LEER.txt documenta las cuentas de demostración que realmente crea el script inicial. No son credenciales del servidor.

## 6. Ajustes para el portafolio

- Se conserva el código de la aplicación y se sustituye el servidor personal por localhost.
- Se normalizan documentos, teléfonos y correos de ejemplo; se mantienen créditos de autoría en la documentación.
- Se rectifican la secuencia de instalación, las cuentas de prueba y el tema inicial descritos en LEER.txt.
- repository_link.txt apunta al proyecto publicado y sustituye el marcador de posición del original.
- Binarios, paquetes, carpetas de empleados, cachés, configuraciones de asistentes y archivos temporales quedan fuera de Git.

## 7. Validación y límites

El registro [VALIDACION.md](../VALIDACION.md) contiene el resultado de compilación, el análisis sintáctico de SQL y las limitaciones de ejecución. La batería de [pruebas funcionales](docs/PRUEBAS-FUNCIONALES.md) verifica 53 casos contra LocalDB y se ejecuta también en Actions. No equivale a una revisión visual de todos los formularios. Las correcciones de la versión pública abarcan instalación, seed, sesiones, relación usuario/empleado, estados de habitación, check-out y reportes vacíos. El mecanismo SHA256 corresponde a la implementación académica conservada.
