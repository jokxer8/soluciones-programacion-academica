/* =========================================================
   MANDATO 3: CARGA DE DATOS (DML) - SQL SERVER
   Base nueva (recien creada con el Mandato 2)
   Requerido:
     - 5 coches, 2 agencias, 3 garajes, 10 clientes, 20 reservas
   Extra:
     - 1 reserva con 2 coches en la misma reserva
   ========================================================= */

USE ALQUILER_COCHES;

/* =========================
   1) AGENCIAS (2)
   IDs esperados: 1..2
   ========================= */
INSERT INTO Agencia (nombre_agencia) VALUES
('Agencia Central'),   -- ID = 1
('Agencia Norte');     -- ID = 2

/* =========================
   2) GARAJES (3)
   IDs esperados: 1..3
   ========================= */
INSERT INTO Garaje (nombre) VALUES
('Garaje A'),          -- ID = 1
('Garaje B'),          -- ID = 2
('Garaje C');          -- ID = 3

/* =========================
   3) CLIENTES (10)
   IDs esperados: 1..10
   PK compuesta: (codigo_cliente, DNI)
   ========================= */

-- 5 clientes sin avalador (IDs 1..5)
INSERT INTO Cliente (DNI, nombre, telefono, direccion, codigo_cliente_avala, DNI_cliente_avala) VALUES
('001-0000001-1','Ana Perez','809-555-0101','Santo Domingo',NULL,NULL),  -- ID=1
('001-0000002-2','Luis Gomez','809-555-0102','Santiago',NULL,NULL),      -- ID=2
('001-0000003-3','Maria Diaz','809-555-0103','La Vega',NULL,NULL),       -- ID=3
('001-0000004-4','Carlos Ruiz','809-555-0104','San Cristobal',NULL,NULL),-- ID=4
('001-0000005-5','Elena Torres','809-555-0105','Bonao',NULL,NULL);       -- ID=5

-- 5 clientes avalados (IDs 6..10), avalados por los 5 anteriores
INSERT INTO Cliente (DNI, nombre, telefono, direccion, codigo_cliente_avala, DNI_cliente_avala) VALUES
('001-0000006-6','Pedro Castillo','809-555-0106','Higuey', 1,'001-0000001-1'), -- ID=6
('001-0000007-7','Rosa Martinez','809-555-0107','San Pedro', 2,'001-0000002-2'),-- ID=7
('001-0000008-8','Juan Lopez','809-555-0108','Puerto Plata', 3,'001-0000003-3'),-- ID=8
('001-0000009-9','Sofia Hernandez','809-555-0109','Moca', 4,'001-0000004-4'),   -- ID=9
('001-0000010-0','Miguel Jimenez','809-555-0110','Bani', 5,'001-0000005-5');    -- ID=10

/* =========================
   4) COCHES (5)
   ========================= */
INSERT INTO Coche (matricula, modelo, marca, color, codigo_garaje) VALUES
('A001','Civic','Honda','Negro', 1),
('A002','Corolla','Toyota','Blanco', 1),
('B101','Sentra','Nissan','Gris', 2),
('B102','Rio','Kia','Rojo', 2),
('C201','Mazda3','Mazda','Azul', 3);

/* =========================
   5) RESERVAS (20) - CABECERA
   IDs esperados: 1..20
   ========================= */
INSERT INTO Reserva (litros, indicador, fecha_inicio, fecha_fin, codigo_cliente, DNI_cliente, codigo_agencia) VALUES
(10.50,'Normal','2026-01-05','2026-01-07',  1,'001-0000001-1', 1), -- Reserva 1
( 8.00,'Normal','2026-01-08','2026-01-08',  2,'001-0000002-2', 1), -- Reserva 2
(12.25,'VIP'   ,'2026-01-10','2026-01-12',  3,'001-0000003-3', 2), -- Reserva 3
( 6.75,'Normal','2026-01-13','2026-01-13',  4,'001-0000004-4', 2), -- Reserva 4
( 9.10,'Normal','2026-01-14','2026-01-16',  5,'001-0000005-5', 1), -- Reserva 5
( 7.00,'Normal','2026-01-17','2026-01-17',  6,'001-0000006-6', 1), -- Reserva 6
(11.00,'VIP'   ,'2026-01-18','2026-01-19',  7,'001-0000007-7', 2), -- Reserva 7
( 5.50,'Normal','2026-01-20','2026-01-20',  8,'001-0000008-8', 2), -- Reserva 8
( 4.25,'Normal','2026-01-21','2026-01-22',  9,'001-0000009-9', 1), -- Reserva 9
(13.00,'VIP'   ,'2026-01-23','2026-01-25', 10,'001-0000010-0', 1), -- Reserva 10
( 6.00,'Normal','2026-02-01','2026-02-02',  1,'001-0000001-1', 2), -- Reserva 11
( 9.50,'Normal','2026-02-03','2026-02-03',  2,'001-0000002-2', 2), -- Reserva 12
( 8.80,'VIP'   ,'2026-02-04','2026-02-06',  3,'001-0000003-3', 1), -- Reserva 13
(10.00,'Normal','2026-02-07','2026-02-07',  4,'001-0000004-4', 1), -- Reserva 14
( 7.30,'Normal','2026-02-08','2026-02-09',  5,'001-0000005-5', 2), -- Reserva 15
( 5.00,'Normal','2026-02-10','2026-02-10',  6,'001-0000006-6', 2), -- Reserva 16
(12.00,'VIP'   ,'2026-02-11','2026-02-12',  7,'001-0000007-7', 1), -- Reserva 17
( 6.60,'Normal','2026-02-13','2026-02-14',  8,'001-0000008-8', 1), -- Reserva 18
( 9.90,'Normal','2026-02-15','2026-02-16',  9,'001-0000009-9', 2), -- Reserva 19
(11.40,'VIP'   ,'2026-02-17','2026-02-19', 10,'001-0000010-0', 2); -- Reserva 20

/* =========================
   6) RESERVA_COCHE (DETALLE)
   - Reservas 1..19: 1 coche
   - Reserva 20: 2 coches (lo solicitado)
   ========================= */

INSERT INTO Reserva_Coche (codigo_reserva, matricula_coche, precio_alquiler) VALUES
( 1,'A001',1500.00),
( 2,'A002',1200.00),
( 3,'B101',1800.00),
( 4,'B102',1100.00),
( 5,'C201',1600.00),
( 6,'A001',1500.00),
( 7,'A002',1200.00),
( 8,'B101',1800.00),
( 9,'B102',1100.00),
(10,'C201',1600.00),
(11,'A002',1200.00),
(12,'A001',1500.00),
(13,'B102',1100.00),
(14,'B101',1800.00),
(15,'C201',1600.00),
(16,'A001',1500.00),
(17,'A002',1200.00),
(18,'B101',1800.00),
(19,'B102',1100.00),

-- Reserva 20 con 2 coches en la MISMA reserva
(20,'A001',1500.00),
(20,'C201',1600.00);