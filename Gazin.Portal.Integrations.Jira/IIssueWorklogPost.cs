using Gazin.Portal.Integrations.Jira.Data.Dtos;
using Gazin.Portal.Integrations.Jira.Data.Dtos.Inputs;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public interface IIssueWorklogPost
    {
        Task<WorklogDto> Execute(string username, string password, string issueId, WorklogInputDto request, CancellationToken cancellationToken);
    }
}