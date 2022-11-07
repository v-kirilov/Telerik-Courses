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
    public class DeletePhoneNumber_Should
    {
        [TestMethod]
        public void Should_DeletePhoneNumber_When_UserIsAdmin()
        {
            // Arrange
            List<PhoneNumber> phoneNumbers = TestHelper.TestListPhoneNumber;
            PhoneNumber phoneNumberToDelete = phoneNumbers[0];
            phoneNumberToDelete.User = TestHelper.TestUser;
            phoneNumberToDelete.User.Role = TestHelper.TestAdmin;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.Delete(phoneNumberToDelete.UserId, phoneNumberToDelete.Id))
                .Callback(() => phoneNumbers.Remove(phoneNumberToDelete));

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);

            // Act 
            numbersSystemSut.Delete(phoneNumberToDelete.UserId, phoneNumberToDelete.Id, phoneNumberToDelete.User);

            //Assert
            CollectionAssert.DoesNotContain(phoneNumbers, phoneNumberToDelete);
        }

        [TestMethod]
        public void ThrowsException_When_UserIsNotAdmin()
        {
            // Arrange

            List<PhoneNumber> phoneNumbers = TestHelper.TestListPhoneNumber;
            PhoneNumber phoneNumberToDelete = phoneNumbers[0];
            phoneNumberToDelete.User = TestHelper.TestUser;
            phoneNumberToDelete.User.Role = TestHelper.TestRole;

            var repositoryMock = new Mock<IPhoneNumbersRepository>();
            var modelMapperMock = new Mock<IModelMapper>();

            repositoryMock
                .Setup(n => n.Delete(phoneNumberToDelete.UserId, phoneNumberToDelete.Id))
                .Throws(new UnauthorizedOperationException("You are not authorized to delete a phone number!"));

            var numbersSystemSut = new PhoneNumbersService(repositoryMock.Object, modelMapperMock.Object);


            // Act && Assert
            Assert.ThrowsException<UnauthorizedOperationException>(() => numbersSystemSut.Delete(phoneNumberToDelete.UserId, phoneNumberToDelete.Id, phoneNumberToDelete.User));
        }
    }
}
