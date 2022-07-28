using Cosmetics.Core;
using Cosmetics.Core.Contracts;
using Cosmetics.Models.Contracts;
using Cosmetics.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Cosmetics.Tests.Helpers.TestData;

namespace Cosmetics.Tests.Core
{
    [TestClass]
    public class CosmeticsRepositoryTests
    {
        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new Repository();
        }

        [TestMethod]
        public void CreateCategory_Should_AddCategoryToList()
        {
            // Arrange, Act
            repository.CreateCategory("TestCategory");

            // Assert
            Assert.AreEqual(1, repository.Categories.Count);
        }

        [TestMethod]
        public void CreateShampoo_Should_AddProductToList()
        {
            // Arrange, Act
            repository.CreateShampoo(ShampooData.ValidName, ShampooData.ValidBrand, 10m, GenderType.Men, 1000, UsageType.EveryDay);

            // Assert
            Assert.AreEqual(1, repository.Products.Count);
        }

        [TestMethod]
        public void CreateToothpaste_Should_AddProductToList()
        {
            // Arrange, Act
            repository.CreateToothpaste(ToothpasteData.ValidName, ToothpasteData.ValidBrand, 10m, GenderType.Men, "test1,test2");

            // Assert
            Assert.AreEqual(1, repository.Products.Count);
        }

        [TestMethod]
        public void FindCategoryByName_Should_ReturnCategory_When_CategoryExists()
        {
            // Arrange
            string categoryName = "TestCategory";
            repository.CreateCategory(categoryName);

            // Act
            ICategory category = repository.FindCategoryByName(categoryName);

            // Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(categoryName, category.Name);
        }

        [TestMethod]
        public void FindProductByName_Should_ReturnProduct_When_ProductExists()
        {
            // Arrange
            string productName = ShampooData.ValidName;
            repository.CreateShampoo(productName, ShampooData.ValidBrand, 10m, GenderType.Men, 1000, UsageType.EveryDay);

            // Act
            IProduct product = repository.FindProductByName(productName);

            // Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(productName, product.Name);
        }

        [TestMethod]
        public void CategoryExists_Should_ReturnTrue_When_CategoryExists()
        {
            // Arrange
            string categoryName = "TestCategory";
            repository.CreateCategory(categoryName);

            // Act
            bool categoryExists = repository.CategoryExists(categoryName);

            // Assert
            Assert.IsTrue(categoryExists);
        }

        [TestMethod]
        public void ProductExists_Should_ReturnTrue_When_ProductExists()
        {
            // Arrange
            string productName = ShampooData.ValidName;
            repository.CreateShampoo(productName, ShampooData.ValidBrand, 10m, GenderType.Men, 1000, UsageType.EveryDay);

            // Act
            bool productExists = repository.ProductExists(productName);

            // Assert
            Assert.IsTrue(productExists);
        }
    }
}
