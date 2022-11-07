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
    public class CreateUserSpecificCommentReaction_Should
    {
        [TestMethod]
        public void CreateCommentReactions_When_ParamsAreValid()
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
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.ToModel(commentReactionDto, comment.Id, user))
                            .Returns(expectedCommentReaction);
            var serviceCommentMock = new Mock<ICommentsService>();
            serviceCommentMock.Setup(service => service.CreateCommentReaction(post.Id, comment.Id, user.Id, expectedCommentReaction))
                                .Returns(commentReactionDto);

            var sut = TestUserHelper.InitializeUsersService(repoUserMock, repoCommentMock, serviceCommentMock, modelMapperMock);

            // Act
            var actualCommentReaction = sut.CreateUserSpecificCommentReaction(user.Id, comment.Id, commentReactionDto, user);

            // Assert
            Assert.AreEqual(expectedCommentReaction.Id, actualCommentReaction.Id);
            Assert.AreEqual(expectedCommentReaction.User.Username, actualCommentReaction.Author);
            Assert.AreEqual(expectedCommentReaction.Reaction.ToString(), actualCommentReaction.Reaction);
        }
    }
}
