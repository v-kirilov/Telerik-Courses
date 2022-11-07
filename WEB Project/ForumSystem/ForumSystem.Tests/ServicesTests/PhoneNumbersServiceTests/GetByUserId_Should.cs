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
using ForumSystem.Exceptions;

namespace ForumSystem.Tests.ServicesTests.PhoneNumbersServiceTests
{
    [TestClass]
    public class GetByUserId_Should
    {
        [TestMethod]
        public void Should_GetPhoneNumberByUserId_When_UserHasPhoneNumber()
        {
            // Arrange

            PhoneNumber expectedPhoneNumber = TestHelper.TestPhoneNumber;
            PhoneNumberDto expectedPhoneNumberDto = TestHelper.TestPhoneNumberDto;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.GetByUserId(It.IsAny<int>()))
                .Returns(expectedPhoneNumber);

            modelMapperMock
                .Setup(n => n.ToDto(expectedPhoneNumber))
                .Returns(expectedPhoneNumberDto);

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);

            // Act
            PhoneNumberDto actualPhoneNumber = numbersSystemSut.GetByUserId(It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedPhoneNumberDto.Id, actualPhoneNumber.Id);
            Assert.AreEqual(expectedPhoneNumberDto.Number, actualPhoneNumber.Number);
            Assert.AreEqual(expectedPhoneNumberDto.Username, actualPhoneNumber.Username);
        }

        [TestMethod]
        public void ThrowsException_When_UserHasNoNumber()
        {
            // Arrange

            PhoneNumber expectedPhoneNumber = TestHelper.TestPhoneNumber;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.GetByUserId(expectedPhoneNumber.UserId))
                .Throws(new EntityNotFoundException($"The user with id: {expectedPhoneNumber.UserId} has not a phone number."));

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => numbersSystemSut.GetByUserId(expectedPhoneNumber.UserId));
        }
    }
}
