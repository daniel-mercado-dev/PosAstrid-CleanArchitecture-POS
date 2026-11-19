using Microsoft.Extensions.DependencyInjection;
using PosAstrid.Domain.Repositories;
using PosAstrid.Infrastructure.Repositories;

namespace PosAstrid.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IVentaRepository, VentaRepository>();
            return services;
        }
    }
}