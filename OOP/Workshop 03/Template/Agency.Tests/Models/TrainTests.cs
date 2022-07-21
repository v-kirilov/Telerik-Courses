using Agency.Exceptions;
using Agency.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agency.Tests.Models
{
    [TestClass]
    public class TrainTests
    {
        [TestMethod]
        [DataRow(Train.PassengerCapacityMinValue - 1)]
        [DataRow(Train.PassengerCapacityMaxValue + 1)]
        public void Constructor_Should_ThrowException_When_PassengerCapacity_OutOfBounds(int testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Train(
                    id: 1,
                    passengerCapacity: testValue,
                    pricePerKilometer: Train.PricePerKilometerMinValue,
                    carts: Train.CartsMinValue));
        }

        [TestMethod]
        [DataRow(Train.PricePerKilometerMinValue - 0.1)]
        [DataRow(Train.PricePerKilometerMaxValue + 0.1)]
        public void Constructor_Should_ThrowException_When_PricePerKmOutOfBounds(double testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Train(
                    id: 1,
                    passengerCapacity: Train.PassengerCapacityMinValue,
                    pricePerKilometer: testValue,
                    carts: Train.CartsMinValue));
        }

        [TestMethod]
        [DataRow(Train.CartsMinValue - 1)]
        [DataRow(Train.CartsMaxValue + 1)]
        public void Constructor_Should_Throw_When_CartsLessThanMinValue(int testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Train(
                    id: 1,
                    passengerCapacity: Train.PassengerCapacityMinValue,
                    pricePerKilometer: Train.PricePerKilometerMinValue,
                    carts: testValue));
        }
    }
}
