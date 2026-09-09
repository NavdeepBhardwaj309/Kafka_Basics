using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyApp.Core.Options;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositoires;

namespace MyApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            // services.AddDbContext<AppDbContext>((provider,options) =>
            // {    // hardcoded dbconnstr
                //options.UseSqlServer("Server=localhost;Database=MyApp.Api.Db;User Id=sa;Password=Nav*0001;TrustServerCertificate=True;");
                //by USING iCONFIGURATION INTERFACE
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            // });
             // as the class is static we cannot add dependeny injection directly, we can use provider and getrequiredservices to add interface and dependecny
            services.AddDbContext<AppDbContext>((provider,options) =>
            {    
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
            });
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //typed client
            services.AddHttpClient("");

            return services;
        }
    }
}