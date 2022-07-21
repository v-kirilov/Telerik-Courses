using System;
using System.Text;
using Agency.Exceptions;
using Agency.Models.Contracts;

namespace Agency.Models
{
    public class Ticket : ITicket
    {
        private double administrativeCosts;
        private IJourney journey;
        private int id;
        public Ticket(int id, IJourney journey, double administrativeCosts)
        {
            this.Journey=journey;
            this.AdministrativeCosts =administrativeCosts;
            this.Id = id;
        }


        public double AdministrativeCosts
        {
            get
            {
                return this.administrativeCosts;
            }
            set
            {
                InvalidUserInputException.ValidatePrice(value);

                this.administrativeCosts = value;
            }
        }

        public IJourney Journey
        {
            get
            {
                return this.journey;
            }
            set
            {
                this.journey = value;
            }
        }

        public int Id { get; private set; }

        public double CalculatePrice()
        {
            return this.AdministrativeCosts*this.Journey.CalculateTravelCosts();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Ticket ----");
            sb.AppendLine($"Destination: {this.Journey.Destination}");
            sb.AppendLine($"Price: {this.CalculatePrice()}");

            return sb.ToString();
        }
    }
}
