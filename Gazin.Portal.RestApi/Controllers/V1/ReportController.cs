using Gazin.Portal.Core.Business;
using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Gazin.Portal.RestApi.Configurations.RoutesConfiguration;

namespace Gazin.Portal.RestApi.Controllers.V1
{
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class ReportController : ApiBaseController
    {
        private readonly IReportBusiness reportBusiness;

        public ReportController(IReportBusiness reportBusiness)
        {
            this.reportBusiness = reportBusiness;
        }

        [HttpGet(Report.Worklog)]
        [ProducesResponseType(typeof(IEnumerable<WorklogReportDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWorklogPeriod([FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate, CancellationToken cancellationToken)
            => await ExecuteActionAsync(reportBusiness.ReportPeriod, FillInput<WorklogReportInputDto>(dto =>
            {
                dto.StartDate = StartDate.Date;
                dto.EndDate = EndDate.Date;
                dto.Accounts = new string[] { GetSid() };
            }), cancellationToken);
    }
}