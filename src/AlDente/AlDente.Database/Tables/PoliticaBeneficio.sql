CREATE TABLE [dbo].[PoliticaBeneficio]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Descripcion] NVARCHAR(200) NOT NULL, 
	[TipoBeneficioId]  INT NOT NULL,
    [NumeroMinimo] INT NOT NULL,
	[Periodo] INT NOT NULL,
	[EstadoReservaId] INT NOT NULL,
	[RestauranteId] INT NOT NULL,
	CONSTRAINT [FK_PoliticaBeneficio_TipoBeneficio] FOREIGN KEY ([TipoBeneficioId]) REFERENCES [dbo].[TipoBeneficio]([Id]),
	CONSTRAINT [FK_PoliticaBeneficio_EstadoReservaId] FOREIGN KEY ([EstadoReservaId]) REFERENCES [dbo].[EstadoReserva]([Id]),
	CONSTRAINT [FK_PoliticaBeneficio_RestauranteId] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id])
)
