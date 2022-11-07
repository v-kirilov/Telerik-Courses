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

namespace ForumSystem.Tests.ServicesTests.CommentsServiceTests
{
    [TestClass]
    public class DeleteComment_Should
    {
        [TestMethod]
        public void Should_DeleteComment_When_UserIsAuthorized()
        {
            // Arrange
            List<Comment> comments = TestHelper.TestListComments;
            Comment commentToDelete = comments[0];
            commentToDelete.User = TestHelper.TestUser;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(commentToDelete);
            repositoryMock
                .Setup(r => r.Delete(It.IsAny<int>(), It.IsAny<int>()))
                .Callback(() => comments.Remove(commentToDelete));

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            commentServiceSut.Delete(It.IsAny<int>(), It.IsAny<int>(), commentToDelete.User);

            // Assert
            CollectionAssert.DoesNotContain(comments, commentToDelete);
        }

        [TestMethod]
        public void ThrowException_When_UserIsNotAuthorized()
        {
            // Arrange
            Comment commentToDelete = TestHelper.TestListComments[0];
            commentToDelete.User = TestHelper.TestUser;
            User user = TestHelper.TestUser2;
            user.Role = TestHelper.TestRole;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(commentToDelete);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<UnauthorizedOperationException>(() => commentServiceSut.Delete(commentToDelete.PostId, commentToDelete.Id, user));
        }
    }
}
