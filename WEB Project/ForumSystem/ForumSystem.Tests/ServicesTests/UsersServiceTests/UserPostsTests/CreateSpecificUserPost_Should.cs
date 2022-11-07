using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
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
    public class CreateSpecificUserPost_Should
    {
        [TestMethod]
        public void CreatePost_When_ParamsAreValid()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            expectedUser.Posts = new List<Post>() { TestUserHelper.GetTestPost() };
            var expectedPost = expectedUser.Posts.ToList()[0];
            var expectedPostDto = TestUserHelper.GetTestPostDto();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapPostCreate(expectedPostDto))
                           .Returns(expectedPost);
            modelMapperMock.Setup(mapper => mapper.MapPostCreate(expectedPostDto, expectedUser.Id))
                           .Returns(expectedPost);

            var servicePostsMock = new Mock<IPostsService>();
            servicePostsMock.Setup(serv => serv.Create(expectedPost))
                            .Returns(expectedPostDto);


            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, servicePostsMock, modelMapperMock);

            // Act
            var actualPostDto = sut.CreateSpecificUserPost(expectedUser.Id, expectedPostDto);

            // Assert
            Assert.AreEqual(expectedPost.Id, actualPostDto.Id);
            Assert.AreEqual(expectedPost.Title, actualPostDto.Title);
            Assert.AreEqual(expectedPost.Content, actualPostDto.Content);
        }


        [TestMethod]
        [ExpectedException(typeof(BlockedUserException))]
        public void ThrowBlockedUserException_When_UserIsBlocked()
        {
            //Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();
            expectedUser.Posts = new List<Post>() { TestUserHelper.GetTestPost() };
            expectedUser.IsBlocked = true;
            var expectedPost = expectedUser.Posts.ToList()[0];
            var expectedPostDto = TestUserHelper.GetTestPostDto();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(expectedUser.Id))
                         .Returns(expectedUser);

            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapPostCreate(expectedPostDto))
                           .Returns(expectedPost);
            modelMapperMock.Setup(mapper => mapper.MapPostCreate(expectedPostDto, expectedUser.Id))
                           .Returns(expectedPost);

            var servicePostsMock = new Mock<IPostsService>();
            servicePostsMock.Setup(serv => serv.Create(expectedPost))
                            .Returns(expectedPostDto);


            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, servicePostsMock, modelMapperMock);

            // Act
            var actualPostDto = sut.CreateSpecificUserPost(expectedUser.Id, expectedPostDto);
        }
    }
}
