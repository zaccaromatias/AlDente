CREATE TABLE [dbo].[TipoBeneficio]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Codigo] NVARCHAR(50) NOT NULL,
    [Descripcion] NVARCHAR(200) NOT NULL, 
    [Descuento] INT NOT NULL,
    CONSTRAINT [UK_TipoBeneficio_Codigo] UNIQUE ([Codigo])

)
