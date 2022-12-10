using MatchScore.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MatchScore.Models.DTO
{
    public class RoundDto
    {
        [JsonProperty(Order = 1)]
        public int Id { get; set; }

        [JsonProperty(Order = 2)]
        public int TournamentId { get; set; }

        [JsonProperty(Order = 3)]
        public int RoundNumber { get; set; }

        [JsonProperty(Order = 4)]
        public ICollection<MatchDto> Matches { get; set; }

        [JsonProperty(Order = 5)]
        
        public DateTime StartDate { get; set; }
        public List<string> RoundPlayers { get; set; } = new List<string>();
        public Status Status { get; set; }

    }
}
