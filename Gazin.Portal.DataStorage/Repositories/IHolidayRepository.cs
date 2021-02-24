using Gazin.Portal.Data.Entities;
using MeControla.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gazin.Portal.DataStorage.Repositories
{
    public interface IHolidayRepository : IAsyncRepository<Holiday>
    {
        Task<IList<DateTime>> GetOnlyDates();
    }
}