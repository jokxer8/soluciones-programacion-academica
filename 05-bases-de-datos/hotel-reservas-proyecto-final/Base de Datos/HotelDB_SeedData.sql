/* =========================================================
   HOTEL DB — SEED DATA
   Script independiente: inserta datos de prueba realistas.
   Ejecutar DESPUÉS de HotelDB_Proyecto_Final.sql
   ========================================================= */

USE HotelDB;
GO

-- =========================================================
-- 1) SUCURSALES ADICIONALES
-- =========================================================
INSERT INTO dbo.Sucursal (NombreSucursal, Ciudad, Direccion, Telefono, Email, Estado)
VALUES
('Hotel Montaña',   'Jarabacoa',    'Calle del Pino #45',           '000-000-0011', 'demo011@example.com', 1),
('Hotel Colonial',  'Santo Domingo','Zona Colonial, Calle Las Damas','000-000-0012', 'demo012@example.com',1),
('Hotel Caribe',    'La Romana',    'Av. del Mar #200',             '000-000-0013', 'demo013@example.com',  1);
GO

-- =========================================================
-- 2) TIPOS DE HABITACIÓN ADICIONALES
-- =========================================================
INSERT INTO dbo.TipoHabitacion (NombreTipo, Capacidad, PrecioBase, Descripcion)
VALUES
('Junior Suite',    2, 6500,  'Suite compacta con jacuzzi'),
('Familiar',        6, 9500,  'Habitación amplia para familias'),
('Penthouse',       4, 15000, 'Planta alta, vista panorámica');
GO

-- =========================================================
-- 3) HABITACIONES ADICIONALES
--    IdSucursal: 1=Central, 2=Playa, 3=Montaña, 4=Colonial, 5=Caribe
--    IdTipoHabitacion: 1=Simple,2=Doble,3=Suite,4=JuniorSuite,5=Familiar,6=Penthouse
--    IdEstadoHabitacion: 1=DISPONIBLE,2=OCUPADA,3=MANTENIMIENTO,4=LIMPIEZA
-- =========================================================
INSERT INTO dbo.Habitacion (IdSucursal, IdTipoHabitacion, IdEstadoHabitacion, Numero, Piso, PrecioPorNoche, Descripcion)
VALUES
-- Sucursal 1 - Hotel Central
(1, 1, 1, '103', 1, 3500, 'Simple piso 1'),
(1, 1, 1, '104', 1, 3500, 'Simple piso 1 - vista jardín'),
(1, 2, 1, '103-D', 1, 5000, 'Doble piso 1 - camas queen'),
(1, 4, 1, '202', 2, 6500, 'Junior Suite piso 2'),
(1, 5, 3, '203', 2, 9500, 'Familiar piso 2 - en mantenimiento'),
(1, 6, 1, '501', 5, 15000,'Penthouse con terraza'),
-- Sucursal 2 - Hotel Playa
(2, 1, 1, '101', 1, 3800, 'Simple vista piscina'),
(2, 2, 2, '102', 1, 5200, 'Doble ocupada'),
(2, 4, 1, '201', 2, 7000, 'Junior Suite vista mar'),
(2, 5, 1, '202', 2, 10000,'Familiar vista mar'),
(2, 6, 1, '601', 6, 16000,'Penthouse premium'),
-- Sucursal 3 - Hotel Montaña
(3, 1, 1, '101', 1, 3200, 'Simple con chimenea'),
(3, 1, 1, '102', 1, 3200, 'Simple vista montaña'),
(3, 2, 1, '201', 2, 4800, 'Doble camas twin'),
(3, 2, 1, '202', 2, 4800, 'Doble camas queen'),
(3, 3, 1, '301', 3, 8000, 'Suite premium'),
(3, 4, 4, '302', 3, 6200, 'Junior Suite - en limpieza'),
-- Sucursal 4 - Hotel Colonial
(4, 1, 1, '101', 1, 3600, 'Simple estilo colonial'),
(4, 2, 1, '102', 1, 5100, 'Doble estilo colonial'),
(4, 3, 1, '201', 2, 8700, 'Suite colonial premium'),
(4, 5, 1, '202', 2, 9800, 'Familiar colonial'),
-- Sucursal 5 - Hotel Caribe
(5, 1, 1, '101', 1, 4000, 'Simple frente al mar'),
(5, 2, 1, '102', 1, 5500, 'Doble frente al mar'),
(5, 3, 1, '201', 2, 9000, 'Suite deluxe'),
(5, 6, 1, '701', 7, 18000,'Penthouse caribeño');
GO

