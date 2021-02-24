using Gazin.Portal.Integrations.Jira.Configurations;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public class IssueWorklogGet : BaseJiraIntegration, IIssueWorklogGet
    {
        public IssueWorklogGet(IJiraConfiguration jwtConfiguration)
            : base(jwtConfiguration)
        { }

        protected override string URL { get; set; } = "/rest/api/3/issue/{issueIdOrKey}/worklog/{worklogId}";

        public async Task<WorklogDto> Execute(string username,
                                              string password,
                                              string issueId,
                                              string worklogId,
                                              CancellationToken cancellationToken)
        {
            URL = URL.Replace("{issueIdOrKey}", issueId)
                     .Replace("{worklogId}", worklogId);

            return await GetAsync<WorklogDto>(username, password, cancellationToken);
        }
    }
}