using System;
using System.Text.Json.Serialization;

namespace Gazin.Portal.Data.Dtos.Inputs
{
    public class IssueWorklogInputDto : BaseInputDto
    {
        [JsonIgnore]
        public string Issue { get; set; }
        [JsonIgnore]
        public string Worklog { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Comment { get; set; }
    }
}