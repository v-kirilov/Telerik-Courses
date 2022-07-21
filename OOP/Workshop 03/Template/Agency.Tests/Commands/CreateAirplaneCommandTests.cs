using Agency.Commands;
using Agency.Core;
using Agency.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Agency.Tests.Commands
{
    [TestClass]
    public class CreateAirplaneCommandTests
    {
        [TestMethod]
        [DataRow(3)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var repository = new Repository();
            var command = new CreateAirplaneCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_PassengerCapacityNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "capacity", "2", "True" }.ToList();
            var repository = new Repository();
            var command = new CreateAirplaneCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_PriceNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "10", "price", "True" }.ToList();
            var repository = new Repository();
            var command = new CreateAirplaneCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewAirplane_When_ValidParameters()
        {
            // Arrange
            var commandParameters = new string[] { "10", "2", "True" }.ToList();
            var repository = new Repository();
            var command = new CreateAirplaneCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(repository.Vehicles.Count > 0);
        }
    }
}
