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
    public class GetSpecificUserComments_Should
    {
        [TestMethod]
        public void GetComment_When_ParamsAreValid()
        {
            // Arrange
            var expectedUser = TestUserHelper.GetTestDefaultUser();
            var expectedComment = TestUserHelper.GetTestComment();
            var post = TestUserHelper.GetTestPost();
            var comments = new List<Comment>() { expectedComment };
            expectedUser.Comments = comments;
            expectedComment.Post = post;
            post.User = expectedUser;

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUser = (UserCommentsDto)sut.GetSpecificUserComments(expectedUser.Id);
            var actualComment = actualUser.Comments[0];

            // Assert
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(actualComment.Id, expectedComment.Id);
            Assert.AreEqual(actualComment.Content, expectedComment.CommentContent);
        }
    }
}
