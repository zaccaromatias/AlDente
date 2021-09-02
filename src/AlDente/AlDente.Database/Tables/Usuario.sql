CREATE TABLE [dbo].[Usuario]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TipoUsuarioId] INT NOT NULL,
    [Email] NVARCHAR(320) NOT NULL,    
    [Nombre] NVARCHAR(500) NOT NULL, 
    [Apellido] NVARCHAR(500) NOT NULL, 
    [DNI] INT NOT NULL, 
    [Password] NVARCHAR(200) NULL, 
    [EstadoId] INT NOT NULL, 
    [Telefono] NVARCHAR(200) NOT NULL,     
    [UsuarioCreadorId] INT NULL, --Empleado que crea al cliente
    CONSTRAINT [FK_Usuario_Estado] FOREIGN KEY ([EstadoId]) REFERENCES [dbo].[EstadoUsuario]([Id]), 
    CONSTRAINT [FK_Usuario_UsuarioCreador] FOREIGN KEY ([UsuarioCreadorId]) REFERENCES [dbo].[Usuario]([Id]), 
    CONSTRAINT [AK_Usuario_Email] UNIQUE ([Email]),    
    CONSTRAINT [AK_Usuario_DNI] UNIQUE ([DNI])   
)
