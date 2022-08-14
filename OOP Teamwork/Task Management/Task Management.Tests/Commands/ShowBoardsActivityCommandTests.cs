using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands;
using Task_Management.Core;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;

namespace Task_Management.Tests.Commands
{
    [TestClass]
    public class ShowBoardsActivityCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParameterAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            var team = repo.CreateTeam("Team test");

            IList<string> commandParameters = new List<string> { "Test parameter1", "Test parameter2"};
            // Act
            var sut = new ShowBoardsActivityCommand(commandParameters, repo);
            var result = sut.Execute();
        }

        [TestMethod]
        public void ShouldRetrurnString_WhenThereAreAnyBoardsToShow()
        {
            // Arrange
            IRepository repo = new Repository();
            var team = repo.CreateTeam("Team test");
            var board = new Board("Test board");
            team.AddBoard(board);

            IList<string> commandParameters = new List<string> { board.Name };
            // Act
            var sut = new ShowBoardsActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldReturnMessage_WhenThereAreNoTeams()
        {
            // Arrange
            IRepository repo = new Repository();

            IList<string> commandParameters = new List<string> { "Test parameter1" };

            // Act
            var sut = new ShowBoardsActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual("There are no teams created!", result);
        }
    }
}
