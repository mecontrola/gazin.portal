using Gazin.Portal.Integrations.Jira.Configurations.Extensions;
using MeControla.Core.Configurations;
using MeControla.Core.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gazin.Portal.Core.IoC
{
    public class ConfigurationIInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            var jiraConfiguration = configuration.GetJiraConfiguration();
            services.AddSingleton(jiraConfiguration);
        }
    }
}