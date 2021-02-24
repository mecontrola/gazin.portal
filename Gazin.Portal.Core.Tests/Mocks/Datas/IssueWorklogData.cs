using System;

namespace Gazin.Portal.Core.Tests.Mocks.Datas
{
    public static class IssueWorklogData
    {
        public static readonly string ISSUE = "TST-369";
        public static readonly DateTime DATE = new DateTime(2021, 2, 10);
        public static readonly TimeSpan START_TIME = new TimeSpan(15, 0, 0);
        public static readonly TimeSpan START_TIME_GREATER = new TimeSpan(16, 0, 0);
        public static readonly TimeSpan END_TIME = new TimeSpan(15, 30, 0);
    }
}