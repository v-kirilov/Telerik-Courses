using MatchScore.Models.Enums;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MatchScore.Helpers;

namespace MatchScore.Models.DTO
{
    public class MatchDto
    {
        public MatchDto()
        {
            Date = default(DateTime);
            TournamentId = null;
            RoundId = null;
            DirectorId = 0;
            Players = new List<string>() { "", "" };
            Score1 = 0;
            Score2 = 0;
        }
        public MatchDto(string format, int? tournamentId, int? roundId, int directorId, string status, List<string> players)
        {
            Format = format;
            TournamentId = tournamentId;
            RoundId = roundId;
            DirectorId = directorId;
            Status = status;
            Players = players;
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        [CustomDateAttribute(ErrorMessage = "Value for {0} must be after {1}")]
        public DateTime Date { get; set; }
        public string Format { get; set; }
        public int? TournamentId { get; set; }
        public int? RoundId { get; set; }
        public int DirectorId { get; set; }
        public string Status { get; set; }
        public List<string> Players { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }

    }
}
