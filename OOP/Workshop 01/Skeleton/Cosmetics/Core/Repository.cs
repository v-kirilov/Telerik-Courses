using Cosmetics.Core.Contracts;
using Cosmetics.Models;
using System;
using System.Collections.Generic;

namespace Cosmetics.Core
{
    public class Repository : IRepository
    {
        private readonly List<Product> products;
        private readonly List<Category> categories;
        private readonly ShoppingCart shoppingCart;

        public Repository()
        {
            this.products = new List<Product>();
            this.categories = new List<Category>();

            this.shoppingCart = new ShoppingCart();
        }

        public ShoppingCart ShoppingCart
        {
            get
            {
                return this.shoppingCart;
            }
        }

        public List<Category> Categories
        {
            get
            {
                return new List<Category>(this.categories);
            }
        }

        public List<Product> Products
        {
            get
            {
                return new List<Product>(this.products);
            }
        }

        public Product FindProductByName(string productName)
        {
            
            
            foreach (var product in products)
            {
                if (product.Name==productName)
                {
                   
                    return product;
                }
            }
            throw new ArgumentException($"Product {productName} does not exist");
           
            /**
             * Hint: You have to go through every product and see if one has name equal to productName.
             *       If not, "throw new ArgumentException("Product {productName} does not exist");"
             */
        }

        public Category FindCategoryByName(string categoryName)
        {
            foreach (var category in categories)
            {
                if (category.Name == categoryName)
                {

                    return category;
                }
            }
            throw new ArgumentException($"Category {categoryName} does not exist");
            /**
             * Hint: You have to go through every category and see if one has name equal to categoryName.
             *       If not, "throw new ArgumentException("Category {categoryName} does not exist");"
             */
        }

        public void CreateCategory(string categoryName)
        {
            categories.Add(new Category(categoryName));
            //throw new NotImplementedException("Not implemented yet.");
        }

        public void CreateProduct(string name, string brand, double price, GenderType gender)
        {
            products.Add(new Product(name, brand, price, gender));
            //throw new NotImplementedException("Not implemented yet.");
        }

        public bool CategoryExist(string categoryName)
        {
            foreach (var cat in categories)
            {
                if (cat.Name==categoryName)
                {
                    return true;
                }
            }
            return false;
            /**
             * Hint: You have to go through each category and see if one has name equal to categoryName.
             *       If there is one, return true. If not, return false.
             */
        }

        public bool ProductExist(string productName)
        {
            foreach (var product in products)
            {
                if (product.Name == productName)
                {
                    return true;
                }
            }
            return false;
            /**
             * Hint: You have to go through each product and see if one has name equal to productName.
             *       If there is one, return true. If not, return false.
             */
        }
    }
}
