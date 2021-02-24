using Gazin.Portal.Integrations.Jira.Configurations;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public class IssueWorklogDelete : BaseJiraIntegration, IIssueWorklogDelete
    {
        public IssueWorklogDelete(IJiraConfiguration jwtConfiguration)
            : base(jwtConfiguration)
        { }

        protected override string URL { get; set; } = "/rest/api/3/issue/{issueIdOrKey}/worklog/{worklogId}";

        public async Task<bool> Execute(string username,
                                        string password,
                                        string issueId,
                                        string worklogId,
                                        CancellationToken cancellationToken)
        {
            URL = URL.Replace("{issueIdOrKey}", issueId)
                     .Replace("{worklogId}", worklogId);

            return await DeleteAsyncIsOk(username, password, cancellationToken);
        }
    }
}