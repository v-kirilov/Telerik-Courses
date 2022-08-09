using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Tests.Models
{
    [TestClass]

    public class StoryTests
    {
        [TestMethod]
        public void ShouldThrowExceptionIfTitleIsTooShort()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;

            // Act
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => new Story(id, title, description, priority, size, member));

        }

        [TestMethod]
        public void ShouldThrowExceptionIfDescriptionIsTooShort()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "descr";
            Priority priority = Priority.Low;
            Size size = Size.Small;

            // Act
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => new Story(id, title, description, priority, size, member));

        }

        [TestMethod]
        public void ShouldChangeStoryPriority()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, member);
            story.Priority = Priority.Medium;
            // Assert
            Assert.AreEqual(Priority.Medium, story.Priority);

        }

        [TestMethod]
        public void ShouldKeepStoryPriority()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, member);
            story.Priority = Priority.Low;
            // Assert
            Assert.AreEqual(Priority.Low, story.Priority);

        }

        [TestMethod]
        public void ShouldChangeStorySize()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, member);
            story.Size = Size.Medium;
            // Assert
            Assert.AreEqual(Size.Medium, story.Size);

        }

        [TestMethod]
        public void ShouldKeepStorySize()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, member);
            story.Size = Size.Small;
            // Assert
            Assert.AreEqual(Size.Small, story.Size);

        }

        [TestMethod]
        public void ShouldChangeStoryStatus()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, member);
            story.Status = Status.InProgress;
            // Assert
            Assert.AreEqual(Status.InProgress, story.Status);

        }

        [TestMethod]
        public void ShouldKeepStoryStatus()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, member);
            story.Status = Status.NotDone;
            // Assert
            Assert.AreEqual(Status.NotDone, story.Status);

        }

        [TestMethod]
        public void ShouldChangeStoryAssignee()
        {
            // Arrange
            IMember pesho = new Member("Pesho");
            IMember tosho = new Member("Tosho");

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, pesho);
            story.Assignee = tosho;
            // Assert
            Assert.AreEqual(tosho, story.Assignee);

        }

        [TestMethod]
        public void ShouldHaveNoAssignee()
        {
            // Arrange
            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, null);
            story.Assignee = null;
            // Assert
            Assert.AreEqual(null, story.Assignee);

        }

        [TestMethod]
        public void SouldCreateStoryWhenParametersAreValid()
        {
            // Arrange
            IMember pesho = new Member("Pesho");

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Priority priority = Priority.Low;
            Size size = Size.Small;
            // Act
            IStory story = new Story(id, title, description, priority, size, pesho);
            // Assert
            Assert.AreEqual("title_Must_Be_Long", story.Title);

        }

      
    }
}
