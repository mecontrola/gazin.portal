using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Gazin.Portal.Integrations.Jira.IoC
{
    public class JiraInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IAuthSessionGet, AuthSessionGet>();
            services.TryAddSingleton<IIssueGet, IssueGet>();
            services.TryAddSingleton<IIssueWorklogDelete, IssueWorklogDelete>();
            services.TryAddSingleton<IIssueWorklogGet, IssueWorklogGet>();
            services.TryAddSingleton<IIssueWorklogGetAll, IssueWorklogGetAll>();
            services.TryAddSingleton<IIssueWorklogPost, IssueWorklogPost>();
            services.TryAddSingleton<IIssueWorklogPut, IssueWorklogPut>();
            services.TryAddSingleton<ISearchPost, SearchPost>();
        }
    }
}