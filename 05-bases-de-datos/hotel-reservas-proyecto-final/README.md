# Sistema de Reservas de Hotel - Proyecto Final

## Descripción de la actividad

Proyecto grupal de C# Windows Forms y SQL Server que integra diseño relacional y gestión hotelera.

---

## Objetivo

Integrar una aplicación de escritorio con una base relacional para administrar clientes, habitaciones, reservas, servicios, facturación y operaciones del hotel.

---

## Conceptos aplicados

- C# y .NET Framework 4.8.
- Windows Forms.
- SQL Server y T-SQL.
- ADO.NET y consultas parametrizadas.
- Procedimientos, vistas y triggers.
- Usuarios, roles y auditoría.

---

## Actividad desarrollada

La aplicación utiliza formularios para la interfaz y Database.cs y clases *Data.cs para el acceso a datos. El script inicial crea 18 tablas, índices y objetos de programación. La migración incorpora CheckInOut, AuditLog y ConfiguracionSistema, prepara las claves para el hash que utiliza la aplicación y actualiza la generación de facturas. Los documentos de arquitectura y diccionario acompañan al DER y al modelo relacional; el SQL es la referencia de implementación cuando existen diferencias.

---

## Archivos de esta carpeta

- [Base de Datos](Base%20de%20Datos): código o documentación.
- [HotelReservas](HotelReservas): código o documentación.
- [HotelReservas.slnx](HotelReservas.slnx).
- [LEER.txt](LEER.txt).
- [diagrama-hotel.drawio](diagrama-hotel.drawio).
- [docs](docs): código o documentación.
- [modelo-relacional-hotel.xlsx](modelo-relacional-hotel.xlsx).
- [repository_link.txt](repository_link.txt).
- [Desarrollo y análisis](desarrollo.md).

---

## Cómo revisar la actividad

Seguir la instalación descrita en desarrollo.md. Ejecutar el esquema inicial y después la migración en una instancia de práctica; opcionalmente cargar el seed. Restaurar NuGet y compilar HotelReservas.csproj con Visual Studio y .NET Framework 4.8. El script inicial elimina HotelDB si existe.

---

## Relación con los archivos originales

Los materiales proceden de la carpeta 30. Base de Datos del archivo académico del autor. El [inventario](../../archivos-originales/bases-de-datos/INVENTARIO.md) registra el origen y las adaptaciones para publicación.

## Autoría

Kelvin Del Castillo Torres, Gabriel Galasso y Camila Vasquez, según los créditos conservados del proyecto.

---

## Estado

Proyecto académico incorporado y documentado. Las comprobaciones de compilación y las limitaciones de ejecución se registran en VALIDACION.md.

[Volver a Bases de Datos](../README.md)
