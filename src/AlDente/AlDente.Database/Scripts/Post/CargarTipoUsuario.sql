DROP TABLE IF EXISTS #TipoUsuario;
CREATE TABLE #TipoUsuario(Id INT, Codigo NVARCHAR(50), Descripcion NVARCHAR(200));
INSERT INTO #TipoUsuario(Id,Codigo,Descripcion)
VALUES
(1,'Cliente','Cliente'), 
(2,'Empleado','Empleado'); 

INSERT INTO dbo.TipoUsuario(Id,Codigo,Descripcion)
SELECT x.Id
,x.Codigo
,x.Descripcion
FROM #TipoUsuario x
WHERE NOT EXISTS (SELECT 1 FROM dbo.TipoUsuario e WHERE e.Id = x.Id);

UPDATE e
SET e.Codigo = x.Codigo
,e.Descripcion = x.Descripcion
FROM dbo.#TipoUsuario e
INNER JOIN #TipoUsuario x
ON x.Id = e.Id;

DELETE e
FROM dbo.#TipoUsuario e
WHERE NOT EXISTS (SELECT 1 FROM #TipoUsuario x where x.Id = e.Id);
GO