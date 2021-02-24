using System.Collections.Generic;

namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class IssueFieldsWorklogDto : PaginationDto
    {
        public IList<DocumentFormat> Worklogs { get; set; }
    }
}