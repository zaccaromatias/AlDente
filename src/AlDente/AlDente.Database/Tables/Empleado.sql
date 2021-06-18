CREATE TABLE [dbo].[Empleado]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DNI] INT NOT NULL,
    [DescripcionPuesto] NVARCHAR(50) NOT NULL, 
    [Nombre] NVARCHAR(50) NOT NULL, 
    [Apellido] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NULL, 
    [FechaCreacion] DATETIME NOT NULL,
    [EstadoEmpleadoId]  INT NOT NULL,
    [RestauranteId] INT NOT NULL
    CONSTRAINT [FK_Empleado_EstadoEmpleado] FOREIGN KEY ([EstadoEmpleadoId]) REFERENCES [dbo].[EstadoEmpleado]([Id]), 
    CONSTRAINT [FK_Empleado_Restaurante] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id]), 
    CONSTRAINT [AK_Empleado_Email] UNIQUE ([Email]),
    CONSTRAINT [AK_Empleado_DNI] UNIQUE ([DNI])   
)