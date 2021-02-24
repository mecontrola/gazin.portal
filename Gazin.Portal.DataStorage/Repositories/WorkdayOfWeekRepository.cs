using Gazin.Portal.Data.Entities;
using MeControla.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gazin.Portal.DataStorage.Repositories
{
    public class WorkdayOfWeekRepository : BaseAsyncRepository<WorkdayOfWeek>, IWorkdayOfWeekRepository
    {
        public WorkdayOfWeekRepository(IDbAppContext context)
            : base(context, context.WorkdayOfWeeks)
        { }

        public async Task<IList<DayOfWeek>> GetActives()
            => await dbSet.AsNoTracking()
                          .Where(itm => itm.Active)
                          .Select(itm => itm.DayOfWeek)
                          .ToListAsync();
    }
}