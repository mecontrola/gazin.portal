using System.Collections.Generic;

namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class ErrorsDto
    {
        public IList<string> ErrorMessages { get; set; }
    }
}