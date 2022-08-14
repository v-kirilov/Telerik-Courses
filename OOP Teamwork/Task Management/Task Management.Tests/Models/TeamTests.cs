using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;
using Task_Management.Models;

namespace Task_Management.Tests.Models
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void ConstructorShouldSetCorrectName_WhenNameIsValid()
        {
            // Arrange
            string teamName = "TestName";

            // Act 
            var sut = new Team(teamName);

            // Assert
            Assert.AreEqual(teamName, sut.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ConstructorShouldThrowException_WhenNameLengthLessThanMin()
        {
            // Arrange 
            var invalidName = new string('a', 4);

            // Act
            var sut = new Team(invalidName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException))]
        public void ConstructorShouldThrowException_WhenNameLengthMoreThanMax()
        {
            // Arrange 
            var invalidName = new string('a', 16);

            // Act
            var sut = new Team(invalidName);
        }

        [TestMethod]
        public void ShouldAddMemberTypeToMembersList()
        {
            // Arrange
            var team = new Team("Test team");
            var member = new Member("Test member");
            var currMembersCount = team.Members.Count;

            // Act 
            team.AddMember(member);

            // Assert
            Assert.AreEqual(currMembersCount + 1, team.Members.Count);
        }

        [TestMethod]
        public void ShouldAddMemberWithNameToMembersList()
        {
            // Arrange
            var team = new Team("Test team");
            var member = new Member("Test member");
            var currMembersCount = team.Members.Count;

            // Act 
            team.AddMember("Test member");

            // Assert
            Assert.AreEqual(currMembersCount + 1, team.Members.Count);
        }

        [TestMethod]
        public void ShouldAddBoardTypeToBoardsList()
        {
            // Arrange
            var team = new Team("Test team");
            var board = new Board("Test board");
            var currBoardsCount = team.Boards.Count;

            // Act 
            team.AddBoard(board);

            // Assert
            Assert.AreEqual(currBoardsCount + 1, team.Boards.Count);
        }

        [TestMethod]
        public void ShouldAddBoardWithNameToBoardsList()
        {
            // Arrange
            var team = new Team("Test team");
            var board = new Board("Test board");
            var currBoardsCount = team.Boards.Count;

            // Act 
            team.AddBoard("Test board");

            // Assert
            Assert.AreEqual(currBoardsCount + 1, team.Boards.Count);
        }
    }
}
