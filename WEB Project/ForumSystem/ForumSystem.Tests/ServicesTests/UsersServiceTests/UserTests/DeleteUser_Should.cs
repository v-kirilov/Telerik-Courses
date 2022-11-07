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
    public class DeleteUser_Should
    {
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_ParamsAreInvalid()
        {
            //Arrange
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                         .Returns(adminUser);

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            sut.Delete(adminUser.Id, defaultUser);
        }

        [TestMethod]
        public void DeleteUser_When_ParamsAreValid()
        {
            // Arrange
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            var users = new List<User>() { defaultUser, adminUser };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                         .Returns(adminUser);
            repoUsersMock.Setup(repo => repo.Delete(It.IsAny<int>()))
                         .Callback(() => users.Remove(defaultUser));

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act 
            sut.Delete(defaultUser.Id, adminUser);

            // Assert
            CollectionAssert.DoesNotContain(users, defaultUser);
        }
    }
}
