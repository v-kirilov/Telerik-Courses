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
    public class ChangeBugCommandTests
    {
        [TestMethod]
        public void ShouldChangePriorityOfBug()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

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


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            var command = new CreateBugCommand(commandParameters, repo);
            var createBug = command.Execute();
            // Act
            List<string> changeList = new List<string> { "1", "Priority", "Low" };
            var changeCommand = new ChangeBugCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Changed the priority of bug with ID: 1 from High to Low", returned);

        }

        [TestMethod]
        public void ShouldReturnMessagePriorityIsAlreadyTheSame()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

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


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            var command = new CreateBugCommand(commandParameters, repo);
            var createBug = command.Execute();
            // Act
            List<string> changeList = new List<string> { "1", "Priority", "High" };
            var changeCommand = new ChangeBugCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Bug with ID: 1 is already: High", returned);

        }

        [TestMethod]
        public void ShouldChangeSeverityOfBug()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

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


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            var command = new CreateBugCommand(commandParameters, repo);
            var createBug = command.Execute();
            // Act
            List<string> changeList = new List<string> { "1", "Severity", "Major" };
            var changeCommand = new ChangeBugCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Changed the severity of bug with ID: 1 from Critical to Major", returned);

        }
        [TestMethod]
        public void ShouldReturnMessageSeverityIsAlreadyTheSame()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

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


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            var command = new CreateBugCommand(commandParameters, repo);
            var createBug = command.Execute();
            // Act
            List<string> changeList = new List<string> { "1", "Severity", "Critical" };
            var changeCommand = new ChangeBugCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Bug with ID: 1 is already: Critical", returned);

        }


        [TestMethod]
        public void ShouldChangeStatusOfBug()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

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


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            var command = new CreateBugCommand(commandParameters, repo);
            var createBug = command.Execute();
            // Act
            List<string> changeList = new List<string> { "1", "Status", "Fixed" };
            var changeCommand = new ChangeBugCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Changed the status of bug with ID: 1 from Active to Fixed", returned);

        }

        [TestMethod]
        public void ShouldReturnMessageStatusIsAlreadyTheSame()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

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


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            var command = new CreateBugCommand(commandParameters, repo);
            var createBug = command.Execute();
            // Act
            List<string> changeList = new List<string> { "1", "Status", "Active" };
            var changeCommand = new ChangeBugCommand(changeList, repo);
            var returned = changeCommand.Execute();

            // Assert
            Assert.AreEqual("Bug with ID: 1 is already: Active", returned);

        }

        [TestMethod]
        public void ShouldThrowEntityNotFoundException()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

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


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            var command = new CreateBugCommand(commandParameters, repo);
            var createBug = command.Execute();
            // Act
            List<string> changeList = new List<string> { "1", "WrongChange", "Active" };
            var changeCommand = new ChangeBugCommand(changeList, repo);

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
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

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


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            var command = new CreateBugCommand(commandParameters, repo);
            var createBug = command.Execute();
            // Act
            List<string> changeList = new List<string> { "1", "Status"};
            var changeCommand = new ChangeBugCommand(changeList, repo);

            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => changeCommand.Execute());


        }
    }
}
