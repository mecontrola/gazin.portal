using System.Collections.Generic;

namespace Gazin.Portal.Integrations.Jira.Data.Dtos
{
    public class DocumentFormatContent
    {
        public string Type { get; set; }
        public IList<DocumentFormatContentValue> Content { get; set; } = new List<DocumentFormatContentValue>();
    }
}