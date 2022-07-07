using Cosmetics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using static Cosmetics.Tests.Helpers.TestData;

namespace Cosmetics.Tests.Models
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void Constructor_Should_ThrowException_When_NameLengthOutOfBounds()
        {
            Assert.ThrowsException<ArgumentException>(() => new Category("x"));
        }

        [TestMethod]
        public void Constructor_Should_CreateCategory_When_NameIsValid()
        {
            Category category = new Category(CategoryData.ValidName);

            Assert.AreEqual(CategoryData.ValidName, category.Name);
        }

        [TestMethod]
        public void Constructor_Should_InitializeNewListOfProducts_When_CategoryCreated()
        {
            Category category = new Category(CategoryData.ValidName);

            Assert.IsNotNull(category.Products);
        }

        [TestMethod]
        public void AddProduct_Should_AddProductToList()
        {
            // Arrange
            Category category = new Category(CategoryData.ValidName);
            Product productToAdd = new Product(ProductData.ValidName, ProductData.ValidBrand, 10, GenderType.Men);

            // Act
            category.AddProduct(productToAdd);

            // Assert
            Assert.AreEqual(1, category.Products.Count);
        }

        [TestMethod]
        public void RemoveProduct_Should_RemoveProductFromList()
        {
            // Arrange
            Category category = new Category(CategoryData.ValidName);
            Product product = new Product(ProductData.ValidName, ProductData.ValidBrand, 10, GenderType.Men);
            category.AddProduct(product);

            // Act
            category.RemoveProduct(product);

            // Assert
            Assert.AreEqual(0, category.Products.Count);
        }
    }
}
