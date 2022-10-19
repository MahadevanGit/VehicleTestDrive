using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Vtd.Account.RestService.Lib.Domain.DBContext;
using Vtd.Account.RestService.Lib.Domain.Repository;
using Vtd.Account.RestService.Lib.Domain.Services;
using Vtd.Account.RestService.Lib.Infra.Bootstrap;

namespace Account.RestService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //Add common settings file inside AddCombinedConfig method
            Configuration = new ConfigurationBuilder()
                .AddCombinedConfig(env) //Add this using Vtd.Account.RestService.Lib.Infra.Bootstrap for AddCombinedConfig(env)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AccountDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AccountDBContext"),
               sqlOptions =>
               {
                   sqlOptions.EnableRetryOnFailure(
                       maxRetryCount: 5,
                       maxRetryDelay: TimeSpan.FromSeconds(3),
                       errorNumbersToAdd: null);
               }));

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
            
            //addlogsToAzure
            services.AddApplicationInsightsTelemetry((options)=> {
                options.ConnectionString = Configuration["ApplicationInsights:ConnectionString"];
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
            services.AddScoped<IAzureServiceBus, AzureServiceBus>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
