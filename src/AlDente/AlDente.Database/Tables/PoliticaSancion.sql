CREATE TABLE [dbo].[PoliticaSancion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Descripcion] NVARCHAR(200) NOT NULL, 
	[TipoSancionId]  INT NOT NULL,
    [NumeroMaximo] INT NOT NULL,
	[Periodo] INT NOT NULL,
	[EstadoReservaId] INT NOT NULL,
	[RestauranteId] INT NOT NULL,
	CONSTRAINT [FK_PoliticaSancion_TipoSancion] FOREIGN KEY ([TipoSancionId]) REFERENCES [dbo].[TipoSancion]([Id]),
	CONSTRAINT [FK_PoliticaSancion_EstadoReservaId] FOREIGN KEY ([EstadoReservaId]) REFERENCES [dbo].[EstadoReserva]([Id]),
	CONSTRAINT [FK_PoliticaSancion_RestauranteId] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id])
)
