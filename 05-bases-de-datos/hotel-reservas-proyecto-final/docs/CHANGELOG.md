# Changelog

## [Portafolio — tarea #8] - 2026-10-16

- PBKDF2-SHA256 con sal individual, formato versionado y coste configurable.
- Migración de SHA256 al acceso correcto; comparación binaria y actualización condicional.
- Claves nuevas preservan Unicode/espacios; lista administrativa sin hashes; edición vacía conserva la clave.
- Fase2 repetible y 14 casos nuevos: 79 pruebas locales correctas.
- [Guía de instalación y seguridad](SEGURIDAD-CONTRASENAS.md).

## [1.1.0] - 2026-04-06
### Agregado
- Rol RECEPCION con migracion de usuarios existentes
- Hash SHA256 para contrasenas (migracion automatica de datos)
- MantenimientoForm: CRUD completo de mantenimientos
- Check-in / Check-out workflow con tabla CheckInOut
- Dashboard operativo con KPIs en tiempo real
- Metricas del sector: RevPAR, ADR, Occupancy Rate, GOPPAR
- Audit trail (tabla AuditLog + helper RegistrarAccion)
- Export a Excel desde reportes (EPPlus 4.5.3.3)
- Tabla ConfiguracionSistema con parametros globales
- ConfiguracionForm para administrar parametros del sistema

## [1.0.0] - 2026-04-06
### Estado inicial
- Login con autenticacion basica (contrasenas en texto plano — migrado en 1.1.0)
- CRUD: Clientes, Habitaciones, Reservas, Facturas
- Sistema de reportes operativos y gerenciales
- Gestion de empleados y usuarios (Administracion)
- Theming claro/oscuro
- Sucursales, tipos de habitacion, servicios adicionales, promociones
- Stored procedures: sp_ValidarLogin, sp_InsertarReserva, sp_GenerarFactura

### Conocido (resuelto en 1.1.0)
- Contrasenas en texto plano
- Rol RECEPCION no implementado
- Sin formulario de mantenimiento
- Sin metricas hoteleras estandar
