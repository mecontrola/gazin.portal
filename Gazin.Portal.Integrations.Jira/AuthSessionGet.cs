using Gazin.Portal.Integrations.Jira.Configurations;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public class AuthSessionGet : BaseJiraIntegration, IAuthSessionGet
    {
        public AuthSessionGet(IJiraConfiguration jwtConfiguration)
            : base(jwtConfiguration)
        { }

        protected override string URL { get; set; } = "/rest/auth/1/session";

        public async Task<AuthenticationDto> IsAuthenticated(string username, string password, CancellationToken cancellationToken)
            => await AuthenticationAsync(username, password, cancellationToken);
    }
}