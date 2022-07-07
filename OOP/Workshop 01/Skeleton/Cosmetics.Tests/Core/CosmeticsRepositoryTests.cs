using System;
using Cosmetics.Core;
using Cosmetics.Models;
using Cosmetics.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cosmetics.Tests.Core
{
    [TestClass]
    public class CosmeticsRepositoryTests
    {
        private Repository repository;

        [TestInitialize]
        public void SetUp()
        {
            repository = TestUtilities.InitializeRepository();
        }

        [TestMethod]
        public void Constructor_Should_InitializeAllCollections()
        {
            // Arrange, Act, Assert
            Assert.IsNotNull(repository.Products);
            Assert.IsNotNull(repository.Categories);
        }

        [TestMethod]
        public void Constructor_Should_InitializeShoppingCart()
        {
            // Arrange, Act, Assert
            Assert.IsNotNull(repository.ShoppingCart);
        }

        [TestMethod]
        public void GetCategories_Should_ReturnCopyOfCollection()
        {
            // Arrange
            repository.CreateCategory(TestData.CategoryData.ValidName);
            repository.Categories.Clear();

            // Act, Assert
            Assert.AreEqual(1, repository.Categories.Count);
        }

        [TestMethod]
        public void GetProducts_Should_ReturnCopyOfCollection()
        {
            // Arrange
            repository.CreateProduct(
                    TestData.ProductData.ValidName,
                    TestData.ProductData.ValidBrand,
                    2, GenderType.Women);
            repository.Products.Clear();

            // Act, Assert
            Assert.AreEqual(1, repository.Products.Count);
        }

        [TestMethod]
        public void CategoryExists_Should_ReturnTrue_WhenCategoryExists()
        {
            // Arrange
            string categoryName = TestData.CategoryData.ValidName;
            repository.CreateCategory(categoryName);

            // Act, Assert
            Assert.IsTrue(repository.CategoryExist(categoryName));
        }

        [TestMethod]
        public void ProductExists_Should_ReturnTrue_WhenProductExists()
        {
            // Arrange
            string productName = TestData.ProductData.ValidName;
            repository.CreateProduct(
                    TestData.ProductData.ValidName,
                    TestData.ProductData.ValidBrand,
                    2, GenderType.Women);

            // Act, Assert
            Assert.IsTrue(repository.ProductExist(productName));
        }

        [TestMethod]
        public void CreateProduct_Should_CreateSuccessfully_WhenArgumentsAreValid()
        {
            // Arrange
            repository.CreateProduct(
                    TestData.ProductData.ValidName,
                    TestData.ProductData.ValidBrand,
                    2, GenderType.Women);

            // Act, Assert
            Assert.AreEqual(1, repository.Products.Count);
        }

        [TestMethod]
        public void CreateCategory_Should_CreateSuccessfully_WhenArgumentsAreValid()
        {
            // Arrange
            repository.CreateCategory(TestData.CategoryData.ValidName);

            // Act, Assert
            Assert.AreEqual(1, repository.Categories.Count);
        }

        [TestMethod]
        public void FindCategoryByName_Should_ReturnCategory_IfExists()
        {
            // Arrange
            string categoryName = TestData.CategoryData.ValidName;
            repository.CreateCategory(categoryName);

            // Act
            Category found = repository.FindCategoryByName(categoryName);

            // Assert
            Assert.AreEqual(found.Name, categoryName);
        }

        [TestMethod]
        public void FindCategoryByName_Should_ThrowException_IfDoesNotExist()
        {
            // Arrange, Act, Assert
            Assert.ThrowsException<ArgumentException>(() => repository.FindCategoryByName("name"));
        }

        [TestMethod]
        public void FindProductByName_Should_ReturnCategory_IfExists()
        {
            // Arrange
            string productName = TestData.ProductData.ValidName;
            repository.CreateProduct(
                    TestData.ProductData.ValidName,
                    TestData.ProductData.ValidBrand,
                    2, GenderType.Women);

            // Act
            Product found = repository.FindProductByName(productName);

            // Assert
            Assert.AreEqual(found.Name, productName);
        }

        [TestMethod]
        public void FindProductByName_Should_ThrowException_IfDoesNotExist()
        {
            // Arrange, Act, Assert
            Assert.ThrowsException<ArgumentException>(() => repository.FindProductByName("name"));
        }
    }
}
