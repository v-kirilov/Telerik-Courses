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
    public class UpdateUserSpecificPost_Should
    {
        [TestMethod]
        public void UpdatePost_When_ParamsAreValid()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            var expectedPost = TestUserHelper.GetTestPost();
            var expectedPostDto = TestUserHelper.GetTestPostDto();
            expectedPost.User = expectedUser;
            expectedPost.UserId = expectedUser.Id;
            expectedUser.Posts = new List<Post>() { expectedPost };
            expectedUser.Posts = new List<Post>() { expectedPost };
            var posts = new List<Post>() { expectedPost };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);

            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapPostCreate(expectedPostDto))
                           .Returns(expectedPost);
            modelMapperMock.Setup(mapper => mapper.MapPostCreate(expectedPostDto, expectedUser.Id))
                           .Returns(expectedPost);

            var servicePostsMock = new Mock<IPostsService>();
            servicePostsMock.Setup(serv => serv.Update(expectedUser.Id, expectedPostDto))
                            .Returns(expectedPostDto);


            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, servicePostsMock, modelMapperMock, repoPostsMock);

            // Act
            var actualPostDto = sut.UpdateUserSpecificPost(expectedUser.Id, expectedPost.Id, expectedPostDto, expectedUser);

            // Assert
            Assert.AreEqual(expectedPost.Id, actualPostDto.Id);
            Assert.AreEqual(expectedPost.Title, actualPostDto.Title);
            Assert.AreEqual(expectedPost.Content, actualPostDto.Content);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_UsersAreNotTheSame()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            User anotherUser = TestUserHelper.GetTestAdminUser();
            var expectedPost = TestUserHelper.GetTestPost();
            var expectedPostDto = TestUserHelper.GetTestPostDto();
            expectedPost.User = expectedUser;
            expectedPost.UserId = expectedUser.Id;
            expectedUser.Posts = new List<Post>() { expectedPost };
            expectedUser.Posts = new List<Post>() { expectedPost };
            var posts = new List<Post>() { expectedPost };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);

            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapPostCreate(expectedPostDto))
                           .Returns(expectedPost);
            modelMapperMock.Setup(mapper => mapper.MapPostCreate(expectedPostDto, expectedUser.Id))
                           .Returns(expectedPost);

            var servicePostsMock = new Mock<IPostsService>();
            servicePostsMock.Setup(serv => serv.Update(expectedUser.Id, expectedPostDto))
                            .Returns(expectedPostDto);


            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, servicePostsMock, modelMapperMock, repoPostsMock);

            // Act
            var actualPostDto = sut.UpdateUserSpecificPost(expectedUser.Id, expectedPost.Id, expectedPostDto, anotherUser);
        }
    }
}
