
using Dealership.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Dealership.Models
{
    public class Car : Vehicle, ICar
    {
      
        public const int MinSeats = 1;
        public const int MaxSeats = 10;
        public const string InvalidSeatsError = "Seats must be between 1 and 10!";

        public Car(string make, string model, decimal price, int seats)
            : base(make, model, price, VehicleType.Car)
        {
            Validator.ValidateIntRange(seats, MinSeats, MaxSeats, InvalidSeatsError);
            this.Seats = seats;
        }

        public int Seats
        {
            get;
        }
           
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.Print());
            sb.AppendLine($"  Seats: {this.Seats}");
            if (this.comments.Count == 0)
            {
                sb.AppendLine("    --NO COMMENTS--");
            }
            else
            {
                sb.AppendLine("    --COMMENTS--");
                foreach (var coment in this.comments)
{
                    sb.AppendLine(coment.ToString());

                }
                sb.AppendLine("    --COMMENTS--");

            }

            return sb.ToString();
        }

        public override string MyType()
        {
            return "Car";
        }
    }
}
