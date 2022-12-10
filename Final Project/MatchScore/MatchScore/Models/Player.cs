using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }   
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public int? SportClubId { get; set; }
        public SportClub SportClub { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
        public List<Tournament> Tournaments { get; set; }
        public List<Round> Rounds { get; set; }
        public List<Scores> Scores { get; set; }

    }
}
