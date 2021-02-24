namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class IssueDto
    {
        public string Expand { get; set; }
        public string Id { get; set; }
        public string Self { get; set; }
        public string Key { get; set; }
        public IssueFieldsDto Fields { get; set; }
    }
}