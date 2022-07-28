
using Dealership.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Models
{
    public class Motorcycle : Vehicle, IMotorcycle
    {

        public const int CategoryMinLength = 3;
        public const int CategoryMaxLength = 10;
        public const string InvalidCategoryError = "Category must be between 3 and 10 characters long!";

        public Motorcycle(string make, string model, decimal price, string category)
            : base(make, model, price, VehicleType.Motorcycle)
        {
            Validator.ValidateIntRange(category.Length, CategoryMinLength, CategoryMaxLength, InvalidCategoryError);

            this.Category = category;
        }
        public string Category
        {
            get;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.Print());
            sb.AppendLine($"  Category: {this.Category}");
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
            return "Motorcycle";
        }
    }
}