-- =========================================================
-- 4) EMPLEADOS ADICIONALES
-- =========================================================
INSERT INTO dbo.Empleado (IdSucursal, Nombre, Apellido, Cedula, Telefono, Correo, Direccion, FechaNacimiento, FechaIngreso, Cargo, EstadoLaboral, Estado)
VALUES
(1, 'Laura',    'Santos',       'DEMO-000008', '000-000-0014', 'demo014@example.com',   'Los Jardines, DN',      '1995-03-15', '2023-01-10', 'Recepcionista',    'ACTIVO', 1),
(1, 'Miguel',   'Reyes',        'DEMO-000009', '000-000-0015', 'demo015@example.com',    'Villa Consuelo, DN',    '1990-07-22', '2022-06-01', 'Jefe de Piso',     'ACTIVO', 1),
(1, 'Sofia',    'Vargas',       'DEMO-000010', '000-000-0016', 'demo016@example.com',   'Evaristo Morales, DN',  '1993-11-08', '2021-03-15', 'Ama de Llaves',    'ACTIVO', 1),
(2, 'Carlos',   'Medina',       'DEMO-000011', '000-000-0017', 'demo017@example.com',   'Higüey',                '1988-05-30', '2020-08-20', 'Gerente de Hotel', 'ACTIVO', 1),
(2, 'Patricia', 'Cruz',         'DEMO-000012', '000-000-0018', 'demo018@example.com',     'Punta Cana',            '1996-09-14', '2023-02-01', 'Recepcionista',    'ACTIVO', 1),
(2, 'Roberto',  'Herrera',      'DEMO-000013', '000-000-0019', 'demo019@example.com',  'Bávaro',                '1992-12-03', '2022-11-15', 'Conserje',         'ACTIVO', 1),
(3, 'Valeria',  'Torres',       'DEMO-000014', '000-000-0020', 'demo020@example.com',   'Constanza',             '1997-04-27', '2024-01-05', 'Recepcionista',    'ACTIVO', 1),
(3, 'Andres',   'Montero',      'DEMO-000015', '000-000-0021', 'demo021@example.com',  'Jarabacoa',             '1985-08-16', '2019-07-01', 'Mantenimiento',    'ACTIVO', 1),
(4, 'Carmen',   'Jimenez',      'DEMO-000016', '000-000-0022', 'demo022@example.com',  'Ciudad Colonial, DN',   '1991-02-19', '2021-09-10', 'Recepcionista',    'ACTIVO', 1),
(4, 'Pedro',    'Almonte',      'DEMO-000017', '000-000-0023', 'demo023@example.com',  'Gazcue, DN',            '1987-06-11', '2020-04-15', 'Chef',             'VACACIONES', 1),
(5, 'Diana',    'Paulino',      'DEMO-000018', '000-000-0024', 'demo024@example.com',  'La Romana',             '1994-10-25', '2022-03-20', 'Recepcionista',    'ACTIVO', 1),
(5, 'Hector',   'Espinal',      'DEMO-000019', '000-000-0025', 'demo025@example.com',  'Casa de Campo, LR',     '1989-01-07', '2018-12-01', 'Gerente de Hotel', 'ACTIVO', 1),
(1, 'Natalia',  'Castillo',     'DEMO-000020', '000-000-0026', 'demo026@example.com', 'Los Prados, DN',        '1998-07-30', '2024-06-01', 'Cajera',           'ACTIVO', 1),
(2, 'Juan',     'Taveras',      'DEMO-000021', '000-000-0027', 'demo027@example.com',  'El Cortecito',          '1993-03-22', '2023-08-15', 'Botones',          'ACTIVO', 1),
(3, 'Isabel',   'Marte',        'DEMO-000022', '000-000-0028', 'demo028@example.com',    'Las Lagunas, JBQ',      '2000-05-18', '2025-01-10', 'Camarera',         'ACTIVO', 1);
GO

-- =========================================================
-- 5) USUARIOS PARA LOS NUEVOS EMPLEADOS (RECEPCIÓN)
-- =========================================================
-- IdRol: 1=ADMIN, 2=EMPLEADO
-- IdEmpleado depende del IDENTITY; asumimos que quedaron 5-19
-- Para ser seguros usamos subconsultas
INSERT INTO dbo.Usuarios (IdEmpleado, NombreUsuario, Clave, NombreCompleto, IdRol, Estado)
SELECT e.IdEmpleado,
       LOWER(LEFT(e.Nombre,1) + e.Apellido),
       LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '1234'), 2)),
       e.Nombre + ' ' + e.Apellido,
       2,
       1
