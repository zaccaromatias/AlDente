CREATE TABLE [dbo].[Beneficio]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,     
    [Fecha] DATETIME NOT NULL,
    [TipoBeneficioId] INT NOT NULL,
    [ClienteId] INT NOT NULL,
    [RestauranteId] INT NOT NULL,
    [Aplicado]         BIT NOT NULL DEFAULT(0),
    [FechaAplicado]         DATETIME NULL,
    [FechaPedidoDeAplicacion]         DATETIME NULL,
    [Codigo]        NVARCHAR(50)        NULL
    CONSTRAINT [FK_Beneficio_Cliente] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Usuario]([Id]), 
    CONSTRAINT [FK_Beneficio_TipoSancion] FOREIGN KEY ([TipoBeneficioId]) REFERENCES [dbo].[TipoBeneficio]([Id]),
    CONSTRAINT [FK_Beneficio_RestauranteId] FOREIGN KEY ([RestauranteId]) REFERENCES [dbo].[Restaurante]([Id])
    
)
