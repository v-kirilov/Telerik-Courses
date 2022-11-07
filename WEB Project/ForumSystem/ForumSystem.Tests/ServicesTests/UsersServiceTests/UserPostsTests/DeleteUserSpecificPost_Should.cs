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
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserPostsTests
{
    [TestClass]
    public class DeleteUserSpecificPost_Should
    {
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_UsersAreNotTheSame()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            User anotherUser = TestUserHelper.GetTestAdminUser();
            var expectedPost = TestUserHelper.GetTestPost();
            var expectedPostDto = TestUserHelper.GetTestPostDto();
            expectedPost.User = anotherUser;
            expectedPost.UserId = anotherUser.Id;
            anotherUser.Posts = new List<Post>() { expectedPost };
            var posts = new List<Post>() { expectedPost };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);


            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock);

            // Act
            sut.DeleteUserSpecificPost(anotherUser.Id, expectedPost.Id, expectedUser);
        }

        [TestMethod]
        public void DeletePost_When_ParamsAreValid()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            User anotherUser = TestUserHelper.GetTestAdminUser();
            var expectedPost = TestUserHelper.GetTestPost();
            var expectedPostDto = TestUserHelper.GetTestPostDto();
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

            var servicePostsMock = new Mock<IPostsService>();
            servicePostsMock.Setup(service => service.DeletePost(expectedPost.Id))
                            .Callback(() => posts.Remove(expectedPost));

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock, servicePostsMock);

            // Act
            sut.DeleteUserSpecificPost(expectedUser.Id, expectedPost.Id, expectedUser);

            // Assert 
            CollectionAssert.DoesNotContain(posts, expectedPost);
        }
    }
}
