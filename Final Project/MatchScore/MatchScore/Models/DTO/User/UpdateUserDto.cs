using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Models.DTO
{
    public class UpdateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
