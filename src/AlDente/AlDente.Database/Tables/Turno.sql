CREATE TABLE [dbo].[Turno]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [HoraInicio] TIME NULL, 
    [HoraFin] TIME NULL,    
    [DiaLaboralId] INT NOT NULL,     
    CONSTRAINT [FK_Turno_DiaLaboral] FOREIGN KEY ([DiaLaboralId]) REFERENCES [DiaLaboral]([Id])
)
