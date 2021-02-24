using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gazin.Portal.Data.Dtos.Inputs
{
    public class WorklogReportInputDto : BaseInputDto
    {
        [JsonIgnore]
        public IList<string> Accounts { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}