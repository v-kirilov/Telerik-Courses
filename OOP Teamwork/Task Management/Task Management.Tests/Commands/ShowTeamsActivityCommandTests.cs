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
    public class ShowTeamsActivityCommandTests
    {
        [TestMethod]
        public void ShouldRetrurnString_WhenThereIsAnyTeamActivityToShow()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = repo.CreateMember("Test member 1");
            var team1 = repo.CreateTeam("Test team 1");
            team1.AddBoard("Test board");
            team1.AddMember(member1);

            IList<string> commandParameters = new List<string> { team1.Name };

            // Act
            var sut = new ShowTeamsActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldRetrurnMessage_WhenThereIsNoMemberToShow()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = new Member("Test member 1");
            var team1 = repo.CreateTeam("Test team 1");
            team1.AddMember(member1);

            IList<string> commandParameters = new List<string> { team1.Name };

            // Act
            var sut = new ShowTeamsActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull("There are no members created!", result);
        }

        [TestMethod]
        public void ShouldReturnMessage_WhenThereAreNoMembersAndBoardsInATeam()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = repo.CreateMember("Test member 1");
            var team1 = repo.CreateTeam("Test team 1");

            IList<string> commandParameters = new List<string> { team1.Name };

            // Act
            var sut = new ShowTeamsActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull($"There are no members and boards in team:[{team1}]!", result);
        }

        [TestMethod]
        public void ShouldReturnMessage_WhenThereAreNoTeamsInRepo()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = repo.CreateMember("Test member 1");
            

            IList<string> commandParameters = new List<string> { "Test parameter" };

            // Act
            var sut = new ShowTeamsActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull($"There are no teams created.", result);
        }

        [TestMethod]
        public void ShouldReturnMessage_WhenThereAreNoTeamWithGivenNameInRepo()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = repo.CreateMember("Test member 1");
            var team1 = repo.CreateTeam("Test team 1");


            IList<string> commandParameters = new List<string> { "Test parameter" };

            // Act
            var sut = new ShowTeamsActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull($"There are no teams with name: [{team1}].", result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParameterAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            var team1 = repo.CreateMember("Test team1");
            var otherParameter = "Test parameter";

            IList<string> commandParameters = new List<string> { team1.Name, otherParameter };
            // Act
            var sut = new ShowTeamsActivityCommand(commandParameters, repo);
            var result = sut.Execute();
        }
    }
}
