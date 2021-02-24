using Gazin.Portal.Integrations.Jira.Data.Dtos;
using Gazin.Portal.Integrations.Jira.Data.Dtos.Inputs;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public interface ISearchPost
    {
        Task<SearchDto> Execute(string username, string password, SearchInputDto request, CancellationToken cancellationToken);
    }
}