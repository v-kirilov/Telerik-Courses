using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;
using Task_Management.Models;

namespace Task_Management.Tests.Models
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void ConstructorShouldSetCorrectName_WhenNameIsValid()
        {
            // Arrange
            string boardName = "TestName";

            // Act 
            var sut = new Board(boardName);

            // Assert
            Assert.AreEqual(boardName, sut.Name);
        }

        [TestMethod]
        public void ConstructorShouldAddToEventLog_WhenBoardIsCreated()
        {
            // Arrange
            string boardName = "TestName";            

            // Act 
            var sut = new Board(boardName);

            // Assert
            Assert.AreEqual(1 , sut.EventLogs.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ConstructorShouldThrowException_WhenNameLengthLessThanMin()
        {
            // Arrange 
            var invalidName = new string('a', 4);

            // Act
            var sut = new Board(invalidName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ConstructorShouldThrowException_WhenNameMoreThanMax()
        {
            // Arrange 
            var invalidName = new string('a', 11);

            // Act
            var sut = new Board(invalidName);
        }

        [TestMethod]
        public void ShouldAddTask_WhenTaskIsUnique()
        {
            // Arrange
            var board = new Board("Test board");
            var task = new Feedback(123, "Test feedback", "Test description", 5);
            var currTasksCount = board.Tasks.Count;

            // Act 
            board.AddTask(task);

            // Assert
            Assert.AreEqual(currTasksCount + 1, board.Tasks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenTaskIsAlreadyInTheBoard()
        {
            // Arrange
            var board = new Board("Test board");
            var task = new Feedback(123, "Test feedback", "Test description", 5);
            board.AddTask(task);

            // Act 
            board.AddTask(task);
        }
    }
}
