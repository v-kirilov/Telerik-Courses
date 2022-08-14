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
    public class AddCommentCommandTests
    {
        
        [TestMethod]
        public void ShouldExecuteCommandWhenParametersAreValid()
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

            // Act
            List<string> comentPar = new List<string> { "1","Content","Pesho" };
            var comentCommand = new AddCommentCommand(comentPar, repo);
            var commentReturn = comentCommand.Execute();

            // Assert
            Assert.AreEqual("Comment with author: Pesho was added to task with ID: 1", commentReturn);
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
            var command = new CreateStoryCommand(commandParameters, repo);
            var returned = command.Execute();

            // Act
            List<string> comentPar = new List<string> { "1", "Content", "Tosho" };
            var comentCommand = new AddCommentCommand(comentPar, repo);

            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => comentCommand.Execute());
        }

        [TestMethod]
        public void ShouldThrowInvalidUserInputExceptionForParameters()
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

            // Act
            List<string> comentPar = new List<string> { "1","2", "Content", "Tosho" };
            var comentCommand = new AddCommentCommand(comentPar, repo);

            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => comentCommand.Execute());
        }

    }
}
