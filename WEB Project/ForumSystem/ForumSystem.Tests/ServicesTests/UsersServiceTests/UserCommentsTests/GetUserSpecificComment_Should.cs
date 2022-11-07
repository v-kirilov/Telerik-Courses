using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Repositories.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserCommentsTests
{
    [TestClass]
    public class GetUserSpecificComment_Should
    {
        [TestMethod]
        public void GetSpecificComment_When_ParamsAreValid()
        {
            // Arrange
            var expectedUser = TestUserHelper.GetTestDefaultUser();
            var expectedComment = TestUserHelper.GetTestComment();
            var post = TestUserHelper.GetTestPost();
            expectedComment.Post = post;
            expectedComment.User = expectedUser;
            expectedComment.UserId = expectedUser.Id;
            post.User = expectedUser;
            var comments = new List<Comment>() { expectedComment };
            expectedUser.Comments = comments;

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);
            var repoCommentsMock = new Mock<ICommentsRepository>();
            repoCommentsMock.Setup(repo => repo.GetAll())
                            .Returns(comments);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoCommentsMock);

            // Act
            var actualComment = sut.GetUserSpecificComment(expectedUser.Id, expectedComment.Id);

            // Assert
            Assert.AreEqual(actualComment.Id, expectedComment.Id);
            Assert.AreEqual(actualComment.CommentContent, expectedComment.CommentContent);
            Assert.AreEqual(actualComment.Author, expectedComment.User.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void ThorowEntityNotFoundException_When_ParamsAreInvalid()
        {
            // Arrange
            var expectedUser = TestUserHelper.GetTestDefaultUser();
            var anotherUser = TestUserHelper.GetTestAdminUser();
            var otherId = 2;
            var expectedComment = TestUserHelper.GetTestComment();
            var post = TestUserHelper.GetTestPost();
            expectedComment.Post = post;
            expectedComment.User = anotherUser;
            expectedComment.UserId = anotherUser.Id;
            post.User = expectedUser;
            var comments = new List<Comment>() { expectedComment };
            anotherUser.Comments = comments;

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);
            var repoCommentsMock = new Mock<ICommentsRepository>();
            repoCommentsMock.Setup(repo => repo.GetAll())
                            .Returns(comments);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoCommentsMock);

            // Act
            var actualComment = sut.GetUserSpecificComment(expectedUser.Id, otherId);

        }
    }
}
