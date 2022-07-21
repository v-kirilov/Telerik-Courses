using Agency.Commands.Abstracts;
using Agency.Core.Contracts;
using Agency.Exceptions;
using Agency.Models;
using System;
using System.Collections.Generic;

namespace Agency.Commands
{
    public class CreateAirplaneCommand : BaseCommand
    {
        public CreateAirplaneCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            // Parameters:
            //  [0] - passenger capacity
            //  [1] - price per km
            //  [2] - is low cost?
            int passengerCapacity = this.ParseIntParameter(this.CommandParameters[0], "passengerCapacity");
            double pricePerKilometer = this.ParseDoubleParameter(this.CommandParameters[1], "pricePerKilometer");
            bool isLowCost = this.ParseBoolParameter(this.CommandParameters[2], "isLoCost");

            var airplane = this.Repository.CreateAirplane(passengerCapacity, pricePerKilometer, isLowCost);

            return $"Vehicle with ID {airplane.Id} was created.";
        }
    }
}
