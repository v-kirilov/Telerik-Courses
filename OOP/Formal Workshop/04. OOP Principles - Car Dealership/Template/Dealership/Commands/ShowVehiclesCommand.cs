using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models.Contracts;
using System.Collections.Generic;

namespace Dealership.Commands
{
    public class ShowVehiclesCommand : BaseCommand
    {
        public ShowVehiclesCommand(List<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        protected override bool RequireLogin
        {
            get { return true; }
        }

        protected override string ExecuteCommand()
        {
            if (this.CommandParameters.Count < 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
            }

            string username = this.CommandParameters[0];

            return this.ShowUserVehicles(username);
        }

        private string ShowUserVehicles(string username)
        {
            IUser user = this.Repository.GetUser(username);

            return user.PrintVehicles();
        }
    }
}
