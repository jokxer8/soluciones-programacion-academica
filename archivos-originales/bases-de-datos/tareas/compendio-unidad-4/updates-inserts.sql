-- -- UPDATES E INSERTS -- --

insert into Provincia(provincia)
	values
	('Azua'),
	('Bahoruco'),
	('Barahona'),
	('Dajabón'),
	('Distrito Nacional'),
	('Duarte'),
	('El Seibo'),
	('Elías Piña'),
	('Espaillat'),
	('Hato Mayor'),
	('Hermanas Mirabal'),
	('Independencia'),
	('La Altagracia'),
	('La Romana'),
	('La Vega'),
	('María Trinidad Sánchez'),
	('Monseñor Nouel'),
	('Monte Cristi'),
	('Monte Plata'),
	('Pedernales'),
	('Peravia'),
	('Puerto Plata'),
	('Samaná'),
	('San Cristobal'),
	('San José de Ocoa'),
	('San Juan'),
	('San Pedro de Macorís'),
	('Sánchez Ramirez'),
	('Santiago'),
	('Santiago Rodríguez'),
	('Santo Domingo'),
	('Valverde')

UPDATE Agencia
    SET id_provincia = 31,
        calle      = 'Av. Winston Churchill',
        edificio   = 'Torre Empresarial, Piso 3',
        sector     = 'Piantini',
        municipio  = 'Santo Domingo de Guzmán'
    WHERE codigo_agencia = 1;

UPDATE Agencia
    SET id_provincia = 31,
        calle      = 'Av. John F. Kennedy',
        edificio   = 'Plaza Kennedy, Local 12',
        sector     = 'La Julia',
        municipio  = 'Santo Domingo de Guzmán'
    WHERE codigo_agencia = 2;

select*from agencia


insert into Nacionalidad(nacionalidad)
	values
		('Dominicano'), --1
		('Mexicano'), ----2
		('Venezolano'), --3
		('Estadounidense'), --4
		('Cubano') -------5



--arreglar la información de cada cliente

UPDATE Cliente
    SET nombre='Luisa',
        apellido='Paulino',
        fecha_nacimiento='2004-05-20',
        sexo='F',
        id_nacionalidad=1
    WHERE codigo_cliente=1;

UPDATE Cliente
    SET nombre='Marcos Antonio',
        apellido='Del Castillo',
        fecha_nacimiento='1987-02-22',
        sexo='M',
        id_nacionalidad=3
    WHERE codigo_cliente=2;

UPDATE Cliente
    SET nombre='Julio Alberto',
        apellido='Gonzales',
        fecha_nacimiento='1999-05-10',
        sexo='M',
        id_nacionalidad=3
    WHERE codigo_cliente=3;

UPDATE Cliente
    SET nombre='Denise',
        apellido='Villanueva',
        fecha_nacimiento='2001-08-15',   
        sexo='F',
        id_nacionalidad=1               
    WHERE codigo_cliente=4;

UPDATE Cliente
    SET nombre='Alberto',
        apellido='De Jesus',
        fecha_nacimiento='1995-03-28',   
        sexo='M',
        id_nacionalidad=1
    WHERE codigo_cliente=5;

UPDATE Cliente
    SET nombre='Marcos Roberto',
        apellido='Del Castillo',
        fecha_nacimiento='1983-11-04',   
        sexo='M',
        id_nacionalidad=4               
    WHERE codigo_cliente=6;

UPDATE Cliente
    SET nombre='Juana Maria',
        apellido='Martes',
        fecha_nacimiento='1990-06-17',   
        sexo='F',
        id_nacionalidad=3               
    WHERE codigo_cliente=7;

UPDATE Cliente
    SET nombre='Enrique',
        apellido='Nunez',
        fecha_nacimiento='1978-09-30',
        sexo='M',
        id_nacionalidad=3
    WHERE codigo_cliente=8;

UPDATE Cliente
    SET nombre='Estela Alberta',
        apellido='Paniagua',
        fecha_nacimiento='1993-12-05',
        sexo='F',
        id_nacionalidad=1
    WHERE codigo_cliente=9;

