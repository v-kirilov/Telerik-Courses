using System;
using System.Collections.Generic;
using System.Text;
using Cosmetics.Models.Contracts;
using Cosmetics.Models.Enums;

namespace Cosmetics.Models
{
    public class Cream : Product, ICream
    {

        private ScentType scent;

        public ScentType Scent
        {
            get
            {
                return this.scent;
            }
            private set
            {
                if (value != ScentType.lavender && value != ScentType.vanilla && value != ScentType.rose)
                {
                    throw new ArgumentException("Scent not correct!");

                }
                this.scent = value;
            }
        }

        public Cream(string name, string brand, decimal price, GenderType gender,ScentType scent) : base(name, brand, price, gender)
        {
            if (name.Length<3||name.Length>15)
            {
                throw new ArgumentException("Name minimum 3 symbols and maximum 15");
            }
            if (brand.Length<3 || brand.Length>15)
            {
                throw new ArgumentException("Brand minimum 3 symbols and maximum 15");
            }
            this.Scent = scent;
        }


        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"#{this.name} {this.brand}");
            sb.AppendLine($" #Price: ${this.price}");
            sb.AppendLine($" #Gender: {this.gender}");
            sb.AppendLine($" #Scent: {this.scent}");
            sb.AppendLine($" ===");

            return sb.ToString();
        }
    }
}
