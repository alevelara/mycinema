using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Models;
using Mycinema.Infrastructure.Repositories.Read;
using Mycinema.Infrastructure.Services;
using System.Data.Common;
using System.Data.SqlClient;

namespace Mycinema.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices( this IServiceCollection services, IConfiguration configuration)
        {
            string uri = configuration.GetSection("TmdbSettings:UrlBase").Value;
            services.Configure<TmdbSettings>(configuration.GetSection("TmdbSettings"));
            services.AddScoped<DbConnection>(provider =>
            {
                return new SqlConnection(configuration.GetConnectionString("ConnectionString"));
            });
            services.AddScoped(typeof(IAsyncReadRepository<>), typeof(GenericReadRepository<>));
            services.AddScoped<IMovieGenreReadRepository, MovieGenreReadRepository>();
            services.AddScoped<IMovieReadRepository, MovieReadRepository>();

            services.AddHttpClient<IHttpClient, HttpClientFactory>("tmdb", http =>
            {
                http.BaseAddress = new Uri(uri);
            });

            services.AddTransient<IHttpClient, HttpClientFactory>();
            services.AddTransient<IHttpClientService, HttpClientService>();

            return services;
        }
    }
}