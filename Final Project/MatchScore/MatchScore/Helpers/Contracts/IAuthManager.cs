using MatchScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Helpers.Contracts
{
    public interface IAuthManager
    {
        User CurrentUser { get; }
        User TryGetUser(string userEmail);
        User TryGetUser(string userEmail, string password);
        void Login(string userEmail, string password);
        void Logout();
    }
}
