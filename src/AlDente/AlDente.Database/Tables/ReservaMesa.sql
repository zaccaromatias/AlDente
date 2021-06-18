CREATE TABLE [dbo].[ReservaMesa]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ReservaId] INT NOT NULL,
	[MesaId]	INT NOT NULL,
	CONSTRAINT [FK_ReservaMesa_Reserva] FOREIGN KEY ([ReservaId]) REFERENCES [dbo].[Reserva]([Id]),
	CONSTRAINT [FK_ReservaMesa_Mesa] FOREIGN KEY ([MesaId]) REFERENCES [dbo].[Mesa]([Id])
)
