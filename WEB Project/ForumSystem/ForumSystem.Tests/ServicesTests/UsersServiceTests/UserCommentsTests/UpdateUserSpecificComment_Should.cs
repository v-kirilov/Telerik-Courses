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

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserCommentsTests
{
    [TestClass]
    public class UpdateUserSpecificComment_Should
    {
        [TestMethod]
        public void UpdateComment_When_ParamsAreValid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            var expectedComment = TestUserHelper.GetTestComment();
            var post = TestUserHelper.GetTestPost();
            post.User = defaultUser;
            expectedComment.Post = post;
            expectedComment.PostId = post.Id;
            expectedComment.User = defaultUser;
            expectedComment.UserId = defaultUser.Id;
            var comments = new List<Comment>() { expectedComment };
            defaultUser.Comments = comments;

            var commentDto = new CommentDto
            {
                CommentId = expectedComment.Id,
                CommentContent = expectedComment.CommentContent
            };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            var repoCommentsMock = new Mock<ICommentsRepository>();
            repoCommentsMock.Setup(repo => repo.GetAll())
                            .Returns(comments);
            var modelMapperMock= new Mock<IModelMapper>();
            modelMapperMock.Setup(model => model.ToModel(commentDto, expectedComment.Id, defaultUser))
                            .Returns(expectedComment);
            var serviceCommentsMock = new Mock<ICommentsService>();
            serviceCommentsMock.Setup(service => service.Update(post.Id, expectedComment.Id, expectedComment, defaultUser.Id))
                               .Returns(commentDto);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoCommentsMock, serviceCommentsMock, modelMapperMock);

            // Act
            var actualUpdatedComment = sut.UpdateUserSpecificComment(defaultUser.Id, expectedComment.Id, commentDto, defaultUser);

            // Assert
            Assert.AreEqual(expectedComment.Id, actualUpdatedComment.Id);
            Assert.AreEqual(expectedComment.CommentContent, actualUpdatedComment.CommentContent);
            Assert.AreEqual(expectedComment.User.Username, actualUpdatedComment.Author);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_ParamsAreInvalid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            var anotherUser = TestUserHelper.GetTestAdminUser();
            var expectedComment = TestUserHelper.GetTestComment();
            var post = TestUserHelper.GetTestPost();
            post.User = defaultUser;
            expectedComment.Post = post;
            expectedComment.PostId = post.Id;
            expectedComment.User = defaultUser;
            expectedComment.UserId = defaultUser.Id;
            var comments = new List<Comment>() { expectedComment };
            defaultUser.Comments = comments;

            var commentDto = new CommentDto
            {
                CommentId = expectedComment.Id,
                CommentContent = expectedComment.CommentContent
            };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            var repoCommentsMock = new Mock<ICommentsRepository>();
            repoCommentsMock.Setup(repo => repo.GetAll())
                            .Returns(comments);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(model => model.ToModel(commentDto, expectedComment.Id, defaultUser))
                            .Returns(expectedComment);
            var serviceCommentsMock = new Mock<ICommentsService>();
            serviceCommentsMock.Setup(service => service.Update(post.Id, expectedComment.Id, expectedComment, defaultUser.Id))
                               .Returns(commentDto);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoCommentsMock, serviceCommentsMock, modelMapperMock);

            // Act
            var actualUpdatedComment = sut.UpdateUserSpecificComment(defaultUser.Id, expectedComment.Id, commentDto, anotherUser);
        }
    }
}
