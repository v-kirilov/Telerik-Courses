using Agency.Commands.Abstracts;
using Agency.Core.Contracts;
using Agency.Exceptions;

using System.Collections.Generic;

namespace Agency.Commands
{
    public class CreateTrainCommand : BaseCommand
    {
        public CreateTrainCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 3)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - passenger capacity
            //  [1] - price per km
            //  [2] - carts count
            int passengerCapacity = this.ParseIntParameter(base.CommandParameters[0], "passengerCapacity");
            double pricePerKilometer = this.ParseDoubleParameter(base.CommandParameters[1], "pricePerKilometer");
            int cartsCount = this.ParseIntParameter(base.CommandParameters[2], "cartsCount");

            var train = this.Repository.CreateTrain(passengerCapacity, pricePerKilometer, cartsCount);
            return $"Vehicle with ID {train.Id} was created.";
        }
    }
}
