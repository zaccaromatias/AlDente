using AlDente.Contracts.Reservas;
using AlDente.DataAccess.Core;
using AlDente.Entities.Reservas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Reservas
{
    public interface IReservaRepository : IRepository<Reserva>
    {
    }
}
