using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Repositories.Contracts;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;

namespace MatchScore.Helpers
{
    public class AuthManager : IAuthManager
    {
        private const string NotAuthorizedErrorMessage = "You need to authorize first.";
        private const string UserNotFoundErrorMessage = "The user was not found.";
        private const string InvalidUserEmailOrPasswordErrorMessage = "Invalid user email or password";
        private const string PasswordErrorMessage = "You need to provide password.";


        private const string CURRENT_USER = "CURRENT_USER";
        private readonly IUsersRepository usersRepository;
        private readonly IHttpContextAccessor contextAccessor;

        public AuthManager(IUsersRepository usersRepository, IHttpContextAccessor contextAccessor)
        {
            this.usersRepository = usersRepository;
            this.contextAccessor = contextAccessor;
        }

        public User TryGetUser(string credentials)
        {
            if (string.IsNullOrEmpty(credentials))
            {
                throw new UnauthorizedOperationException(NotAuthorizedErrorMessage);
            }
            try
            {
                var credentialsArray = credentials.Split(':');
                var userEmail = credentialsArray[0];
                var password = credentialsArray[1];

                User user = this.usersRepository.GetByEmail(userEmail);

                var encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

                if (user.Password != encodedPassword)
                {
                    throw new UnauthorizedOperationException(InvalidUserEmailOrPasswordErrorMessage);
                }

                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorizedOperationException(UserNotFoundErrorMessage);
            }
            catch (IndexOutOfRangeException)
            {
                throw new UnauthorizedOperationException(PasswordErrorMessage);
            }
        }

        public User TryGetUser(string email, string password)
        {
            try
            {
                User user = this.usersRepository.GetByEmail(email);
                var encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

                if (user.Password != encodedPassword)
                {
                    throw new UnauthorizedOperationException(InvalidUserEmailOrPasswordErrorMessage);
                }

                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorizedOperationException(InvalidUserEmailOrPasswordErrorMessage);
            }
        }

        public User CurrentUser
        {
            get
            {
                try
                {
                    string email = this.contextAccessor.HttpContext.Session.GetString(CURRENT_USER);
                    User user = this.usersRepository.GetByEmail(email);
                    return user;
                }
                catch (EntityNotFoundException)
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    this.contextAccessor.HttpContext.Session.SetString(CURRENT_USER, value.Email);
                }
                else
                {
                    this.contextAccessor.HttpContext.Session.Remove(CURRENT_USER);
                }
            }
        }

        public void Login(string email, string password)
        {
            this.CurrentUser = this.TryGetUser(email, password);
        }

        public void Logout()
        {
            this.CurrentUser = null;
        }

    }
}
