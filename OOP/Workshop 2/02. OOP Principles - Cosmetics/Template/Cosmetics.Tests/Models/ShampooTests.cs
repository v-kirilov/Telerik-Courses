using Cosmetics.Models;
using Cosmetics.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using static Cosmetics.Tests.Helpers.TestData;

namespace Cosmetics.Tests.Models
{
    [TestClass]
    public class ShampooTests
    {

        [TestMethod]
        public void Constructor_Should_ThrowException_When_NameLengthOutOfBounds()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new Shampoo(
                    name: ShampooData.InvalidName,
                    brand: ShampooData.ValidBrand,
                    price: 10m,
                    gender: GenderType.Men,
                    millilitres: 10,
                    usage: UsageType.EveryDay));
        }

        [TestMethod]
        public void Constructor_Should_ThrowException_When_BrandLengthOutOfBounds()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new Shampoo(
                    name: ShampooData.ValidName,
                    brand: ShampooData.InvalidBrand,
                    price: 10m,
                    gender: GenderType.Women,
                    millilitres: 10,
                    usage: UsageType.EveryDay));
        }

        [TestMethod]
        public void Constructor_Should_ThrowException_When_PriceIsNegative()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new Shampoo(ShampooData.ValidName, ShampooData.ValidBrand, -1m, GenderType.Men, 10, UsageType.EveryDay));
        }

        [TestMethod]
        public void Constructor_Should_ThrowException_When_MillilitresAreNegative()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new Shampoo(ShampooData.ValidName, ShampooData.ValidBrand, 10m, GenderType.Men, -10, UsageType.EveryDay));
        }

        [TestMethod]
        public void Constructor_Should_CreateShampoo_When_ValidValuesArePassed()
        {
            var shampoo = new Shampoo(ShampooData.ValidName, ShampooData.ValidBrand, 10m, GenderType.Women, 10, UsageType.EveryDay);
            Assert.IsInstanceOfType(shampoo, typeof(Shampoo));
        }
    }
}
