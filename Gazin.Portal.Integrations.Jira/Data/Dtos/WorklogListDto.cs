using System.Collections.Generic;

namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class WorklogListDto : PaginationDto
    {
        public IList<WorklogDto> Worklogs { get; set; }
    }
}