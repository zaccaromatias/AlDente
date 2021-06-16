CREATE TABLE [dbo].[Restaurante]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Descripcion] NVARCHAR(50) NULL, 
    [Direccion] NVARCHAR(50) NULL, 
    [Telefono] NVARCHAR(50) NULL, 
    [UrlMenu] NVARCHAR(50) NULL
)
