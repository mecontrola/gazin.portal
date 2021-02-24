namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class IssueFieldsDto
    {
        public UserDto Creator { get; set; }
        public UserDto Reporter { get; set; }
        public IssueFieldsWorklogDto Worklog { get; set; }
    }
}