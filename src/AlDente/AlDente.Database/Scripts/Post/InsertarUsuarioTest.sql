GO
INSERT INTO dbo.Usuario([TipoUsuarioId],Apellido,Nombre,DNI,Email,Telefono,EstadoId,[Password])
SELECT 
  1		AS TipoUsuarioId
,'Test' AS Apellido
,'Test' AS Nombre
,1234 AS DNI
,'test@gmail.com' AS Email
,'1234' AS Telefono
,2 AS EstadoId
,'test' AS [Password]
WHERE NOT EXISTS(SELECT 1 FROM dbo.Usuario c WHERE (c.TipoUsuarioId = 1) AND (c.Email = 'test@gmail.com' OR c.DNI = 1234))
GO
INSERT INTO dbo.Usuario([TipoUsuarioId],Apellido,Nombre,DNI,Email,Telefono,EstadoId,[Password])
SELECT 
  2		AS TipoUsuarioId
,'Empleado' AS Apellido
,'Empleado' AS Nombre
,12345 AS DNI
,'empleado@gmail.com' AS Email
,'1234' AS Telefono
,2 AS EstadoId
,'test' AS [Password]
WHERE NOT EXISTS(SELECT 1 FROM dbo.Usuario c WHERE (c.TipoUsuarioId = 2) AND (c.Email = 'empleado@gmail.com' OR c.DNI = 12345))
GO
