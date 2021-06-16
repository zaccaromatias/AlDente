CREATE TABLE [dbo].[Reservas]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FechaCreacion] DATETIME NOT NULL, 
    [FechaReserva] DATETIME NOT NULL, 
    [Notas] NVARCHAR(50) NOT NULL, 
    [FechaAsistencia] DATETIME NOT NULL, 
    [CantidadComensales] NCHAR(10) NOT NULL
)
