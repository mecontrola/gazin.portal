using Gazin.Portal.Integrations.Jira.Configurations;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public class IssueGet : BaseJiraIntegration, IIssueGet
    {
        public IssueGet(IJiraConfiguration jwtConfiguration)
            : base(jwtConfiguration)
        { }

        protected override string URL { get; set; } = "/rest/api/3/issue/{issueIdOrKey}";

        public async Task<IssueDto> Execute(string username,
                                              string password,
                                              string issueId,
                                              CancellationToken cancellationToken)
        {
            URL = URL.Replace("{issueIdOrKey}", issueId);

            return await GetAsync<IssueDto>(username, password, cancellationToken);
        }
    }
}