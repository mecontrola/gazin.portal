using Gazin.Portal.Core.Services;
using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Gazin.Portal.Core.IoC
{
    public class ServiceInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddTransient<IWorkdayOfWeekService, WorkdayOfWeekService>();
        }
    }
}