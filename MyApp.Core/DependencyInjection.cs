using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyApp.Core.Options;
using Microsoft.Extensions.Options;

namespace MyApp.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.sectionName));
            return services;
        }
    }
}