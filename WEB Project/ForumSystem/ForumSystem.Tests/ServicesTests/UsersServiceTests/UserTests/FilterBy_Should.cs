using ForumSystem.Exceptions;
using ForumSystem.Models;
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
    public class FilterBy_Should
    {
        [TestMethod]
        public void ReturnCorrectUsers_When_ParamsAreValid()
        {
            // Arrange
            PaginatedList<User> expectedUsers = new PaginatedList<User>() { TestUserHelper.GetTestDefaultUser(), TestUserHelper.GetTestAdminUser() };
            var filterParams = TestUserHelper.GetTestUserQueryParameters();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.FilterBy(filterParams))
                         .Returns(expectedUsers);

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUsers = sut.FilterBy(filterParams);

            // Assert
            Assert.AreEqual(expectedUsers[0].Id, actualUsers[0].Id);
            Assert.AreEqual(expectedUsers[0].FirstName, actualUsers[0].FirstName);
            Assert.AreEqual(expectedUsers[0].Email, actualUsers[0].Email);
            Assert.AreEqual(expectedUsers[0].LastName, actualUsers[0].LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void ThrowEntityNotFoundException_When_ParamsAreInvalid()
        {
            // Arrange
            PaginatedList<User> expectedUsers = new PaginatedList<User>{};
            var filterParams = TestUserHelper.GetTestUserQueryParameters();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.FilterBy(filterParams))
                         .Returns(expectedUsers);

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUsers = sut.FilterBy(filterParams);
        }
    }
}
