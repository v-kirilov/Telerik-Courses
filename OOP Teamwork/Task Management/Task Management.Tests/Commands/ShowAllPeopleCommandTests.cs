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
    public class ShowAllPeopleCommandTests
    {
        [TestMethod]
        public void ShouldShowAllPeople_WhenThereAreAnyPeopleToShow()
        {
            // Arrange
            IRepository repo = new Repository();

            var member1 = repo.CreateMember("Test member 1");
            var member2 = repo.CreateMember("Test member 2");

            var expectedResult = new StringBuilder();
            expectedResult.AppendLine(member1.ToString());
            expectedResult.AppendLine(member2.ToString());

            // Act
            var sut = new ShowAllPeopleCommand(repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual(expectedResult.ToString(), result);

        }

        [TestMethod]
        public void ShouldRetrunMessage_WhenThereAreNoPeopleToShow()
        {
            // Arrange
            IRepository repo = new Repository();

            // Act
            var sut = new ShowAllPeopleCommand(repo);
            var result = sut.Execute();

            // Assert
            Assert.AreEqual("There are no members created!", result);

        }
    }
}
