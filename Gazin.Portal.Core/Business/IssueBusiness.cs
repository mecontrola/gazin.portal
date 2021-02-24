using Gazin.Portal.Data.Dtos.Inputs;
using Gazin.Portal.Integrations.Jira;
using System.Threading;
using System.Threading.Tasks;
using GazinIssueDto = Gazin.Portal.Data.Dtos.IssueDto;
using JiraIssueDto = Gazin.Portal.Integrations.Jira.Data.Dtos.IssueDto;

namespace Gazin.Portal.Core.Business
{
    public class IssueBusiness : IIssueBusiness
    {
        private readonly IIssueGet issueGet;

        public IssueBusiness(IIssueGet issueGet)
        {
            this.issueGet = issueGet;
        }

        public async Task<GazinIssueDto> Get(IssueInputDto input, CancellationToken cancellationToken)
        {
            var response = await issueGet.Execute(input.Username, input.Password, input.Issue, cancellationToken);

            return CreateResponse(response);
        }

        private static GazinIssueDto CreateResponse(JiraIssueDto response)
            => new GazinIssueDto
            {
                Id = response.Id,
                Key = response.Key
            };
    }
}