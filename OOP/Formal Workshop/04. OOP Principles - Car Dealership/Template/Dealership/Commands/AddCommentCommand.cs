using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models.Contracts;
using System.Collections.Generic;

namespace Dealership.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        public AddCommentCommand(List<string> parameters, IRepository repository)
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

            string content = this.CommandParameters[0];
            string author = this.CommandParameters[1];
            int vehicleIndex = this.ParseIntParameter(this.CommandParameters[2], "vehicleIndex") - 1;

            return this.AddComment(content, author, vehicleIndex);
        }

        private string AddComment(string content, string author, int vehicleIndex)
        {
            IUser user = this.Repository.GetUser(author);

            Validator.ValidateIntRange(
                vehicleIndex,
                0,
                user.Vehicles.Count,
                "The vehicle does not exist!");

            IVehicle vehicle = user.Vehicles[vehicleIndex];

            IComment comment = this.Repository.CreateComment(content, this.Repository.LoggedUser.Username);

            this.Repository.LoggedUser.AddComment(comment, vehicle);

            return $"{this.Repository.LoggedUser.Username} added comment successfully!";
        }
    }
}
