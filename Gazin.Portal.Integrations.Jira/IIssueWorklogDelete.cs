using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public interface IIssueWorklogDelete
    {
        Task<bool> Execute(string username, string password, string issueId, string worklogId, CancellationToken cancellationToken);
    }
}