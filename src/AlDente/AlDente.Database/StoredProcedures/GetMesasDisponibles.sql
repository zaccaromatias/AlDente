CREATE PROCEDURE [dbo].[GetMesasDisponibles]
	@Fecha DateTime,
	@TurnoId int
AS
BEGIN
	--Mesas Disponibles en una fecha y turno
	SELECT m.Id ,m.Capacidad
	FROM dbo.Mesa m
	WHERE NOT EXISTS
	(
		SELECT 1
		FROM dbo.ReservaMesa rm
		INNER JOIN dbo.Reserva r
			ON r.Id = rm.ReservaId
		WHERE rm.MesaId = m.Id
		AND CAST(r.FechaReserva AS Date) = CAST(@Fecha AS Date)
		AND r.TurnoId = @TurnoId
	)
END
