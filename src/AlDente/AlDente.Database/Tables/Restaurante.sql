CREATE TABLE [dbo].[Restaurante]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Descripcion] NVARCHAR(200) NULL, 
    [Direccion] NVARCHAR(200) NULL, 
    [Telefono] NVARCHAR(50) NULL, 
    [UrlMenu] NVARCHAR(500) NULL
)
