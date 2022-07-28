using Cosmetics.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmetics.Models
{
    class ShoppingCart : IShoppingCart
    {
        private readonly ICollection<IProduct> productList;

        public ShoppingCart()
        {
            this.productList = new List<IProduct>();
        }

        public ICollection<IProduct> ProductList
        {
            get { return new List<IProduct>(this.productList); }
        }

        public void AddProduct(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            this.productList.Add(product);
        }

        public void RemoveProduct(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            this.productList.Remove(product);
        }

        public bool ContainsProduct(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            return this.productList.Any(x => x.Name == product.Name);
        }

        public decimal TotalPrice()
        {
            return this.productList.Sum(x => x.Price);
        }
    }
}
