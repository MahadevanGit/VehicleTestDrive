using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.RestService.Lib.Domain.DBContext;
using Vehicle.RestService.Lib.Domain.Models;
using Vehicle.RestService.Lib.Infra.Bootstrap;

namespace Vehicle.RestService
{
    public class Startup
    {
        [Obsolete]
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //Add common settings file inside AddCombinedConfig method
            Configuration = new ConfigurationBuilder()
                .AddCombinedConfig(env) //Add this using Vehicle.RestService.Lib.Infra.Bootstrap for AddCombinedConfig(env)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication(
                authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(
                jwtBearerOptions =>
                {
                    var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,

                        //for local test
                        ValidateIssuer = false,
                        ValidateAudience = false

                        //for local prod
                        //ValidateIssuer = true,
                        //ValidateAudience = true
                    };
                });

            services.AddDbContext<VehicleDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VehicleDBContext"),
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(3),
                        errorNumbersToAdd: null);
                }));

            services.AddScoped<IVehicleService, VehicleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // note: UseAuthentication is always top of UseAuthorization
            app.UseAuthentication();
            // note: UseAuthorization is always below of UseAuthentication
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
