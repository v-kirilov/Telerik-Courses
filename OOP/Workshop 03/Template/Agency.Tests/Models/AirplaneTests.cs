using Agency.Exceptions;
using Agency.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agency.Tests.Models
{
    [TestClass]
    public class AirplaneTests
    {
        [TestMethod]
        [DataRow(Airplane.PassengerCapacityMinValue - 1)]
        [DataRow(Airplane.PassengerCapacityMaxValue + 1)]
        public void Constructor_Should_ThrowException_When_PassengerCapacity_OutOfBounds(int testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Airplane(
                    id: 1,
                    passengerCapacity: testValue,
                    pricePerKilometer: Airplane.PricePerKilometerMinValue,
                    isLowCost: true));
        }

        [TestMethod]
        [DataRow(Airplane.PricePerKilometerMinValue - 0.1)]
        [DataRow(Airplane.PricePerKilometerMaxValue + 0.1)]
        public void Constructor_Should_ThrowException_When_PricePerKmOutOfBounds(double testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Airplane(
                    id: 1,
                    passengerCapacity: Airplane.PassengerCapacityMinValue,
                    pricePerKilometer: testValue,
                    isLowCost: true));
        }
    }
}
