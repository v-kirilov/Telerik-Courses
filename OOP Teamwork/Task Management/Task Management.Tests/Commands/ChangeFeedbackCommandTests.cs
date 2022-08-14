using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands;
using Task_Management.Core.Contracts;
using Task_Management.Core;
using Task_Management.Exceptions;

namespace Task_Management.Tests.Commands
{
    [TestClass]
    public class ChangeFeedbackCommandTests
    {
        [TestMethod]
        public void ShouldChangeFeedBackRating()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string rating = "5";

            List<string> memberParam = new List<string> { "Pesho" };
            var createMember = new CreateMemberCommand(memberParam, repo);
            var memberExec = createMember.Execute();

            List<string> teamParam = new List<string> { "PeshoTeam" };
            var createTeam = new CreateTeamCommand(teamParam, repo);
            var teamExec = createTeam.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "PeshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> addMemberParam = new List<string> { "PeshoTeam", "Pesho" };
            var addMember = new AddMemberToTeamCommand(addMemberParam, repo);
            var addMemberExec = addMember.Execute();


            List<string> commandParameters = new List<string> { title, description, rating, "PeshoBoard" };
            var command = new CreateFeedbackCommand(commandParameters, repo);
            var createFeedback = command.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Rating", "6" };
            var changeCommand = new ChangeFeedbackCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Changed the rating of feedback with ID: 1 from 5 to 6", returned);

        }

        [TestMethod]
        public void ShouldReturnMessageRatingIsAlreadyTheSame()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string rating = "5";

            List<string> memberParam = new List<string> { "Pesho" };
            var createMember = new CreateMemberCommand(memberParam, repo);
            var memberExec = createMember.Execute();

            List<string> teamParam = new List<string> { "PeshoTeam" };
            var createTeam = new CreateTeamCommand(teamParam, repo);
            var teamExec = createTeam.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "PeshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> addMemberParam = new List<string> { "PeshoTeam", "Pesho" };
            var addMember = new AddMemberToTeamCommand(addMemberParam, repo);
            var addMemberExec = addMember.Execute();


            List<string> commandParameters = new List<string> { title, description, rating, "PeshoBoard" };
            var command = new CreateFeedbackCommand(commandParameters, repo);
            var createFeedback = command.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Rating", "5" };
            var changeCommand = new ChangeFeedbackCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Feedback with ID: 1 is already: 5", returned);

        }

        [TestMethod]
        public void ShouldChangeFeedbaclStatus()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string rating = "5";

            List<string> memberParam = new List<string> { "Pesho" };
            var createMember = new CreateMemberCommand(memberParam, repo);
            var memberExec = createMember.Execute();

            List<string> teamParam = new List<string> { "PeshoTeam" };
            var createTeam = new CreateTeamCommand(teamParam, repo);
            var teamExec = createTeam.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "PeshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> addMemberParam = new List<string> { "PeshoTeam", "Pesho" };
            var addMember = new AddMemberToTeamCommand(addMemberParam, repo);
            var addMemberExec = addMember.Execute();


            List<string> commandParameters = new List<string> { title, description, rating, "PeshoBoard" };
            var command = new CreateFeedbackCommand(commandParameters, repo);
            var createFeedback = command.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Status", "Done" };
            var changeCommand = new ChangeFeedbackCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Changed the status of feedback with ID: 1 from New to Done", returned);

        }

        [TestMethod]
        public void ShouldReturnMessageStatusIsAlreadyTheSame()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string rating = "5";

            List<string> memberParam = new List<string> { "Pesho" };
            var createMember = new CreateMemberCommand(memberParam, repo);
            var memberExec = createMember.Execute();

            List<string> teamParam = new List<string> { "PeshoTeam" };
            var createTeam = new CreateTeamCommand(teamParam, repo);
            var teamExec = createTeam.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "PeshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> addMemberParam = new List<string> { "PeshoTeam", "Pesho" };
            var addMember = new AddMemberToTeamCommand(addMemberParam, repo);
            var addMemberExec = addMember.Execute();


            List<string> commandParameters = new List<string> { title, description, rating, "PeshoBoard" };
            var command = new CreateFeedbackCommand(commandParameters, repo);
            var createFeedback = command.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Status", "New" };
            var changeCommand = new ChangeFeedbackCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Feedback with ID: 1 is already: New", returned);

        }


        [TestMethod]
        public void ShouldThrowInvalidUserInputException()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string rating = "5";

            List<string> memberParam = new List<string> { "Pesho" };
            var createMember = new CreateMemberCommand(memberParam, repo);
            var memberExec = createMember.Execute();

            List<string> teamParam = new List<string> { "PeshoTeam" };
            var createTeam = new CreateTeamCommand(teamParam, repo);
            var teamExec = createTeam.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "PeshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> addMemberParam = new List<string> { "PeshoTeam", "Pesho" };
            var addMember = new AddMemberToTeamCommand(addMemberParam, repo);
            var addMemberExec = addMember.Execute();


            List<string> commandParameters = new List<string> { title, description, rating, "PeshoBoard" };
            var command = new CreateFeedbackCommand(commandParameters, repo);
            var createFeedback = command.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Status" };
            var changeCommand = new ChangeFeedbackCommand(changeList, repo);

            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => changeCommand.Execute());

        }

        [TestMethod]
        public void ShouldThrowEntityNotFoundException()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string rating = "5";

            List<string> memberParam = new List<string> { "Pesho" };
            var createMember = new CreateMemberCommand(memberParam, repo);
            var memberExec = createMember.Execute();

            List<string> teamParam = new List<string> { "PeshoTeam" };
            var createTeam = new CreateTeamCommand(teamParam, repo);
            var teamExec = createTeam.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "PeshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> addMemberParam = new List<string> { "PeshoTeam", "Pesho" };
            var addMember = new AddMemberToTeamCommand(addMemberParam, repo);
            var addMemberExec = addMember.Execute();


            List<string> commandParameters = new List<string> { title, description, rating, "PeshoBoard" };
            var command = new CreateFeedbackCommand(commandParameters, repo);
            var createFeedback = command.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "NotValid", "2"};
            var changeCommand = new ChangeFeedbackCommand(changeList, repo);

            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => changeCommand.Execute());

        }
    }
}
