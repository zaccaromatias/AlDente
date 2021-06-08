CREATE TABLE [dbo].[EstadoCliente]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Codigo] NVARCHAR(50) NOT NULL, 
    [Descripcion] NVARCHAR(200) NOT NULL, 
    CONSTRAINT [AK_EstadoCliente_Codigo] UNIQUE ([Codigo])
)
