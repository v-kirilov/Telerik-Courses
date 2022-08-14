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
    public class ListAllTasksCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParametersAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            IList<string> commandParameters = new List<string> { "Parameter 1", "Parameter 2" };

            // Act
            var sut = new ListAllTasksCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        public void ShoudlRetrunAllTasks_WhenParameterValueIsAll()
        {
            // Arrange
            IRepository repo = new Repository();
            var task1 = repo.CreateFeedback("Title test", "Description", 4);
            var task2 = repo.CreateFeedback("Title test2", "Description2", 5);
            var sb = new StringBuilder();
            sb.AppendLine($"1: {task1.ToString().Trim()}");
            sb.AppendLine($"2: {task2.ToString().Trim()}");
            var expected = sb.ToString().Trim();

            IList<string> commandParameters = new List<string> { "All" };

            // Act
            var result = new ListAllTasksCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShoudlRetrunAllTasks_WhenFilterIsExsistingTitle()
        {
            // Arrange
            IRepository repo = new Repository();
            var task1 = repo.CreateFeedback("Title test", "Description", 4);
            var task2 = repo.CreateFeedback("Title test2", "Description2", 5);
            var sb = new StringBuilder();
            
            var expected = sb.AppendLine($"1: {task1.ToString().Trim()}").ToString().Trim();

            IList<string> commandParameters = new List<string> { "Title test" };

            // Act
            var result = new ListAllTasksCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShoudlThrowException_WhenGivenTitleIsNotExisting()
        {
            // Arrange
            IRepository repo = new Repository();
            var task1 = repo.CreateFeedback("Title test", "Description", 4);
            var task2 = repo.CreateFeedback("Title test2", "Description2", 5);
            var sb = new StringBuilder();

            //sb.AppendLine($"2: {task2.ToString().Trim()}");
            var expected = sb.AppendLine($"1: {task1.ToString().Trim()}").ToString().Trim();

            IList<string> commandParameters = new List<string> { "TitleWrong" };

            // Act
            var result = new ListAllTasksCommand(commandParameters, repo).Execute();

        }
    }
}
