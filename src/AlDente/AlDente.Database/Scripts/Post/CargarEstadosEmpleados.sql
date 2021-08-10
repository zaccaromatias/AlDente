DROP TABLE IF EXISTS #EstadosEmpleado;
CREATE TABLE #EstadosEmpleado(Id INT, Codigo NVARCHAR(50), Descripcion NVARCHAR(200));
INSERT INTO #EstadosEmpleado(Id,Codigo,Descripcion)
VALUES
(1,'Activo','Activo'), 
(2,'Inactivo','Inactivo'); 

INSERT INTO dbo.EstadoEmpleado(Id,Codigo,Descripcion)
SELECT x.Id
,x.Codigo
,x.Descripcion
FROM #EstadosEmpleado x
WHERE NOT EXISTS (SELECT 1 FROM dbo.EstadoEmpleado e WHERE e.Id = x.Id);

UPDATE e
SET e.Codigo = x.Codigo
,e.Descripcion = x.Descripcion
FROM dbo.EstadoEmpleado e
INNER JOIN #EstadosEmpleado x
ON x.Id = e.Id;

DELETE e
FROM dbo.EstadoEmpleado e
WHERE NOT EXISTS (SELECT 1 FROM #EstadosEmpleado x where x.Id = e.Id);
GO