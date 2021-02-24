using Gazin.Portal.Core.Business;
using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using static Gazin.Portal.RestApi.Configurations.RoutesConfiguration;

namespace Gazin.Portal.RestApi.Controllers.V1
{
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class IssueController : ApiBaseController
    {
        private readonly IIssueBusiness issueBusiness;

        public IssueController(IIssueBusiness issueBusiness)
        {
            this.issueBusiness = issueBusiness;
        }

        [HttpGet(Issue.GET)]
        [ProducesResponseType(typeof(IssueDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken)
            => await ExecuteActionAsync(issueBusiness.Get, FillInput<IssueInputDto>(dto =>
            {
                dto.Issue = id;
            }), cancellationToken);
    }
}