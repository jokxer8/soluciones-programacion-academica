use master
use ALQUILER_COCHES

insert into Agencia (nombre_agencia)
	values ('Auto Dominicana'), ('Volando Rent a Car')

select * from Agencia --verificar entrada de datos en Agencia


insert into Cliente (DNI, nombre_cliente, telefono, direccion)
	values
	('12345678A', 'Luisa Paulino', 8492718087, 'Av. Winston Churchill 45'),
	('87654321B', 'Marcos Antonio del Castillo', 8493745151, 'Calle El Conde 112'),
	('11223344C', 'Julio Alberto Gonzales', 8092517489, 'Av. 27 de Febrero 890'),
	('99887766D', 'Denise Villanueva', 8496354271, 'Calle Las Damas 33'),
	('55443322E', 'Alberto de Jesus', 8093554875, 'Av. Abraham Lincoln 567')

insert into Cliente (DNI, nombre_cliente, telefono, direccion, codigo_cliente_avala, DNI_cliente_avala)
	values
	('44332211F', 'Marcos Roberto del Castillo', 8092135688, 'Calle Arzobispo Meriño 78', 1, '12345678A'),
	('66778899G', 'Juana María Martes', 8496574256, 'Av. John F. Kennedy 234', 2, '87654321B'),
	('33221100H', 'Enrique Nuñez', 8497135225, 'Calle Hostos 19', 5, '55443322E'),
	('77889900J', 'Estela Alberta Panigua', 8091234567, 'Av. Independencia 401', 3, '11223344C'),
	('22110099K', 'Luisa Gonzalo', 8497894563, 'Calle Padre Billini 55', 4, '99887766D')

select * from Cliente --verificar entrada de datos en Cliente


insert into Garaje(nombre_garaje)
	values ('Garaje Don Paulino'), ('Garaje El Millón'), ('Garaje Gloria')

select * from Garaje --verificar entreada de datos en Garaje


insert into Coche(marca, modelo, color, matricula, precio_reserva, codigo_garaje)
	values
	('Kia', 'Picanto', 'blanco', 'A539428652', 30, 1),
	('Hyundai', 'Avante', 'blanco', 'A346215751', 35, 1),
	('Suzuki', 'Vitara', 'azul', 'AA63859531', 45, 3),
	('Honda', 'CRV', 'negro', 'A564142889', 65, 2),
	('Toyota', 'Corolla', 'gris', 'AA31354768', 40, 2),
	('Kia', 'Sonet', 'blanco', 'A529768531', 55, 2)

select * from Coche --verificar entrada de datos en Coche


select * from Cliente
select * from Agencia
--para ver la información necesaria para intrudicur datos a Reserva

insert into Reserva(codigo_cliente, DNI_cliente, codigo_agencia, fecha_inicio, fecha_fin, litros, indicador)
	values
	(1,  '12345678A', 2, '2026-03-05 18:00:00', '2026-03-11 12:00:00', 63.65, 'sucio'),-----------| 1
	(1,  '12345678A', 2, '2026-03-05 18:00:00', '2026-03-11 12:00:00', 60.00, 'rayado, limpio'),--| 2
	(5,  '55443322E', 1, '2026-03-04 16:00:00', '2026-03-08 12:00:00', 47.00, 'limpio'),
	(9,  '77889900J', 2, '2026-03-13 14:00:00', '2026-03-15 14:00:00', 63.61, 'limpio'),
	(7,  '66778899G', 1, '2026-03-13 17:00:00', '2026-03-15 12:00:00', 34.98, 'chocado, sucio'),
	(8,  '33221100H', 2, '2026-03-27 08:00:00', '2026-03-30 13:00:00', 45.00, 'limpio'),
	(10, '22110099K', 2, '2026-04-01 18:00:00', '2026-04-05 12:00:00', 60.03, 'sucio'),-----------| 7
	(10, '22110099K', 2, '2026-04-01 18:00:00', '2026-04-05 12:00:00', 44.89, 'limpio'),----------| 8
	(4,  '99887766D', 1, '2026-04-17 17:00:00', '2626-04-20 08:00:00', 46.79, 'rayado, sucio'),
	(6,  '44332211F', 1, '2026-04-19 10:00:00', '2026-04-25 10:00:00', 35.00, 'limpio'),
	(3,  '11223344C', 2, '2026-04-20 08:00:00', '2026-04-25 12:00:00', 60.00, 'limpio'),
	(8,  '33221100H', 1, '2026-05-01 14:00:00', '2026-05-09 12:00:00', 47.02, 'rayado, limpio'),--| 12
	(8,  '33221100H', 1, '2026-05-01 14:00:00', '2026-05-09 12:00:00', 47.00, 'limpio'),----------| 13
	(5,  '55443322E', 2, '2026-05-08 15:00:00', '2026-05-11 08:00:00', 44.86, 'limpio'),
	(2,  '87654321B', 1, '2026-05-14 16:00:00', '2026-05-17 12:00:00', 47.00, 'sucio'),
	(1,  '12345678A', 2, '2026-05-22 11:00:00', '2026-05-24 12:00:00', 45.01, 'limpio'),
	(9,  '77889900J', 2, '2026-05-24 16:00:00', '2026-05-31 09:00:00', 63.65, 'rayado, sucio'),
	(4,  '99887766D', 1, '2026-06-03 18:00:00', '2026-06-07 10:00:00', 34.93, 'limpio'),
	(2,  '87654321B', 2, '2026-06-03 14:00:00', '2026-06-07 12:00:00', 59.83, 'limpio'),
	(10, '22110099K', 2, '2026-06-12 13:00:00', '2026-06-16 10:00:00', 63.65, 'sucio'),
	(7,  '66778899G', 1, '2026-06-19 15:00:00', '2026-06-21 14:00:00', 46.92, 'limpio'),----------| 21
	(7,  '66778899G', 1, '2026-06-19 15:00:00', '2026-06-21 14:00:00', 47.02, 'rayado, limpio'),--| 22
	(4,  '99887766D', 1, '2026-07-02 17:00:00', '2026-07-06 08:00:00', 47.00, 'sucio'),
	(3,  '11223344C', 2, '2026-07-03 09:00:00', '2026.07.05 12:00:00', 63.65, 'limpio')

select * from Reserva --verificar entrada de datos en Reserva


select * from Coche
select codigo_cliente, codigo_reserva from Reserva
	order by codigo_reserva
--para ver la información necesaria para intrudicur datos a Reserva
	
insert into ReservaCoche(codigo_reserva,  matricula_coche)
	values
	(1,  'A564142889'), (1,  'AA31354768'),
	(2,  'A539428652'),
	(3,  'A564142889'),
	(4,  'A346215751'),
	(5,  'AA63859531'),
	(6,  'AA31354768'), (6,  'AA63859531'),
	(7,  'A529768531'),
	(8,  'A346215751'),
	(9,  'AA31354768'),
	(10, 'A529768531'), (10, 'A539428652'),
	(11, 'AA63859531'),
	(12, 'A539428652'),
	(13, 'AA63859531'),
	(14, 'A564142889'),
	(15, 'A346215751'),
	(16, 'AA31354768'),
	(17, 'A564142889'),
	(18, 'A529768531'), (18, 'A539428652'),
	(19, 'A529768531'),
	(20, 'A564142889')

select * from ReservaCoche --verificar entrada de datos en Coche