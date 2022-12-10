using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Models.DTO
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string SportClub { get; set; }
        public string UserEmail { get; set; }
        public string PhotoUrl { get; set; }
        public List<string> Tournaments { get; set; }
        public int TournamentsPlayed { get; set; }
        public int TournamentsWon { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesLost { get; set; }
        public int MatchesDraw { get; set; }
        public string MostPlayedOpponent { get; set; }
        public string BestOpponent { get; set; }
        public string BestWinLossRatio { get; set; }
        public string WorstOpponent { get; set; }
        public string WorstWinLossRatio { get; set; }
    }
}
