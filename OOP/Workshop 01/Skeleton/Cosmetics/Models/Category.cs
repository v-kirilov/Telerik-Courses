using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmetics.Models
{
    public class Category
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 15;
        private string name;
        private  List<Product> products = new List<Product>();

        public Category(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value.Length<2 || value.Length>15)
                {
                    throw new ArgumentException("Minimum category name’s length is 2 symbols and maximum is 15 symbols.");
                }
                this.name = value;
            }
        }

        public List<Product> Products
        {
            get
            {
               // return this.products;
                // List encapsulation is tricky.
                List<Product> copy = new List<Product>(this.products);  //-Create a new copy of the List and return it.
                 return copy;
            }
            
        }

        public void AddProduct(Product product)
        {
            this.products.Add(product);
            this.products.OrderBy(p => p.Brand).OrderByDescending(p => p.Price).ToList();

        }

        public void RemoveProduct(Product product)
        {
            if (!products.Contains(product))
            {
                throw new ArgumentException("product is not found");
            }
            products.Remove(product);
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            

            sb.AppendLine($"#Category: {this.Name}");
            foreach (var product in products)
            {
                sb.AppendLine($"#{product.Name} {product.Brand}");
                sb.AppendLine($"#Price: ${product.Price}");
                sb.AppendLine($"#Gender {product.Gender}");
                sb.AppendLine($"===");
            }
            if (products.Count==0)
            {
                sb.AppendLine($"#No products in this category");
            }

            return sb.ToString();
           
        }
    }
}

