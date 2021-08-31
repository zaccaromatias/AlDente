CREATE TABLE [dbo].[Usuario]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[NombreUsuario] NVARCHAR (500) NOT NULL,
	[TipoUsuario] NVARCHAR (1) NOT NULL,
	[ClienteId] INT NOT NULL, -- UN CLIENTE TIENE UN TIPO DE USUARIO
	[EmpleadoId] INT NOT NULL, -- UN EMPLEADO TIENE UN TIPO DE USUARIO
    CONSTRAINT [FK_Cliente_Id] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[EstadoCliente]([Id]), 
    CONSTRAINT [FK_Empleado_Id] FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleado]([Id]), 
)
