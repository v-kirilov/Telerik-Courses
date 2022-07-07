using Cosmetics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Cosmetics.Tests.Helpers.TestData;

namespace Cosmetics.Tests.Models
{
    [TestClass]
    public class ShoppingCartTests
    {
        private ShoppingCart cart;

        [TestInitialize]
        public void SetUp()
        {
            cart = new ShoppingCart();
        }

        [TestMethod]
        public void Constructor_Should_InitializeNewListOfProducts_When_ShoppingCartCreated()
        {
            Assert.IsNotNull(cart.Products);
        }

        [TestMethod]
        public void AddProduct_Should_AddProductToList()
        {
            // Arrange, Act
            Product productToAdd = new Product(ProductData.ValidName, ProductData.ValidBrand, 10, GenderType.Women);
            cart.AddProduct(productToAdd);

            // Assert
            Assert.AreEqual(1, cart.Products.Count);
        }

        [TestMethod]
        public void RemoveProduct_Should_RemoveProductFromList()
        {
            // Arrange
            Product product = new Product(ProductData.ValidName, ProductData.ValidBrand, 10, GenderType.Men);
            cart.AddProduct(product);

            // Act
            cart.RemoveProduct(product);

            // Assert
            Assert.AreEqual(0, cart.Products.Count);
        }

        [TestMethod]
        public void ContainsProduct_Should_ReturnTrue_When_ProductFound()
        {
            // Arrange
            Product product = new Product(ProductData.ValidName, ProductData.ValidBrand, 10, GenderType.Women);
            cart.AddProduct(product);

            // Act
            bool isFound = cart.ContainsProduct(product);

            // Assert
            Assert.IsTrue(isFound);
        }

        [TestMethod]
        public void ContainsProduct_Should_ReturnFalse_When_ProductNotFound()
        {
            // Arrange
            Product product = new Product(ProductData.ValidName, ProductData.ValidBrand, 10, GenderType.Women);

            // Act
            bool isFound = cart.ContainsProduct(product);

            // Assert
            Assert.IsFalse(isFound);
        }
    }
}
