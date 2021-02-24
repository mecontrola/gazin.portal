using Gazin.Portal.Data.Dtos.Inputs;
using data = Gazin.Portal.Core.Tests.Mocks.Datas.WorklogReportData;

namespace Gazin.Portal.Core.Tests.Mocks.Dtos.Inputs
{
    public class WorklogReportInputDtoMock
    {
        public static WorklogReportInputDto Create()
            => new WorklogReportInputDto
            {
                StartDate = data.START_DATE,
                EndDate = data.END_DATE
            };

        public static WorklogReportInputDto CreateStartGreaterThanEnd()
            => new WorklogReportInputDto
            {
                StartDate = data.START_DATE_GREATER,
                EndDate = data.END_DATE
            };

        public static WorklogReportInputDto CreateEmpty()
            => new WorklogReportInputDto();
    }
}