FROM dbo.Empleado e
WHERE NOT EXISTS (SELECT 1 FROM dbo.Usuarios u WHERE u.IdEmpleado = e.IdEmpleado);
GO

-- =========================================================
-- 6) CLIENTES ADICIONALES (30 clientes)
-- =========================================================
INSERT INTO dbo.Cliente (Nombres, Apellidos, TipoDocumento, Documento, Telefono, Correo, Direccion)
VALUES
('Juan Carlos',     'Ramirez',      'CEDULA',   'DEMO-000023', '000-000-0029', 'demo029@example.com',   'Santiago de los Caballeros'),
('Maria Elena',     'Ureña',        'CEDULA',   'DEMO-000024', '000-000-0030', 'demo030@example.com',     'San Pedro de Macorís'),
('Pedro Antonio',   'Abreu',        'CEDULA',   'DEMO-000025', '000-000-0031', 'demo031@example.com',     'La Romana'),
('Rosa Estela',     'Guerrero',     'CEDULA',   'DEMO-000026', '000-000-0032', 'demo032@example.com',   'Puerto Plata'),
('Roberto Carlos',  'Dipre',        'CEDULA',   'DEMO-000027', '000-000-0033', 'demo033@example.com',      'Boca Chica'),
('Alejandra',       'Tejada',       'CEDULA',   'DEMO-000028', '000-000-0034', 'demo034@example.com',     'Azua'),
('Fernando',        'Beltré',       'CEDULA',   'DEMO-000029', '000-000-0035', 'demo035@example.com',     'Moca'),
('Gloria',          'Nin',          'CEDULA',   'DEMO-000030', '000-000-0036', 'demo036@example.com',        'Bonao'),
('Ramón',           'Pimentel',     'CEDULA',   'DEMO-000031', '000-000-0037', 'demo037@example.com',   'Salcedo'),
('Claudia',         'Vargas',       'CEDULA',   'DEMO-000032', '000-000-0038', 'demo038@example.com',    'Santo Domingo Este'),
-- Turistas extranjeros
('James',           'Wilson',       'PASAPORTE','US-22001001',   '000-000-0039','demo039@example.com',    'Miami, FL, USA'),
('Emily',           'Johnson',      'PASAPORTE','US-22001002',   '000-000-0040','demo040@example.com',   'New York, NY, USA'),
('Pierre',          'Dupont',       'PASAPORTE','FR-33001001',   '000-000-0041','demo041@example.com',    'Paris, France'),
('Helga',           'Müller',       'PASAPORTE','DE-44001001',   '000-000-0042', 'demo042@example.com',    'Berlin, Germany'),
('Carlos',          'Rodríguez',    'PASAPORTE','ES-55001001',   '000-000-0043', 'demo043@example.com', 'Madrid, Spain'),
('Valentina',       'Rossi',        'PASAPORTE','IT-66001001',   '000-000-0044', 'demo044@example.com',     'Rome, Italy'),
('Thomas',          'Brown',        'PASAPORTE','UK-77001001',   '000-000-0045', 'demo045@example.com',     'London, UK'),
('Yuki',            'Tanaka',       'PASAPORTE','JP-88001001',   '000-000-0046','demo046@example.com',    'Tokyo, Japan'),
('Lucas',           'Oliveira',     'PASAPORTE','BR-99001001',   '000-000-0047', 'demo047@example.com',  'São Paulo, Brazil'),
('Sofia',           'García',       'PASAPORTE','MX-11001001',   '000-000-0048', 'demo048@example.com',    'Mexico City, Mexico'),
-- Nacionales adicionales
('Josefa',          'Santana',      'CEDULA',   'DEMO-000033', '000-000-0049', 'demo049@example.com',    'San Francisco de Macorís'),
('Freddy',          'Matos',        'CEDULA',   'DEMO-000034', '000-000-0050', 'demo050@example.com',      'Cotuí'),
('Yolanda',         'Arias',        'CEDULA',   'DEMO-000035', '000-000-0051', 'demo051@example.com',      'Higüey'),
('Emmanuel',        'Bello',        'CEDULA',   'DEMO-000036', '000-000-0052', 'demo052@example.com',      'Barahona'),
('Martha',          'Caba',         'CEDULA',   'DEMO-000037', '000-000-0053', 'demo053@example.com',       'Nagua'),
('Oscar',           'Disla',        'CEDULA',   'DEMO-000038', '000-000-0054', 'demo054@example.com',      'Mao, Valverde'),
('Brenda',          'Encarnación',  'CEDULA',   'DEMO-000039', '000-000-0055', 'demo055@example.com','Monte Cristi'),
('Esteban',         'Feliz',        'CEDULA',   'DEMO-000040', '000-000-0056', 'demo056@example.com',      'Dajabón'),
('Amelia',          'German',       'CEDULA',   'DEMO-000041', '000-000-0057', 'demo057@example.com',     'Neiba'),
('Cristian',        'Hidalgo',      'CEDULA',   'DEMO-000042', '000-000-0058', 'demo058@example.com',    'Constanza');
GO

