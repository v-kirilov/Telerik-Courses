using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests
{
    [TestClass]
    public class CreateUser_Should
    {
        [TestMethod]
        public void ReturnCorrectUser_When_ParamsAreValid()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultCreateUserDto();
            User expectedUser = TestUserHelper.GetTestDefaultUser();

            User anotherUser = TestUserHelper.GetTestAdminUser();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.Create(expectedUser))
                         .Returns(expectedUser);
            repoUsersMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                         .Throws(new EntityNotFoundException());
            repoUsersMock.Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                         .Throws(new EntityNotFoundException());
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserCreate(userDto))
                            .Returns(expectedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUser = (UserDto)sut.Create(userDto);

            // Assert
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.Username, actualUser.Username);
            Assert.AreEqual(expectedUser.IsBlocked, actualUser.IsBlocked);
            Assert.AreEqual(expectedUser.Role.Name, actualUser.Role);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateEntityException))]
        public void ThrowDuplicateEnitityException_When_EmailIsDuplicated()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultCreateUserDto();
            User expectedUser = TestUserHelper.GetTestDefaultUser();

            User anotherUser = TestUserHelper.GetTestAdminUser();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.Create(expectedUser))
                         .Returns(expectedUser);
            repoUsersMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                         .Returns(expectedUser);
            repoUsersMock.Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                         .Throws(new EntityNotFoundException());
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserCreate(userDto))
                            .Returns(expectedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUser = (UserDto)sut.Create(userDto);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateEntityException))]
        public void ThrowDuplicateEnitityException_When_UsernameIsDuplicated()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultCreateUserDto();
            User expectedUser = TestUserHelper.GetTestDefaultUser();

            User anotherUser = TestUserHelper.GetTestAdminUser();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.Create(expectedUser))
                         .Returns(expectedUser);
            repoUsersMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                         .Throws(new EntityNotFoundException());               
            repoUsersMock.Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                         .Returns(expectedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserCreate(userDto))
                            .Returns(expectedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUser = (UserDto)sut.Create(userDto);
        }
    }
}
