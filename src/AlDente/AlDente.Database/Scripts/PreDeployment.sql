/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS (SELECT 1 FROM sys.columns c  INNER JOIN sys.tables t ON t.object_id = c.object_id WHERE c.name= 'DiaLaboralId' AND t.name='Turno')
BEGIN
	DELETE dbo.ReservaMesa;
	DELETE dbo.Reserva;
	DELETE dbo.Turno;
END