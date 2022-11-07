using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using ForumSystem.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserPostsTests
{
    [TestClass]
    public class GetUserSpecificPost_Should
    {
        [TestMethod]
        public void GetPost_When_ParamsAreValid()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            var expectedPost = TestUserHelper.GetTestPost();
            expectedPost.User = expectedUser;
            expectedPost.UserId = expectedUser.Id;
            expectedUser.Posts = new List<Post>() { expectedPost };
            var posts = new List<Post>() { expectedPost };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock);

            // Act
            var actualPost = sut.GetUserSpecificPost(expectedUser.Id, expectedPost.Id);

            // Assert
            Assert.AreEqual(expectedPost.Id, actualPost.Id);
            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(expectedPost.Content, actualPost.Content);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void ThrowEntityNotFoundException_When_PostIdDoesNotCorrespondToUserId()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            var expectedPost = TestUserHelper.GetTestPost();
            var posts = new List<Post>() { expectedPost };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock);

            // Act
            var actualPost = sut.GetUserSpecificPost(expectedUser.Id, expectedPost.Id);
        }
    }
}
