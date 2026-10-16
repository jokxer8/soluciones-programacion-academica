/* ============================================================
   02_carga_datos_reservas.sql
   Base de Datos: Reservas de Coches
   Motor: SQL Server (T-SQL)
   Contenido:
   - Inserts de prueba cumpliendo el mandato:
     5 coches, 2 agencias, 3 garajes, 10 clientes, 20 reservas
   ============================================================ */

-- Recomendación: ejecutar primero 01_creacion_bd_reservas.sql

/* =========================
   AGENCIAS (2)
   ========================= */
INSERT INTO dbo.Agencia (codigo, nombre) VALUES
(1, 'Agencia Centro'),
(2, 'Agencia Norte');

/* =========================
   GARAJES (3)
   ========================= */
INSERT INTO dbo.Garaje (codigo, nombre) VALUES
(10, 'Garaje Central'),
(20, 'Garaje Aeropuerto'),
(30, 'Garaje Zona Norte');

/* =========================
   CLIENTES (10)
   Nota: codigo_avalador es nullable (0..1)
   ========================= */
INSERT INTO dbo.Cliente (codigo, DNI, nombre, telefono, direccion, codigo_avalador) VALUES
(100, '001-0000001-1', 'Ana Pérez',       '809-555-0100', 'Santo Domingo', NULL),
(101, '001-0000002-2', 'Carlos Gómez',    '809-555-0101', 'Santiago',      100),
(102, '001-0000003-3', 'María López',     '809-555-0102', 'La Vega',       100),
(103, '001-0000004-4', 'Juan Rodríguez',  '809-555-0103', 'San Cristóbal', NULL),
(104, '001-0000005-5', 'Luisa Martínez',  '809-555-0104', 'Bonao',         103),
(105, '001-0000006-6', 'Pedro Sánchez',   '809-555-0105', 'Higüey',        NULL),
(106, '001-0000007-7', 'Sofía Castillo',  '809-555-0106', 'Baní',          105),
(107, '001-0000008-8', 'Diego Herrera',   '809-555-0107', 'Azua',          NULL),
(108, '001-0000009-9', 'Paola Díaz',      '809-555-0108', 'Puerto Plata',  107),
(109, '001-0000010-0', 'Ricardo Núñez',   '809-555-0109', 'San Pedro',     NULL);

/* =========================
   COCHES (5)
   ========================= */
INSERT INTO dbo.Coche (matricula, modelo, marca, color, codigo_garaje) VALUES
('A001', 'Corolla',  'Toyota',  'Blanco', 10),
('A002', 'Civic',    'Honda',   'Negro',  10),
('B100', 'Rio',      'Kia',     'Gris',   20),
('C777', 'CX-5',     'Mazda',   'Rojo',   30),
('D505', 'Sentra',   'Nissan',  'Azul',   20);

/* =========================
   RESERVAS (20)
   - Cada reserva referencia: 1 cliente, 1 agencia, 1 coche
   ========================= */
INSERT INTO dbo.Reserva
(codigo, fecha_inicio, fecha_fin, litros, indicador, precio_alquiler, codigo_cliente, codigo_agencia, matricula_coche)
VALUES
(2000, '2026-01-02', '2026-01-03', 15.50, 'Activa',    2500.00, 100, 1, 'A001'),
(2001, '2026-01-04', '2026-01-04',  0.00, 'Cerrada',   2200.00, 101, 1, 'A002'),
(2002, '2026-01-05', '2026-01-07', 30.00, 'Activa',    1800.00, 102, 2, 'B100'),
(2003, '2026-01-08', '2026-01-10', 25.00, 'Activa',    3200.00, 103, 2, 'C777'),
(2004, '2026-01-11', '2026-01-11',  5.25, 'Cerrada',   2000.00, 104, 1, 'D505'),

(2005, '2026-01-12', '2026-01-13', 10.00, 'Activa',    2500.00, 105, 2, 'A001'),
(2006, '2026-01-14', '2026-01-16', 22.75, 'Activa',    2200.00, 106, 1, 'A002'),
(2007, '2026-01-17', '2026-01-17',  0.00, 'Cerrada',   1800.00, 107, 2, 'B100'),
(2008, '2026-01-18', '2026-01-19', 12.00, 'Activa',    3200.00, 108, 1, 'C777'),
(2009, '2026-01-20', '2026-01-22', 28.40, 'Activa',    2000.00, 109, 2, 'D505'),

(2010, '2026-01-23', '2026-01-24',  9.10, 'Activa',    2500.00, 100, 1, 'A001'),
(2011, '2026-01-25', '2026-01-26', 11.00, 'Activa',    2200.00, 101, 2, 'A002'),
(2012, '2026-01-27', '2026-01-29', 33.30, 'Activa',    1800.00, 102, 1, 'B100'),
(2013, '2026-01-30', '2026-02-01', 27.00, 'Cerrada',   3200.00, 103, 2, 'C777'),
(2014, '2026-02-02', '2026-02-02',  4.00, 'Cerrada',   2000.00, 104, 1, 'D505'),

(2015, '2026-02-03', '2026-02-04',  8.50, 'Activa',    2500.00, 105, 2, 'A001'),
(2016, '2026-02-05', '2026-02-06', 14.20, 'Activa',    2200.00, 106, 1, 'A002'),
(2017, '2026-02-07', '2026-02-09', 31.60, 'Activa',    1800.00, 107, 2, 'B100'),
(2018, '2026-02-10', '2026-02-11', 13.75, 'Activa',    3200.00, 108, 1, 'C777'),
(2019, '2026-02-12', '2026-02-14', 26.90, 'Cerrada',   2000.00, 109, 2, 'D505');
