using Gazin.Portal.Integrations.Jira.Data.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public interface IIssueWorklogGetAll
    {
        Task<WorklogListDto> Execute(string username, string password, string issueId, CancellationToken cancellationToken);
    }
}