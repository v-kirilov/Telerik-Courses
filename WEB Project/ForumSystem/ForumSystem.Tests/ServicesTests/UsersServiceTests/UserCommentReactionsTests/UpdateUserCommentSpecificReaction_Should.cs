using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
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
    public class UpdateUserCommentSpecificReaction_Should
    {
        [TestMethod]
        public void UpdateCommentReactions_When_ParamsAreValid()
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

            var commentReactionDto = new CommentReactionDto
            {
                Id = expectedCommentReaction.Id,
                Author = expectedCommentReaction.User.Username,
                Reaction = expectedCommentReaction.Reaction.ToString()
            };

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
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.ToModel(commentReactionDto, comment.Id, user))
                            .Returns(expectedCommentReaction);
            var serviceCommentMock = new Mock<ICommentsService>();
            serviceCommentMock.Setup(service => service.UpdateCommentReaction(post.Id, comment.Id, expectedCommentReaction.Id, expectedCommentReaction, user.Id))
                                .Returns(commentReactionDto);            

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock, serviceCommentMock, modelMapperMock);

            // Act
            var actualCommentReaction = sut.UpdateUserCommentSpecificReaction(user.Id, comment.Id, expectedCommentReaction.Id, commentReactionDto, user);

            // Assert
            Assert.AreEqual(expectedCommentReaction.Id, actualCommentReaction.Id);
            Assert.AreEqual(expectedCommentReaction.User.Username, actualCommentReaction.Author);
            Assert.AreEqual(expectedCommentReaction.Reaction.ToString(), actualCommentReaction.Reaction);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_ParamsAreInvalid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var anotherUser = TestUserHelper.GetTestAdminUser();
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

            var commentReactionDto = new CommentReactionDto
            {
                Id = expectedCommentReaction.Id,
                Author = expectedCommentReaction.User.Username,
                Reaction = expectedCommentReaction.Reaction.ToString()
            };

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
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.ToModel(commentReactionDto, comment.Id, user))
                            .Returns(expectedCommentReaction);
            var serviceCommentMock = new Mock<ICommentsService>();
            serviceCommentMock.Setup(service => service.UpdateCommentReaction(post.Id, comment.Id, expectedCommentReaction.Id, expectedCommentReaction, user.Id))
                                .Returns(commentReactionDto);

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock, serviceCommentMock, modelMapperMock);

            // Act
            var actualCommentReaction = sut.UpdateUserCommentSpecificReaction(user.Id, comment.Id, expectedCommentReaction.Id, commentReactionDto, anotherUser);
        }
    }
}
