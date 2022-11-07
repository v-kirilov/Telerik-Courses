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

namespace ForumSystem.Tests.ServicesTests.CommentsServiceTests
{
	[TestClass]
	public class GetCommentsByUserId_Should
	{
        [TestMethod]
        public void Should_GetCommentsByUserId_When_IdIsValid()
        {
            // Arrange
            List<Comment> expectedComments = TestHelper.TestListComments.FindAll(c => c.UserId == 1);
            List<CommentDto> expectedCommentDtos = TestHelper.TestListCommentDtos.FindAll(c => c.CommentId == 1 || c.CommentId == 4);

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentsByUser((int)expectedComments[0].UserId))
                .Returns(expectedComments);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComments[0]))
				.Returns(expectedCommentDtos[0]);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComments[1]))
                .Returns(expectedCommentDtos[1]);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            List<CommentDto> actualCommentDtos = commentServiceSut.GetCommentsByUser((int)expectedComments[0].UserId);

            // Assert
            Assert.AreEqual(expectedCommentDtos[0], actualCommentDtos[0]);
            Assert.AreEqual(expectedCommentDtos[1], actualCommentDtos[1]);

        }

        [TestMethod]
        public void ThrowException_When_CommentNotFound()
        {
            // Arrange
            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(c => c.GetCommentsByUser(It.IsAny<int>()))
                .Throws(new EntityNotFoundException());

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => commentServiceSut.GetCommentsByUser(It.IsAny<int>()));
        }
    }
}
