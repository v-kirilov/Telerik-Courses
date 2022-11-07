using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO.Contracts
{
    public interface IUserDto
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
    }
}
