# Diccionario de Datos — HotelDB

## Tablas preexistentes

### dbo.Roles
Roles de usuario del sistema.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdRol | INT IDENTITY | NO | PK |
| NombreRol | VARCHAR(20) | NO | ADMIN, EMPLEADO, SOPORTE, RECEPCION |
| Descripcion | VARCHAR(100) | SI | Descripcion del rol |

### dbo.Usuarios
Cuentas de acceso al sistema.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdUsuario | INT IDENTITY | NO | PK |
| IdEmpleado | INT | SI | FK a Empleado (NULL = usuario sin empleado) |
| NombreUsuario | VARCHAR(50) | NO | Login unico |
| Clave | VARCHAR(256) | NO | PBKDF2-SHA256 versionado; SHA256 heredado solo hasta migración por acceso válido |
| NombreCompleto | VARCHAR(100) | NO | Nombre para mostrar |
| IdRol | INT | NO | FK a Roles |
| Estado | BIT | NO | 1=activo, 0=inactivo |
| FechaCreacion | DATETIME | NO | Fecha de creacion |

### dbo.Empleado
Empleados del hotel.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdEmpleado | INT IDENTITY | NO | PK |
| IdSucursal | INT | NO | FK a Sucursal |
| Nombre | VARCHAR(50) | NO | Nombre |
| Apellido | VARCHAR(50) | NO | Apellido |
| Cedula | VARCHAR(20) | NO | Documento unico |
| Telefono | VARCHAR(20) | SI | |
| Correo | VARCHAR(120) | SI | |
| Direccion | VARCHAR(250) | SI | |
| FechaNacimiento | DATE | SI | |
| FechaIngreso | DATE | NO | |
| Cargo | VARCHAR(80) | NO | |
| FotoPath | VARCHAR(500) | SI | Ruta relativa a bin\Debug\ArchivosEmpleados\ |
| CvPath | VARCHAR(500) | SI | Ruta relativa a bin\Debug\ArchivosEmpleados\ |
| EstadoLaboral | VARCHAR(30) | NO | ACTIVO, VACACIONES, LICENCIA, SUSPENDIDO, CANCELADO |
| Estado | BIT | NO | 1=activo |

### dbo.Sucursal
Sedes del hotel.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdSucursal | INT IDENTITY | NO | PK |
| NombreSucursal | VARCHAR(100) | NO | |
| Ciudad | VARCHAR(100) | NO | |
| Direccion | VARCHAR(200) | NO | |
| Telefono | VARCHAR(20) | SI | |
| Email | VARCHAR(100) | SI | |
| Estado | BIT | NO | 1=activa |

### dbo.Cliente
Huespedes registrados.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdCliente | INT IDENTITY | NO | PK |
| Nombres | VARCHAR(60) | NO | |
| Apellidos | VARCHAR(60) | NO | |
| TipoDocumento | VARCHAR(20) | NO | CEDULA, PASAPORTE |
| Documento | VARCHAR(25) | NO | Numero de documento unico |
| Telefono | VARCHAR(20) | SI | |
| Correo | VARCHAR(100) | SI | |
| Direccion | VARCHAR(200) | SI | |
| FechaRegistro | DATETIME | NO | |

### dbo.TipoHabitacion
Tipos de habitacion y precio base.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdTipoHabitacion | INT IDENTITY | NO | PK |
| NombreTipo | VARCHAR(50) | NO | Simple, Doble, Suite |
| Capacidad | INT | NO | Personas maximas |
| PrecioBase | DECIMAL(10,2) | NO | Precio sugerido por noche |
| Descripcion | VARCHAR(200) | SI | |

### dbo.EstadoHabitacion
Catalogo de estados de habitacion.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdEstadoHabitacion | INT IDENTITY | NO | PK |
| NombreEstado | VARCHAR(30) | NO | DISPONIBLE, OCUPADA, MANTENIMIENTO, LIMPIEZA |

