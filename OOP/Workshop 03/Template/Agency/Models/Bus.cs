using Agency.Exceptions;
using Agency.Models.Contracts;
using System;
using System.Text;

namespace Agency.Models
{
    public class Bus : Vehicle, IBus
    {
        public const int PassengerCapacityMinValue = 10;
        public const int PassengerCapacityMaxValue = 50;
        public const double PricePerKilometerMinValue = 0.10;
        public const double PricePerKilometerMaxValue = 2.50;
        private int id;
        private bool hasFreeTv;

        public Bus(int id, int passengerCapacity, double pricePerKilometer, bool hasFreeTv) : base(passengerCapacity, pricePerKilometer)
        {
            ValidatePassengers(passengerCapacity);
            this.HasFreeTv = hasFreeTv;
            this.Id = id;
        }

        public bool HasFreeTv
        {
            get
            {
                return hasFreeTv;
            }
            private set
            {
                hasFreeTv = value;
            }
        }
        public int Id { get; private set; }

        protected override void ValidatePassengers(int passengers)
        {
            if (passengers < PassengerCapacityMinValue || passengers > PassengerCapacityMaxValue)
            {
                throw new InvalidUserInputException($"A bus cannot have less than {PassengerCapacityMinValue} passengers or more than {PassengerCapacityMaxValue} passengers.");
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Bus ----");
            sb.AppendLine($"Passenger capacity: {this.PassangerCapacity}");
            sb.AppendLine($"Price per kilometer: {this.PricePerKilometer}");
            sb.AppendLine($"Has free TV: {this.HasFreeTv}");

            return sb.ToString();
        }

    }
}
