using System;

namespace Gazin.Portal.Integrations.Jira.Exceptions
{
    public class JiraException : Exception
    {
        public JiraException(string message)
            : base(message)
        { }
    }
}