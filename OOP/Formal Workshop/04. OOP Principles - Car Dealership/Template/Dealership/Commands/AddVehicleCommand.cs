using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models;
using Dealership.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Dealership.Commands
{
    public class AddVehicleCommand : BaseCommand
    {
        public AddVehicleCommand(List<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        protected override bool RequireLogin
        {
            get { return true; }
        }

        protected override string ExecuteCommand()
        {
            if (this.CommandParameters.Count < 5)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 5, Received: {this.CommandParameters.Count}");
            }

            VehicleType vehicleType = this.ParseVehicleTypeParameter(this.CommandParameters[0], "vehicleType");
            string make = this.CommandParameters[1];
            string model = this.CommandParameters[2];
            decimal price = this.ParseDecimalParameter(this.CommandParameters[3], "price");
            string additionalParam = this.CommandParameters[4];

            return this.AddVehicle(vehicleType, make, model, price, additionalParam);
        }

        private string AddVehicle(VehicleType type, string make, string model, decimal price, string additionalParam)
        {
            IVehicle vehicle;
            switch (type)
            {
                case VehicleType.Car:
                    int seats = this.ParseIntParameter(additionalParam, "seats");
                    vehicle = this.Repository.CreateCar(make, model, price, seats);
                    break;
                case VehicleType.Motorcycle:
                    vehicle = this.Repository.CreateMotorcycle(make, model, price, additionalParam);
                    break;
                case VehicleType.Truck:
                    int weightCapacity = this.ParseIntParameter(additionalParam, "weightCapacity");
                    vehicle = this.Repository.CreateTruck(make, model, price, weightCapacity);
                    break;
                default:
                    throw new InvalidOperationException($"Creating vehicle type: {type} is not supported!");
            }

            this.Repository.LoggedUser.AddVehicle(vehicle);

            return $"{this.Repository.LoggedUser.Username} added vehicle successfully!";
        }
    }
}
