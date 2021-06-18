CREATE TABLE [dbo].[EstadoReserva]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Codigo] NVARCHAR(50) NOT NULL,
    [Descripcion] NVARCHAR(50) NULL,     
    CONSTRAINT [AK_EstadoReserva_Codigo] UNIQUE ([Codigo])
)
