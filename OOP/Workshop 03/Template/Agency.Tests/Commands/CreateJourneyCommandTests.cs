using Agency.Commands;
using Agency.Core;
using Agency.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Agency.Tests.Commands
{
    [TestClass]
    public class CreateJourneyCommandTests
    {
        [TestMethod]
        [DataRow(4)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var repository = new Repository();
            var command = new CreateJourneyCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_DistanceNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "start", "destination", "distance", "1" }.ToList();
            var repository = new Repository();
            var command = new CreateJourneyCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_VehicleIdNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "start", "destination", "10", "index" }.ToList();
            var repository = new Repository();
            var command = new CreateJourneyCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewJourney_When_ValidParameters()
        {
            // Arrange
            var repository = new Repository();
            repository.CreateBus(10, 2, true);

            var commandParameters = new string[] { "start", "destination", "10", "1" }.ToList();
            var command = new CreateJourneyCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(repository.Journeys.Count > 0);
        }
    }
}
