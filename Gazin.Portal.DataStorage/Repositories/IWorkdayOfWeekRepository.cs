using Gazin.Portal.Data.Entities;
using MeControla.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gazin.Portal.DataStorage.Repositories
{
    public interface IWorkdayOfWeekRepository : IAsyncRepository<WorkdayOfWeek>
    {
        Task<IList<DayOfWeek>> GetActives();
    }
}