-- =========================================================
-- 7) SERVICIOS ADICIONALES
-- =========================================================
INSERT INTO dbo.ServicioAdicional (NombreServicio, Precio, Estado)
VALUES
('Almuerzo',            800,  1),
('Cena',               1000,  1),
('Piscina VIP',         500,  1),
('Tour Ciudad',        2000,  1),
('Alquiler Bicicleta',  400,  1),
('Masaje Relajante',   1800,  1),
('Traslado Aeropuerto',1200,  1),
('Minibar Recargado',   950,  1),
('Baby Shower Kit',    1500,  1);
GO

-- =========================================================
-- 8) PROMOCIONES ADICIONALES
-- =========================================================
INSERT INTO dbo.Promocion (NombrePromocion, TipoDescuento, ValorDescuento, FechaInicio, FechaFin, Estado)
VALUES
('Luna de Miel 15%',      'P', 15,   '2025-01-01', '2030-12-31', 1),
('Semana Santa',          'M', 2000, '2025-04-10', '2030-04-20', 1),
('Navidad 20%',           'P', 20,   '2025-12-15', '2030-12-31', 1),
('Cliente Frecuente 5%',  'P', 5,    '2025-01-01', '2030-12-31', 1),
('Estadía Larga -$1500',  'M', 1500, '2025-01-01', '2030-12-31', 1),
('Fin de Semana',         'P', 8,    '2025-01-01', '2030-12-31', 1);
GO

-- =========================================================
-- 9) RESERVAS (pasadas, activas, futuras)
--    Se usan IDs directos para controlar los datos.
--    Asumimos los IDs de cliente y habitación en el orden
--    en que fueron insertados. Ajusta si tu IDENTITY difiere.
--
--    Empleados operativos (los que gestionan reservas):
--      Kelvin=1, Maria=2, Jose=3, Gabriel=4,
--      Laura=5, Miguel=6, Patricia=9, Carmen=13, Diana=15
--
--    Habitaciones originales: 1-6 (Sucursales 1 y 2)
--    Habitaciones nuevas comienzan en IdHabitacion=7+
--
--    Clientes originales: 1-4
--    Nuevos clientes: 5-34
--
--    EstadoReserva: 1=PENDIENTE,2=CONFIRMADA,3=CANCELADA,4=FINALIZADA
-- =========================================================

-- Las referencias del seed se desplazan respecto a los datos de la base inicial.
CREATE TABLE #SeedOffsets (ReservaOffset int, FacturaOffset int);
INSERT #SeedOffsets VALUES (CONVERT(int, IDENT_CURRENT('dbo.Reserva')), CONVERT(int, IDENT_CURRENT('dbo.Factura')));
GO

