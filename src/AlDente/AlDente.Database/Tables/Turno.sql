CREATE TABLE [dbo].[Turno]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [HoraInicio] DATETIME NULL, 
    [HoraFin] DATETIME NULL,
    [ReservaId] INT NOT NULL, 
    [RestauranteId] INT NOT NULL, 
    CONSTRAINT [FK_Turno_Reserva] FOREIGN KEY ([ReservaId]) REFERENCES [Reservas]([Id]), 
    CONSTRAINT [FK_Turno_Restaurante] FOREIGN KEY ([RestauranteId]) REFERENCES [Restaurante]([Id]), 
)
