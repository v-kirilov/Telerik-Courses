using Cosmetics.Models;
using System.Collections.Generic;

namespace Cosmetics.Core.Contracts
{
    public interface IRepository
    {
        ShoppingCart ShoppingCart { get; }

        List<Category> Categories { get; }

        List<Product> Products { get; }

        Product FindProductByName(string productName);

        Category FindCategoryByName(string categoryName);

        void CreateCategory(string categoryToAdd);

        void CreateProduct(string name, string brand, double price, GenderType gender);

        bool CategoryExist(string categoryName);

        bool ProductExist(string productName);
    }
}
