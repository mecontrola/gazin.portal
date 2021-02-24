using System.Collections.Generic;

namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class SearchDto : PaginationDto
    {
        public IList<IssueDto> Issues { get; set; }
    }
}