UPDATE Cliente
    SET nombre='Luisa',
        apellido='Gonzalo',
        fecha_nacimiento='2000-04-22',
        sexo='F',
        id_nacionalidad=2
    WHERE codigo_cliente=10;


UPDATE Coche SET anio_fabricacion = 2019 WHERE matricula = 'A346215751'; -- Hyundai Avante
UPDATE Coche SET anio_fabricacion = 2022 WHERE matricula = 'A529768531'; -- Kia Sonet
UPDATE Coche SET anio_fabricacion = 2017 WHERE matricula = 'A539428652'; -- Kia Picanto
UPDATE Coche SET anio_fabricacion = 2021 WHERE matricula = 'A564142889'; -- Honda CRV
UPDATE Coche SET anio_fabricacion = 2018 WHERE matricula = 'AA31354768'; -- Toyota Corolla
UPDATE Coche SET anio_fabricacion = 2020 WHERE matricula = 'AA63859531'; -- Suzuki Vitara

-- Una vez confirmado que no hay NULLs, modificar la columna a NOT NULL
ALTER TABLE Coche
    ALTER COLUMN anio_fabricacion int NOT NULL

-- Actualizar indicador_kilometraje en Reserva (codigo_reserva 1 al 24)
UPDATE Reserva SET indicador_kilometraje = 45230 WHERE codigo_reserva = 1;
UPDATE Reserva SET indicador_kilometraje = 45230 WHERE codigo_reserva = 2;
UPDATE Reserva SET indicador_kilometraje = 78150 WHERE codigo_reserva = 3;
UPDATE Reserva SET indicador_kilometraje = 32400 WHERE codigo_reserva = 4;
UPDATE Reserva SET indicador_kilometraje = 61780 WHERE codigo_reserva = 5;
UPDATE Reserva SET indicador_kilometraje = 29900 WHERE codigo_reserva = 6;
UPDATE Reserva SET indicador_kilometraje = 88450 WHERE codigo_reserva = 7;
UPDATE Reserva SET indicador_kilometraje = 88450 WHERE codigo_reserva = 8;
UPDATE Reserva SET indicador_kilometraje = 54320 WHERE codigo_reserva = 9;
UPDATE Reserva SET indicador_kilometraje = 41100 WHERE codigo_reserva = 10;
UPDATE Reserva SET indicador_kilometraje = 73560 WHERE codigo_reserva = 11;
UPDATE Reserva SET indicador_kilometraje = 19870 WHERE codigo_reserva = 12;
UPDATE Reserva SET indicador_kilometraje = 19870 WHERE codigo_reserva = 13;
UPDATE Reserva SET indicador_kilometraje = 62340 WHERE codigo_reserva = 14;
UPDATE Reserva SET indicador_kilometraje = 38900 WHERE codigo_reserva = 15;
UPDATE Reserva SET indicador_kilometraje = 51200 WHERE codigo_reserva = 16;
UPDATE Reserva SET indicador_kilometraje = 94780 WHERE codigo_reserva = 17;
UPDATE Reserva SET indicador_kilometraje = 27650 WHERE codigo_reserva = 18;
UPDATE Reserva SET indicador_kilometraje = 43210 WHERE codigo_reserva = 19;
UPDATE Reserva SET indicador_kilometraje = 66890 WHERE codigo_reserva = 20;
UPDATE Reserva SET indicador_kilometraje = 35470 WHERE codigo_reserva = 21;
UPDATE Reserva SET indicador_kilometraje = 35470 WHERE codigo_reserva = 22;
UPDATE Reserva SET indicador_kilometraje = 80120 WHERE codigo_reserva = 23;
UPDATE Reserva SET indicador_kilometraje = 57340 WHERE codigo_reserva = 24;

SELECT * FROM Reserva -- verificar

-- Una vez confirmado que no hay NULLs, modificar la columna a NOT NULL
ALTER TABLE Reserva
    ALTER COLUMN indicador_kilometraje int NOT NULL