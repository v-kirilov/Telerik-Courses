
using Dealership.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Dealership.Models
{
    public class Truck : Vehicle, ITruck
    {
      
        public const int MinCapacity = 1;
        public const int MaxCapacity = 100;
        public const string InvalidCapacityError = "Weight capacity must be between 1 and 100!";


        public Truck(string make, string model, decimal price, int weightCapacity)
            : base(make, model, price, VehicleType.Truck)
        {
            Validator.ValidateIntRange(weightCapacity, MinCapacity, MaxCapacity, InvalidCapacityError);
            this.WeightCapacity = weightCapacity;
        }


        public int WeightCapacity
        {
            get;
           
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.Print());
            sb.AppendLine($"  Weight Capacity: {this.WeightCapacity}t");
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
            return "Truck";
        }


    }
}
