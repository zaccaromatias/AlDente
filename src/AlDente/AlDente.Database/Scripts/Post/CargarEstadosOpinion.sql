DROP TABLE IF EXISTS #EstadoOpinion;
CREATE TABLE #EstadoOpinion(Id INT, Codigo NVARCHAR(50), Descripcion NVARCHAR(200));
INSERT INTO #EstadoOpinion(Id,Codigo,Descripcion)
VALUES
(1,'Nuevo','Nuevo'), 
(2,'Publicado','Publicado'), 
(3,'Removido','Removido'),
(4,'Inapropiado','Inapropiado');


INSERT INTO dbo.EstadoOpinion(Id,Codigo,Descripcion)
SELECT x.Id
,x.Codigo
,x.Descripcion
FROM #EstadoOpinion x
WHERE NOT EXISTS (SELECT 1 FROM dbo.EstadoOpinion e WHERE e.Id = x.Id);

UPDATE e
SET e.Codigo = x.Codigo
,e.Descripcion = x.Descripcion
FROM dbo.EstadoOpinion e
INNER JOIN #EstadoOpinion x
ON x.Id = e.Id;

DELETE e
FROM dbo.EstadoOpinion e
WHERE NOT EXISTS (SELECT 1 FROM #EstadoOpinion x where x.Id = e.Id);
GO