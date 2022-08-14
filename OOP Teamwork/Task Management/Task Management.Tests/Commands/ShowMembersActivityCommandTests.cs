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
    public class ShowMembersActivityCommandTests
    {
        [TestMethod]
        public void ShouldRetrurnString_WhenThereAreAnyMembersToShow()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = repo.CreateMember("Test member 1");

            IList<string> commandParameters = new List<string> { member1.Name};

            // Act
            var sut = new ShowMembersActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldRetrunMessage_WhenThereAreNoPeopleToShow()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = new Member("Test member1");

            IList<string> commandParameters = new List<string> { member1.Name };
            // Act
            var sut = new ShowMembersActivityCommand(commandParameters, repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual("There are no members created!", result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParameterAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            var member1 = repo.CreateMember("Test member1");
            var otherParameter = "Test parameter";

            IList<string> commandParameters = new List<string> { member1.Name, otherParameter };
            // Act
            var sut = new ShowMembersActivityCommand(commandParameters, repo);
            var result = sut.Execute();
        }


    }
}
