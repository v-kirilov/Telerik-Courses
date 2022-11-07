using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models.DTO;
using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.PhoneNumbersServiceTests
{
    [TestClass]
    public class UpdatePhoneNumber_Should
    {
        [TestMethod]
        public void Should_UpdatePhoneNumber_When_UserIsAdmin()
        {
            // Arrange
            PhoneNumber phoneNumberBeforeUpdate = TestHelper.TestPhoneNumber;
            PhoneNumber expectedPhoneNumber = TestHelper.TestUpdatedPhoneNumber;
            expectedPhoneNumber.User = TestHelper.TestUser;
            expectedPhoneNumber.User.Role = TestHelper.TestAdmin;
            PhoneNumberDto expectedPhoneNumberDto = TestHelper.TestUpdatedPhoneNumberDto;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.Update(phoneNumberBeforeUpdate.UserId, phoneNumberBeforeUpdate.Id, expectedPhoneNumber))
                .Returns(expectedPhoneNumber);

            modelMapperMock
                .Setup(n => n.ToDto(expectedPhoneNumber))
                .Returns(expectedPhoneNumberDto);

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);

            // Act

            PhoneNumberDto actualPhoneNumber = numbersSystemSut.Update(phoneNumberBeforeUpdate.UserId, phoneNumberBeforeUpdate.Id, expectedPhoneNumber, expectedPhoneNumber.User);

            // Assert
            Assert.AreEqual(expectedPhoneNumberDto.Id, actualPhoneNumber.Id);
            Assert.AreEqual(expectedPhoneNumberDto.Number, actualPhoneNumber.Number);
            Assert.AreEqual(expectedPhoneNumberDto.Username, actualPhoneNumber.Username);
        }

        [TestMethod]
        public void ThrowsException_When_UserIsNotAdmin()
        {
            // Arrange

            PhoneNumber expectedPhoneNumber = TestHelper.TestPhoneNumber;
            expectedPhoneNumber.User = TestHelper.TestUser;
            expectedPhoneNumber.User.Role = TestHelper.TestRole;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.Update(expectedPhoneNumber.UserId, expectedPhoneNumber.Id, expectedPhoneNumber))
                .Throws(new UnauthorizedOperationException("You are not authorized to update a phone number!"));

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);


            // Act && Assert
            Assert.ThrowsException<UnauthorizedOperationException>(() => numbersSystemSut.Update(expectedPhoneNumber.UserId, expectedPhoneNumber.Id, expectedPhoneNumber, expectedPhoneNumber.User));
        }
    }
}
