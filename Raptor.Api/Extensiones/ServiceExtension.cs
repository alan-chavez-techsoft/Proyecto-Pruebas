using Microsoft.EntityFrameworkCore;
using Raptor.Api.Infraestructura;
using Raptor.Api.Services;
using Raptor.Dominio.Repositories;

namespace Raptor.Api.Extensiones
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigureContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(
            options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlServer"))
            );
            return services;
        }
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IConsultasRepository, ConsultasRepository>();
            services.AddScoped<ConsultaService>();
            return services;
        }
    }
}