-- ---- Reservas FINALIZADAS (historial, hace varios meses) -----
INSERT INTO dbo.Reserva (IdCliente, IdHabitacion, IdEmpleado, IdEstadoReserva, FechaReserva, FechaEntrada, FechaSalida, CantidadHuespedes, Observacion, Subtotal, Descuento, Total)
VALUES
-- Enero 2026
(1,  1,  2, 4, '2026-01-03', '2026-01-05', '2026-01-08', 1, NULL,              10500.00,  0,     10500.00),
(2,  2,  2, 4, '2026-01-10', '2026-01-12', '2026-01-15', 2, NULL,              15000.00,  0,     15000.00),
(11, 5,  3, 4, '2026-01-08', '2026-01-14', '2026-01-18', 2, 'Vista al mar',    20800.00,  1000,  19800.00),
(12, 6,  3, 4, '2026-01-15', '2026-01-20', '2026-01-26', 2, NULL,              52800.00,  0,     52800.00),
(13, 3,  2, 4, '2026-01-18', '2026-01-22', '2026-01-25', 3, 'Suite familiar',  25500.00,  2000,  23500.00),
-- Febrero 2026
(3,  4,  5, 4, '2026-02-01', '2026-02-03', '2026-02-06', 1, NULL,              11400.00,  0,     11400.00),
(14, 7,  5, 4, '2026-02-05', '2026-02-08', '2026-02-12', 1, NULL,              15200.00,  0,     15200.00),
(15, 8,  9, 4, '2026-02-10', '2026-02-14', '2026-02-18', 2, NULL,              20800.00,  0,     20800.00),
(16, 9,  9, 4, '2026-02-12', '2026-02-15', '2026-02-20', 2, 'Junior Suite',    35000.00,  1500,  33500.00),
(17, 10, 3, 4, '2026-02-20', '2026-02-22', '2026-02-26', 4, NULL,              40000.00,  0,     40000.00),
-- Marzo 2026
(4,  2,  2, 4, '2026-03-01', '2026-03-03', '2026-03-06', 2, NULL,              15000.00,  0,     15000.00),
(18, 3,  2, 4, '2026-03-04', '2026-03-06', '2026-03-09', 3, 'Aniversario',     25500.00,  1000,  24500.00),
(19, 12, 5, 4, '2026-03-05', '2026-03-08', '2026-03-11', 1, NULL,               9600.00,  0,      9600.00),
(20, 13, 5, 4, '2026-03-07', '2026-03-10', '2026-03-13', 2, NULL,              14400.00,  0,     14400.00),
(21, 16, 3, 4, '2026-03-09', '2026-03-12', '2026-03-16', 2, NULL,              19200.00,  0,     19200.00),
(22, 17, 9, 4, '2026-03-15', '2026-03-17', '2026-03-21', 1, NULL,              14400.00,  0,     14400.00),
(23, 18, 9, 4, '2026-03-18', '2026-03-20', '2026-03-24', 3, NULL,              34800.00,  2000,  32800.00),
(24, 21, 3, 4, '2026-03-20', '2026-03-23', '2026-03-27', 1, NULL,              14400.00,  0,     14400.00),
(25, 22, 3, 4, '2026-03-22', '2026-03-25', '2026-03-29', 2, NULL,              20400.00,  0,     20400.00),
-- Semana Santa
(26, 6,  3, 4, '2026-03-25', '2026-03-28', '2026-04-02', 2, 'Semana Santa',    44000.00,  2000,  42000.00),
(27, 11, 5, 4, '2026-03-26', '2026-03-29', '2026-04-03', 4, NULL,              57000.00,  0,     57000.00),
(28, 5,  9, 4, '2026-03-27', '2026-03-30', '2026-04-04', 2, NULL,              36400.00,  0,     36400.00);
GO

-- ---- Reservas CONFIRMADAS (actualmente en el hotel) -----
INSERT INTO dbo.Reserva (IdCliente, IdHabitacion, IdEmpleado, IdEstadoReserva, FechaReserva, FechaEntrada, FechaSalida, CantidadHuespedes, Observacion, Subtotal, Descuento, Total)
VALUES
(29, 2,  2, 2, '2026-04-05', '2026-04-07', '2026-04-12', 2, NULL,              25000.00, 0,     25000.00),
(30, 8,  9, 2, '2026-04-06', '2026-04-08', '2026-04-13', 2, NULL,              26000.00, 1000,  25000.00),
(31, 3,  2, 2, '2026-04-07', '2026-04-09', '2026-04-14', 3, 'Viaje de negocios',42500.00,0,     42500.00),
(32, 9,  9, 2, '2026-04-07', '2026-04-09', '2026-04-15', 2, NULL,              42000.00, 0,     42000.00),
(33, 12, 5, 2, '2026-04-08', '2026-04-09', '2026-04-11', 1, NULL,               6400.00, 0,      6400.00),
(34, 15, 5, 2, '2026-04-08', '2026-04-09', '2026-04-14', 2, NULL,              24000.00, 0,     24000.00),
(1,  20, 13,2, '2026-04-08', '2026-04-09', '2026-04-16', 2, NULL,              40800.00, 0,     40800.00),
(11, 23, 13,2, '2026-04-08', '2026-04-09', '2026-04-13', 2, NULL,              27500.00, 0,     27500.00);
GO

