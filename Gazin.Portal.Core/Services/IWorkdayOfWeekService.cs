using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Services
{
    public interface IWorkdayOfWeekService
    {
        Task<IList<DateTime>> GetAvailableDays(DateTime startDate, DateTime endDate);
    }
}