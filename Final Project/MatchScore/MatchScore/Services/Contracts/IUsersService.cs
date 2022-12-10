using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services.Contracts
{
    public interface IUsersService
    {
        List<UserDto> GetAll();
        UserDto GetById(int id);
        UserDto GetByEmail(string email);
        PaginatedList<UserDto> FilterBy(UserQueryParameters filterParameters);
        UserDto Create(User user);
        UserDto Update(int id, User userIncoming, User authUser);
        void Delete(int id, User authUser);
        bool EmailExists(string email);
    }
}
