using CosmeticsShop.Models;
using System;
using System.Collections.Generic;

namespace CosmeticsShop.Core
{
    public class CosmeticsRepository
    {
        private readonly List<Category> categories;
        private readonly List<Product> products;

        public CosmeticsRepository()
        {
            this.categories = new List<Category>();
            this.products = new List<Product>();
        }

        public List<Category> Categories
        {
            get
            {
                return this.categories;
            }
        }

        public List<Product> Products
        {
            get
            {
                return this.products;
            }
        }

        public void CreateCategory(string categoryName)
        {
            InvalidUserInputException.ValidateIfCategoryExists(this.Categories, categoryName);
            this.categories.Add(new Category(categoryName));
        }

        public void CreateProduct(string name, string brand, double price, GenderType gender)
        {
            InvalidUserInputException.ValidateIfProductExists(this.products, name);
            this.products.Add(new Product(name, brand, price, gender));
        }

        public bool CategoryExist(string name)
        {
            foreach (var category in this.categories)
            {
                if (category.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public Category FindCategoryByName(string name)
        {
            foreach (var category in this.categories)
            {
                if (category.Name == name)
                {
                    return category;
                }
            }

            throw new ArgumentException($"Category {name} does not exist.");
        }

        public bool ProductExist(string name)
        {
            foreach (var product in this.products)
            {
                if (product.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public Product FindProductByName(string name)
        {
            foreach (var product in this.products)
            {
                if (product.Name == name)
                {
                    return product;
                }
            }

            throw new ArgumentException($"Product {name} does not exist.");

        }
    }
}
