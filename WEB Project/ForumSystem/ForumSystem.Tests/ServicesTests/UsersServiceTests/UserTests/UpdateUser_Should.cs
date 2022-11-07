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
    public class UpdateUser_Should
    {
        [TestMethod]
        public void UpdateUser_When_ParamsAreValid()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                Id = defaultUser.Id,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                Role = TestUserHelper.GetTestDefaultRole(),
                IsBlocked = true
            };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateFirstName(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdateLastName(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdatePhoneNumber(It.IsAny<int>(), It.IsAny<PhoneNumber>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                        .Throws(new EntityNotFoundException());
            repoUsersMock.Setup(repo => repo.UpdateEmail(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdateRole(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdateIsBlocked(It.IsAny<int>(), It.IsAny<bool>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, adminUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, adminUser);

            // Assert
            Assert.AreEqual(expectedUpdatedUser.Id, actualUpdatedUser.Id);
            Assert.AreEqual(expectedUpdatedUser.FirstName, actualUpdatedUser.FirstName);
            Assert.AreEqual(expectedUpdatedUser.LastName, actualUpdatedUser.LastName);
            Assert.AreEqual(expectedUpdatedUser.Email, actualUpdatedUser.Email);
            Assert.AreEqual(expectedUpdatedUser.Username, actualUpdatedUser.Username);
            Assert.AreEqual(expectedUpdatedUser.IsBlocked, actualUpdatedUser.IsBlocked);
            Assert.AreEqual(expectedUpdatedUser.Role.Name, actualUpdatedUser.Role);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_UserNotAdminOrUserHimself()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                Id = defaultUser.Id,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                Role = TestUserHelper.GetTestDefaultRole(),
                IsBlocked = true
            };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);

            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock);

            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, expectedUpdatedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_TryingToUpdateUsername()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                Id = defaultUser.Id,
                Username = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                Role = TestUserHelper.GetTestDefaultRole(),
                IsBlocked = true
            };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, adminUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);


            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, adminUser);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateEntityException))]
        public void ThrowDuplicateEntityException_When_UpdateDuplicateEmail()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                Id = defaultUser.Id,
                FirstName = "Test",
                LastName = "Test",
                Email = "test@abv.bg",
                Role = TestUserHelper.GetTestDefaultRole(),
                IsBlocked = true
            };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateFirstName(It.IsAny<int>(), It.IsAny<string>()))
                        .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateLastName(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdatePhoneNumber(It.IsAny<int>(), It.IsAny<PhoneNumber>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, adminUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);


            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, adminUser);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_UpdatePasswordByOtherUser()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                Password = "Test",
                IsBlocked = true
            };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateFirstName(It.IsAny<int>(), It.IsAny<string>()))
                        .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdateLastName(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                         .Throws(new EntityNotFoundException());
            repoUsersMock.Setup(repo => repo.UpdateEmail(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdateRole(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdateIsBlocked(It.IsAny<int>(), It.IsAny<bool>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, adminUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);


            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, adminUser);
        }

        [TestMethod]
        public void UpdatePassword_When_ParamsAreValid()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                Password = "Test",
                IsBlocked = true,
                Role = TestUserHelper.GetTestDefaultRole()
            };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdatePhoneNumber(It.IsAny<int>(), It.IsAny<PhoneNumber>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdatePassword(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, defaultUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, defaultUser);

            // Assert
            Assert.AreEqual(expectedUpdatedUser.Id, actualUpdatedUser.Id);
            Assert.AreEqual(expectedUpdatedUser.FirstName, actualUpdatedUser.FirstName);
            Assert.AreEqual(expectedUpdatedUser.LastName, actualUpdatedUser.LastName);
            Assert.AreEqual(expectedUpdatedUser.Email, actualUpdatedUser.Email);
            Assert.AreEqual(expectedUpdatedUser.Username, actualUpdatedUser.Username);
            Assert.AreEqual(expectedUpdatedUser.IsBlocked, actualUpdatedUser.IsBlocked);
            Assert.AreEqual(expectedUpdatedUser.Role.Name, actualUpdatedUser.Role);
        }

        [TestMethod]
        public void UpdatePhoneNumber_When_PhoneNumberNotNull()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                Password = "Test",
                IsBlocked = true,
                Role = TestUserHelper.GetTestDefaultRole(),
                PhoneNumber = TestUserHelper.GetTestPhoneNumber()
            };
            PhoneNumber phoneNumber = TestUserHelper.GetTestPhoneNumber();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdatePassword(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            repoUsersMock.Setup(repo => repo.UpdatePhoneNumber(It.IsAny<int>(), It.IsAny<PhoneNumber>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, defaultUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, defaultUser);

            // Assert
            Assert.AreEqual(expectedUpdatedUser.Id, actualUpdatedUser.Id);
            Assert.AreEqual(expectedUpdatedUser.FirstName, actualUpdatedUser.FirstName);
            Assert.AreEqual(expectedUpdatedUser.LastName, actualUpdatedUser.LastName);
            Assert.AreEqual(expectedUpdatedUser.Email, actualUpdatedUser.Email);
            Assert.AreEqual(expectedUpdatedUser.Username, actualUpdatedUser.Username);
            Assert.AreEqual(expectedUpdatedUser.IsBlocked, actualUpdatedUser.IsBlocked);
            Assert.AreEqual(expectedUpdatedUser.Role.Name, actualUpdatedUser.Role);
        }

        [TestMethod]
        public void UpdateRole_When_ParamsAreValid()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                IsBlocked = false,
                Role = TestUserHelper.GetTestAdminRole()
            };
            PhoneNumber phoneNumber = TestUserHelper.GetTestPhoneNumber();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdatePhoneNumber(It.IsAny<int>(), It.IsAny<PhoneNumber>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateRole(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, adminUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, adminUser);

            // Assert
            Assert.AreEqual(expectedUpdatedUser.Id, actualUpdatedUser.Id);
            Assert.AreEqual(expectedUpdatedUser.FirstName, actualUpdatedUser.FirstName);
            Assert.AreEqual(expectedUpdatedUser.LastName, actualUpdatedUser.LastName);
            Assert.AreEqual(expectedUpdatedUser.Email, actualUpdatedUser.Email);
            Assert.AreEqual(expectedUpdatedUser.Username, actualUpdatedUser.Username);
            Assert.AreEqual(expectedUpdatedUser.IsBlocked, actualUpdatedUser.IsBlocked);
            Assert.AreEqual(expectedUpdatedUser.Role.Name, actualUpdatedUser.Role);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_UpdateRoleByDefaultUser()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                IsBlocked = false,
                Role = TestUserHelper.GetTestAdminRole()
            };
            PhoneNumber phoneNumber = TestUserHelper.GetTestPhoneNumber();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdatePhoneNumber(It.IsAny<int>(), It.IsAny<PhoneNumber>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateRole(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, defaultUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, defaultUser);
        }

        [TestMethod]
        public void UpdateIsBlocked_When_ParamsAreValid()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                IsBlocked = true,
                Role = TestUserHelper.GetTestDefaultRole()
            };
            PhoneNumber phoneNumber = TestUserHelper.GetTestPhoneNumber();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdatePhoneNumber(It.IsAny<int>(), It.IsAny<PhoneNumber>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateRole(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateIsBlocked(It.IsAny<int>(), It.IsAny<bool>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, adminUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, adminUser);

            // Assert
            Assert.AreEqual(expectedUpdatedUser.Id, actualUpdatedUser.Id);
            Assert.AreEqual(expectedUpdatedUser.FirstName, actualUpdatedUser.FirstName);
            Assert.AreEqual(expectedUpdatedUser.LastName, actualUpdatedUser.LastName);
            Assert.AreEqual(expectedUpdatedUser.Email, actualUpdatedUser.Email);
            Assert.AreEqual(expectedUpdatedUser.Username, actualUpdatedUser.Username);
            Assert.AreEqual(expectedUpdatedUser.IsBlocked, actualUpdatedUser.IsBlocked);
            Assert.AreEqual(expectedUpdatedUser.Role.Name, actualUpdatedUser.Role);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_UpdateIsBlockedByDefaultUser()
        {
            //Arrange
            var userDto = TestUserHelper.GetTestDefaultUpdateUserDto();
            User defaultUser = TestUserHelper.GetTestDefaultUser();
            User adminUser = TestUserHelper.GetTestAdminUser();
            User expectedUpdatedUser = new User
            {
                IsBlocked = true,
                Role = TestUserHelper.GetTestDefaultRole()
            };
            PhoneNumber phoneNumber = TestUserHelper.GetTestPhoneNumber();

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdatePhoneNumber(It.IsAny<int>(), It.IsAny<PhoneNumber>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateRole(It.IsAny<int>(), It.IsAny<string>()))
                         .Returns(defaultUser);
            repoUsersMock.Setup(repo => repo.UpdateIsBlocked(It.IsAny<int>(), It.IsAny<bool>()))
                         .Returns(expectedUpdatedUser);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapUserUpdate(defaultUser.Id, userDto, defaultUser))
                            .Returns(expectedUpdatedUser);
            UsersService sut = TestUserHelper.InitializeUsersService(repoUsersMock, modelMapperMock);

            // Act
            var actualUpdatedUser = (UserDto)sut.Update(defaultUser.Id, userDto, defaultUser);
        }
    }
}
