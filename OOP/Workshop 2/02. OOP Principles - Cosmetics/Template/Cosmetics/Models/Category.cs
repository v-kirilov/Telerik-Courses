using Cosmetics.Helpers;
using Cosmetics.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmetics.Models
{
    public class Category : ICategory
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 15;

        private string name;
        private readonly ICollection<IProduct> products;

        public Category(string name)
        {
            this.Name = name;
            this.products = new List<IProduct>();
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                ValidationHelper.ValidateStringLength(value, NameMinLength, NameMaxLength);
                this.name = value;
            }
        }

        public ICollection<IProduct> Products
        {
            get
            {
                return new List<IProduct>(this.products);
            }
        }

        public void AddProduct(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            this.products.Add(product);
        }

        public void RemoveProduct(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            var productFound = this.products.FirstOrDefault(x => x.Name == product.Name);

            if (productFound == null)
            {
                throw new ArgumentNullException();
            }

            this.products.Remove(productFound);
        }

        public string Print()
        {
            if (!this.products.Any())
            {
                return $"#Category: {this.Name}\r\n #No products in this category";
            }

            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"#Category: {this.Name}");

            foreach (var product in this.products)
            {
                strBuilder.AppendLine(product.Print());
            }

            return strBuilder.ToString().Trim();
        }
    }
}
