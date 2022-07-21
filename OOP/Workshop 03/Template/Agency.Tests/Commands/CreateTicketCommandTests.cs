using Agency.Commands;
using Agency.Core;
using Agency.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Agency.Tests.Commands
{
    [TestClass]
    public class CreateTicketCommandTests
    {
        [TestMethod]
        [DataRow(2)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var repository = new Repository();
            var command = new CreateTicketCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_JourneyIdNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "journey", "1" }.ToList();
            var repository = new Repository();
            var command = new CreateTicketCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_VehicleIdNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "2", "cost" }.ToList();
            var repository = new Repository();
            var command = new CreateTicketCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewJourney_When_ValidParameters()
        {
            // Arrange
            var repository = new Repository();
            var bus = repository.CreateBus(10, 2, true);
            repository.CreateJourney("start", "destination", 10, bus);

            var commandParameters = new string[] { "2", "1" }.ToList();
            var command = new CreateTicketCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(repository.Tickets.Count > 0);
        }
    }
}
