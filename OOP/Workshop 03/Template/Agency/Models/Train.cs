using Agency.Exceptions;
using Agency.Models.Contracts;
using System;
using System.Text;

namespace Agency.Models
{
    public class Train : Vehicle, ITrain
    {
        public const int PassengerCapacityMinValue = 30;
        public const int PassengerCapacityMaxValue = 150;
        public const double PricePerKilometerMinValue = 0.10;
        public const double PricePerKilometerMaxValue = 2.50;
        public const int CartsMinValue = 1;
        public const int CartsMaxValue = 15;
        private int id;
        private int carts;

        public Train(int id, int passengerCapacity, double pricePerKilometer, int carts) : base(passengerCapacity, pricePerKilometer)
        {
            ValidatePassengers(passengerCapacity);
            this.Carts = carts;
            this.Id = id;
        }
        public int Id
        {
            get;
            private set;
        }

        public int Carts
        {
            get
            {
                return this.carts;
            }
            private set
            {
                if (value < CartsMinValue || value > CartsMaxValue)
                {
                    throw new InvalidUserInputException($"A train cannot have less than {CartsMinValue} cart or more than {CartsMaxValue} carts.");
                }
                carts = value;
            }
        }
        protected override void ValidatePassengers(int passengers)
        {
            if (passengers < PassengerCapacityMinValue || passengers > PassengerCapacityMaxValue)
            {
                throw new InvalidUserInputException($"A train cannot have less than {PassengerCapacityMinValue} passengers or more than {PassengerCapacityMaxValue} passengers.");
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Train ----");
            sb.AppendLine($"Passenger capacity: {this.PassangerCapacity}");
            sb.AppendLine($"Price per kilometer: {this.PricePerKilometer}");
            sb.AppendLine($"Carts amount: {this.Carts}");

            return sb.ToString();
        }
    }
}
