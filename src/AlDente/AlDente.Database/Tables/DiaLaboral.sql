CREATE TABLE [dbo].[DiaLaboral]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DiaDeLaSemana] INT NOT NULL, 
    [HoraInicio] TIME NOT NULL, 
    [HoraFin] TIME NOT NULL, 
    [RestauranteId] INT NOT NULL
    CONSTRAINT [FK_DiaLaboral_Restaurante] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id]), 
    CONSTRAINT [AK_DiaLaboral_DiaDeLaSemana] UNIQUE (DiaDeLaSemana)

)
