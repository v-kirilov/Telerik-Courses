using Agency.Exceptions;
using Agency.Models.Contracts;
using System;
using System.Text;

namespace Agency.Models
{
    public class Airplane : Vehicle, IAirplane
    {
        public const int PassengerCapacityMinValue = 1;
        public const int PassengerCapacityMaxValue = 800;
        public const double PricePerKilometerMinValue = 0.10;
        public const double PricePerKilometerMaxValue = 2.50;
        private int id;
        private bool isLowCost;

        public int Id
        {
            get;
            private set;
        }

        public Airplane(int id, int passengerCapacity, double pricePerKilometer, bool isLowCost) : base(passengerCapacity, pricePerKilometer)
        {

            this.IsLowCost = isLowCost;
            this.Id = id;
        }

        public bool IsLowCost
        {
            get
            {
                return isLowCost;
            }
            private set
            {
                isLowCost = value;
            }
        }

        protected override void ValidatePassengers(int passengers)
        {
            if (passengers < PassengerCapacityMinValue || passengers > PassengerCapacityMaxValue)
            {
                throw new InvalidUserInputException($"An airplane cannot have less than {PassengerCapacityMinValue} passengers or more than {PassengerCapacityMaxValue} passengers.");
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Airplane ----");
            sb.AppendLine($"Passenger capacity: {this.PassangerCapacity}");
            sb.AppendLine($"Price per kilometer: {this.PricePerKilometer}");
            sb.AppendLine($"Is low-cost: {this.isLowCost}");

            return sb.ToString();
        }
    }
}