### dbo.Habitacion
Habitaciones fisicas del hotel.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdHabitacion | INT IDENTITY | NO | PK |
| IdSucursal | INT | NO | FK a Sucursal |
| IdTipoHabitacion | INT | NO | FK a TipoHabitacion |
| IdEstadoHabitacion | INT | NO | FK a EstadoHabitacion |
| Numero | VARCHAR(10) | NO | Numero de habitacion |
| Piso | INT | NO | |
| PrecioPorNoche | DECIMAL(10,2) | NO | Precio real de venta |
| Descripcion | VARCHAR(200) | SI | |

### dbo.EstadoReserva
Catalogo de estados de reserva.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdEstadoReserva | INT IDENTITY | NO | PK |
| NombreEstado | VARCHAR(30) | NO | PENDIENTE, CONFIRMADA, CANCELADA, FINALIZADA |

### dbo.Reserva
Reservas de habitaciones.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdReserva | INT IDENTITY | NO | PK |
| IdCliente | INT | NO | FK a Cliente |
| IdHabitacion | INT | NO | FK a Habitacion |
| IdEmpleado | INT | NO | FK a Empleado (quien creo la reserva) |
| IdEstadoReserva | INT | NO | FK a EstadoReserva |
| FechaReserva | DATETIME | NO | Fecha de creacion |
| FechaEntrada | DATE | NO | Dia de check-in planificado |
| FechaSalida | DATE | NO | Dia de check-out planificado |
| CantidadHuespedes | INT | NO | |
| Observacion | VARCHAR(250) | SI | |
| Subtotal | DECIMAL(10,2) | NO | Hospedaje + servicios |
| Descuento | DECIMAL(10,2) | NO | Descuento de promociones |
| Total | DECIMAL(10,2) | NO | Neto despues de descuento |

### dbo.ServicioAdicional
Catalogo de servicios adicionales.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdServicioAdicional | INT IDENTITY | NO | PK |
| NombreServicio | VARCHAR(50) | NO | |
| Precio | DECIMAL(10,2) | NO | Precio por unidad |
| Estado | BIT | NO | 1=activo |

### dbo.ReservaServicio
Servicios adicionales vinculados a una reserva.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdReserva | INT | NO | PK + FK a Reserva |
| IdServicioAdicional | INT | NO | PK + FK a ServicioAdicional |
| Cantidad | INT | NO | |
| PrecioUnitario | DECIMAL(10,2) | NO | Precio al momento de la reserva |
| Subtotal | DECIMAL(10,2) | NO | Cantidad * PrecioUnitario |

### dbo.Promocion
Promociones y descuentos.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdPromocion | INT IDENTITY | NO | PK |
| NombrePromocion | VARCHAR(100) | NO | |
| TipoDescuento | CHAR(1) | NO | P=Porcentaje, M=Monto fijo |
| ValorDescuento | DECIMAL(10,2) | NO | |
| FechaInicio | DATE | NO | |
| FechaFin | DATE | NO | |
| Estado | BIT | NO | 1=activa |

### dbo.ReservaPromocion
Promociones aplicadas a una reserva.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdReserva | INT | NO | PK + FK a Reserva |
| IdPromocion | INT | NO | PK + FK a Promocion |
| DescuentoAplicado | DECIMAL(10,2) | NO | Monto calculado al momento de aplicar |

### dbo.Factura
Facturas generadas para reservas.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdFactura | INT IDENTITY | NO | PK |
| IdReserva | INT | NO | FK a Reserva (unico) |
| FechaFactura | DATETIME | NO | |
| Subtotal | DECIMAL(10,2) | NO | Base antes de descuento e impuesto |
| Descuento | DECIMAL(10,2) | NO | Total de descuentos |
| Impuesto | DECIMAL(10,2) | NO | ITBIS calculado |
| Total | DECIMAL(10,2) | NO | Monto final a pagar |
| Estado | VARCHAR(20) | NO | PENDIENTE, PAGADA, ANULADA |

### dbo.MetodoPago
Metodos de pago disponibles.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdMetodoPago | INT IDENTITY | NO | PK |
| NombreMetodo | VARCHAR(30) | NO | EFECTIVO, TARJETA, TRANSFERENCIA |

### dbo.Pago
Pagos realizados contra facturas.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdPago | INT IDENTITY | NO | PK |
| IdFactura | INT | NO | FK a Factura |
| IdMetodoPago | INT | NO | FK a MetodoPago |
| FechaPago | DATETIME | NO | |
| Monto | DECIMAL(10,2) | NO | |
| Referencia | VARCHAR(100) | SI | Numero de transaccion |
| Observacion | VARCHAR(250) | SI | |

