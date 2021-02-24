namespace Gazin.Portal.Integrations.Jira.Data.Dtos.Inputs
{
    public class WorklogInputDto
    {
        public long TimeSpentSeconds { get; set; }
        public DocumentFormat Comment { get; set; }
        public string Started { get; set; }
    }
}