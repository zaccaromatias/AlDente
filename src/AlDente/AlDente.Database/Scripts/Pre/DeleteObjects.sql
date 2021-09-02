IF NOT EXISTS (SELECT 1 FROM sys.columns c  INNER JOIN sys.tables t ON t.object_id = c.object_id WHERE c.name= 'DiaLaboralId' AND t.name='Turno')
BEGIN
	DELETE dbo.ReservaMesa;
	DELETE dbo.Reserva;
	DELETE dbo.Turno;
END



IF NOT EXISTS (SELECT 1 FROM sys.tables t  WHERE t.name='TipoUsuario')
BEGIN
	DROP TABLE IF EXISTS dbo.ReservaMesa
	DROP TABLE IF EXISTS dbo.Reserva
	DROP TABLE IF EXISTS dbo.Opinion
	DROP TABLE IF EXISTS dbo.Sancion;
	DROP TABLE IF EXISTS dbo.Beneficio;
	DROP TABLE IF EXISTS dbo.Cliente;
	DROP TABLE IF EXISTS dbo.EstadoCliente;
	DROP TABLE IF EXISTS dbo.Empleado;
	DROP TABLE IF EXISTS dbo.EstadoEmpleado;
END