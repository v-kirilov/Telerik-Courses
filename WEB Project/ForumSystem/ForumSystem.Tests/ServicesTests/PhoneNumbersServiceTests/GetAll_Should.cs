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

namespace ForumSystem.Tests.ServicesTests.PhoneNumbersServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Should_GetAllPhoneNumbers()
        {
            // Arrange
            List<PhoneNumber> expectedPhoneNumbers = TestHelper.TestListPhoneNumber;
            List<PhoneNumberDto> expectedPhoneNumbersDto = TestHelper.TestListPhoneNumberDto;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.GetAll())
                .Returns(expectedPhoneNumbers);

            modelMapperMock
                .Setup(n => n.ToDto(expectedPhoneNumbers[0]))
                .Returns(expectedPhoneNumbersDto[0]);

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);

            // Act

            List<PhoneNumberDto> actualPhoneNumbersDto = numbersSystemSut.GetAll();

            // Assert

            Assert.AreEqual(expectedPhoneNumbersDto[0].Id, actualPhoneNumbersDto[0].Id);
            Assert.AreEqual(expectedPhoneNumbersDto[0].Number, actualPhoneNumbersDto[0].Number);
            Assert.AreEqual(expectedPhoneNumbersDto[0].Username, actualPhoneNumbersDto[0].Username);

        }
    }
}
