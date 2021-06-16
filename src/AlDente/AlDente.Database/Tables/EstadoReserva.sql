CREATE TABLE [dbo].[EstadoReserva]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Descripcion] NVARCHAR(50) NULL, 
    [ReservaId] INT NULL
    CONSTRAINT [FK_EstadoReserva_Reserva] FOREIGN KEY ([ReservaId]) REFERENCES [Reservas]([Id]), 
)
