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
    public class GetById_Should
    {
        [TestMethod]
        public void Should_GetPhoneNumberById_When_PhoneNumberExists()
        {
            // Arrange

            PhoneNumber expectedPhoneNumber = TestHelper.TestPhoneNumber;
            PhoneNumberDto expectedPhoneNumberDto = TestHelper.TestPhoneNumberDto;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.GetById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expectedPhoneNumber);

            modelMapperMock
                .Setup(n => n.ToDto(expectedPhoneNumber))
                .Returns(expectedPhoneNumberDto);

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);

            // Act
            PhoneNumberDto actualPhoneNumber = numbersSystemSut.GetById(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedPhoneNumberDto.Id, actualPhoneNumber.Id);
            Assert.AreEqual(expectedPhoneNumberDto.Number, actualPhoneNumber.Number);
            Assert.AreEqual(expectedPhoneNumberDto.Username, actualPhoneNumber.Username);
        }

        [TestMethod]
        public void ThrowsException_When_UserHasNoNumber_WithThisId()
        {
            // Arrange

            PhoneNumber expectedPhoneNumber = TestHelper.TestPhoneNumber;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.GetById(expectedPhoneNumber.UserId, expectedPhoneNumber.Id))
                .Throws(new EntityNotFoundException($"The user with id: {expectedPhoneNumber.UserId} has not a phone number with id: {expectedPhoneNumber.Id}."));

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => numbersSystemSut.GetById(expectedPhoneNumber.UserId, expectedPhoneNumber.Id));
        }
    }
}
