using System.Collections.Generic;

namespace Gazin.Portal.Data.Dtos
{
    public class WorklogReportDto
    {
        public string Date { get; set; }
        public string TotalHours { get; set; }
        public IList<WorlogIssueReportDto> Worklogs { get; set; }
    }
}