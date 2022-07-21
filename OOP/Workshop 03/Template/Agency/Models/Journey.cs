using System;
using System.Text;
using Agency.Exceptions;
using Agency.Models.Contracts;

namespace Agency.Models
{
    public class Journey : IJourney
    {
        public const int StartLocationMinLength = 5;
        public const int StartLocationMaxLength = 25;
        public const int DestinationMinLength = 5;
        public const int DestinationMaxLength = 25;
        public const int DistanceMinValue = 5;
        public const int DistanceMaxValue = 5000;

        public Journey(int id, string from, string to, int distance, IVehicle vehicle)
        {
            this.StartLocation = from;
            this.Destination = to;
            this.Distance = distance;
            this.Vehicle = vehicle;
            this.Id = id;
        }

        private string startLocation;

        public string StartLocation
        {
            get
            {
                return this.startLocation;
            }
            private set
            {
                if (value.Length<StartLocationMinLength || value.Length>StartLocationMaxLength)
                {
                    throw new InvalidUserInputException($"The StartingLocation's length cannot be less than {StartLocationMinLength} or more than {StartLocationMaxLength} symbols long.");
                }
                this.startLocation = value;
            }
        }
        private string destination;

        public string Destination
        {
            get
            {
                return this.destination;
            }
            private set
            {
                if (value.Length < DestinationMinLength || value.Length > DestinationMaxLength)
                {
                    throw new InvalidUserInputException($"The Destination's length cannot be less than {DestinationMinLength} or more than {DestinationMaxLength} symbols long.");
                }
                this.destination = value;
            }
        }
        private int distance;

        public int Distance
        {
            get
            {
                return this.distance;
            }
            private set
            {
                if (value < DistanceMinValue || value > DistanceMaxValue)
                {
                    throw new InvalidUserInputException($"The Distance cannot be less than {DestinationMinLength} or more than {DestinationMaxLength} kilometers.");
                }
                this.distance = value;
            }
        }

        private IVehicle vehicle;

        public IVehicle Vehicle
        {
            get { return vehicle; }
            set { vehicle = value; }
        }

        public int Id
        {
            get;
        }

        public double CalculateTravelCosts()
        {
            return this.Distance * vehicle.PricePerKilometer;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Journey ----");
            sb.AppendLine($"Start location: {this.StartLocation}");
            sb.AppendLine($"Destination: {this.Destination}");
            sb.AppendLine($"Distance: { this.Distance}");
            sb.AppendLine($"Travel costs: {this.CalculateTravelCosts():f2}");

            return sb.ToString();
        }
    }
}
