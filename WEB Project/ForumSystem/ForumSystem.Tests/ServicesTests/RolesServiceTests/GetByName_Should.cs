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
    public class GetByName_Should
    {
        [TestMethod]
        public void GetRole_When_ParamsAreValid()
        {
            // Arrange
            var expectedRole = TestUserHelper.GetTestDefaultRole();

            var repoRoleMock = new Mock<IRolesRepository>();
            repoRoleMock.Setup(repo => repo.GetByName(expectedRole.Name))
                            .Returns(expectedRole);

            var sut = new RolesService(repoRoleMock.Object);

            // Act
            var actualRole = sut.GetByName(expectedRole.Name);

            // Assert
            Assert.AreEqual(expectedRole.Id, actualRole.Id);
            Assert.AreEqual(expectedRole.Name, actualRole.Name);
        }
    }
}
