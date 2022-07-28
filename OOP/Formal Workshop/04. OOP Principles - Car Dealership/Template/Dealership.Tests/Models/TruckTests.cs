using Dealership.Models;
using Dealership.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dealership.Tests.Models
{
    [TestClass]
    public class TruckTests
    {
        [TestMethod]
        public void Truck_Should_ImplementITruckInterface()
        {
            var type = typeof(Truck);
            var isAssignable = typeof(ITruck).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Truck class does not implement ITruck interface!");
        }

        [TestMethod]
        public void Truck_Should_ImplementIVehicleInterface()
        {
            var type = typeof(Truck);
            var isAssignable = typeof(IVehicle).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Truck class does not implement IVehicle interface!");
        }

        [TestMethod]
        public void Truck_Should_DeriveFromVehicle()
        {
            var type = typeof(Truck);
            var isAssignable = typeof(Vehicle).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Truck class does not derive from Vehicle base class!");
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_MakeLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Truck("1", "model", 10, 10));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_MakeLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new Truck("1234567890123456", "model", 10, 10));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_ModelLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Truck("make", "", 10, 10));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_ModelLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new Truck("make", "1234567890123456", 10, 10));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PriceIsNegative()
        {
            Assert.ThrowsException<ArgumentException>(() => new Truck("make", "model", -10, 10));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PriceIsAbove100000()
        {
            Assert.ThrowsException<ArgumentException>(() => new Truck("make", "model", 1000001.0m, 10));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_WeightCapacityIsNegative()
        {
            Assert.ThrowsException<ArgumentException>(() => new Truck("make", "model", 10, -10));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_WeightCapacityIsAbove100()
        {
            Assert.ThrowsException<ArgumentException>(() => new Truck("make", "model", 10, 101));
        }

        [TestMethod]
        public void Constructor_Should_CreateNewTruck_When_ParametersAreCorrect()
        {
            // Arrange, Act
            Truck truck = new Truck("make", "model", 10, 10);

            // Assert
            Assert.AreEqual("make", truck.Make);
        }
    }
}
