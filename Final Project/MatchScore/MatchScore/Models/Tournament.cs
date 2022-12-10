using MatchScore.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MatchScore.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TournamentFormat Format { get; set; }
        public TournamentPrize Prize { get; set; }
        public int DirectorId { get; set; }
        public User Director { get; set; }
        public List<Round> Rounds { get; set; } = new List<Round>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public Status Status { get; set; }
        public List<Player> TournamentPlayers { get; set; } = new List<Player>();
        public List<Standing> Standings { get; set; } = new List<Standing>();
        public string Winner { get; set; }
	}
}
