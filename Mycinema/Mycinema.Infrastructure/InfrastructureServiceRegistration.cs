using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mycinema.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection RegisterConfiguration( this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}