using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Infrastructure.Repositories;
using Mycinema.Infrastructure.Repositories.Read;
using System.Data.Common;
using System.Data.SqlClient;

namespace Mycinema.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection RegisterConfiguration( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbConnection>(provider =>
            {
                return new SqlConnection(configuration.GetConnectionString("ConnectionString"));
            });

            services.AddScoped(typeof(IAsyncReadRepository<>), typeof(GenericReadRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}