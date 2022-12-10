using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string PlayerFullName { get; set; }

        public List<TournamentDto> Tournaments { get; set; }

        public List<MatchDto> Matches { get; set; }
    }
}
