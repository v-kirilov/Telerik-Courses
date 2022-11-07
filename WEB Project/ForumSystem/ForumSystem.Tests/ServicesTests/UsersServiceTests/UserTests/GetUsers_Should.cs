using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.DTO.Contracts;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using ForumSystem.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests
{
    [TestClass]
    public class GetUsers_Should
    {
        [TestMethod]
        public void RetrunCorrectDefaultUser_When_ParamsAreValid()
        {
            // Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                         .Returns(expectedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUser = (UserDto)sut.GetById(expectedUser.Id);

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
        public void RetrunCorrectAdminUser_When_ParamsAreValid()
        {
            // Arrange
            User expectedUser = TestUserHelper.GetTestAdminUser();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                         .Returns(expectedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUser = (UserDto)sut.GetById(expectedUser.Id);

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
        public void RetrunCorrectDefaultUser_When_GetByUsernameIsCalled()
        {
            // Arrange
            User expectedUser = TestUserHelper.GetTestDefaultUser();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                         .Returns(expectedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUser = sut.GetByUsername(expectedUser.Username);

            // Assert
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.Username, actualUser.Username);
            Assert.AreEqual(expectedUser.IsBlocked, actualUser.IsBlocked);
            Assert.AreEqual(expectedUser.Role, actualUser.Role);
        }

        [TestMethod]
        public void RetrunCorrectListOfUsers_When_GetAllIsCalled()
        {
            // Arrange
            List<User> expectedUsers = new List<User>() { TestUserHelper.GetTestDefaultUser(), TestUserHelper.GetTestAdminUser() };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetAll())
                         .Returns(expectedUsers);

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUsers = sut.GetAll();

            // Assert
            CollectionAssert.AreEqual(expectedUsers, actualUsers);
        }


    }
}
