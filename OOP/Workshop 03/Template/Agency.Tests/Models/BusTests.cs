using Agency.Exceptions;
using Agency.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agency.Tests.Models
{
    [TestClass]
    public class BusTests
    {
        [TestMethod]
        [DataRow(Bus.PassengerCapacityMinValue - 1)]
        [DataRow(Bus.PassengerCapacityMaxValue + 1)]
        public void Constructor_Should_ThrowException_When_PassengerCapacity_OutOfBounds(int testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Bus(
                    id: 1,
                    passengerCapacity: testValue,
                    pricePerKilometer: Bus.PricePerKilometerMinValue,
                    hasFreeTv: true));
        }

        [TestMethod]
        [DataRow(Bus.PricePerKilometerMinValue - 0.1)]
        [DataRow(Bus.PricePerKilometerMaxValue + 0.1)]
        public void Constructor_Should_ThrowException_When_PricePerKmOutOfBounds(double testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Bus(
                    id: 1,
                    passengerCapacity: Bus.PassengerCapacityMinValue,
                    pricePerKilometer: testValue,
                    hasFreeTv: true));
        }
    }
}
