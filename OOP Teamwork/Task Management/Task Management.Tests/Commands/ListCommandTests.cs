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
    public class ListCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenParametersAreLessThanExpected()
        {
            // Arrange
            IRepository repo = new Repository();
            IList<string> commandParameters = new List<string> { "1", "2" };

            // Act
            var sut = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenTaskIsNotValid()
        {
            // Arrange
            IRepository repo = new Repository();
            IList<string> commandParameters = new List<string> { "Wrong", "Active", "Title" };

            // Act
            var sut = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        public void ShouldReturnBug_WhenStatusFilterIsCorrect()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "bugs", "Active", "Title" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the bug:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldReturnBug_WhenAssigneeFilterIsCorrect()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "bugs", "Testmemer", "Priority" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the bug:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldReturnAllBugs_WhenAllParameterIsUsed()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "bugs", "all", "Severity" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the bug:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenBugFilterDoesntMatchStatusOrAssignee()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "bugs", "wrong", "Severity" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the bug:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenBugSortIsNotValid()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "bugs", "all", "wrong" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the bug:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        public void ShouldReturnBug_WhenUsedCorrectTwoFilters()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "bugs", "Active", "and", "Testmemer", "Title" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the bug:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenBugUsedTwoFiltersAndStatusIsNotFound()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "bugs", "Wrong", "and", "Testmemer", "Title" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the bug:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenBugUsedTwoFiltersAndAssigneeIsNotFound()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateBug("Titletest123", "Description", "1: StepsTest", Task_Management.Models.Enums.Bug.Priority.High,
                Task_Management.Models.Enums.Bug.Severity.Critical, new Member("Testmemer"));
            IList<string> commandParameters = new List<string> { "bugs", "Active", "and", "Wrong", "Title" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the bug:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        public void ShouldReturnStory_WhenStatusFilterIsCorrect()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "stories", "NotDone", "Title" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldReturnStory_WhenAssigneeFilterIsCorrect()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "stories", "Testmember", "Size" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldReturnAllStories_WhenAllIsUsed()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "stories", "all", "Priority" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenStoryFilterDoesntMatchStatusOrAssignee()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "stories", "wronf", "Title" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldReturnStory_WhenUsedCorrectTwoFilters()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "stories", "NotDone", "and", "Testmember", "Size" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenStoryUsedTwoFiltersAndStatusIsNotFound()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "stories", "wrong", "and", "Testmember", "Size" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenStoryUsedTwoFiltersAndAssigneeIsNotFound()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "stories", "NotDone", "and", "wrong", "Size" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenStorySortIsNotValid()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateStory("Title test", "Description", Priority.Low, Size.Small, new Member("Testmember"));
            IList<string> commandParameters = new List<string> { "stories", "NotDone", "and", "Testmember", "wrong" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        public void ShouldReturnFeedback_WhenStatusFilterIsCorrect()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateFeedback("Titletest123", "Description", 5);
            IList<string> commandParameters = new List<string> { "feedbacks", "New", "Title" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldReturnFeedback_WhenAllIsUsed()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateFeedback("Titletest123", "Description", 5);
            IList<string> commandParameters = new List<string> { "feedbacks", "all", "rating" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenFeedbackFilterDoesntMatchTheFilter()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateFeedback("Titletest123", "Description", 5);
            IList<string> commandParameters = new List<string> { "feedbacks", "wrong", "rating" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenFeedbackTryingToFilterByTwoParameters()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateFeedback("Titletest123", "Description", 5);
            IList<string> commandParameters = new List<string> { "feedbacks", "New", "and", "wrong", "rating" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ShouldThrowException_WhenFeedbackSortIsNotValid()
        {
            // Arrange
            IRepository repo = new Repository();
            var task = repo.CreateFeedback("Titletest123", "Description", 5);
            IList<string> commandParameters = new List<string> { "feedbacks", "New", "wrong" };
            var expected = new StringBuilder().AppendLine($"1: {task.ToString().Trim()}")
                .AppendLine("  History of the story:")
                .AppendLine($"  {task.ViewHistory()}")
                .ToString().Trim();

            // Act
            var result = new ListCommand(commandParameters, repo).Execute();
        }
    }
}
