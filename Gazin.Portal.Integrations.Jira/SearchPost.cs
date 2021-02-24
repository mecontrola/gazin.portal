using Gazin.Portal.Integrations.Jira.Configurations;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using Gazin.Portal.Integrations.Jira.Data.Dtos.Inputs;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public class SearchPost : BaseJiraIntegration, ISearchPost
    {
        public SearchPost(IJiraConfiguration jwtConfiguration)
            : base(jwtConfiguration)
        { }

        protected override string URL { get; set; } = "/rest/api/3/search";

        public async Task<SearchDto> Execute(string username,
                                              string password,
                                              SearchInputDto request,
                                              CancellationToken cancellationToken)
            => await PostAsync<SearchInputDto, SearchDto>(username, password, request, cancellationToken);
    }
}