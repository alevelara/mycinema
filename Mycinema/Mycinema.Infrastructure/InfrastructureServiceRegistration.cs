using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Models;
using Mycinema.Infrastructure.Repositories;
using Mycinema.Infrastructure.Repositories.Read;
using System.Data.Common;
using System.Data.SqlClient;

namespace Mycinema.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices( this IServiceCollection services, IConfiguration configuration)
        {
            string uri = configuration.GetSection("TmdbSettings.UrlBase").Value;
            services.AddScoped<DbConnection>(provider =>
            {
                return new SqlConnection(configuration.GetConnectionString("ConnectionString"));
            });

            services.AddScoped(typeof(IAsyncReadRepository<>), typeof(GenericReadRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHttpClient("tmdb", http =>
            {
                http.BaseAddress = new Uri(uri);
            });
            services.Configure<TmdbSettings>(conf => configuration.GetSection("TmdbSettings"));

            return services;
        }
    }
}