-- ---- Reservas PENDIENTES (próximas) -----
INSERT INTO dbo.Reserva (IdCliente, IdHabitacion, IdEmpleado, IdEstadoReserva, FechaReserva, FechaEntrada, FechaSalida, CantidadHuespedes, Observacion, Subtotal, Descuento, Total)
VALUES
(12, 4,  3, 1, '2026-04-09', '2026-04-15', '2026-04-18', 1, NULL,              11400.00, 0,     11400.00),
(13, 6,  3, 1, '2026-04-09', '2026-04-16', '2026-04-21', 2, NULL,              44000.00, 0,     44000.00),
(14, 14, 5, 1, '2026-04-09', '2026-04-18', '2026-04-22', 2, NULL,              19200.00, 0,     19200.00),
(16, 19, 9, 1, '2026-04-09', '2026-04-20', '2026-04-25', 3, 'Cumpleaños',      43500.00, 2000,  41500.00),
(18, 24, 15,1, '2026-04-09', '2026-04-22', '2026-04-26', 2, NULL,              22000.00, 0,     22000.00),
(20, 1,  2, 1, '2026-04-09', '2026-04-25', '2026-04-30', 1, NULL,              17500.00, 0,     17500.00);
GO

-- ---- Reservas CANCELADAS -----
INSERT INTO dbo.Reserva (IdCliente, IdHabitacion, IdEmpleado, IdEstadoReserva, FechaReserva, FechaEntrada, FechaSalida, CantidadHuespedes, Observacion, Subtotal, Descuento, Total)
VALUES
(2,  3, 2, 3, '2026-02-15', '2026-02-20', '2026-02-25', 2, 'Cancelación por cliente', 42500.00, 0, 42500.00),
(5,  5, 3, 3, '2026-03-01', '2026-03-10', '2026-03-14', 2, 'No show',                 20800.00, 0, 20800.00),
(15, 2, 5, 3, '2026-03-15', '2026-03-20', '2026-03-23', 1, 'Cancelación voluntaria',  15000.00, 0, 15000.00);
GO

