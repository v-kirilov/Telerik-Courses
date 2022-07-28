using Cosmetics.Models.Enums;
using System;
using Cosmetics.Helpers;
using Cosmetics.Models.Contracts;
using System.Text;

namespace Cosmetics.Models
{
    public class Toothpaste : Product, IToothpaste
    {

       private string ingredients;

        public string Ingredients
        {
            get { return this.ingredients; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                this.ingredients = value;
            }
        }

        public Toothpaste(string name, string brand, decimal price, GenderType gender, string ingredients)
            : base(name, brand, price, gender)
        {
            if (ingredients == null)
            {
                throw new ArgumentNullException();
            }

            this.ingredients = ingredients;
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"#{this.name} {this.brand}");
            sb.AppendLine($" #Price: ${this.price}");
            sb.AppendLine($" #Gender: {this.gender}");
            sb.AppendLine($" #Ingredients: {this.ingredients}");
            sb.AppendLine($" ===");

            return sb.ToString();
        }
    }
}
