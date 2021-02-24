using Gazin.Portal.Data.Entities;
using MeControla.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gazin.Portal.DataStorage.Repositories
{
    public class HolidayRepository : BaseAsyncRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(IDbAppContext context)
            : base(context, context.Holidays)
        { }

        public async Task<IList<DateTime>> GetOnlyDates()
            => await dbSet.AsNoTracking()
                          .Select(itm => itm.Date)
                          .ToListAsync();
    }
}