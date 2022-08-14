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
    public class AddMemberToTeamCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParameterAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            IList<string> commandParameters = new List<string> { "Test parameter1", "TestParameter2", "TestParameter3" };

            // Act
            var sut = new AddMemberToTeamCommand(commandParameters, repo);
            var result = sut.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldReturnMessage_WhenMemberDoesNotExist()
        {
            // Arrange
            IRepository repo = new Repository();
            var member = new Member("Test member");
            IList<string> commandParameters = new List<string> { "Test parameter1", "TestParameter2" };

            // Act
            var sut = new AddMemberToTeamCommand(commandParameters, repo);
            var result = sut.Execute();
        }

        [TestMethod]
        public void ShouldAddMemberToTeam_WhenParametersAreValid()
        {
            // Arrange
            IRepository repo = new Repository();
            var member = repo.CreateMember("Test member");
            var team = repo.CreateTeam("Test team");
            IList<string> commandParameters = new List<string> { team.Name, member.Name };

            // Act
            var sut = new AddMemberToTeamCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual($"Member with name: [{member.Name}] has been added to team: [{team.Name}]!", result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenTeamDoesNotExist()
        {
            // Arrange
            IRepository repo = new Repository();
            var member = repo.CreateMember("Test member");
            var team = repo.CreateTeam("Test team");
            IList<string> commandParameters = new List<string> { "TestParameter2", member.Name };

            // Act
            var sut = new AddMemberToTeamCommand(commandParameters, repo);
            var result = sut.Execute();
        }
    }
}
