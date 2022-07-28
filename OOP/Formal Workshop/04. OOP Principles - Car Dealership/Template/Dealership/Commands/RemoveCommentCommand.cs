using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models.Contracts;
using System.Collections.Generic;

namespace Dealership.Commands
{
    public class RemoveCommentCommand : BaseCommand
    {
        public RemoveCommentCommand(List<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        protected override bool RequireLogin
        {
            get { return true; }
        }

        protected override string ExecuteCommand()
        {
            if (this.CommandParameters.Count < 3)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            int vehicleIndex = this.ParseIntParameter(this.CommandParameters[0], "vehicleIndex") - 1;
            int commentIndex = this.ParseIntParameter(this.CommandParameters[1], "vehicleIndex") - 1;
            string username = this.CommandParameters[2];

            return this.RemoveComment(vehicleIndex, commentIndex, username);
        }

        private string RemoveComment(int vehicleIndex, int commentIndex, string username)
        {
            IUser user = this.Repository.GetUser(username);

            Validator.ValidateIntRange(
                vehicleIndex,
                0,
                user.Vehicles.Count,
                "The vehicle does not exist!");

            IVehicle vehicle = user.Vehicles[vehicleIndex];

            Validator.ValidateIntRange(
                commentIndex,
                0,
                user.Vehicles[vehicleIndex].Comments.Count,
                "Cannot remove comment! The comment does not exist!");


            var comment = user.Vehicles[vehicleIndex].Comments[commentIndex];

            this.Repository.LoggedUser.RemoveComment(comment, vehicle);

            return $"{this.Repository.LoggedUser.Username} removed comment successfully!";
        }
    }
}
