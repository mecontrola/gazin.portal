using System;
using System.Collections.Generic;

namespace Gazin.Portal.Core.Helpers
{
    public interface IGenerateRangeDatesHelper
    {
        IList<DateTime> Create(DateTime initDate, DateTime endDate);
    }
}