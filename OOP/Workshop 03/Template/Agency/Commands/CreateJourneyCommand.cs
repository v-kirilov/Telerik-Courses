using Agency.Commands.Abstracts;
using Agency.Core.Contracts;
using Agency.Exceptions;
using Agency.Models.Contracts;

using System.Collections.Generic;

namespace Agency.Commands
{
    public class CreateJourneyCommand : BaseCommand
    {
        public CreateJourneyCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 4)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 4, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - start location
            //  [1] - end location (destination)
            //  [2] - distance in km
            //  [3] - the id of the vehicle used
            string startLocation = base.CommandParameters[0];
            string destination = base.CommandParameters[1];
            int distance = this.ParseIntParameter(base.CommandParameters[2], "distance");
            int vehicleId = this.ParseIntParameter(base.CommandParameters[3], "vehicleId");
            IVehicle vehicle = base.Repository.FindVehicleById(vehicleId);

            var journey = base.Repository.CreateJourney(startLocation, destination, distance, vehicle);
            return $"Journey with ID {journey.Id} was created.";
        }
    }
}
