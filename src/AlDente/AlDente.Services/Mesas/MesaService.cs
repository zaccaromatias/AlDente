using AlDente.Contracts.Mesas;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Mesas;
using AlDente.Entities.Mesas;
using AlDente.Services.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Mesas
{
    public class MesaService : BaseService, IMesaService
    {
        private IMesaRepository mesaRepository;

        public MesaService(IUnitOfWork unitOfWork, IMesaRepository mesaRepository)
            : base(unitOfWork)
        {
            mesaRepository.Attach(this.unitOfWork);
            this.mesaRepository = mesaRepository;
        }


        public async Task<IEnumerable<MesaDTO>> GetAll()
        {
            var mesas = await mesaRepository.GetAllAsync();
            return mesas.Select(x => new MesaDTO
            {
                Id = x.Id,
                Capacidad = x.Capacidad
            });
        }

        public async Task Create(MesaDTO mesaDto)
        {
            await Try(async () =>
            {
                await mesaRepository.AddAsync(new Mesa
                {
                    Capacidad = mesaDto.Capacidad
                });
            });
        }

        public async Task Delete(int id)
        {
            await mesaRepository.DeleteAsync(id);
        }

        public async Task Update(MesaDTO mesaDto)
        {
            await Try(async () =>
            {
                await mesaRepository.UpdateAsync(new Mesa
                {
                    Id = mesaDto.Id,
                    Capacidad = mesaDto.Capacidad
                });
            });
        }

        public async Task<IEnumerable<CombinacionDTO>> GetMesasDisponibles(ReservaParamDTO parameter)
        {
            return await this.Try(async () =>
            {
                IEnumerable<MesaDTO> mesasDisponibles = (await mesaRepository.GetMesasDisponibles(parameter.Fecha, parameter.TurnoId)).Select(x => new MesaDTO { Capacidad = x.Capacidad, Id = x.Id });
                List<CombinacionDTO> combinaciones = new List<CombinacionDTO>();
                CargarLasMesasConCapacidadParaLosComensales(parameter, mesasDisponibles, combinaciones);
                foreach (var mesa1 in mesasDisponibles.Where(x => x.Capacidad < parameter.Comensales))
                {
                    CargarCombinacionDeDosMesasConCapacidadParaLosComensales(parameter, mesasDisponibles, combinaciones, mesa1);
                    CargarCombinacionDeTresMesasConCapacidadParaLosComensales(parameter, mesasDisponibles, combinaciones, mesa1);
                }
                if (!combinaciones.Any())
                    return combinaciones;
                var minimo = combinaciones.Select(x => x.Mesas.Sum(m => m.Capacidad)).Min();
                //Devolvemos solo la minima combinacion con menor capacidad
                var menorCombinacion = combinaciones.First(x => x.Mesas.Sum(m => m.Capacidad) == minimo);
                return new List<CombinacionDTO> { menorCombinacion };
            });
        }

        private static void CargarCombinacionDeTresMesasConCapacidadParaLosComensales(ReservaParamDTO parameter, IEnumerable<MesaDTO> mesasDisponibles, List<CombinacionDTO> combinaciones, MesaDTO mesa1)
        {
            //Buscamos Combinacion de 3 mesas 
            var duosDeMesasQueNoCumplen = mesasDisponibles
           .Where(mesa2 => mesa1.Capacidad + mesa2.Capacidad < parameter.Comensales)
           .Where(mesa2 => mesa2.Id != mesa1.Id)
           .Select(mesa2 => new { Mesa1 = mesa1, Mesa2 = mesa2, Capacidad = mesa1.Capacidad + mesa2.Capacidad });

            foreach (var duoDeMesas in duosDeMesasQueNoCumplen)
            {
                var tercerasMesas = mesasDisponibles
                .Where(mesa3 => duoDeMesas.Capacidad + mesa3.Capacidad >= parameter.Comensales)
                .Where(mesa3 => mesa3.Id != duoDeMesas.Mesa1.Id)
                .Where(mesa3 => mesa3.Id != duoDeMesas.Mesa2.Id)
                .Where(mesa3 => !combinaciones.Any(comb => comb.Mesas.Any(me => me.Id == mesa3.Id) && comb.Mesas.Any(me => me.Id == duoDeMesas.Mesa1.Id) && comb.Mesas.Any(me => me.Id == duoDeMesas.Mesa2.Id)))
                .Where(mesa3 => !combinaciones.Any(comb => comb.Mesas.Any(me => me.Capacidad == mesa3.Capacidad) && comb.Mesas.Any(me => me.Capacidad == duoDeMesas.Mesa1.Capacidad) && comb.Mesas.Any(me => me.Capacidad == duoDeMesas.Mesa2.Capacidad)));

                combinaciones.AddRange(tercerasMesas.Select(mesa3 => new CombinacionDTO
                {
                    Mesas = new List<MesaDTO>
                            {
                                duoDeMesas.Mesa1,
                                duoDeMesas.Mesa2,
                                mesa3
                            }
                }));

            }
        }

        private static void CargarCombinacionDeDosMesasConCapacidadParaLosComensales(ReservaParamDTO parameter, IEnumerable<MesaDTO> mesasDisponibles, List<CombinacionDTO> combinaciones, MesaDTO mesa1)
        {
            //Buscamos Combinacion con una segunda mesa distinta y que ya no este cargada en otra combinacion
            var duosDeMesasQueSiCumplen = mesasDisponibles
            .Where(mesa2 => mesa1.Capacidad + mesa2.Capacidad >= parameter.Comensales)
            .Where(mesa2 => mesa2.Id != mesa1.Id)
            .Where(mesa2 => !combinaciones.Any(comb => comb.Mesas.Any(me => me.Id == mesa2.Id) && comb.Mesas.Any(me => me.Id == mesa1.Id)))
            .Where(mesa2 => !combinaciones.Any(comb => comb.Mesas.Any(me => me.Capacidad == mesa2.Capacidad) && comb.Mesas.Any(me => me.Capacidad == mesa1.Capacidad)));

            combinaciones.AddRange(duosDeMesasQueSiCumplen.Select(mc => new CombinacionDTO
            {
                Mesas = new List<MesaDTO>
                        {
                            mesa1,mc
                        }
            }));
        }

        private static void CargarLasMesasConCapacidadParaLosComensales(ReservaParamDTO parameter, IEnumerable<MesaDTO> mesasDisponibles, List<CombinacionDTO> combinaciones)
        {
            //Agrega Si ya no hay agregadas con la misma capacidad
            foreach (var item in mesasDisponibles.Where(mesa1 => mesa1.Capacidad >= parameter.Comensales))
            {
                if (!combinaciones.Any(x => x.Mesas.Any(me => me.Capacidad == item.Capacidad)))
                {
                    combinaciones.Add(new CombinacionDTO
                    {
                        Mesas = new List<MesaDTO>
                    {
                        item
                    }
                    });
                }
            }
        }
    }
}
