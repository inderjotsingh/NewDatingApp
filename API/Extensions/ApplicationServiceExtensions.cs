using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        // convert the generated class ApplicationServiceExtensions as static. 
        // Static means we don't need to create a new instance of this class to use it.

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // services.AddScoped<ITokenService, TokenService>();
            
            services.AddDbContext<DataContext>(options => 
            {
                // options.UseSqlite("Connection string");
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}