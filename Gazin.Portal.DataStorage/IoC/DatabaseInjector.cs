using Gazin.Portal.DataStorage.Repositories;
using MeControla.Core.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Gazin.Portal.DataStorage.IoC
{
    public class DatabaseInjector : IInjector
    {
        private const string DATABASE_NAME = "LocalDB";

        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddDbContext<DbAppContext>(opt => opt.UseInMemoryDatabase(databaseName: DATABASE_NAME));
            services.TryAddScoped<IDbAppContext, DbAppContext>();

            services.TryAddTransient<IHolidayRepository, HolidayRepository>();
            services.TryAddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.TryAddTransient<IWorkdayOfWeekRepository, WorkdayOfWeekRepository>();
        }
    }
}