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
    public class WorklogController : ApiBaseController
    {
        private const string ROUTE_GET_NAME = "ISSUE-WORKLOG-GET-ROUTE";

        private readonly IIssueWorklogBusiness issueWorklogBusiness;

        public WorklogController(IIssueWorklogBusiness issueWorklogBusiness)
        {
            this.issueWorklogBusiness = issueWorklogBusiness;
        }

        [HttpGet(Worklog.GET, Name = ROUTE_GET_NAME)]
        [ProducesResponseType(typeof(IssueWorklogDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] string id, [FromRoute] string worklogId, CancellationToken cancellationToken)
            => await ExecuteActionAsync(issueWorklogBusiness.Get, FillInput<IssueWorklogInputDto>(dto =>
            {
                dto.Issue = id;
                dto.Worklog = worklogId;
            }), cancellationToken);

        [HttpPost(Worklog.GET_ALL)]
        [ProducesResponseType(typeof(IssueWorklogDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertWorkInIssue([FromRoute] string id, [FromBody] IssueWorklogInputDto request, CancellationToken cancellationToken)
            => await ExecuteInsertAsync(ROUTE_GET_NAME, issueWorklogBusiness.InsertWorklog, FillInput(request, dto =>
            {
                dto.Issue = id;
            }), cancellationToken);

        [HttpPut(Worklog.GET)]
        [ProducesResponseType(typeof(IssueWorklogDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateWorkInIssue([FromRoute] string id, [FromRoute] string worklogId, [FromBody] IssueWorklogInputDto request, CancellationToken cancellationToken)
            => await ExecutePutAsync(issueWorklogBusiness.UpdateWorklog, FillInput(request, dto =>
            {
                dto.Issue = id;
                dto.Worklog = worklogId;
            }), cancellationToken);

        [HttpDelete(Worklog.GET)]
        [ProducesResponseType(typeof(IssueWorklogDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWorkInIssue([FromRoute] string id, [FromRoute] string worklogId, [FromBody] IssueWorklogInputDto request, CancellationToken cancellationToken)
            => await ExecuteDeleteAsync(issueWorklogBusiness.RemoveWorklog, FillInput(request, dto =>
            {
                dto.Issue = id;
                dto.Worklog = worklogId;
            }), cancellationToken);
    }
}