using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Task_Management.Core;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Tests.Core
{
    [TestClass]

    public class RepositoryTests
    {
        [TestMethod]
        public void CreateDuplicateTeam()
        {
            // Arrange

            IRepository repo = new Repository();
            // Act
            repo.CreateTeam("PeshoTeam");
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => repo.CreateTeam("PeshoTeam"));
        }

        [TestMethod]
        public void CreateDuplicateMember()
        {
            // Arrange

            IRepository repo = new Repository();
            // Act
            repo.CreateMember("Pesho");
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => repo.CreateMember("Pesho"));
        }

        [TestMethod]
        public void ShouldCreateBug()
        {
            // Arrange
            IRepository repo = new Repository();
            IMember member = new Member("Pesho");
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            Severity severity = Severity.Critical;
            Task_Management.Models.Enums.Bug.Priority priority = Task_Management.Models.Enums.Bug.Priority.High;
            // Act
            IBug newBug = repo.CreateBug(title, description, steps, priority, severity, member);
            // Assert
            Assert.AreEqual("title_Must_be_Big", newBug.Title);

        }

        [TestMethod]
        public void ShouldCreateStory()
        {
            // Arrange
            IMember pesho = new Member("Pesho");
            IRepository repo = new Repository();
            string title = "title_Must_Be_Long";
            string description = "description";
            Task_Management.Models.Enums.Story.Priority priority = Task_Management.Models.Enums.Story.Priority.Low;
            Task_Management.Models.Enums.Story.Size size = Task_Management.Models.Enums.Story.Size.Small;
            // Act
            IStory story = repo.CreateStory(title, description, priority, size, pesho);
            // Assert
            Assert.AreEqual("title_Must_Be_Long", story.Title);

        }

        [TestMethod]
        public void ShouldCreateFeedback()
        {
            // Arrange
            IRepository repo = new Repository();
            IMember pesho = new Member("Pesho");
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 5;

            // Act
            IFeedback feedback = repo.CreateFeedback(title, description, rating);
            // Assert
            Assert.AreEqual("title_Must_Be_Long", feedback.Title);

        }

        [TestMethod]
        public void ShouldFindTeamByName()
        {
            // Arrange
            IRepository repo = new Repository();

            repo.CreateTeam("PeshoTeam");
            // Act
            ITeam newTeam = repo.FindTeamByName("PeshoTeam");
            // Assert
            Assert.AreEqual("PeshoTeam", newTeam.Name);

        }

        [TestMethod]
        public void ShouldThrowExceptionIfNoTeamWithNameExists()
        {
            // Arrange
            IRepository repo = new Repository();

            repo.CreateTeam("PeshoTeam");
            // Act
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => repo.FindTeamByName("ToshoTeam"));
        }

        [TestMethod]
        public void ShouldFindMemberByName()
        {
            // Arrange
            IRepository repo = new Repository();

            repo.CreateMember("Pesho");
            // Act
            IMember member = repo.FindMemberByName("Pesho");
            // Assert
            Assert.AreEqual("Pesho", member.Name);

        }

        [TestMethod]
        public void ShouldThrowExceptionIfNoMemberWithNameExists()
        {
            // Arrange
            IRepository repo = new Repository();

            repo.CreateMember("Pesho");
            // Act
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => repo.FindMemberByName("Tosho"));
        }

        [TestMethod]
        public void ShouldFindBoardByName()
        {
            // Arrange
            IRepository repo = new Repository();

            ITeam peshoTeam = repo.CreateTeam("PeshoTeam");
            peshoTeam.AddBoard("PeshoBoard");
            // Act
            IBoard newBoard = repo.FindBoardByName("PeshoBoard");
            // Assert
            Assert.AreEqual("PeshoBoard", newBoard.Name);

        }

        [TestMethod]
        public void ShouldThrowExceptionIfNoBoardWithNameExists()
        {
            // Arrange
            IRepository repo = new Repository();

            ITeam peshoTeam = repo.CreateTeam("PeshoTeam");
            peshoTeam.AddBoard("PeshoBoard");
            // Act
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => repo.FindBoardByName("ToshoBoard"));

        }

        [TestMethod]
        public void ShouldFindBugById()
        {
            // Arrange
            IRepository repo = new Repository();
            IMember member = new Member("Pesho");
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            Severity severity = Severity.Critical;
            Task_Management.Models.Enums.Bug.Priority priority = Task_Management.Models.Enums.Bug.Priority.High;

            // Act
            IBug newBug = repo.CreateBug(title, description, steps, priority, severity, member);
            var myBug = repo.FindBugById(1);

            // Assert
            Assert.AreEqual(1,myBug.Id);

        }
        [TestMethod]
        public void ShouldThrowExceptionIfNoBugWithIdExists()
        {
            // Arrange
            IRepository repo = new Repository();

            // Act
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => repo.FindBugById(2));
        }

        [TestMethod]
        public void ShouldFindStoryById()
        {
            // Arrange
            IRepository repo = new Repository();
            IMember pesho = new Member("Pesho");
            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            Task_Management.Models.Enums.Story.Priority priority = Task_Management.Models.Enums.Story.Priority.Low;
            Task_Management.Models.Enums.Story.Size size = Task_Management.Models.Enums.Story.Size.Small;

            // Act
            IStory story =repo.CreateStory(title, description, priority, size, pesho);
            var myStory = repo.FindStoryById(1);

            // Assert
            Assert.AreEqual(1, myStory.Id);
        }

        [TestMethod]

        public void ShouldThrowExceptionIfNoStoryWithIdExists()
        {
            // Arrange
            IRepository repo = new Repository();

            // Act
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => repo.FindStoryById(2));

        }

        [TestMethod]
        public void ShouldFindFeedbackById()
        {
            // Arrange
            IRepository repo = new Repository();
            IMember pesho = new Member("Pesho");
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 5;

            // Act
            IFeedback feedback = repo.CreateFeedback(title, description, rating);
            var myFeedback = repo.FindFeedbackById(1);

            // Assert
            Assert.AreEqual(1, feedback.Id);
        }

        [TestMethod]

        public void ShouldThrowExceptionIfNoFeedbackWithIdExists()
        {
            // Arrange
            IRepository repo = new Repository();

            // Act
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => repo.FindFeedbackById(2));

        }


        [TestMethod]

        public void ShouldFindTaskById()
        {
            // Arrange
            IRepository repo = new Repository();
            IMember member = new Member("Pesho");
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            Severity severity = Severity.Critical;
            Task_Management.Models.Enums.Bug.Priority priority = Task_Management.Models.Enums.Bug.Priority.High;
            int rating = 5;

            Task_Management.Models.Enums.Story.Priority storyPriority = Task_Management.Models.Enums.Story.Priority.Low;
            Task_Management.Models.Enums.Story.Size size = Task_Management.Models.Enums.Story.Size.Small;

            // Act
            ITask newBug = repo.CreateBug(title, description, steps, priority, severity, member);
            var myBug = repo.FindTaskById(1);

            ITask feedback = repo.CreateFeedback(title, description, rating);
            var myFeedback = repo.FindTaskById(2);

            ITask story = repo.CreateStory(title, description, storyPriority, size, member);
            var myStory = repo.FindTaskById(3);

            // Assert
            Assert.AreEqual(1, myBug.Id);
            Assert.AreEqual(2, myFeedback.Id);
            Assert.AreEqual(3, myStory.Id);

        }
        [TestMethod]

        public void ShouldThrowExceptionIfNoTaskWithIdExists()
        {
            // Arrange
            IRepository repo = new Repository();

            // Act
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => repo.FindTaskById(2));

        }
        [TestMethod]

        public void ShouldCreateLists()
        {
            // Arrange
            IRepository repo = new Repository();

            // Act
            // Assert
            Assert.AreEqual(0, repo.Teams.Count);
            Assert.AreEqual(0, repo.Members.Count);
            Assert.AreEqual(0, repo.Bugs.Count);
            Assert.AreEqual(0, repo.Stories.Count);
            Assert.AreEqual(0, repo.Feedbacks.Count);

        }
    }
}
