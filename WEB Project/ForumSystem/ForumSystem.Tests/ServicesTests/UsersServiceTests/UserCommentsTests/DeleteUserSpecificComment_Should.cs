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
    public class DeleteUserSpecificComment_Should
    {
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_ParamsAreInvalid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            var anotherUser = new User
            {
                Id = 5,
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                Email = "EmailTest",
                Role = TestUserHelper.GetTestDefaultRole()
            };
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
            var serviceCommentsMock = new Mock<ICommentsService>();

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoCommentsMock, serviceCommentsMock);

            // Act
            sut.DeleteUserSpecificComment(defaultUser.Id, expectedComment.Id, anotherUser);
        }

        [TestMethod]
        public void DeleteComment_When_ParamsAreValid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            var anotherUser = new User
            {
                Id = 5,
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                Email = "EmailTest",
                Role = TestUserHelper.GetTestDefaultRole()
            };
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
            var serviceCommentsMock = new Mock<ICommentsService>();
            serviceCommentsMock.Setup(service => service.Delete(post.Id, expectedComment.Id, defaultUser))
                               .Callback(() => comments.Remove(expectedComment));

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoCommentsMock, serviceCommentsMock);

            // Act
            sut.DeleteUserSpecificComment(defaultUser.Id, expectedComment.Id, defaultUser);

            // Assert
            CollectionAssert.DoesNotContain(comments, expectedComment);
        }
    }
}
