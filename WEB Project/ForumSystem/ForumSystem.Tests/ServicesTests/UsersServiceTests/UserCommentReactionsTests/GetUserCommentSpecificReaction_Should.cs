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
    public class GetUserCommentSpecificReaction_Should
    {
        [TestMethod]
        public void GetCommentReaction_When_ParamsAreValid()
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
            repoCommentMock.Setup(repo => repo.GetCommentById(comment.Id))
                            .Returns(comment);

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock);

            // Act
            var actualCommentReaction = sut.GetUserCommentSpecificReaction(user.Id, comment.Id, expectedCommentReaction.Id);

            // Assert
            Assert.AreEqual(expectedCommentReaction.Id, actualCommentReaction.Id);
            Assert.AreEqual(expectedCommentReaction.User.Username, actualCommentReaction.Author);
            Assert.AreEqual(expectedCommentReaction.Reaction.ToString(), actualCommentReaction.Reaction);
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
            repoCommentMock.Setup(repo => repo.GetCommentById(comment.Id))
                            .Returns(comment);

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock);

            // Act
            var actualCommentReaction = sut.GetUserCommentSpecificReaction(user.Id, comment.Id, expectedCommentReaction.Id);
        }
    }
}
