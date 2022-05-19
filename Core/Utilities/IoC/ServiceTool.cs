using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) //dotnet servicelerini al ve build et
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}