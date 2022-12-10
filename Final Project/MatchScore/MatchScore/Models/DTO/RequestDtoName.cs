using Newtonsoft.Json;

namespace MatchScore.Models.DTO
{
    public class RequestDtoName : RequestDto
    {
        [JsonProperty(Order = 4)]
        public string PlayerFullName { get; set; }
    }
}
