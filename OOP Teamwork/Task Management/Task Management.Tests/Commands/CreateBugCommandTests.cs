using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models;
using Task_Management.Core.Contracts;
using Task_Management.Core;
using Task_Management.Commands;
using Task_Management.Exceptions;

namespace Task_Management.Tests.Commands
{
    [TestClass]

    public class CreateBugCommandTests
    {
        [TestMethod]
        public void ShouldExecuteCommandWhenParametersAreValid()
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

            //ITeam peshoTeam = new Team("PeshoTeam");
            List<string> teamParam = new List<string> { "PeshoTeam" };
            var createTeam = new CreateTeamCommand(teamParam, repo);
            var teamExec = createTeam.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "PeshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> addMemberParam = new List<string> { "PeshoTeam", "Pesho" };
            var addMember = new AddMemberToTeamCommand(addMemberParam,repo);
            var addMemberExec = addMember.Execute();


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            // Act
            var command = new CreateBugCommand(commandParameters, repo);
            var returned = command.Execute();
            // Assert
            Assert.AreEqual("Bug with ID: 1 and title: title_Must_be_Big was created.", returned);
        }

        [TestMethod]
        public void ShouldThrowExceptionInvalidNumberOfArguments()
        {
            // Arrange
            IRepository repo = new Repository();

            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            string priority = "High";
            string severity = "Critical";
            IBoard peshoBoard = new Board("PeshoBoard");

            List<string> commandParameters = new List<string> { description, steps, priority, severity, "PeshoBoard" };
            // Act
            var command = new CreateBugCommand(commandParameters, repo);
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => command.Execute());
        }

        
        [TestMethod]
        public void ShouldThrowExceptionMemberIsNotInATeam()
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

            //ITeam peshoTeam = new Team("PeshoTeam");
            List<string> teamParam = new List<string> { "PeshoTeam" };
            var createTeam = new CreateTeamCommand(teamParam, repo);
            var teamExec = createTeam.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "PeshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            // Act
            var command = new CreateBugCommand(commandParameters, repo);
            //var returned = command.Execute();
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => command.Execute());

        }


        [TestMethod]
        public void ShouldThrowExceptionWhenBoardIsNotInCorrectTeam()
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

            List<string> teamParam2 = new List<string> { "ToshoTeam" };
            var createTeam2 = new CreateTeamCommand(teamParam2, repo);
            var teamExec2 = createTeam2.Execute();

            List<string> boardParam = new List<string> { "PeshoBoard", "ToshoTeam" };
            var createBoard = new CreateBoardCommand(boardParam, repo);
            var boardExec = createBoard.Execute();

            List<string> addMemberParam = new List<string> { "PeshoTeam", "Pesho" };
            var addMember = new AddMemberToTeamCommand(addMemberParam, repo);
            var addMemberExec = addMember.Execute();


            List<string> commandParameters = new List<string> { title, description, steps, priority, severity, "Pesho", "PeshoBoard" };
            // Act
            var command = new CreateBugCommand(commandParameters, repo);
            //var returned = command.Execute();
            // Assert
            Assert.ThrowsException<EntityNotFoundException>(() => command.Execute());

        }

    }
}
