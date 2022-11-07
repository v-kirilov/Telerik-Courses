using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserPosts
{
    [TestClass]
    public class GetSpecificUserPosts_Should
    {
        [TestMethod]
        public void ReturnUserPosts_When_ParamsAreValid()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            expectedUser.Posts = new List<Post>() { TestUserHelper.GetTestPost() };
            var expectedPost = expectedUser.Posts.ToList()[0];

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUser = (UserPostsDto)sut.GetSpecificUserPosts(expectedUser.Id);
            var actualPost = actualUser.Posts[0];

            // Assert
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedPost.Id, actualPost.Id);
            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(expectedPost.Content, actualPost.Content);
        }
    }
}
