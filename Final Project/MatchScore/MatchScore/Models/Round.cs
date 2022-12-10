using MatchScore.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchScore.Models
{
    public class Round
    {
        public Round()
        {
            StartDate = DateTime.Now;
            Status = Status.Current;
        }
        public int Id { get; set; }
        public int RoundNumber { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public List<Match> Matches { get; set; } = new List<Match>();
        public DateTime StartDate { get; set; }
        public bool IsDeleted { get; set; }
        public Status Status { get; set; }
        [NotMapped]
        public List<string> RoundPlayers { get; set; } = new List<string>();

    }
}
