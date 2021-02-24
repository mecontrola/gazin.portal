using Gazin.Portal.Integrations.Jira.Configurations;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using Gazin.Portal.Integrations.Jira.Data.Dtos.Inputs;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public class IssueWorklogPost : BaseJiraIntegration, IIssueWorklogPost
    {
        public IssueWorklogPost(IJiraConfiguration jwtConfiguration)
            : base(jwtConfiguration)
        { }

        protected override string URL { get; set; } = "/rest/api/3/issue/{issueIdOrKey}/worklog";

        public async Task<WorklogDto> Execute(string username,
                                              string password,
                                              string issueId,
                                              WorklogInputDto request,
                                              CancellationToken cancellationToken)
        {
            URL = URL.Replace("{issueIdOrKey}", issueId);

            return await PostAsync<WorklogInputDto, WorklogDto>(username, password, request, cancellationToken);
        }
    }
}