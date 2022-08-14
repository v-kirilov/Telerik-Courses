using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;
using Task_Management.Models;

namespace Task_Management.Tests.Models
{
    [TestClass]
    public class MemberTests
    {
        [TestMethod]
        public void ConstructorShouldSetCorrectName_WhenNameIsValid()
        {
            // Arrange
            string memberName = "TestName";

            // Act 
            var sut = new Member(memberName);

            // Assert
            Assert.AreEqual(memberName, sut.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ConstructorShouldThrowException_WhenNameLengthLessThanMin()
        {
            // Arrange 
            var invalidName = new string('a', 4);

            // Act
            var sut = new Member(invalidName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ConstructorShouldThrowException_WhenNameMoreThanMax()
        {
            // Arrange 
            var invalidName = new string('a', 16);

            // Act
            var sut = new Member(invalidName);
        }

        [TestMethod]
        public void ConstructorShouldAddToEventLog_WhenMemberIsCreated()
        {
            // Arrange
            string memberName = "TestName";

            // Act 
            var sut = new Member(memberName);

            // Assert
            Assert.AreEqual(1, sut.EventLogs.Count);
        }

        [TestMethod]
        public void ShouldAddTask_WhenTaskIsUnique()
        {
            // Arrange
            var member = new Member("Test member");
            var task = new Feedback(123, "Test feedback", "Test description", 5);
            var currTasksCount = member.Tasks.Count;

            // Act 
            member.AddTask(task);

            // Assert
            Assert.AreEqual(currTasksCount + 1, member.Tasks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenTaskIsAlreadyAssignedToTheMember()
        {
            // Arrange
            var member = new Member("Test member");
            var task = new Feedback(123, "Test feedback", "Test description", 5);
            member.AddTask(task);

            // Act 
            member.AddTask(task);
        }

        [TestMethod]
        public void ShouldRemoveTask_WhenTheTaskIsAlreadyAssigneedToTheMember()
        {
            // Arrange
            var member = new Member("Test member");
            var task = new Feedback(123, "Test feedback", "Test description", 5);
            member.AddTask(task);
            var currTasksCount = member.Tasks.Count;

            // Act 
            member.RemoveTask(task);

            // Assert
            Assert.AreEqual(currTasksCount - 1, member.Tasks.Count);
        }

        [TestMethod]
        public void ShouldThrowException_WhenWeRemoveTask_ThatIsNotAssigneedToTheMember()
        {
            // Arrange
            var member = new Member("Test member");
            var task = new Feedback(123, "Test feedback", "Test description", 5);
            member.AddTask(task);
            var currTasksCount = member.Tasks.Count;

            // Act 
            member.RemoveTask(task);

            // Assert
            Assert.AreEqual(currTasksCount - 1, member.Tasks.Count);
        }
    }
}
