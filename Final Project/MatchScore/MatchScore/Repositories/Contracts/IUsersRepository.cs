using MatchScore.Models;
using MatchScore.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Repositories.Contracts
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        User GetById(int id);
        User GetByEmail(string email);
        PaginatedList<User> FilterBy(UserQueryParameters filterParameters);
        User Create(User user);
        User UpdatePassword(int id, string password);
        User UpdateRole(int id, string roleName);
        void Delete(int id);
    }
}
