using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserCommentReactionsTests
{
    [TestClass]
    public class DeleteUserCommentSpecificReaction_Should
    {
        [TestMethod]
        public void DeleteCommentReaction_When_ParamsAreValid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var comment = TestUserHelper.GetTestComment();
            var expectedCommentReaction = TestUserHelper.GetTestCommentReaction();
            var post = TestUserHelper.GetTestPost();

            comment.Post = post;
            comment.PostId = post.Id;
            comment.User = user;
            comment.UserId = user.Id;
            expectedCommentReaction.User = user;
            expectedCommentReaction.UserId = user.Id;
            expectedCommentReaction.Comment = comment;
            expectedCommentReaction.CommentId = comment.Id;

            var expectedCommentReactions = new List<CommentReaction>() { expectedCommentReaction };
            var comments = new List<Comment>() { comment };
            comment.CommentReactions = expectedCommentReactions;
            post.Comments = comments;

            var repoUserMock = new Mock<IUsersRepository>();
            repoUserMock.Setup(repo => repo.GetById(user.Id))
                          .Returns(user);
            var repoCommentMock = new Mock<ICommentsRepository>();
            repoCommentMock.Setup(repo => repo.GetAll())
                            .Returns(comments);
            repoCommentMock.Setup(repo => repo.GetCommentById(comment.Id))
                            .Returns(comment);
            var serviceCommentMock = new Mock<ICommentsService>();
            serviceCommentMock.Setup(service => service.DeleteCommentReaction(post.Id, comment.Id, expectedCommentReaction.Id, user.Id))
                                .Callback(() => expectedCommentReactions.Remove(expectedCommentReaction));

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock, serviceCommentMock);

            // Act
            sut.DeleteUserCommentSpecificReaction(user.Id, comment.Id, expectedCommentReaction.Id, user);

            // Assert
            CollectionAssert.DoesNotContain(expectedCommentReactions, expectedCommentReaction);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_ParamsAreInvalid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var comment = TestUserHelper.GetTestComment();
            var expectedCommentReaction = TestUserHelper.GetTestCommentReaction();
            var post = TestUserHelper.GetTestPost();

            var anotherUser = new User()
            {
                Id = 7,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                Role = TestUserHelper.GetTestDefaultRole()
            };

            comment.Post = post;
            comment.PostId = post.Id;
            comment.User = user;
            comment.UserId = user.Id;
            expectedCommentReaction.User = user;
            expectedCommentReaction.UserId = user.Id;
            expectedCommentReaction.Comment = comment;
            expectedCommentReaction.CommentId = comment.Id;

            var expectedCommentReactions = new List<CommentReaction>() { expectedCommentReaction };
            var comments = new List<Comment>() { comment };
            comment.CommentReactions = expectedCommentReactions;
            post.Comments = comments;

            var repoUserMock = new Mock<IUsersRepository>();
            repoUserMock.Setup(repo => repo.GetById(user.Id))
                          .Returns(user);
            var repoCommentMock = new Mock<ICommentsRepository>();
            repoCommentMock.Setup(repo => repo.GetAll())
                            .Returns(comments);
            repoCommentMock.Setup(repo => repo.GetCommentById(comment.Id))
                            .Returns(comment);
            var serviceCommentMock = new Mock<ICommentsService>();
            serviceCommentMock.Setup(service => service.DeleteCommentReaction(post.Id, comment.Id, expectedCommentReaction.Id, user.Id))
                                .Callback(() => expectedCommentReactions.Remove(expectedCommentReaction));

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock, serviceCommentMock);

            // Act
            sut.DeleteUserCommentSpecificReaction(user.Id, comment.Id, expectedCommentReaction.Id, anotherUser);
        }
    }
}
