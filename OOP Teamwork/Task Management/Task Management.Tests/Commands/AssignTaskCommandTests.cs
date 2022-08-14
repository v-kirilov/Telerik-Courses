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
    public class AssignTaskCommandTests
    {


        [TestMethod]
        public void ShouldExecuteCommandWhenParametersAreValidForBug()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

            List<string> memberParam1 = new List<string> { "Pesho" };
            List<string> memberParam2 = new List<string> { "Tosho" };
            var createPesho = new CreateMemberCommand(memberParam1, repo);
            var createTosho = new CreateMemberCommand(memberParam2, repo);
            var memberExec1 = createPesho.Execute();
            var memberExec2 = createTosho.Execute();

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
            var createBug = new CreateBugCommand(commandParameters, repo);
            var createBugReturn = createBug.Execute();

            List<string> unassignList = new List<string> { "1" };
            var unassigne = new UnassignTaskCommand(unassignList, repo);
            var unassigneReturn = unassigne.Execute();

            // Act
            List<string> assignList = new List<string> { "1", "Pesho" };
            var assign = new AssignTaskCommand(assignList, repo);
            var assignReturn = assign.Execute();
            // Assert
            Assert.AreEqual("Task with ID 1 assigned to Pesho.", assignReturn);
        }


        [TestMethod]
        public void ShouldThrowInvalidUserInputExceptionForBug()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

            List<string> memberParam1 = new List<string> { "Pesho" };
            List<string> memberParam2 = new List<string> { "Tosho" };
            var createPesho = new CreateMemberCommand(memberParam1, repo);
            var createTosho = new CreateMemberCommand(memberParam2, repo);
            var memberExec1 = createPesho.Execute();
            var memberExec2 = createTosho.Execute();

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
            var createBug = new CreateBugCommand(commandParameters, repo);
            var createBugReturn = createBug.Execute();

            // Act
            List<string> assignList = new List<string> { "1", "Tosho" };
            var assign = new AssignTaskCommand(assignList, repo);
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => assign.Execute());

        }

        [TestMethod]
        public void ShouldExecuteCommandWhenParametersAreValidForStory()
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
            var command = new CreateStoryCommand(commandParameters, repo);
            var returned = command.Execute();

            List<string> unassignList = new List<string> { "1" };
            var unassigne = new UnassignTaskCommand(unassignList, repo);
            var unassigneReturn = unassigne.Execute();

            // Act
            List<string> assignList = new List<string> { "1", "Pesho" };
            var assign = new AssignTaskCommand(assignList, repo);
            var assignReturn = assign.Execute();
            // Assert
            Assert.AreEqual("Task with ID 1 assigned to Pesho.", assignReturn);
        }

        [TestMethod]

        public void ShouldThrowInvalidUserInputExceptionForStory()
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
            var command = new CreateStoryCommand(commandParameters, repo);
            var returned = command.Execute();

            List<string> unassignList = new List<string> { "1" };
            var unassigne = new UnassignTaskCommand(unassignList, repo);
            var unassigneReturn = unassigne.Execute();

            // Act
            List<string> assignList = new List<string> { "1", "Pesho" };
            var assign = new AssignTaskCommand(assignList, repo);
            var assignReturn = assign.Execute();
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => assign.Execute());

        }

        [TestMethod]
        public void ShouldThrowInvalidUserInputExceptionForNumberOfParameters()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";

            List<string> memberParam1 = new List<string> { "Pesho" };
            List<string> memberParam2 = new List<string> { "Tosho" };
            var createPesho = new CreateMemberCommand(memberParam1, repo);
            var createTosho = new CreateMemberCommand(memberParam2, repo);
            var memberExec1 = createPesho.Execute();
            var memberExec2 = createTosho.Execute();

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
            var createBug = new CreateBugCommand(commandParameters, repo);
            var createBugReturn = createBug.Execute();

            List<string> unassignList = new List<string> { "1" };
            var unassigne = new UnassignTaskCommand(unassignList, repo);
            var unassigneReturn = unassigne.Execute();

            // Act
            List<string> assignList = new List<string> { "1","2", "Pesho" };
            var assign = new AssignTaskCommand(assignList, repo);
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => assign.Execute());

        }

        [TestMethod]
        public void ShouldThrowInvalidUserInputExceptionForFeedback()
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
            var returned = command.Execute();

            // Act
            List<string> assignList = new List<string> { "1", "Pesho" };
            var assign = new AssignTaskCommand(assignList, repo);

            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => assign.Execute());

        }
    }
}
