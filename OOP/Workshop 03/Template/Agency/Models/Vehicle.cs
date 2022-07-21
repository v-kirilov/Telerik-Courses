using Agency.Exceptions;
using Agency.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agency.Models
{
    public abstract class Vehicle : IVehicle
    {
        private int passangerCapacity;
        private double pricePerKilometer;
        //private int id;

        public int PassangerCapacity
        {
            get
            {
                return passangerCapacity;
            }
            private set
            {
                InvalidUserInputException.ValidatePassengers(1, 800, value);
                passangerCapacity = value;
            }
        }

        public double PricePerKilometer
        {
            get
            {
                return pricePerKilometer;
            }
            private set
            {
                if (value < 0.1 || value > 2.5)
                {
                    throw new InvalidUserInputException("A vehicle with a price per kilometer lower than $0.10 or higher than $2.50 cannot exist!");

                }
                pricePerKilometer = value;
            }
        }

        public int Id
        {
            get;

            set;

        }

        public Vehicle(int passengerCapacity, double pricePerKilometer)
        {
            ValidatePassengers(passengerCapacity);
            this.PassangerCapacity = passengerCapacity;
            this.PricePerKilometer = pricePerKilometer;

        }

        protected abstract void ValidatePassengers(int passengers);



    }
}

