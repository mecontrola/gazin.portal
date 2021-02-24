using Gazin.Portal.Core.Business;
using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Gazin.Portal.Core.IoC
{
    public class BusinessInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddTransient<IAuthorizationBusiness, AuthorizationBusiness>();
            services.TryAddTransient<IIssueBusiness, IssueBusiness>();
            services.TryAddTransient<IIssueWorklogBusiness, IssueWorklogBusiness>();
            services.TryAddTransient<IRefreshTokenBusiness, RefreshTokenBusiness>();
            services.TryAddTransient<IReportBusiness, ReportBusiness>();
        }
    }
}