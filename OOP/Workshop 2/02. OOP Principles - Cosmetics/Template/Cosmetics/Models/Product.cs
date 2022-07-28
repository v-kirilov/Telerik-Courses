using Cosmetics.Helpers;
using Cosmetics.Models.Contracts;
using Cosmetics.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Cosmetics.Models
{
    public abstract class Product : IProduct
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 10;
        public const int BrandMinLength = 2;
        public const int BrandMaxLength = 10;
        public string name;
        public string brand;
        public decimal price;
        public GenderType gender;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                ValidationHelper.ValidateStringLength(value, NameMinLength, NameMaxLength);

                this.name = value;
            }
        }
        public string Brand
        {
            get
            {
                return this.brand;
            }
            private set
            {
                ValidationHelper.ValidateStringLength(value, BrandMinLength, BrandMaxLength);


                this.brand = value;
            }
        }
        public decimal Price
        {
            get
            {
                return price;
            }
            private set
            {

                ValidationHelper.ValidateNonNegative(value, "Price");
                this.price = value;
            }
        }

        public GenderType Gender
        {
get
{
                return gender;
            }
            private set
            {
                if (value != GenderType.Men && value != GenderType.Women && value != GenderType.Unisex)
                {
                    throw new ArgumentException($"Gender must be valid!");

                }
                this.gender = value;
            }
        }

        public Product(string name, string brand, decimal price, GenderType gender)
        {
            this.Name = name;
            this.Brand = brand;
            this.Price = price;
            this.Gender = gender;
        }
        public abstract string Print();
        
    }
}
