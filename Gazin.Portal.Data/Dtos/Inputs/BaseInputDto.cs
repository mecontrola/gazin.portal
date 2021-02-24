using System.Text.Json.Serialization;

namespace Gazin.Portal.Data.Dtos.Inputs
{
    public abstract class BaseInputDto
    {
        [JsonIgnore]
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}