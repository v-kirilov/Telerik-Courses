using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public Player Player { get; set; }

        public List<Tournament> Tournaments { get; set; }
        public List<Match> Matches { get; set; }
        public List<Round> Rounds { get; set; }
        public List<Request> Requests { get; set; }
    }
}
