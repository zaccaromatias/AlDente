CREATE TABLE [dbo].[Empleado]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Descripcion] NVARCHAR(50) NOT NULL, 
    [Nombre] NVARCHAR(50) NOT NULL, 
    [Apellido] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NULL, 
    [FechaCreacion] DATETIME NOT NULL
)
