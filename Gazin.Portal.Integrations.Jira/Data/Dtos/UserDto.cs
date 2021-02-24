namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class UserDto
    {
        public string Self { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }
        //public AvatarUrls AvatarUrls { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public string TimeZone { get; set; }
        public string AccountType { get; set; }
    }
}