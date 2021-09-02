CREATE TABLE [dbo].[EstadoUsuario]
(
	[Id] INT NOT NULL, 
    [Codigo] NVARCHAR(50) NOT NULL, 
    [Descripcion] NVARCHAR(200) NOT NULL, 
    CONSTRAINT [PK_EstadoCliente_Id] Primary Key ([Id]),
    CONSTRAINT [AK_EstadoCliente_Codigo] UNIQUE ([Codigo])
)
