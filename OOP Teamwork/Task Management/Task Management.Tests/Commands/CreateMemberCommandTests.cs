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
    public class CreateMemberCommandTests
    {
        [TestMethod]
        public void ShouldCreateMember_WhenNameIsValid()
        {
            // Arrange
            IRepository repo = new Repository();
            string name = "Test member";
            IList<string> commandParameters = new List<string> { name };

            // Act
            var sut = new CreateMemberCommand(commandParameters, repo);
            var returned = sut.Execute();

            // Assert
            Assert.AreEqual($"Member with name: [{name}] was created!", returned);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShoudThrowException_WhenParameterAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            string name = "Test member";
            string otherParameter = "Test parameter";
            IList<string> commandParameters = new List<string> { name, otherParameter };

            // Act
            var sut = new CreateMemberCommand(commandParameters, repo);
            sut.Execute();
        }
    }
}
