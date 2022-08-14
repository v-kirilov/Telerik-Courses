using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands;
using Task_Management.Core;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Tests.Commands
{
    [TestClass]
    public class ListTasksWithAssigneeCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParametersAreMoreThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            IList<string> commandParameters = new List<string> { "1", "2", "3", "4"};

            // Act
            var sut = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        public void ShouldReturnStory_WhenFilterIsCorrectStatus()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "NotDone"};
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}").ToString().Trim();

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void ShouldReturnStory_WhenFilterIsCorrectAssignee()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "Testmember" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}").ToString().Trim();

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void ShouldReturnAllStories_WhenFilterIsAll()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            var task2 = repo.CreateStory("Title test2", "Description2", Priority.Low, Size.Small, new Member("Testmember2"));
            IList<string> commandParameters = new List<string> { "all" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine($"2: {task2.ToString().Trim()}").ToString().Trim();

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void ShouldReturnBug_WhenFilterIsCorrectStatus()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High, 
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "Active" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}").ToString().Trim();

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void ShouldReturnBug_WhenFilterIsCorrectAssignee()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "Testmember" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}").ToString().Trim();

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void ShouldReturnAllBugs_WhenFilterIsAll()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmember"));
            var task2 = repo.CreateBug("Titletest1233", "Description2", "1: StepsTest2", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmember2"));
            IList<string> commandParameters = new List<string> { "all" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine($"2: {task2.ToString().Trim()}").ToString().Trim();

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenNoBugOrStoryMatchTheFilter()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmember"));
            var task2 = repo.CreateStory("Title test2", "Description2", Priority.Low, Size.Small, new Member("Testmember2"));
            IList<string> commandParameters = new List<string> { "WrongParameter" };

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        public void ShouldReturnBugs_WhenFiltersAreCorrect()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "Active", "and", "Testmember" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}").ToString().Trim();

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void ShouldReturnStories_WhenFiltersAreCorrect()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "NotDone", "and", "Testmember" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}").ToString().Trim();

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenNoBugOrStoryMatchTheFilters()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmember"));
            var task2 = repo.CreateStory("Title test2", "Description2", Priority.Low, Size.Small, new Member("Testmember2"));
            IList<string> commandParameters = new List<string> { "WrongParameter", "and", "WrongParameter" };

            // Act
            var result = new ListTasksWithAssigneeCommand(commandParameters, repo).Execute();
        }
    }
}
