using FluentValidation.AspNetCore;
using Gazin.Portal.Integrations.Jira.Configurations.Extensions;
using MeControla.Core.Configurations;
using MeControla.Core.Configurations.Extensions;
using MeControla.Core.Configurations.Managers;
using MeControla.Core.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Gazin.Portal.RestApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
                    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });
            services.AddApplicationServices();

            var jiraConfiguration = Configuration.GetJiraConfiguration();
            services.AddSingleton(jiraConfiguration);

            var swaggerConfiguration = Configuration.GetSwaggerConfiguration();
            services.AddSingleton(swaggerConfiguration);

            var jwtConfiguration = Configuration.GetJWTConfiguration();
            services.AddSingleton(jwtConfiguration);

            var jwtManager = new JWTManager(jwtConfiguration);
            services.AddSingleton<IJWTManager>(jwtManager);

            services.AddCors()
                    .AddControllers(opt =>
                    {
                        opt.EnableEndpointRouting = false;
                        opt.Filters.Add<ValidationFilter>();
                    })
                    .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
                    .AddNewtonsoftJson()
                    .AddFluentValidation(opt =>
                    {
                        opt.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    });

            services.AddAppExceptionHandler();
            //services.AddCacheSettings(Configuration.GetRedisCacheConfiguration());
            services.AddAuthenticationSettings(jwtConfiguration, jwtManager);
            services.AddSwaggerSettings(swaggerConfiguration);
            //services.AddApplicationServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseHsts();
            
            app.UseAppExceptionHandler(loggerFactory);
            //app.UseApplicationBuilder(Configuration);

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseAuthenticationSettings(Configuration.GetCorsConfiguration(), Configuration.GetJWTConfiguration());
            app.UseSwaggerSettings(Configuration.GetSwaggerConfiguration());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}