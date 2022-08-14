using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands;
using Task_Management.Core;
using Task_Management.Core.Contracts;

namespace Task_Management.Tests.Commands
{
    [TestClass]
    public class ShowAllTeamsCommandTests
    {
        [TestMethod]
        public void ShouldShowAllTeams_WhenThereAreAnyTeamsToShow()
        {
            // Arrange
            IRepository repo = new Repository();

            var team1 = repo.CreateTeam("Test team 1");
            var team2 = repo.CreateTeam("Test team 2");

            var expectedResult = new StringBuilder();
            expectedResult.AppendLine(team1.ToString().Trim());
            expectedResult.AppendLine(team2.ToString().Trim());

            // Act
            var sut = new ShowAllTeams(repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual(expectedResult.ToString(), result);

        }

        [TestMethod]
        public void ShouldRetrunMessage_WhenThereAreNoTeamsToShow()
        {
            // Arrange
            IRepository repo = new Repository();

            // Act
            var sut = new ShowAllTeams(repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual("There are no teams created!", result);

        }
    }
}