-- =========================================================
-- 10) SERVICIOS ADICIONALES EN RESERVAS
--     IdServicioAdicional: 1=Desayuno,2=Transporte,3=Lavandería,
--       4=Spa,5=Almuerzo,6=Cena,7=Piscina VIP,8=Tour Ciudad,
--       9=Bicicleta,10=Masaje,11=Traslado Aero,12=Minibar
-- =========================================================
DECLARE @ReservaOffset int = (SELECT ReservaOffset FROM #SeedOffsets);
INSERT INTO dbo.ReservaServicio (IdReserva, IdServicioAdicional, Cantidad, PrecioUnitario, Subtotal)
VALUES
-- Reservas finalizadas
(@ReservaOffset+1,  1, 3, 600,  1800),  -- Desayunos
(@ReservaOffset+2,  1, 6, 600,  3600),  -- Desayunos
(@ReservaOffset+2,  3, 1, 800,   800),  -- Lavandería
(@ReservaOffset+3,  4, 2, 2500, 5000),  -- Spa
(@ReservaOffset+3,  1, 4, 600,  2400),  -- Desayunos
(@ReservaOffset+4,  4, 1, 2500, 2500),  -- Spa
(@ReservaOffset+4,  6, 6, 1000, 6000),  -- Cenas
(@ReservaOffset+5,  1, 3, 600,  1800),  -- Desayunos
(@ReservaOffset+5,  8, 2, 2000, 4000),  -- Tours
(@ReservaOffset+6,  2, 1, 1500, 1500),  -- Transporte
(@ReservaOffset+7,  1, 4, 600,  2400),  -- Desayunos
(@ReservaOffset+8, 11, 2, 1200, 2400),  -- Traslado aeropuerto
(@ReservaOffset+9,  4, 2, 2500, 5000),  -- Spa
(@ReservaOffset+10, 5, 8, 800,  6400),  -- Almuerzos
(@ReservaOffset+10, 6, 8, 1000, 8000),  -- Cenas
(@ReservaOffset+11, 1, 3, 600,  1800),  -- Desayunos
(@ReservaOffset+12, 8, 1, 2000, 2000),  -- Tour
(@ReservaOffset+14, 1, 6, 600,  3600),  -- Desayunos
(@ReservaOffset+15, 3, 2, 800,  1600),  -- Lavandería
(@ReservaOffset+18, 7, 4, 500,  2000),  -- Piscina VIP
(@ReservaOffset+21,10, 2, 1800, 3600),  -- Masajes
(@ReservaOffset+22, 1, 5, 600,  3000),  -- Desayunos
-- Reservas activas (confirmadas)
(@ReservaOffset+23, 1, 5, 600,  3000),  -- Desayunos
(@ReservaOffset+24,11, 2, 1200, 2400),  -- Traslado aeropuerto
(@ReservaOffset+25, 4, 1, 2500, 2500),  -- Spa
(@ReservaOffset+26, 1, 6, 600,  3600),  -- Desayunos
(@ReservaOffset+26, 6, 6, 1000, 6000),  -- Cenas
(@ReservaOffset+28, 1, 5, 600,  3000),  -- Desayunos
(@ReservaOffset+28, 4, 2, 2500, 5000),  -- Spa
(@ReservaOffset+30,12, 1, 950,   950);  -- Minibar
GO

-- =========================================================
-- 11) FACTURAS Y PAGOS (solo para reservas FINALIZADAS)
-- =========================================================
DECLARE @ReservaOffset int = (SELECT ReservaOffset FROM #SeedOffsets);
INSERT INTO dbo.Factura (IdReserva, FechaFactura, Subtotal, Descuento, Impuesto, Total, Estado)
VALUES
(@ReservaOffset+1,  '2026-01-08', 12300.00, 0,     1476.00, 13776.00, 'PAGADA'),
(@ReservaOffset+2,  '2026-01-15', 19400.00, 0,     2328.00, 21728.00, 'PAGADA'),
(@ReservaOffset+3,  '2026-01-18', 26200.00, 1000,  3024.00, 28224.00, 'PAGADA'),
(@ReservaOffset+4,  '2026-01-26', 61300.00, 0,     7356.00, 68656.00, 'PAGADA'),
(@ReservaOffset+5,  '2026-01-25', 31300.00, 2000,  3516.00, 32816.00, 'PAGADA'),
(@ReservaOffset+6,  '2026-02-06', 12900.00, 0,     1548.00, 14448.00, 'PAGADA'),
(@ReservaOffset+7,  '2026-02-12', 17600.00, 0,     2112.00, 19712.00, 'PAGADA'),
(@ReservaOffset+8,  '2026-02-18', 23200.00, 0,     2784.00, 25984.00, 'PAGADA'),
(@ReservaOffset+9,  '2026-02-20', 38500.00, 1500,  4440.00, 41440.00, 'PAGADA'),
(@ReservaOffset+10, '2026-02-26', 54400.00, 0,     6528.00, 60928.00, 'PAGADA'),
(@ReservaOffset+11, '2026-03-06', 16800.00, 0,     2016.00, 18816.00, 'PAGADA'),
(@ReservaOffset+12, '2026-03-09', 26500.00, 1000,  3060.00, 28560.00, 'PAGADA'),
(@ReservaOffset+13, '2026-03-11',  9600.00, 0,     1152.00, 10752.00, 'PAGADA'),
(@ReservaOffset+14, '2026-03-13', 18000.00, 0,     2160.00, 20160.00, 'PAGADA'),
(@ReservaOffset+15, '2026-03-16', 20800.00, 0,     2496.00, 23296.00, 'PAGADA'),
(@ReservaOffset+16, '2026-03-21', 14400.00, 0,     1728.00, 16128.00, 'PAGADA'),
(@ReservaOffset+17, '2026-03-24', 36800.00, 2000,  4176.00, 38976.00, 'PAGADA'),
(@ReservaOffset+18, '2026-03-27', 16400.00, 0,     1968.00, 18368.00, 'PAGADA'),
(@ReservaOffset+19, '2026-03-29', 20400.00, 0,     2448.00, 22848.00, 'PAGADA'),
(@ReservaOffset+20, '2026-04-02', 46000.00, 2000,  5280.00, 49280.00, 'PAGADA'),
(@ReservaOffset+21, '2026-04-03', 60600.00, 0,     7272.00, 67872.00, 'PAGADA'),
(@ReservaOffset+22, '2026-04-04', 39400.00, 0,     4728.00, 44128.00, 'PAGADA');
GO

-- Pagos en efectivo y tarjeta
DECLARE @FacturaOffset int = (SELECT FacturaOffset FROM #SeedOffsets);
INSERT INTO dbo.Pago (IdFactura, IdMetodoPago, FechaPago, Monto, Referencia, Observacion)
VALUES
(@FacturaOffset+1,  1, '2026-01-08', 13776.00, NULL,          'Pago completo efectivo'),
(@FacturaOffset+2,  2, '2026-01-15', 21728.00, 'TXN-20260115', NULL),
(@FacturaOffset+3,  2, '2026-01-18', 28224.00, 'TXN-20260118', NULL),
(@FacturaOffset+4,  3, '2026-01-26', 68656.00, 'TRF-20260126', 'Transferencia bancaria'),
(@FacturaOffset+5,  2, '2026-01-25', 32816.00, 'TXN-20260125', NULL),
(@FacturaOffset+6,  1, '2026-02-06', 14448.00, NULL,           NULL),
(@FacturaOffset+7,  2, '2026-02-12', 19712.00, 'TXN-20260212', NULL),
(@FacturaOffset+8,  1, '2026-02-18', 13000.00, NULL,           'Abono efectivo'),
(@FacturaOffset+8,  2, '2026-02-18', 12984.00, 'TXN-20260218', 'Saldo tarjeta'),
(@FacturaOffset+9,  2, '2026-02-20', 41440.00, 'TXN-20260220', NULL),
(@FacturaOffset+10, 3, '2026-02-26', 60928.00, 'TRF-20260226', NULL),
(@FacturaOffset+11, 1, '2026-03-06', 18816.00, NULL,           NULL),
(@FacturaOffset+12, 2, '2026-03-09', 28560.00, 'TXN-20260309', NULL),
(@FacturaOffset+13, 1, '2026-03-11', 10752.00, NULL,           NULL),
(@FacturaOffset+14, 2, '2026-03-13', 20160.00, 'TXN-20260313', NULL),
(@FacturaOffset+15, 2, '2026-03-16', 23296.00, 'TXN-20260316', NULL),
(@FacturaOffset+16, 1, '2026-03-21', 16128.00, NULL,           NULL),
(@FacturaOffset+17, 2, '2026-03-24', 38976.00, 'TXN-20260324', NULL),
(@FacturaOffset+18, 3, '2026-03-27', 18368.00, 'TRF-20260327', NULL),
(@FacturaOffset+19, 2, '2026-03-29', 22848.00, 'TXN-20260329', NULL),
(@FacturaOffset+20, 2, '2026-04-02', 49280.00, 'TXN-20260402', 'Semana Santa'),
(@FacturaOffset+21, 3, '2026-04-03', 67872.00, 'TRF-20260403', NULL),
(@FacturaOffset+22, 2, '2026-04-04', 44128.00, 'TXN-20260404', NULL);
GO

-- =========================================================
-- 12) MANTENIMIENTOS
-- =========================================================
INSERT INTO dbo.Mantenimiento (IdHabitacion, IdEmpleado, FechaInicio, FechaFin, Motivo, Costo, Estado)
VALUES
(5,  8, '2026-04-01', NULL,           'Revisión de plomería y griferías',  4500.00, 'EN PROCESO'),
(10, 8, '2026-03-20', '2026-03-25',   'Cambio de aire acondicionado',      12000.00,'FINALIZADO'),
(16, 8, '2026-04-05', NULL,           'Pintura y mantenimiento general',   3000.00, 'PENDIENTE'),
(1,  8, '2026-02-10', '2026-02-12',   'Reparación de baño',                2500.00, 'FINALIZADO'),
(3,  8, '2026-01-15', '2026-01-16',   'Cambio de iluminación LED',         1800.00, 'FINALIZADO'),
(20, 8, '2026-03-28', '2026-04-01',   'Remodelación parcial',              15000.00,'FINALIZADO'),
(6,  8, '2026-04-08', NULL,           'Inspección preventiva pre-temporada',500.00, 'PENDIENTE');
GO

-- =========================================================
-- 13) PROMOCIONES EN RESERVAS (algunas reservas con descuento)
-- =========================================================
DECLARE @ReservaOffset int = (SELECT ReservaOffset FROM #SeedOffsets);
INSERT INTO dbo.ReservaPromocion (IdReserva, IdPromocion, DescuentoAplicado)
VALUES
(@ReservaOffset+3,  1, 1000.00),  -- Promo Corporativa
(@ReservaOffset+5,  2, 2000.00),  -- Semana Santa (prueba histórica)
(@ReservaOffset+9,  4, 1500.00),  -- Cliente Frecuente
(@ReservaOffset+10, 1, 0.00),     -- Corporativa sin descuento adicional
(@ReservaOffset+12, 1, 1000.00),  -- Corporativa
(@ReservaOffset+17, 3, 2000.00),  -- Navidad
(@ReservaOffset+20, 2, 2000.00),  -- Semana Santa
(@ReservaOffset+24, 4, 1000.00),  -- Cliente Frecuente
(@ReservaOffset+32, 6, 2000.00);  -- Cumpleaños - Fin de Semana
GO

PRINT 'Seed data insertado correctamente.';
GO
