using ForumSystem.Models;
using ForumSystem.Models.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Repositories.Contracts
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        User GetById(int id);
        User GetByUsername(string username);
        User GetByEmail(string email);
        User GetByFirstName(string firstname);
        PaginatedList<User> FilterBy(UserQueryParameters filterParameters);
        User Create(User user);
        User UpdateFirstName(int id, string firstName);
        User UpdateLastName(int id, string lastName);
        User UpdateEmail(int id, string email);
        User UpdatePassword(int id, string password);
        User UpdateRole(int id, string roleName);
        User UpdateIsBlocked(int id, bool isBlocked);
        User UpdatePhoneNumber(int id, PhoneNumber phoneNumber);
        User UpdateProfilePicture(int id, string uniqueFileName);
        void Delete(int id);
    }
}
