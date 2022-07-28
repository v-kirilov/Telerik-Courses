using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Boarder.Models;
using Microsoft.VisualBasic;

namespace Boarder.Tests.Models
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void AssignCorrectValues()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var task = new Task(title, assignee, dueDate);

            //Assert
            Assert.AreEqual(title, task.Title);
            Assert.AreEqual(assignee, task.Assignee);
            Assert.AreEqual(Convert.ToDateTime("01-01-2030"), dueDate);
        }
        [TestMethod]

        public void Throw_When_TitleValueOutOfRange()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            var taskOne = new Task(title, assignee, dueDate);


            //Act
            Assert.ThrowsException<ArgumentException>(() => taskOne.Title = new string('a', 3));
            Assert.ThrowsException<ArgumentException>(() => taskOne.Title = new string('a', 33));
        }
        [TestMethod]

        public void Throw_When_DateIsEarlier()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";

            var dueDate = Convert.ToDateTime("01-01-2030");

            var taskOne = new Issue(title, assignee, dueDate);


            //Act
            Assert.ThrowsException<ArgumentException>(() => taskOne.DueDate = DateTime.MinValue);

        }
        [TestMethod]

        public void Throw_When_StatusIsNotSetProperly()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            var task = new Task(title, assignee, dueDate);


            //Act
            Assert.AreEqual(Status.Todo, task.Status);
        }
        [TestMethod]

        public void Constructor_Should_Throw_When_AssigneeIsNotValid()
        {
            //Arrange
            string title = "This is a test title";
            string assignee = null;
            string emptyAssignee = String.Empty;
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            Assert.ThrowsException<ArgumentException>(() => new Task(title, assignee, dueDate));
            Assert.ThrowsException<ArgumentException>(() => new Task(title, emptyAssignee, dueDate));

            string validAssignee = "Assignee";

            var task = new Task(title, validAssignee, dueDate);

            var exception = Assert.ThrowsException<ArgumentException>(() => task.Assignee = new string('a', 2));
            var exception2 = Assert.ThrowsException<ArgumentException>(() => task.Assignee = new string('a', 32));
        }

        [TestMethod]

        public void Constructor_ShoudCall_AddEventLogMethod()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");
            string DateFormat = "dd-MM-yyyy";
            var viewInfo = $"Task: '{title}', [{Status.Todo}|{dueDate.ToString(DateFormat)}] Assignee: {assignee}";
            //Act
            var task = new Task(title, assignee, dueDate);
            var result = task.ViewInfo();
            //Assert
            Assert.AreEqual(viewInfo, result);

        }
        [TestMethod]

        public void Task_Shoud_DeriveFromBoardItem()
        {
            var type = typeof(Task);
            var isAssignable = typeof(BoardItem).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Task class does not derive from BoardItem class!");
        }
        [TestMethod]

        public void Does_Advance_Status_Method_Work()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";

            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var task = new Task(title, assignee, dueDate);


            //Assert
            task.AdvanceStatus();
            Assert.AreEqual(Status.InProgress, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.Done, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.Verified, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.Verified, task.Status);
        }

        [TestMethod]

        public void Does_Revert_Status_Method_Work()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var task = new Task(title, assignee, dueDate);
            //Assert
            task.AdvanceStatus();
            task.AdvanceStatus();
            task.AdvanceStatus();
            task.RevertStatus();
            Assert.AreEqual(Status.Done, task.Status); 
            task.RevertStatus();
            Assert.AreEqual(Status.InProgress, task.Status);
            task.RevertStatus();
            Assert.AreEqual(Status.Todo, task.Status);
            task.RevertStatus();
            Assert.AreEqual(Status.Todo, task.Status);
        }
    }
}
