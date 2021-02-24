using Gazin.Portal.Integrations.Jira.Data.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public interface IAuthSessionGet
    {
        Task<AuthenticationDto> IsAuthenticated(string username, string password, CancellationToken cancellationToken);
    }
}