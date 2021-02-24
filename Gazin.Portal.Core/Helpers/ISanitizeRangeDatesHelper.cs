using System;
using System.Collections.Generic;

namespace Gazin.Portal.Core.Helpers
{
    public interface ISanitizeRangeDatesHelper
    {
        IList<DateTime> RemoveNotWorkdayAndCommemorativeDates(IList<DateTime> range, IList<DayOfWeek> workdays, IList<DateTime> holidays);
    }
}