CREATE TABLE [dbo].[Opinion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Texto] NVARCHAR(200) NULL,
    [ClienteId] INT NOT NULL,
    [RestauranteId] INT NOT NULL,
    [Calificacion]  INT NOT NULL,
    [EstadoId]  INT NOT NULL,
    [Fecha]     DATETIME NOT NULL,
    [FechaAprovacion]  DATETIME NULL,
    [EmpleadoAprovadorId]   INT  NULL,
    [OpinionPrincipalId]   INT  NULL,
    [RemovidoPor]   INT  NULL,
    [RemovidoFecha]   DATETIME  NULL,
    [RemovidoMotivo]   NVARCHAR(500)  NULL,
    CONSTRAINT [FK_Opinion_Cliente] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Usuario]([Id]), 
    CONSTRAINT [FK_Opinion_Restaurante] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id]), 
    CONSTRAINT [FK_Opinion_EmpleadoAprovador] FOREIGN KEY ([EmpleadoAprovadorId]) REFERENCES [dbo].[Usuario]([Id]), 
    CONSTRAINT [FK_Opinion_RemovidoPor] FOREIGN KEY ([RemovidoPor]) REFERENCES [dbo].[Usuario]([Id]),
    CONSTRAINT [FK_Opinion_OpinionPrincipal] FOREIGN KEY ([OpinionPrincipalId]) REFERENCES [dbo].[Opinion]([Id]),
    CONSTRAINT [FK_Opinion_Estado] FOREIGN KEY ([EstadoId]) REFERENCES [dbo].[EstadoOpinion]([Id]),
    
)
