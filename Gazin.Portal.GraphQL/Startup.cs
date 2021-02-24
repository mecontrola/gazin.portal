using Gazin.Portal.GraphQL.Configurations;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gazin.Portal.GraphQL
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
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();

            services.AddLogging(builder => builder.AddConsole());
            services.AddHttpContextAccessor();

            //services.AddTransient<IHelloWorldBusiness, HelloWorldBusiness>();

            services.AddGraphQL(opt => opt.EnableMetrics = true)
                    .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                    .AddSystemTextJson()//(deserializerSettings => { }, serializerSettings => { })
                    .AddWebSockets()
                    .AddDataLoader()
                    .AddGraphTypes(typeof(PortalSchema));

            services.AddSingleton<PortalQuery>();
            services.AddSingleton<PortalSchema>();
            services.AddSingleton<PortalMutation>();
        }

        private const string PATH_API = "/graphql";

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });

            app.UseWebSockets();
            app.UseGraphQL<PortalSchema>(PATH_API);
            app.UseGraphQLWebSockets<PortalSchema>(PATH_API);
        }
    }
}