using Gazin.Portal.Integrations.Jira.Configurations;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public class IssueWorklogGetAll : BaseJiraIntegration, IIssueWorklogGetAll
    {
        public IssueWorklogGetAll(IJiraConfiguration jwtConfiguration)
            : base(jwtConfiguration)
        { }

        protected override string URL { get; set; } = "/rest/api/3/issue/{issueIdOrKey}/worklog";

        public async Task<WorklogListDto> Execute(string username,
                                                  string password,
                                                  string issueId,
                                                  CancellationToken cancellationToken)
        {
            URL = URL.Replace("{issueIdOrKey}", issueId);

            return await GetAsync<WorklogListDto>(username, password, cancellationToken);
        }
    }
}