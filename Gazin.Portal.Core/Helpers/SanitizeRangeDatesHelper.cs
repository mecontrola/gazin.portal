using System;
using System.Collections.Generic;
using System.Linq;

namespace Gazin.Portal.Core.Helpers
{
    public class SanitizeRangeDatesHelper : ISanitizeRangeDatesHelper
    {
        public IList<DateTime> RemoveNotWorkdayAndCommemorativeDates(IList<DateTime> range,
                                                                     IList<DayOfWeek> workdays,
                                                                     IList<DateTime> holidays)
            => range.Where(itm => workdays.Contains(itm.DayOfWeek))
                    .Where(itm => !holidays.Any(date => date.Day.Equals(itm.Day)
                                                     && date.Month.Equals(itm.Month)))
                    .ToList();
    }
}