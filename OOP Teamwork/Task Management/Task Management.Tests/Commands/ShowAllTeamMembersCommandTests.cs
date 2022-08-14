using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands;
using Task_Management.Core;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;

namespace Task_Management.Tests.Commands
{
    [TestClass]
    public class ShowAllTeamMembersCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParameterAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();

            IList<string> commandParameters = new List<string> { "Test parameter1", "Test parameter2"};
            // Act
            var sut = new ShowAllTeamMembersCommand(commandParameters, repo);
            var result = sut.Execute();
        }

        [TestMethod]
        public void ShouldRetrurnString_WhenThereAreAnyTeamMembersToShow()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = repo.CreateMember("Test member 1");
            var team1 = repo.CreateTeam("Test team 1");
            team1.AddMember(member1);

            IList<string> commandParameters = new List<string> { team1.Name };

            // Act
            var sut = new ShowAllTeamMembersCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldRetrurnMessage_WhenThereAreAnyTeams()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = repo.CreateMember("Test member 1");
            //var team1 = repo.CreateTeam("Test team 1");
            

            IList<string> commandParameters = new List<string> { "Team test" };

            // Act
            var sut = new ShowAllTeamMembersCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual("There are no teams created!", result);
        }
    }
}
