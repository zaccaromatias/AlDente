CREATE TABLE [dbo].[Sancion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,      
    [FechaSansion] DATETIME NOT NULL,
    [TipoSancionId] INT NOT NULL,
    [ClienteId] INT NOT NULL,
    [RestauranteId] INT NOT NULL,
    CONSTRAINT [FK_Sancion_Cliente] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Cliente]([Id]), 
    CONSTRAINT [FK_Sancion_TipoSancion] FOREIGN KEY ([TipoSancionId]) REFERENCES [dbo].[TipoSancion]([Id]),
    CONSTRAINT [FK_Sancion_RestauranteId] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id])
    
)
