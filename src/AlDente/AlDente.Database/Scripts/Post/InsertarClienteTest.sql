GO
INSERT INTO dbo.Cliente(Apellido,Nombre,DNI,Email,Telefono,EstadoClienteId,[Password])
SELECT 'Test' AS Apellido
,'Test' AS Nombre
,1234 AS DNI
,'test@gmail.com' AS Email
,'1234' AS Telefono
,2 AS EstadoClienteId
,'test' AS [Password]
WHERE NOT EXISTS(SELECT 1 FROM dbo.Cliente c WHERE c.Email = 'test@gmail.com' OR c.DNI = 1234)
GO


