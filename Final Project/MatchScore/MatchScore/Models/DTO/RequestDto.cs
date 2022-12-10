using Newtonsoft.Json;

namespace MatchScore.Models.DTO
{
    public class RequestDto
    {
        [JsonProperty(Order = 0)]
        public int Id { get; set; }

        [JsonProperty(Order = 1)]
        public string Status { get; set; }

        [JsonProperty(Order = 2)]
        public string Type { get; set; }

        [JsonProperty(Order = 3)]
        public string UserEmail { get; set; }
    }
}
