using System;
using System.Text;

namespace CosmeticsShop.Models
{
    public class Product
    {
        private const int MinLength = 3;
        private const int BrandMinLength = 2;
        private const int MaxLength = 10;
        private string name;
        private string brand;
        private double price;
        private readonly GenderType gender;

        public Product(string name, string brand, double price, GenderType gender)
        {
            this.Name = name;
            this.Brand = brand;
            this.Price = price;
            this.gender = gender;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                InvalidUserInputException.ValidateProductNameLength(value);
                this.name = value;
            }
        }

        public string Brand
        {
            get
            {
                return this.brand;
            }
            set
            {
                InvalidUserInputException.ValidateProductBrandLength(value);
                this.brand = value;
            }
        }

        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                //if (value <0)
                //{
                //    throw new ArgumentException($"Price can't be negative.");
                //}
                this.price = value;
            }
        }

        public GenderType Gender
        {
            get
            {
                return this.gender;
            }
        }

        public string Print()
        {
            var strBulder = new StringBuilder();
            strBulder.AppendLine($"#{this.name} {this.brand}");
            strBulder.AppendLine($" #Price: ${this.price}");
            strBulder.AppendLine($" #Gender: {this.gender}");

            return strBulder.ToString().Trim();
        }
    }
}
