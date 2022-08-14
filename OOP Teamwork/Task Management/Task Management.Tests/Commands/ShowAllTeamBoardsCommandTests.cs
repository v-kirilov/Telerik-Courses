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
    public class ShowAllTeamBoardsCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParameterAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();

            IList<string> commandParameters = new List<string> { "Test parameter1", "Test parameter2" };
            // Act
            var sut = new ShowAllTeamBoardsCommand(commandParameters, repo);
            var result = sut.Execute();
        }

        [TestMethod]
        public void ShouldReturnString_WhenParametersAreValid()
        {
            // Arrange
            IRepository repo = new Repository();
            var board = new Board("Test board");
            var team1 = repo.CreateTeam("Test team 1");
            team1.AddBoard(board);

            IList<string> commandParameters = new List<string> { team1.Name };

            // Act
            var sut = new ShowAllTeamBoardsCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldReturnString_WhenThereAreNoBoardsInTheTeam()
        {
            // Arrange
            IRepository repo = new Repository();
            var board = new Board("Test board");
            var team1 = repo.CreateTeam("Test team 1");

            IList<string> commandParameters = new List<string> { team1.Name };

            // Act
            var sut = new ShowAllTeamBoardsCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual("There are no boards created!", result);
        }

        [TestMethod]
        public void ShouldReturnString_WhenThereAreNoTeams()
        {
            // Arrange
            IRepository repo = new Repository();
            var board = new Board("Test board");

            IList<string> commandParameters = new List<string> { "Test team" };

            // Act
            var sut = new ShowAllTeamBoardsCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual("There are no teams created!", result);
        }
    }
}
