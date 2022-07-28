using Dealership.Core.Contracts;
using Dealership.Exceptions;

using System.Collections.Generic;

namespace Dealership.Commands
{
    public class LoginCommand : BaseCommand
    {
        public LoginCommand(List<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        protected override bool RequireLogin
        {
            get { return false; }
        }

        protected override string ExecuteCommand()
        {
            if (this.CommandParameters.Count < 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
            }

            string username = this.CommandParameters[0];
            string password = this.CommandParameters[1];

            return this.Login(username, password);
        }

        private string Login(string username, string password)
        {
            if (this.Repository.LoggedUser != null)
            {
                string errorMessage = string.Format(BaseCommand.UserAlreadyLoggedIn, this.Repository.LoggedUser.Username);

                throw new AuthorizationException(errorMessage);
            }

            var user = this.Repository.GetUser(username);
            if (user.Password != password)
            {
                throw new AuthorizationException("Wrong username or password!");
            }

            this.Repository.LogUser(user);

            return $"User {username} successfully logged in!";
        }
    }
}
