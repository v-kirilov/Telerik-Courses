using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.Users.UsersViewModels;

namespace ForumSystem.Helpers.Contracts
{
    public interface IAuthManager
    {
        public User CurrentUser { get; }
        User TryGetUser(string username);
        bool IsUserBlocked(string username);
        User TryGetUser(string username, string password);
        void Login(string username, string password);
        void Logout();
    }
}
