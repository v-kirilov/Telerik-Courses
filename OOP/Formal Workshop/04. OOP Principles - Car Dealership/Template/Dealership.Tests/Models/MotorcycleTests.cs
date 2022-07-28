using Dealership.Models;
using Dealership.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dealership.Tests.Models
{
    [TestClass]
    public class MotorcycleTests
    {
        [TestMethod]
        public void Motorcycle_Should_ImplementIMotorcycleInterface()
        {
            var type = typeof(Motorcycle);
            var isAssignable = typeof(IMotorcycle).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Motorcycle class does not implement IMotorcycle interface!");
        }

        [TestMethod]
        public void Motorcycle_Should_ImplementIVehicleInterface()
        {
            var type = typeof(Motorcycle);
            var isAssignable = typeof(IVehicle).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Motorcycle class does not implement IVehicle interface!");
        }

        [TestMethod]
        public void Motorcycle_Should_DeriveFromVehicle()
        {
            var type = typeof(Motorcycle);
            var isAssignable = typeof(Vehicle).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Motorcycle class does not derive from Vehicle base class!");
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_MakeLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Motorcycle("1", "model", 10, "category"));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_MakeLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new Motorcycle("1234567890123456", "model", 10, "category"));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_ModelLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Motorcycle("make", "", 10, "category"));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_ModelLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new Motorcycle("make", "1234567890123456", 10, "category"));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PriceIsNegative()
        {
            Assert.ThrowsException<ArgumentException>(() => new Motorcycle("make", "model", -10, "category"));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PriceIsAbove100000()
        {
            Assert.ThrowsException<ArgumentException>(() => new Motorcycle("make", "model", 1000001.0m, "category"));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_CategoryLenghtIsBelow1()
        {
            Assert.ThrowsException<ArgumentException>(() => new Motorcycle("make", "model", 10, ""));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_CategoryLenghtIsAbove10()
        {
            Assert.ThrowsException<ArgumentException>(() => new Motorcycle("make", "model", 10, "12345678901"));
        }

        [TestMethod]
        public void Constructor_Should_CreateNewMotorcycle_When_ParametersAreCorrect()
        {
            // Arrange, Act
            Motorcycle motorcycle = new Motorcycle("make", "model", 10, "category");

            // Assert
            Assert.AreEqual("make", motorcycle.Make);
        }
    }
}
