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
    public class ChangeStoryCommandTests
    {
        [TestMethod]
        public void ShouldChangePriority()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string priority = "High";
            string size = "Large";

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


            List<string> commandParameters = new List<string> { title, description, priority, size, "Pesho", "PeshoBoard" };
            // Act
            var createCommand = new CreateStoryCommand(commandParameters, repo);
            var createResult = createCommand.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Priority", "Medium" };
            var changeCommand = new ChangeStoryCommand(changeList, repo);
            var returned = changeCommand.Execute();
            // Assert
            Assert.AreEqual("Changed the priority of status with ID: 1 from High to Medium", returned);
        }

        [TestMethod]
        public void ShouldReturnMessagePriorityIsAlreadyTheSame()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string priority = "High";
            string size = "Large";

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


            List<string> commandParameters = new List<string> { title, description, priority, size, "Pesho", "PeshoBoard" };
            // Act
            var createCommand = new CreateStoryCommand(commandParameters, repo);
            var createResult = createCommand.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Priority", "High" };
            var changeCommand = new ChangeStoryCommand(changeList, repo);
            var returned = changeCommand.Execute();
            // Assert
            Assert.AreEqual("Story with ID: 1 is already: High", returned);
        }


        [TestMethod]
        public void ShouldChangeSize()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string priority = "High";
            string size = "Large";

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


            List<string> commandParameters = new List<string> { title, description, priority, size, "Pesho", "PeshoBoard" };
            // Act
            var createCommand = new CreateStoryCommand(commandParameters, repo);
            var createResult = createCommand.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Size", "Medium" };
            var changeCommand = new ChangeStoryCommand(changeList, repo);
            var returned = changeCommand.Execute();
            // Assert
            Assert.AreEqual("Changed the size of story with ID: 1 from Large to Medium", returned);
        }



        [TestMethod]
        public void ShouldReturnMessageSizeIsAlreadyTheSame()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string priority = "High";
            string size = "Large";

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


            List<string> commandParameters = new List<string> { title, description, priority, size, "Pesho", "PeshoBoard" };
            // Act
            var createCommand = new CreateStoryCommand(commandParameters, repo);
            var createResult = createCommand.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Size", "Large" };
            var changeCommand = new ChangeStoryCommand(changeList, repo);
            var returned = changeCommand.Execute();
            // Assert
            Assert.AreEqual("Story with ID: 1 is already: Large", returned);
        }

        [TestMethod]
        public void ShouldChangeStatus()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string priority = "High";
            string size = "Large";

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


            List<string> commandParameters = new List<string> { title, description, priority, size, "Pesho", "PeshoBoard" };
            // Act
            var createCommand = new CreateStoryCommand(commandParameters, repo);
            var createResult = createCommand.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Status", "InProgress" };
            var changeCommand = new ChangeStoryCommand(changeList, repo);
            var returned = changeCommand.Execute();
            // Assert
            Assert.AreEqual("Changed the status of story with ID: 1 from NotDone to InProgress", returned);
        }



        [TestMethod]
        public void ShouldReturnMessageStatusIsAlreadyTheSame()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string priority = "High";
            string size = "Large";

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


            List<string> commandParameters = new List<string> { title, description, priority, size, "Pesho", "PeshoBoard" };
            // Act
            var createCommand = new CreateStoryCommand(commandParameters, repo);
            var createResult = createCommand.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "Status", "NotDone" };
            var changeCommand = new ChangeStoryCommand(changeList, repo);
            var returned = changeCommand.Execute();
            // Assert
            Assert.AreEqual("Story with ID: 1 is already: NotDone", returned);
        }

        [TestMethod]
        public void ShouldThrowEntityNotFoundException()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string priority = "High";
            string size = "Large";

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


            List<string> commandParameters = new List<string> { title, description, priority, size, "Pesho", "PeshoBoard" };
            // Act
            var createCommand = new CreateStoryCommand(commandParameters, repo);
            var createResult = createCommand.Execute();

            // Act
            List<string> changeList = new List<string> { "1", "WrongType", "NotDone" };
            var changeCommand = new ChangeStoryCommand(changeList, repo);
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => changeCommand.Execute());

        }
        [TestMethod]
        public void ShouldThrowInvalidUserInputException()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string priority = "High";
            string size = "Large";

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


            List<string> commandParameters = new List<string> { title, description, priority, size, "Pesho", "PeshoBoard" };
            // Act
            var createCommand = new CreateStoryCommand(commandParameters, repo);
            var createResult = createCommand.Execute();

            // Act
            List<string> changeList = new List<string> { "WrongType", "NotDone" };
            var changeCommand = new ChangeStoryCommand(changeList, repo);
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => changeCommand.Execute());

        }
    }
}
