using MatchScore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Models
{
    public class Scores
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int Value { get; set; }
        public bool IsDeleted { get; set; }
    }
}
