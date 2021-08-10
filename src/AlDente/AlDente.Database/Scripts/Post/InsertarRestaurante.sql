GO
INSERT INTO dbo.Restaurante
(
	Id,
    Descripcion,
    Direccion,
    Telefono,
    UrlMenu
)
SELECT 
   1, --Id
	N'AlDente', -- Descripcion - nvarchar(50)
    N'Calle N 2010', -- Direccion - nvarchar(50)
    N'3401412073', -- Telefono - nvarchar(50)
    N'https://drive.google.com/u/0/uc?id=1Y_Frdo2t3oM1Yld4r5Dym8VH-haRxFGW&export=download'  -- UrlMenu - nvarchar(50)
WHERE NOT EXISTS
(
	SELECT 1 FROM dbo.Restaurante r WHERE R.ID = 1
)
GO