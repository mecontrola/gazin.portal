using System;

namespace Gazin.Portal.Core.Exceptions
{
    public class TimeSpanRangeException : Exception
    {
        public TimeSpanRangeException(string startTime, string endTime)
            : base($"The {startTime} field is larger than the {endTime} field.")
        { }
    }
}