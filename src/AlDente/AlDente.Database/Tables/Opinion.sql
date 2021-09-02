CREATE TABLE [dbo].[Opinion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Texto] NVARCHAR(200) NULL,
    [ClienteId] INT NOT NULL,
    [RestauranteId] INT NOT NULL,
    [Calificacion]  INT NOT NULL,
    CONSTRAINT [FK_Opinion_Cliente] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Usuario]([Id]), 
    CONSTRAINT [FK_Opinion_Restaurante] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id]), 
    
)
