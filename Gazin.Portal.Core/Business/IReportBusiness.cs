using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Business
{
    public interface IReportBusiness
    {
        Task<IList<WorklogReportDto>> ReportPeriod(WorklogReportInputDto input, CancellationToken cancellationToken);
    }
}