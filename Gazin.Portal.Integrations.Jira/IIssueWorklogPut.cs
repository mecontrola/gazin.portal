using Gazin.Portal.Integrations.Jira.Data.Dtos;
using Gazin.Portal.Integrations.Jira.Data.Dtos.Inputs;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public interface IIssueWorklogPut
    {
        Task<WorklogDto> Execute(string username, string password, string issueId, string worklogId, WorklogInputDto request, CancellationToken cancellationToken);
    }
}