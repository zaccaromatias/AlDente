DROP TABLE IF EXISTS #EstadosClientes;
CREATE TABLE #EstadosClientes(Id INT, Codigo NVARCHAR(50), Descripcion NVARCHAR(200));
INSERT INTO #EstadosClientes(Id,Codigo,Descripcion)
VALUES
(1,'Nuevo','Nuevo'), --Estado que queda cuando es creado por el recepcionista y el usuario debe entrar para setear su password
(2,'Activo','Activo'), -- Cliente puede operar
(3,'Suspendido','Suspendido'),--Cliente Suspendido
(4,'Inactivo','Inactivo'); --Recepcionista da de baja al cliente

INSERT INTO dbo.EstadoCliente(Id,Codigo,Descripcion)
SELECT x.Id
,x.Codigo
,x.Descripcion
FROM #EstadosClientes x
WHERE NOT EXISTS (SELECT 1 FROM dbo.EstadoCliente e WHERE e.Id = x.Id);

UPDATE e
SET e.Codigo = x.Codigo
,e.Descripcion = x.Descripcion
FROM dbo.EstadoCliente e
INNER JOIN #EstadosClientes x
ON x.Id = e.Id;

DELETE e
FROM dbo.EstadoCliente e
WHERE NOT EXISTS (SELECT 1 FROM #EstadosClientes x where x.Id = e.Id);

GO
DROP TABLE IF EXISTS [dbo].[dbo.Test]
GO
DROP TABLE IF EXISTS dbo.Reservas
GO
DROP TABLE IF EXISTS dbo.Politica


