using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models.Contracts;
using System.Collections.Generic;

namespace Dealership.Commands
{
    public class RemoveVehicleCommand : BaseCommand
    {
        public RemoveVehicleCommand(List<string> parameters, IRepository repository)
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

            int vehicleIndex = this.ParseIntParameter(this.CommandParameters[0], "vehicleIndex") - 1;

            return this.RemoveVehicle(vehicleIndex);
        }

        private string RemoveVehicle(int vehicleIndex)
        {
            Validator.ValidateIntRange(
                vehicleIndex,
                0,
                this.Repository.LoggedUser.Vehicles.Count,
                "Cannot remove vehicle! The vehicle does not exist!");

            IVehicle vehicle = this.Repository.LoggedUser.Vehicles[vehicleIndex];

            this.Repository.LoggedUser.RemoveVehicle(vehicle);

            return $"{this.Repository.LoggedUser.Username} removed vehicle successfully!";
        }
    }
}
