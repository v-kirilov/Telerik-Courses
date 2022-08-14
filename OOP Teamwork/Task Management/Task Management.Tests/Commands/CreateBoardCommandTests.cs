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
    public class CreateBoardCommandTests
    {

        [TestMethod]
        public void ShouldCreateBoard_WhenParametersAreValid()
        {
            // Arrange
            IRepository repo = new Repository();
            string teamName = "Test team";
            string boardName = "Test board";
            IList<string> commandParameters = new List<string> { boardName, teamName, };

            // Act
            var sut = new CreateBoardCommand(commandParameters, repo);
            var returned = sut.Execute();

            // Assert
            Assert.AreEqual($"Board with name: [{boardName}] has been added to team: [{teamName}]!", returned);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShoudThrowException_WhenParameterAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            string teamName = "Test team";
            string boardName = "Test board";
            string otherParameter = "Test parameter";
            IList<string> commandParameters = new List<string> { boardName, teamName, otherParameter};

            // Act
            var sut = new CreateBoardCommand(commandParameters, repo);
            sut.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShoudThrowException_WhenBoardAlreadyExistsInATeam()
        {
            // Arrange
            IRepository repo = new Repository();
            string teamName = "Test team";
            string boardName = "Test board";

            var team = repo.CreateTeam(teamName);
            team.AddBoard(boardName);
            
            
            IList<string> commandParameters = new List<string> { boardName, teamName};

            // Act
            var sut = new CreateBoardCommand(commandParameters, repo);
            sut.Execute();
        }
    }
}
