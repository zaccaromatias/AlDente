CREATE TABLE [dbo].[Politica]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Descripcion] NVARCHAR(200) NOT NULL, 
	[TipoSancionId]  INT NOT NULL,
    [NumeroMaximo] INT NOT NULL,
	[Periodo] INT NOT NULL,
	[EstadoReservaId] INT NOT NULL,
	[RestauranteId] INT NOT NULL,
	CONSTRAINT [FK_Politica_TipoSancion] FOREIGN KEY ([TipoSancionId]) REFERENCES [dbo].[Cliente]([Id]),
	CONSTRAINT [FK_Politica_EstadoReservaId] FOREIGN KEY ([EstadoReservaId]) REFERENCES [dbo].[EstadoReserva]([Id]),
	CONSTRAINT [FK_Politica_RestauranteId] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id])
)
