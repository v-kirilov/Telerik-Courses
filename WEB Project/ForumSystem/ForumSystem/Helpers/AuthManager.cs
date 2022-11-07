using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Helpers
{
    public class AuthManager : IAuthManager
	{
		private const string CURRENT_USER = "CURRENT_USER";
		private readonly IUsersService usersService;
        private readonly IHttpContextAccessor contextAccessor;

        public AuthManager(IUsersService usersService, IHttpContextAccessor contextAccessor)
		{
			this.usersService = usersService;
            this.contextAccessor = contextAccessor;
        }

		public User TryGetUser(string username)
		{
            if (string.IsNullOrEmpty(username))
            {
				throw new UnauthorizedOperationException("You need to authorize first.");
			}
			try
			{
				return this.usersService.GetByUsername(username);
			}
			catch (EntityNotFoundException)
			{
				throw new UnauthorizedOperationException("The user was not found.");
			}
		}

		public User TryGetUser(string username, string password)
        {
            try
            {
				User user = this.usersService.GetByUsername(username);

                if (user.Password != password)
                {
					throw new UnauthorizedOperationException("Invalid username or password");
                }

				return user;
            }
            catch (EntityNotFoundException)
            {
				throw new UnauthorizedOperationException("Invalid username or password");
			}
		}

		public User CurrentUser
        {
            get
            {
                try
                {
					string username = this.contextAccessor.HttpContext.Session.GetString(CURRENT_USER);
					User user = this.usersService.GetByUsername(username);
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
					this.contextAccessor.HttpContext.Session.SetString(CURRENT_USER, value.Username);
                }
                else
                {
					this.contextAccessor.HttpContext.Session.Remove(CURRENT_USER);
                }
            }
        }

		public void Login(string username, string password)
        {
			this.CurrentUser = this.TryGetUser(username, password);
        }

		public void Logout()
        {
			this.CurrentUser = null;
        }

		public bool IsUserBlocked(string username)
        {
			User user = this.usersService.GetByUsername(username);
			if (!user.IsBlocked)
			{
				return false;
			}
            else
            {
				throw new BlockedUserException("The user is blocked.");
			}
		}
	}
}
