using System;
using System.Collections.Generic;
using System.Linq;

namespace Gazin.Portal.Core.Helpers
{
    public class GenerateRangeDatesHelper : IGenerateRangeDatesHelper
    {
        public IList<DateTime> Create(DateTime initDate, DateTime endDate)
            => Enumerable.Range(0, 1 + endDate.Subtract(initDate).Days)
                         .Select(offset => initDate.AddDays(offset))
                         .ToList();
    }
}