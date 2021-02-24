using MeControla.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Gazin.Portal.Integrations.Jira.Configurations.Extensions
{
    public static class LoadConfigurationExtension
    {
        public static IJiraConfiguration GetJiraConfiguration(this IConfiguration configuration)
            => configuration.Load<JiraConfiguration>();
    }
}