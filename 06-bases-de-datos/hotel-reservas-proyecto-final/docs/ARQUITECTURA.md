# Arquitectura del Sistema HotelReservas

## Stack
- Lenguaje: C# .NET Framework 4.8
- UI: Windows Forms
- Base de datos: SQL Server (cualquier edicion, incluyendo Express)
- Acceso a datos: System.Data.SqlClient (ADO.NET directo)

## Capas

```
UI (Forms)
    |
    v
Data Classes (*Data.cs)
    |
    v
Database.cs  <-- unico punto de conexion (GetConnection)
    |
    v
SQL Server (HotelDB)
```

### UI Layer — `*Form.cs`
Cada formulario hereda de `Form`, se carga en `MainForm.panelContenido`.
- Constructor: inicializa controles, suscribe a `Theme.OnThemeChanged`, llama `Theme.ApplyToForm(this)`
- `*_Load`: carga combos y datos iniciales
- Los formularios llaman principalmente a `Database` y las clases `*Data.cs`; `FacturasForm.cs` tambien contiene acceso SQL directo.

### Data Layer — `*Data.cs`
Clases estaticas con metodos por operacion CRUD. Patron:
```
using (SqlConnection conn = Database.GetConnection())
{
    conn.Open();
    using (SqlCommand cmd = new SqlCommand(sql, conn)) { ... }
}
```
Los valores de las operaciones se pasan mediante `@parametros`. Algunas consultas construyen dinamicamente nombres de columnas a partir de la inspeccion del esquema; el codigo debe revisarse al ampliar esos casos.

### Database.cs
- `GetConnection()`: retorna `SqlConnection` con la cadena de conexion
- `HashPassword(string)`: delega en PasswordSecurity; PBKDF2-SHA256 con sal aleatoria y coste versionado
- `RegistrarAccion(...)`: INSERT en `dbo.AuditLog` (silencia errores para no interrumpir)
- `ObtenerConfiguracion(clave, default)`: lee `dbo.ConfiguracionSistema`
- Metodos de datos maestros: clientes, habitaciones, sucursales, etc.

## Flujo de Autenticacion
1. `LoginForm` captura usuario/clave
2. `Database.ValidarLogin(usuario, clave)` — recupera con `sp_ValidarLogin` la credencial de la cuenta activa, sin enviar un hash como contraseña
3. `PasswordSecurity.Verify` contrasta PBKDF2 o SHA256 heredado; si procede, migra atómicamente y relee rol/estado. [Detalle de seguridad](SEGURIDAD-CONTRASENAS.md)
4. Si valido: popula `SesionUsuario` (singleton estatico) con `IdUsuario`, `Rol`, `NombreCompleto`
5. Abre `MainForm`, que llama `ConfigurarSegunRol()` para mostrar/ocultar botones del sidebar

## Sistema de Roles
| Rol | Descripcion | Modulos |
|-----|-------------|---------|
| ADMIN | Administrador completo | Todos |
| RECEPCION | Recepcionista | Clientes, Habitaciones, Reservas, Facturas |
| EMPLEADO | Usuario operativo | Clientes, Habitaciones, Reservas, Facturas |
| SOPORTE | Soporte tecnico | Administracion, Mantenimiento |

## Sistema de Theming
- `Assets/Theme.cs` — clase estatica con `AppTheme.Light` / `AppTheme.Dark`
- Evento `Theme.OnThemeChanged` — cada form suscribe en constructor
- Helpers: `Theme.ApplyToForm(form)`, `Theme.ApplyToGrid(dgv)`, `Theme.ApplyButton(btn)`, etc.
- Nunca usar `Color.FromArgb` directo en forms — usar las propiedades de `Theme`

## Mapa de Forms
- `LoginForm` — entry point, valida credenciales
- `MainForm` — shell con sidebar y `panelContenido`
- `ClientesForm` — CRUD clientes
- `HabitacionesForm` — CRUD habitaciones
- `ReservasForm` — CRUD reservas + servicios + promociones + check-in/out
- `FacturasForm` — gestion de facturas y pagos
- `ReportesForm` — reportes operativos, habitaciones, gerenciales, global anual, metricas
- `AdministracionForm` — gestion de empleados y usuarios
- `MantenimientoForm` — CRUD mantenimientos (ADMIN/SOPORTE)
- `DashboardForm` — KPIs en tiempo real, cargado al login
- `ConfiguracionForm` — parametros del sistema (solo ADMIN)
- `SucursalesForm`, `TiposHabitacionForm`, `ServiciosAdicionalesForm`, `PromocionesForm` — catalogos

## Tabla AuditLog
Registra acciones criticas: LOGIN, INSERT/UPDATE/DELETE en Reserva, Factura, CheckInOut, etc.
El metodo `Database.RegistrarAccion` captura cualquier excepcion internamente para no interrumpir el flujo de negocio.

## Check-in / Check-out
- Tabla `dbo.CheckInOut` — registro separado de `dbo.Reserva`
- Check-in: reserva debe estar en estado CONFIRMADA
- Check-out: registra automaticamente la factura via `sp_GenerarFactura` si no existe
- ITBIS leido de `dbo.ConfiguracionSistema` (clave: ITBIS_PORCENTAJE)
