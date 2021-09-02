DROP TABLE IF EXISTS #EstadoUsuario;
CREATE TABLE #EstadoUsuario(Id INT, Codigo NVARCHAR(50), Descripcion NVARCHAR(200));
INSERT INTO #EstadoUsuario(Id,Codigo,Descripcion)
VALUES
(1,'Nuevo','Nuevo'), 
(2,'Activo','Activo'), 
(3,'Suspendido','Suspendido'),
(4,'Inactivo','Inactivo'); 

INSERT INTO dbo.EstadoUsuario(Id,Codigo,Descripcion)
SELECT x.Id
,x.Codigo
,x.Descripcion
FROM #EstadoUsuario x
WHERE NOT EXISTS (SELECT 1 FROM dbo.EstadoUsuario e WHERE e.Id = x.Id);

UPDATE e
SET e.Codigo = x.Codigo
,e.Descripcion = x.Descripcion
FROM dbo.EstadoUsuario e
INNER JOIN #EstadoUsuario x
ON x.Id = e.Id;

DELETE e
FROM dbo.EstadoUsuario e
WHERE NOT EXISTS (SELECT 1 FROM #EstadoUsuario x where x.Id = e.Id);
GO