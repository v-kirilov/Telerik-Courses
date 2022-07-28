using Dealership.Models;
using Dealership.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dealership.Tests.Models
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void Car_Should_ImplementICarInterface()
        {
            var type = typeof(Car);
            var isAssignable = typeof(ICar).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Car class does not implement ICar interface!");
        }

        [TestMethod]
        public void Car_Should_ImplementIVehicleInterface()
        {
            var type = typeof(Car);
            var isAssignable = typeof(IVehicle).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Car class does not implement IVehicle interface!");
        }

        [TestMethod]
        public void Car_Should_DeriveFromVehicle()
        {
            var type = typeof(Car);
            var isAssignable = typeof(Vehicle).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Car class does not derive from Vehicle base class!");
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_MakeLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("1", "model", 10, 4));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_MakeLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("1234567890123456", "model", 10, 4));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_ModelLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("make", "", 10, 4));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_ModelLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("make", "1234567890123456", 10, 4));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PriceIsNegative()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("make", "model", -10, 4));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PriceIsAbove100000()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("make", "model", 1000001.0m, 4));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_SeatsIsNegative()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("make", "model", 10, -4));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_SeatsIsAbove10()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("make", "model", 10, 11));
        }

        [TestMethod]
        public void Constructor_Should_CreateNewCar_When_ParametersAreCorrect()
        {
            // Arrange, Act
            Car car = new Car("make", "model", 10, 4);

            // Assert
            Assert.AreEqual("make", car.Make);
        }

        [TestMethod]
        public void Comments_Should_ReturnCopyOfTheCollection()
        {
            // Arrange
            Car car = new Car("make", "model", 10, 4);
            
            // Act
            car.Comments.Add(new Comment("content", "author"));

            // Assert
            Assert.AreEqual(0, car.Comments.Count);
        }

        [TestMethod]
        public void AddComment_Should_AddCommentToTheCollection()
        {
            // Arrange
            Car car = new Car("make", "model", 10, 4);
            
            // Act
            car.AddComment(new Comment("content", "author"));

            // Assert
            Assert.AreEqual(1, car.Comments.Count);
            Assert.AreEqual("content", car.Comments[0].Content);
        }

        [TestMethod]
        public void RemoveComment_Should_RemoveCommentFromTheCollection()
        {
            // Arrange
            Car car = new Car("make", "model", 10, 4);
            Comment comment = new Comment("content", "author");
            car.AddComment(comment);
            
            // Act
            car.RemoveComment(comment);

            // Assert
            Assert.AreEqual(0, car.Comments.Count);
        }
    }
}
