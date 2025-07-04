using Microsoft.AspNetCore.Mvc;
using Raptor.Api.Dtos;
using Raptor.Api.Services;

namespace Raptor.Api.Controllers
{
    [ApiController]
    [Route("schema")]
    public class SchemaController(ConsultaService service)
    {
        private readonly ConsultaService _service = service;

        [HttpGet]
        public async Task<ActionResult<List<ConsultaDto>>> ConsultarSchema()
        {
            return await _service.ConsultaBD();
        }
    }
}
