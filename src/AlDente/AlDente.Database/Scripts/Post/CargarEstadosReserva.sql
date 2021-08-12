DROP TABLE IF EXISTS #EstadosReserva;
CREATE TABLE #EstadosReserva(Id INT, Codigo NVARCHAR(50), Descripcion NVARCHAR(200));
INSERT INTO #EstadosReserva(Id,Codigo,Descripcion)
VALUES
(1,'Asistida','Asistida'), 
(2,'NoAsistida','No Asistida'), 
(3,'Pendiente','Pendiente'),
(4,'Cancelada','Cancelada'); 

INSERT INTO dbo.EstadoReserva(Id,Codigo,Descripcion)
SELECT x.Id
,x.Codigo
,x.Descripcion
FROM #EstadosReserva x
WHERE NOT EXISTS (SELECT 1 FROM dbo.EstadoReserva e WHERE e.Id = x.Id);

UPDATE e
SET e.Codigo = x.Codigo
,e.Descripcion = x.Descripcion
FROM dbo.EstadoReserva e
INNER JOIN #EstadosReserva x
ON x.Id = e.Id;

DELETE e
FROM dbo.EstadoReserva e
WHERE NOT EXISTS (SELECT 1 FROM #EstadosReserva x where x.Id = e.Id);
GO