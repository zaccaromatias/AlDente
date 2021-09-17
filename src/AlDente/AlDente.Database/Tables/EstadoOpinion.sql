CREATE TABLE [dbo].[EstadoOpinion]
(
	[Id] INT NOT NULL, 
    [Codigo] NVARCHAR(50) NOT NULL, 
    [Descripcion] NVARCHAR(200) NOT NULL, 
    CONSTRAINT [PK_EstadoOpinion_Id] Primary Key ([Id]),
    CONSTRAINT [AK_EstadoOpinion_Codigo] UNIQUE ([Codigo])
)
