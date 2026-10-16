# Sistema de Reservas de Hotel - Proyecto Final

[Inicio](../../README.md) · [Materia](../README.md) · [Desarrollo](desarrollo.md) · [Aplicación](HotelReservas)

![Lenguaje](https://img.shields.io/badge/Lenguaje-SQL%20/%20C%23-lightgrey)

[![Compilación Windows](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/hotel-build.yml/badge.svg?branch=main)](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/hotel-build.yml)

## Qué trabajé y cómo leer los ejemplos

Integro el modelo relacional con una aplicación de escritorio desarrollada en equipo. Las 79 pruebas funcionales documentadas son casos comprobados, no pantallas ni tablas. En el almacenamiento de contraseñas, 600 000 indica iteraciones de derivación; una sal de 16 bytes equivale a 128 bits y la clave derivada de 32 bytes, a 256 bits. Estos tamaños no indican la longitud de la contraseña.

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

Seguir la instalación descrita en desarrollo.md. Ejecutar el esquema inicial, Fase1, el seed opcional y [Fase2 de contraseñas](Base%20de%20Datos/HotelDB_Fase2_Contrasenas.sql), en ese orden, en una instancia de práctica. Para actualizar una base existente se ejecuta solo Fase2 con la nueva aplicación; véase [migración](docs/SEGURIDAD-CONTRASENAS.md). Restaurar NuGet y compilar HotelReservas.csproj con Visual Studio y .NET Framework 4.8. El script inicial elimina HotelDB si existe.

---

## Relación con los archivos originales

Los modelos y documentos de apoyo se encuentran en el [inventario de documentos](../../archivos-originales/bases-de-datos/INVENTARIO.md), junto con las versiones publicadas y sus adaptaciones.

## Autoría

Kelvin Del Castillo Torres, Gabriel Galasso y Camila Vasquez, según los créditos conservados del proyecto.

---

## Estado

Proyecto académico incorporado y documentado. Las comprobaciones de compilación y las limitaciones de ejecución se registran en [VALIDACION.md](../VALIDACION.md).

## Compilación automática

El [workflow del hotel](https://github.com/jokxer8/soluciones-programacion-academica/actions/workflows/hotel-build.yml) restaura los paquetes con NuGet, compila en Release con MSBuild sobre Windows Server 2022 y comprueba el ejecutable, las bibliotecas y los recursos gráficos requeridos.

La [primera ejecución](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38838846909), sobre la revisión `4128f7b`, **finalizó correctamente**: restauración, compilación y verificación de salidas aprobadas. Conserva la advertencia CS0168 ya documentada. La [guía de compilación automática](docs/COMPILACION-AUTOMATICA.md) explica los disparadores, la ejecución manual y el alcance de la comprobación.

La [ejecución #2](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38840293702), sobre `1b72dc8`, incorporó la instalación de una base LocalDB aislada y una primera batería de 53 casos. La [ejecución de cierre de #3/#5/#7](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38843402603), sobre `998db69`, verifica **65 pruebas funcionales correctas**. El [informe funcional](docs/PRUEBAS-FUNCIONALES.md) registra los fallos corregidos, la reproducción y los pendientes. La batería prueba el código C# contra SQL Server real y el inicio técnico de formularios, sin recorrido visual completo.

## Seguimiento de mejoras y pruebas

El [tablero de GitHub Projects](https://github.com/users/jokxer8/projects/1) reúne las tareas con los estados **Pendiente**, **En curso**, **Bloqueado** y **Completado**, además de prioridades P1 y P2.

Instalación, reservas y operación de facturación/pagos se verificaron con SQL Server LocalDB. Las tareas #3, #5 y #7 quedaron [completadas](docs/CIERRE-TAREAS.md): permisos, cierre por inactividad, filtros por sucursal, exportaciones PDF/Excel y errores de conexión verificados. La [tarea #8 de contraseñas](docs/SEGURIDAD-CONTRASENAS.md) está completada: PBKDF2-SHA256, migración compatible y **79 pruebas correctas** locales y en [Actions](https://github.com/jokxer8/soluciones-programacion-academica/actions/runs/38849154210). El [plan de seguimiento](docs/SEGUIMIENTO.md) enlaza cada tarea, sus resultados y los criterios pendientes.

[Volver a Bases de Datos](../README.md)
