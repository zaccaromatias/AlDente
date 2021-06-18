CREATE TABLE [dbo].[TipoSancion]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Codigo] NVARCHAR(50) NOT NULL,
    [Descripcion] NVARCHAR(200) NOT NULL, 
    [DiasSuspension] INT NOT NULL,
    CONSTRAINT [UK_TipoSancion_Codigo] UNIQUE ([Codigo])

)
