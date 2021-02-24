namespace Gazin.Portal.RestApi.Configurations
{
    internal static class RoutesConfiguration
    {
        const string URL_ROOT = "api";
        const string VERSION = "v1";

        const string ROUTE_PREFIX = URL_ROOT + "/" + VERSION;

        public const string PARAM_GUID = "{id}";

        public static class Authentication
        {
            public const string LOGIN = ROUTE_PREFIX + "/authentication";
            public const string REFRESH = ROUTE_PREFIX + "/refresh";
        }

        public static class Issue
        {
            private const string URL_BASE = ROUTE_PREFIX + "/issues";

            public const string GET = URL_BASE + "/" + PARAM_GUID;
        }

        public static class Worklog
        {
            private const string URL_BASE = Issue.GET + "/worklogs";

            public const string GET = URL_BASE + "/{worklogId}";
            public const string GET_ALL = URL_BASE;
        }

        public static class Report
        {
            private const string URL_BASE = ROUTE_PREFIX + "/reports";

            public const string Worklog = URL_BASE + "/worklogs";
        }
    }
}