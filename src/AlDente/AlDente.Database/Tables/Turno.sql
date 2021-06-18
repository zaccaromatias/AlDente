CREATE TABLE [dbo].[Turno]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [HoraInicio] TIME NULL, 
    [HoraFin] TIME NULL,    
    [RestauranteId] INT NOT NULL,     
    CONSTRAINT [FK_Turno_Restaurante] FOREIGN KEY ([RestauranteId]) REFERENCES [Restaurante]([Id])
)
