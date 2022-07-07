using System;
using System.Text;

namespace Cosmetics.Models
{
    public class Product
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 10;
        public const int BrandMinLength = 2;
        public const int BrandMaxLength = 10;
        private double price;
        private string name;
        private string brand;
        private GenderType gender;

        // "Each product in the system has name, brand, price and gender."

        public Product(string name, string brand, double price, GenderType gender)
        {
            this.Name = name;
            this.Brand = brand;
            this.Price = price;
            this.gender = gender;
        }

        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                }
                this.price = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value.Length<3 || value.Length>10)
                {
                    throw new ArgumentException("Minimum product name’s length is 3 symbols and maximum is 10 symbols.");

                }
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
                if (value.Length < 2 || value.Length > 10)
                {
                    throw new ArgumentException("Minimum brand name’s length is 2 symbols and maximum is 10 symbols.");

                }
                this.brand = value;
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
            StringBuilder sb = new StringBuilder();
            // Format:
            //" #[Name] [Brand]
            // #Price: [Price]
            // #Gender: [Gender]

            sb.AppendLine($"#{this.Name} {this.Brand}");
            sb.AppendLine($"#Price: {this.Price}");
            sb.AppendLine($"#Gender {this.Gender}");
            return sb.ToString();
        }

        public override bool Equals(object p)
        {
            if (p == null || !(p is Product))
            {
                return false;
            }

            if (this == p)
            {
                return true;
            }

            Product otherProduct = (Product)p;

            return this.Price == otherProduct.Price
                    && this.Name == otherProduct.Name
                    && this.Brand == otherProduct.Brand
                    && this.Gender == otherProduct.Gender;
        }
    }
}