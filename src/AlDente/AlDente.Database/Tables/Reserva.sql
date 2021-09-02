CREATE TABLE [dbo].[Reserva]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Codigo] NVARCHAR(50)   NOT NULL,
    [FechaCreacion] DATETIME NOT NULL, 
    [FechaReserva] DATETIME NOT NULL, 
    [FechaAsistencia] DATETIME NULL, 
    [FechaCancelacion] DATETIME NULL,
    [MotivoCancelacion] NVARCHAR(200) NULL,    
    [CantidadComensales] INT NOT NULL,
    [EstadoReservaId]   INT NOT NULL,
    [ClienteId]         INT NOT NULL,
    [RestauranteId]     INT NOT NULL,
    [EmpleadoId]        INT NULL,
    [TurnoId]           INT NOT NULL
    CONSTRAINT [AK_Reserva_Codigo] UNIQUE ([Codigo]),
    CONSTRAINT [FK_Reserva_EstadoReserva] FOREIGN KEY ([EstadoReservaId]) REFERENCES [dbo].[EstadoReserva]([Id]), 
    CONSTRAINT [FK_Reserva_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Usuario]([Id]),
    CONSTRAINT [FK_Reserva_RestauranteId] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id]),
    CONSTRAINT [FK_Reserva_EmpleadoId] FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Usuario]([Id]),
    CONSTRAINT [FK_Reserva_TurnoId] FOREIGN KEY ([TurnoId]) REFERENCES [dbo].[Turno]([Id]),
)
