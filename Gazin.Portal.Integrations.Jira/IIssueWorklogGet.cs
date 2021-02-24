using Gazin.Portal.Integrations.Jira.Data.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public interface IIssueWorklogGet
    {
        Task<WorklogDto> Execute(string username, string password, string issueId, string worklogId, CancellationToken cancellationToken);
    }
}