### dbo.Mantenimiento
Trabajos de mantenimiento en habitaciones.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdMantenimiento | INT IDENTITY | NO | PK |
| IdHabitacion | INT | NO | FK a Habitacion |
| IdEmpleado | INT | SI | FK a Empleado (tecnico asignado) |
| FechaInicio | DATETIME | NO | |
| FechaFin | DATETIME | SI | NULL si aun no finalizo |
| Motivo | VARCHAR(250) | NO | Descripcion del trabajo |
| Costo | DECIMAL(10,2) | NO | Costo del mantenimiento |
| Estado | VARCHAR(20) | NO | PENDIENTE, EN PROCESO, FINALIZADO |

---

## Tablas nuevas (Fase 1)

### dbo.CheckInOut
Registro de check-in y check-out por reserva.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdCheckInOut | INT IDENTITY | NO | PK |
| IdReserva | INT | NO | FK a Reserva (unico) |
| IdEmpleadoCheckIn | INT | SI | IdUsuario del empleado que hizo check-in |
| IdEmpleadoCheckOut | INT | SI | IdUsuario del empleado que hizo check-out |
| FechaCheckIn | DATETIME | SI | Fecha/hora real de llegada |
| FechaCheckOut | DATETIME | SI | Fecha/hora real de salida |
| ObservacionIn | VARCHAR(250) | SI | |
| ObservacionOut | VARCHAR(250) | SI | |
| Estado | VARCHAR(20) | NO | PENDIENTE, CHECKED_IN, CHECKED_OUT |

### dbo.AuditLog
Registro de auditoria de acciones del sistema.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| IdLog | BIGINT IDENTITY | NO | PK |
| IdUsuario | INT | SI | FK a Usuarios (NULL si no hay sesion) |
| Accion | VARCHAR(50) | NO | LOGIN, INSERT, UPDATE, DELETE, CHECK_IN, CHECK_OUT, etc. |
| Entidad | VARCHAR(50) | NO | Reserva, Factura, Mantenimiento, etc. |
| IdEntidad | INT | SI | ID del registro afectado |
| Detalle | NVARCHAR(500) | SI | Informacion adicional |
| FechaHora | DATETIME | NO | Timestamp del evento |
| IpMaquina | VARCHAR(50) | SI | Nombre del equipo (Environment.MachineName) |

### dbo.ConfiguracionSistema
Parametros configurables del sistema.
| Columna | Tipo | Nullable | Descripcion |
|---------|------|----------|-------------|
| Clave | VARCHAR(50) | NO | PK — identificador del parametro |
| Valor | VARCHAR(200) | NO | Valor actual |
| Descripcion | VARCHAR(200) | SI | Descripcion del parametro |
| FechaModificacion | DATETIME | NO | Ultima modificacion |

Parametros iniciales:
- `ITBIS_PORCENTAJE` = 18 — porcentaje de ITBIS en facturas
- `MONEDA` = DOP
- `HOTEL_NOMBRE` = Hotel Reservas
- `CHECK_IN_HORA` = 15:00
- `CHECK_OUT_HORA` = 12:00

---

## Vistas

### dbo.vw_RendimientoEmpleados
Resumen de reservas y facturacion por empleado.
Columnas: IdEmpleado, Empleado, NombreSucursal, TotalReservas, TotalFacturado

---

## Stored Procedures

### dbo.sp_ValidarLogin(@NombreUsuario, @Clave)
Tras Fase2 recibe solo NombreUsuario y recupera la cuenta activa con Clave, IdUsuario, NombreUsuario, NombreCompleto y Rol. La verificación y migración criptográfica se realizan en C#; véase [contraseñas](SEGURIDAD-CONTRASENAS.md).

### dbo.sp_InsertarReserva(...)
Inserta reserva validando disponibilidad y calculando subtotal.

### dbo.sp_GenerarFactura(@IdReserva, @PorcImpuesto)
Genera factura para una reserva. Lee ITBIS del parametro o usa valor por defecto (18).
