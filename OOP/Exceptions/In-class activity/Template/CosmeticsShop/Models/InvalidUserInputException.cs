using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using CosmeticsShop.Commands;

namespace CosmeticsShop.Models
{
    internal class InvalidUserInputException : ApplicationException
    {
        private const string InvalidNumberOfArguments = "{0} command expects {1} parameters.";
        private const string InvalidProductNameLength = "Product name should be between {0} and {1} symbols.";
        private const string InvalidCategoryNameLength = "Category name should be between {0} and {1} symbols.";
        private const string InvalidProductBrandLength = "Product brand should be between {0} and {1} symbols.";
        private const int NameMinLength = 3;
        private const int NameMaxLength = 10;
        private const int BrandMinLength = 2;

       

        private const int BrandMaxLength = 10;
        public InvalidUserInputException(string message) : base(message)
        {

        }
        public static void ValidateArgumentsCount(List<string> list, int expectedCount,string type)
        {
            if (list.Count != expectedCount)
            {
                throw new ArgumentException(string.Format(InvalidNumberOfArguments, type, expectedCount));
            }
        }

        public static void ValidateProductNameLength(string name)
        {
            if (name.Length < NameMinLength || name.Length > NameMaxLength)
            {
                throw new ArgumentException(string.Format(InvalidProductNameLength, NameMinLength, NameMaxLength));
            }
        }
        public static void ValidateCategoryNameLength(string name)
        {
            if (name.Length < NameMinLength || name.Length > NameMaxLength)
            {
                throw new ArgumentException(string.Format(InvalidCategoryNameLength, NameMinLength, NameMaxLength));
            }
        }

        public static void ValidateProductBrandLength(string name)
        {
            if (name.Length < NameMinLength || name.Length > NameMaxLength)
            {
                throw new ArgumentException(string.Format(InvalidProductNameLength, BrandMinLength, BrandMaxLength));
            }
        }

        public static void ValidatePrice(string price)
        {
            try
            {
                double realPrice = double.Parse(price, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Third parameter should be price (real number).");
            }
            if (double.Parse(price) < 0)
            {
                throw new ArgumentException($"Price can't be negative.");

            }
        }

        public static void ValidateGenderFormat(string gender)
        {
            if (gender != GenderType.Men.ToString() && gender != GenderType.Unisex.ToString() && gender != GenderType.Women.ToString())
            {
                throw new ArgumentException($"Forth parameter should be one of Men, Women or Unisex.");
            }
        }
        public static void ValidateCommandFormat(string commandTypeValue)
        {
            if (commandTypeValue != Commands.CommandType.AddProductToCategory.ToString() && commandTypeValue != Commands.CommandType.CreateCategory.ToString() && commandTypeValue != Commands.CommandType.CreateProduct.ToString()&& commandTypeValue != Commands.CommandType.ShowCategory.ToString())
            {
                throw new ArgumentException($"Command {commandTypeValue} is not supported.");
            }
        }
        public static void ValidateIfProductExists(List<Product> products, string nameOfProduct)
        {
            foreach (var product in products)
            {
                if (product.Name == nameOfProduct)
                {
                    throw new ArgumentException($"Product {nameOfProduct} already exist.");
                }
            }
        }

        internal static void ValidateIfCategoryExists(List<Category> categories, string categoryName)
        {
            foreach (var category in categories)
            {
                if (category.Name == categoryName)
                {
                    throw new ArgumentException($"Category {categoryName} already exist.");
                }
            }
        }

    }
}
