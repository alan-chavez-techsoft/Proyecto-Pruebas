using Raptor.Api.Dtos;
using Raptor.Dominio.Repositories;

namespace Raptor.Api.Services
{
    public class ConsultaService(IConsultasRepository repo)
    {
        private readonly IConsultasRepository _repo = repo;
        public async Task<List<ConsultaDto>> ConsultaBD()
        {
            var tabla = await _repo.ObtenerTablas();
            var columnas = await _repo.ObtenerColumnas();
            var pk = await _repo.ObtenerPrimaryKeys();
            var fk = await _repo.ObtenerForeignKeys();
            var triggers = await _repo.ObtenerTriggers();
            var indices = await _repo.ObtenerIndices();



            //Temporal para que no de error
            return [new ConsultaDto()];
        }
    }
}
