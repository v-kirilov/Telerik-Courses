using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Boarder.Models;

namespace Boarder.Tests.Models
{
    [TestClass]
    public class IssueTest
    {
        [TestMethod]
        public void Create_Issue_WhenValuesAreCorrect()
        {
            //Arrange
            var title = "This is a test title";
            var description = "MyDescription";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var issueOne = new Issue(title, description, dueDate);

            //Assert
            Assert.AreEqual(title, issueOne.Title);
            Assert.AreEqual(description, issueOne.Description);
            Assert.AreEqual(Convert.ToDateTime("01-01-2030"), issueOne.DueDate);
        }


        [TestMethod]
        public void Throw_When_TitleValueOutOfRange()
        {
            //Arrange
            var title = "This is a test title";
            var description = "MyDescription";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var issueOne = new Issue(title, description, dueDate);

            //Assert
            Assert.ThrowsException<ArgumentException>(() => issueOne.Title = new string('a', 3));
            Assert.ThrowsException<ArgumentException>(() => issueOne.Title = new string('a', 33));
        }


        [TestMethod]
        public void Throw_When_DateIsEarlier()
        {
            //Arrange
            var title = "This is a test title";
            var description = "MyDescription";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var issueOne = new Issue(title, description, dueDate);

            //Assert
            Assert.ThrowsException<ArgumentException>(() => issueOne.DueDate = DateTime.MinValue);

        }
        [TestMethod]
        public void Throw_When_StatusIsNotSetProperly()
        {
            //Arrange
            var title = "This is a test title";
            var description = "MyDescription";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var issueOne = new Issue(title, description, dueDate);

            //Assert
            Assert.AreEqual(Status.Open, issueOne.Status);

        }

        [TestMethod]

        public void Does_Advance_Status_Method_Work()
        {
            //Arrange
            var title = "This is a test title";
            var description = "MyDescription";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var issueOne = new Issue(title, description, dueDate);

            //Assert
            issueOne.AdvanceStatus();
            Assert.AreEqual(Status.Verified, issueOne.Status);
            issueOne.AdvanceStatus();
            Assert.AreEqual(Status.Verified, issueOne.Status);

        }

        [TestMethod]

        public void Does_Revert_Status_Method_Work()
        {
            //Arrange
            var title = "This is a test title";
            var description = "MyDescription";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var issueOne = new Issue(title, description, dueDate);

            //Assert
            issueOne.AdvanceStatus();
            issueOne.RevertStatus();
            Assert.AreEqual(Status.Open, issueOne.Status);
            issueOne.RevertStatus();
            Assert.AreEqual(Status.Open, issueOne.Status);
        }

        [TestMethod]

        public void Issue_Shoud_DeriveFromBoardItem()
        {
            var type = typeof(Issue);
            var isAssignable = typeof(BoardItem).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Issue class does not derive from BoardItem class!");
        }

    }
}
