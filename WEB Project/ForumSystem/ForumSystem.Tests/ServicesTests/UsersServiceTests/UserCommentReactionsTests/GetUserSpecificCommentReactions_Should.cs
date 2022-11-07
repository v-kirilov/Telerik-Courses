using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserCommentReactionsTests
{
    [TestClass]
    public class GetUserSpecificCommentReactions_Should
    {
        [TestMethod]
        public void GetCommentReactions_When_ParamsAreValid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var comment = TestUserHelper.GetTestComment();
            var expectedCommentReaction = TestUserHelper.GetTestCommentReaction();

            comment.User = user;
            comment.UserId = user.Id;
            expectedCommentReaction.User = user;
            expectedCommentReaction.UserId = user.Id;
            expectedCommentReaction.Comment = comment;
            expectedCommentReaction.CommentId = comment.Id;

            var expectedCommentReactions = new List<CommentReaction>() { expectedCommentReaction };
            var comments = new List<Comment>() { comment };
            comment.CommentReactions = expectedCommentReactions;

            var repoUserMock = new Mock<IUsersRepository>();
            repoUserMock.Setup(repo => repo.GetById(user.Id))
                          .Returns(user);
            var repoCommentMock = new Mock<ICommentsRepository>();
            repoCommentMock.Setup(repo => repo.GetAll())
                            .Returns(comments);

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock);

            // Act
            var actualCommentReactions = sut.GetUserSpecificCommentReactions(user.Id, comment.Id);

            // Assert
            Assert.AreEqual(expectedCommentReactions[0].Id, actualCommentReactions[0].Id);
            Assert.AreEqual(expectedCommentReactions[0].User.Username, actualCommentReactions[0].Author);
            Assert.AreEqual(expectedCommentReactions[0].Reaction.ToString(), actualCommentReactions[0].Reaction);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void ThrowEntityNotFoundException_When_ParamsAreInvalid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var comment = TestUserHelper.GetTestComment();
            var expectedCommentReaction = TestUserHelper.GetTestCommentReaction();

            comment.User = user;
            comment.UserId = user.Id;
            expectedCommentReaction.User = user;
            expectedCommentReaction.UserId = user.Id;
            expectedCommentReaction.Comment = comment;
            expectedCommentReaction.CommentId = comment.Id;

            var expectedCommentReactions = new List<CommentReaction>() { expectedCommentReaction };
            var comments = new List<Comment>() { comment };

            var repoUserMock = new Mock<IUsersRepository>();
            repoUserMock.Setup(repo => repo.GetById(user.Id))
                          .Returns(user);
            var repoCommentMock = new Mock<ICommentsRepository>();
            repoCommentMock.Setup(repo => repo.GetAll())
                            .Returns(comments);

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock);

            // Act
            var actualCommentReactions = sut.GetUserSpecificCommentReactions(user.Id, comment.Id);
        }
    }
}
