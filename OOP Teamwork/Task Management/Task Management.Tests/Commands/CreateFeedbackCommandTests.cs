using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Task_Management.Commands;
using Task_Management.Core;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;

namespace Task_Management.Tests.Commands
{
    [TestClass]

    public class CreateFeedbackCommandTests
    {
        [TestMethod]
        public void ShouldExecuteCommandWhenParametersAreValid()
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
            // Act
            var command = new CreateFeedbackCommand(commandParameters, repo);
            var returned = command.Execute();
            // Assert
            Assert.AreEqual("Feedback with ID: 1 and title: title_Must_be_Big was created.", returned);
        }

        [TestMethod]
        public void ShouldThrowExceptionInvalidNumberOfArguments()
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


            List<string> commandParameters = new List<string> { title, description,  "PeshoBoard" };
            // Act
            var command = new CreateFeedbackCommand(commandParameters, repo);
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => command.Execute());

        }
    }
}
