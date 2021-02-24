using Gazin.Portal.Core.Helpers;
using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Gazin.Portal.Core.IoC
{
    public class HelperInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IGenerateRangeDatesHelper, GenerateRangeDatesHelper>();
            services.TryAddSingleton<ISanitizeRangeDatesHelper, SanitizeRangeDatesHelper>();
        }
    }
}