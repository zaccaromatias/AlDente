-- Primary Key clave primaria
-- IDENTITY Autoincremental suma de a 1.
CREATE TABLE [dbo].[Cliente]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] NVARCHAR(320) NOT NULL,    
    [Nombre] NVARCHAR(500) NOT NULL, 
    [Apellido] NVARCHAR(500) NOT NULL, 
    [DNI] INT NOT NULL, 
    [Password] NVARCHAR(200) NULL, 
    [EstadoClienteId] INT NOT NULL, 
    [Telefono] NVARCHAR(200) NOT NULL,     
    [EmpleadoId] INT NULL, --Empleado que crea al cliente
    CONSTRAINT [FK_Cliente_EstadoCliente] FOREIGN KEY ([EstadoClienteId]) REFERENCES [dbo].[EstadoCliente]([Id]), 
    CONSTRAINT [FK_Cliente_Empleado] FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleado]([Id]), 
    CONSTRAINT [AK_Cliente_Email] UNIQUE ([Email]),    
    CONSTRAINT [AK_Cliente_DNI] UNIQUE ([DNI])   
)




