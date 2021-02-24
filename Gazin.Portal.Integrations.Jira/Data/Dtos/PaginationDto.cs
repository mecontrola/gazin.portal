namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class PaginationDto
    {
        public int StartAt { get; set; }
        public int MaxResults { get; set; }
        public int Total { get; set; }
    }
}