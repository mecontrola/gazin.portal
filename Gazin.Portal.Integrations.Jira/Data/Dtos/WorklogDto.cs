using System;

namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class WorklogDto
    {
        public string Self { get; set; }
        public UserDto Author { get; set; }
        public UserDto UpdateAuthor { get; set; }
        public DocumentFormat Comment { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string Started { get; set; }
        public string TimeSpent { get; set; }
        public long TimeSpentSeconds { get; set; }
        public string Id { get; set; }
        public string IssueId { get; set; }
    }
}