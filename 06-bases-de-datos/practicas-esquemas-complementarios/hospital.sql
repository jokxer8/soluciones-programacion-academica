USE [hospital]
GO
/****** Object:  Table [dbo].[INTERNAMIENTOS]    Script Date: 7/26/2022 1:30:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INTERNAMIENTOS](
	[NInternamiento] [int] IDENTITY(1,1) NOT NULL,
	[NHistorial] [varchar](9) NULL,
	[FechaIngreso] [date] NULL,
	[CodigoIdentificacion] [varchar](4) NULL,
	[NPlanta] [int] NULL,
	[NCama] [int] NULL,
	[Alergico] [char](2) NULL,
	[Observaciones] [nchar](10) NULL,
	[CosteTratamiento] [money] NULL,
	[Diagnostico] [varchar](50) NULL,
 CONSTRAINT [PK_INTERNAMIENTOS] PRIMARY KEY CLUSTERED 
(
	[NInternamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MEDICOS]    Script Date: 7/26/2022 1:30:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MEDICOS](
	[CodIdentificacion] [varchar](4) NOT NULL,
	[NombreMedico] [varchar](15) NULL,
	[ApellidoMedico] [varchar](50) NULL,
	[Especialidad] [varchar](25) NULL,
	[FechaIngreso] [date] NULL,
	[Cargo] [varchar](25) NULL,
	[NColegiado] [int] NULL,
	[Observaciones] [nchar](10) NULL,
 CONSTRAINT [PK_MEDICOS] PRIMARY KEY CLUSTERED 
(
	[CodIdentificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PACIENTES]    Script Date: 7/26/2022 1:30:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PACIENTES](
	[NSegSocial] [varchar](15) NULL,
	[Nombre] [varchar](15) NULL,
	[Apellidos] [varchar](30) NULL,
	[Domicilio] [varchar](30) NULL,
	[Poblacion] [varchar](25) NULL,
	[Provincia] [varchar](15) NULL,
	[CodPostal] [varchar](5) NULL,
	[Telefono] [varchar](12) NULL,
	[NHistorial] [varchar](9) NOT NULL,
	[Sexo] [varchar](1) NULL,
	[fecha_nacimiento] [date] NULL,
 CONSTRAINT [PK_Pacientes] PRIMARY KEY CLUSTERED 
(
	[NHistorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[INTERNAMIENTOS] ON 
GO
INSERT [dbo].[INTERNAMIENTOS] ([NInternamiento], [NHistorial], [FechaIngreso], [CodigoIdentificacion], [NPlanta], [NCama], [Alergico], [Observaciones], [CosteTratamiento], [Diagnostico]) VALUES (1, N'10203-F', CAST(N'2009-01-23' AS Date), N'AJH', 5, 121, N'NO', N'Epileptico', NULL, NULL)
GO
INSERT [dbo].[INTERNAMIENTOS] ([NInternamiento], [NHistorial], [FechaIngreso], [CodigoIdentificacion], [NPlanta], [NCama], [Alergico], [Observaciones], [CosteTratamiento], [Diagnostico]) VALUES (2, N'15413-s', CAST(N'2009-03-13' AS Date), N'RLQ', 2, 5, N'SI', N'ALERGICO  ', NULL, NULL)
GO
INSERT [dbo].[INTERNAMIENTOS] ([NInternamiento], [NHistorial], [FechaIngreso], [CodigoIdentificacion], [NPlanta], [NCama], [Alergico], [Observaciones], [CosteTratamiento], [Diagnostico]) VALUES (3, N'11454-L', CAST(N'2009-05-25' AS Date), N'RLQ', 3, 31, N'NO', NULL, NULL, NULL)
GO
INSERT [dbo].[INTERNAMIENTOS] ([NInternamiento], [NHistorial], [FechaIngreso], [CodigoIdentificacion], [NPlanta], [NCama], [Alergico], [Observaciones], [CosteTratamiento], [Diagnostico]) VALUES (4, N'15413-S', CAST(N'2010-01-29' AS Date), N'CEM', 2, 13, N'NO', NULL, NULL, NULL)
GO
INSERT [dbo].[INTERNAMIENTOS] ([NInternamiento], [NHistorial], [FechaIngreso], [CodigoIdentificacion], [NPlanta], [NCama], [Alergico], [Observaciones], [CosteTratamiento], [Diagnostico]) VALUES (5, N'14546-E', CAST(N'2010-02-24' AS Date), N'AJH', 1, 5, N'SI', N'ALERGICO  ', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[INTERNAMIENTOS] OFF
GO
INSERT [dbo].[MEDICOS] ([CodIdentificacion], [NombreMedico], [ApellidoMedico], [Especialidad], [FechaIngreso], [Cargo], [NColegiado], [Observaciones]) VALUES (N'ajh', N'Antonio', N'Jaen Hernandez', N'Pediatria', CAST(N'1982-08-12' AS Date), N'Adjunto', 2113, N'          ')
GO
INSERT [dbo].[MEDICOS] ([CodIdentificacion], [NombreMedico], [ApellidoMedico], [Especialidad], [FechaIngreso], [Cargo], [NColegiado], [Observaciones]) VALUES (N'CEM', N'Carmem', N'Esterill Manrique', N'Psiquiatria', CAST(N'1992-02-13' AS Date), N'Jefe de Seccion', 1231, NULL)
GO
INSERT [dbo].[MEDICOS] ([CodIdentificacion], [NombreMedico], [ApellidoMedico], [Especialidad], [FechaIngreso], [Cargo], [NColegiado], [Observaciones]) VALUES (N'RLQ', N'Rocia', N'Lopez Quijada', N'Medico de familia', CAST(N'1994-09-23' AS Date), N'Titular', 1331, NULL)
GO
INSERT [dbo].[PACIENTES] ([NSegSocial], [Nombre], [Apellidos], [Domicilio], [Poblacion], [Provincia], [CodPostal], [Telefono], [NHistorial], [Sexo], [fecha_nacimiento]) VALUES (N'08/7888888', N'Jose Eduardo', N'Romerales Pinto', N'c/Azorin,34 3o', N'Mostoles', N'Madrid', N'28935', N'91-345-87-45', N'10203-f', N'h', CAST(N'2015-08-23' AS Date))
GO
INSERT [dbo].[PACIENTES] ([NSegSocial], [Nombre], [Apellidos], [Domicilio], [Poblacion], [Provincia], [CodPostal], [Telefono], [NHistorial], [Sexo], [fecha_nacimiento]) VALUES (N'08/7234823', N'Angel', N'Ruiz Picasso', N'c/salmeron,212', N'Madrid', N'Madrid', N'28028', N'91-565-34-33', N'11454-l', N'h', CAST(N'1998-12-04' AS Date))
GO
INSERT [dbo].[PACIENTES] ([NSegSocial], [Nombre], [Apellidos], [Domicilio], [Poblacion], [Provincia], [CodPostal], [Telefono], [NHistorial], [Sexo], [fecha_nacimiento]) VALUES (N'08/7333333', N'Mercedes', N'Romero Carvajal', N'c/Malaga,13', N'Mostoles', N'Madrid', N'28935', N'91-455-67-45', N'14546-e', N'm', CAST(N'1963-02-02' AS Date))
GO
INSERT [dbo].[PACIENTES] ([NSegSocial], [Nombre], [Apellidos], [Domicilio], [Poblacion], [Provincia], [CodPostal], [Telefono], [NHistorial], [Sexo], [fecha_nacimiento]) VALUES (N'08/7555555', N'Martin', N'Fernandez Lopez', N'c/Sastres, 21 ', N'Madrid', N'Madrid', N'28028', N'91-333-33-33', N'15413-s', N'H', CAST(N'2003-12-12' AS Date))
GO
ALTER TABLE [dbo].[INTERNAMIENTOS]  WITH CHECK ADD  CONSTRAINT [FK_INGRESOS_MEDICOS] FOREIGN KEY([CodigoIdentificacion])
REFERENCES [dbo].[MEDICOS] ([CodIdentificacion])
GO
ALTER TABLE [dbo].[INTERNAMIENTOS] CHECK CONSTRAINT [FK_INGRESOS_MEDICOS]
GO
ALTER TABLE [dbo].[INTERNAMIENTOS]  WITH CHECK ADD  CONSTRAINT [FK_INGRESOS_Pacientes] FOREIGN KEY([NHistorial])
REFERENCES [dbo].[PACIENTES] ([NHistorial])
GO
ALTER TABLE [dbo].[INTERNAMIENTOS] CHECK CONSTRAINT [FK_INGRESOS_Pacientes]
GO
ALTER TABLE [dbo].[INTERNAMIENTOS]  WITH CHECK ADD  CONSTRAINT [alergico si o no] CHECK  (([Alergico]='Si' OR [Alergico]='No'))
GO
ALTER TABLE [dbo].[INTERNAMIENTOS] CHECK CONSTRAINT [alergico si o no]
GO
ALTER TABLE [dbo].[INTERNAMIENTOS]  WITH CHECK ADD  CONSTRAINT [CK_INGRESOS_1] CHECK  (([NPlanta]<='10'))
GO
ALTER TABLE [dbo].[INTERNAMIENTOS] CHECK CONSTRAINT [CK_INGRESOS_1]
GO
ALTER TABLE [dbo].[INTERNAMIENTOS]  WITH CHECK ADD  CONSTRAINT [CK_INGRESOS_2] CHECK  (([NCama]<='200'))
GO
ALTER TABLE [dbo].[INTERNAMIENTOS] CHECK CONSTRAINT [CK_INGRESOS_2]
GO
ALTER TABLE [dbo].[PACIENTES]  WITH CHECK ADD  CONSTRAINT [CK_Pacientes] CHECK  (([sexo]='h' OR [sexo]='m'))
GO
ALTER TABLE [dbo].[PACIENTES] CHECK CONSTRAINT [CK_Pacientes]
GO
