using MeControla.Core.Configurations.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Serilog;

namespace Gazin.Portal.RestApi
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                   .LoadApplicationSettings()
                   .UseSerilog((hostContext, loggerConfig) =>
                   {
                       loggerConfig.ReadFrom.Configuration(hostContext.Configuration);
                   }, writeToProviders: true)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>();
                   });
    }
}