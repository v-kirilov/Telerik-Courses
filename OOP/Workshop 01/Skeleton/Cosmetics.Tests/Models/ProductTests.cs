using System;
using Cosmetics.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Cosmetics.Tests.Helpers.TestData;

namespace Cosmetics.Tests.Models
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Constructor_Should_ThrowException_When_NameLengthOutOfBounds()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Product(
                    name: "x",
                    brand: ProductData.ValidBrand,
                    price: 3.6,
                    GenderType.Men));
        }

        [TestMethod]
        public void Constructor_Should_ThrowException_When_BrandLengthOutOfBounds()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Product(
                    name: ProductData.ValidName,
                    brand: "x",
                    price: 3.6,
                    GenderType.Women));
        }

        [TestMethod]
        public void Constructor_Should_ThrowException_When_PriceIsNegative()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Product(
                    name: ProductData.ValidName,
                    brand: ProductData.ValidBrand,
                    price: -2.0,
                    GenderType.Unisex));
        }

        [TestMethod]
        public void Constructor_Should_SetProperties_When_ArgumentsAreValid()
        {
            Product product = new Product(ProductData.ValidName, ProductData.ValidBrand, 3.6, GenderType.Men);

            Assert.AreEqual(ProductData.ValidName, product.Name);
            Assert.AreEqual(ProductData.ValidBrand, product.Brand);
            Assert.AreEqual(3.6, product.Price);
            Assert.AreEqual(GenderType.Men, product.Gender);
        }
    }
}
