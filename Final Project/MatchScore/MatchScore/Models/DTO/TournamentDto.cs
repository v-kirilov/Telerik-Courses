using MatchScore.Helpers;
using MatchScore.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MatchScore.Models.DTO
{
    public class TournamentDto
    {
        [JsonProperty(Order = 1)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [JsonProperty(Order = 2)]
        
        public string Title { get; set; }

        [JsonProperty(Order = 3)]
        public int DirectorId { get; set; }

        [JsonProperty(Order = 4)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required")]

        public TournamentFormat Format { get; set; }

        [JsonProperty(Order = 5)]
        public ICollection<RoundDto> Rounds { get; set; } 

        [JsonProperty(Order = 6)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required.")]
        public TournamentPrize Prize { get; set; }

        [CustomDateAttribute(ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [JsonProperty(Order = 7)]
        public DateTime StartDate { get; set; }

        [JsonProperty(Order = 8)]
        [CustomDateAttribute( ErrorMessage = "Value for {0} must be between {1} and {2}")]

        public DateTime EndDate { get; set; }

        [JsonProperty(Order = 9)]
        public Status Status { get; set; } 

        [JsonProperty(Order = 10)]
        public string DirectorEmail { get; set; }

        [JsonProperty(Order = 11)]
        public List<string> PlayersFullName { get; set; } = new List<string>();
        public string Winner { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Standing> Standings { get; set; } = new List<Standing>();

    }
}
