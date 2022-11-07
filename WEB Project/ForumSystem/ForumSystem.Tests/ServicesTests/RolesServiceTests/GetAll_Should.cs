using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.RolesServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void GetAllRoles_When_ParamsAreValid()
        {
            // Arrange
            var expectedRoles = new List<Role>() { TestUserHelper.GetTestDefaultRole() };

            var repoRoleMock = new Mock<IRolesRepository>();
            repoRoleMock.Setup(repo => repo.GetAll())
                            .Returns(expectedRoles);

            var sut = new RolesService(repoRoleMock.Object);

            // Act
            var actualRole = sut.GetAll();

            // Assert
            Assert.AreEqual(expectedRoles[0].Id, actualRole[0].Id);
            Assert.AreEqual(expectedRoles[0].Name, actualRole[0].Name);
        }
    }
}
