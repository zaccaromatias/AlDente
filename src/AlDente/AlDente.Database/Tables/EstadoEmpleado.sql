CREATE TABLE [dbo].[EstadoEmpleado]
(	
	[Id] INT NOT NULL PRIMARY KEY, 
    [Codigo] NVARCHAR(50) NOT NULL, 
    [Descripcion] NVARCHAR(200) NOT NULL, 
    CONSTRAINT [AK_EstadoEmpleado_Codigo] UNIQUE ([Codigo])
)
