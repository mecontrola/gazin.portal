using Gazin.Portal.Data.Dtos.Inputs;
using data = Gazin.Portal.Core.Tests.Mocks.Datas.IssueWorklogData;

namespace Gazin.Portal.Core.Tests.Mocks.Dtos.Inputs
{
    public static class IssueWorklogInputDtoMock
    {
        public static IssueWorklogInputDto Create()
            => new IssueWorklogInputDto
            {
                Issue = data.ISSUE,
                Date = data.DATE,
                StartTime = data.START_TIME,
                EndTime = data.END_TIME
            };

        public static IssueWorklogInputDto CreateIssueEmpty()
        {
            var dto = Create();
            dto.Issue = null;
            return dto;
        }

        public static IssueWorklogInputDto CreateStartGreaterThanEnd()
        {
            var dto = Create();
            dto.StartTime = data.START_TIME_GREATER;
            return dto;
        }
    }
}