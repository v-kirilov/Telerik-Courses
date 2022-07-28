using Cosmetics.Models.Enums;
using System;
using Cosmetics.Helpers;
using Cosmetics.Models.Contracts;
using System.Text;

namespace Cosmetics.Models
{
    public class Shampoo : Product, IShampoo
    {

        private UsageType usage;
        private int millilitres;

        public int Millilitres
        {
            get
            {
                return this.millilitres;
            }
            private set
            {
                ValidationHelper.ValidateNonNegative(value, "Mililitres");
                this.millilitres = value;
            }
        }

        public UsageType Usage
        {
            get
            {
                return this.usage;
            }
            private set
            {

                if (value != UsageType.EveryDay && value != UsageType.Medical)
                {
                    throw new ArgumentException("Use not correct!");

                }
                this.usage = value;
            }
        }

        public Shampoo(string name, string brand, decimal price, GenderType gender, int millilitres, UsageType usage)
            : base(name, brand, price, gender)
        {

            this.Millilitres = millilitres;
            this.Usage = usage;
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"#{this.name} {this.brand}");
            sb.AppendLine($" #Price: ${this.price}");
            sb.AppendLine($" #Gender: {this.gender}");
            sb.AppendLine($" #Milliliters: {this.millilitres}");
            sb.AppendLine($" #Usage: {this.usage}");
            sb.AppendLine($" ===");

            return sb.ToString();
        }
    }
}
