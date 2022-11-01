using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Mycinema.Application.Behaviours;
using Mycinema.Application.Services.MovieRecommendations;
using Mycinema.Application.Services.TvShowRecommendations;
using System.Reflection;

namespace Mycinema.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IGetMovieRecommendation, GetMovieRecommendation>();
            services.AddTransient<IGetTVShowRecommendation, GetTVShowRecommendation>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}