using FluentValidation;
using Gazin.Portal.Core.Validators;
using Gazin.Portal.Data.Dtos.Inputs;
using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Gazin.Portal.Core.IoC
{
    public class ValidatorInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IValidator<CredentialInputDto>, CredentialInputDtoValidator>();
            services.TryAddSingleton<IValidator<WorklogReportInputDto>, WorklogReportInputDtoValidator>();
        }
    }
}