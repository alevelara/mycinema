﻿using Microsoft.Extensions.DependencyInjection;

namespace Mycinema.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}