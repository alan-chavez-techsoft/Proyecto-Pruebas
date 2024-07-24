using Grpc.Core;

namespace GRPCServer.Services
{
    public class PruebaService : PruebaGRPC.PruebaGRPCBase
    {
        private readonly ILogger<PruebaService> _logger;
        public PruebaService(ILogger<PruebaService> logger)
        {
            _logger = logger;
        }
        public override Task<PruebaResponse> GetPrueba(PruebaRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PruebaResponse
            {
                Message = $"Hola {request.Name}"
            });
        }
    }
}
