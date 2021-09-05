using System.Net;
using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using API.Extensions;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TO reduce the code in startup.cs class we created Extension methods (See commands.txt).
            // In ApplicationServiceExtensions.cs class we move following code so that our startup class's code is reduced. 
            // -------------------------------------START--------------------------------------------

            // // AddScoped is used when an API's scope is to be maintained only until it is being used.
            // services.AddScoped<ITokenService, TokenService>();
            // services.AddDbContext<DataContext>(options => 
            // {
            //     // options.UseSqlite("Connection string");
            //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            // });
            // -------------------------------------END--------------------------------------------

            // To tell the code that fetch from ApplicationServiceExtensions, we write following code:
            services.AddApplicationServices(Configuration);
            
            services.AddControllers();
            services.AddCors(); 
            
            // TO reduce the code in startup.cs class we created Extension methods (See commands.txt).
            // In IdentityServiceExtensions.cs class we move following code so that our startup class's code is reduced. 
            // -------------------------------------START--------------------------------------------
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options => {
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //     });
            // -------------------------------------END--------------------------------------------

            // To tell the code that fetch from IdentityServiceExtensions, we write following code:
            services.AddIdentityServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

            // app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();   // Must come before UseAuthorization

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
