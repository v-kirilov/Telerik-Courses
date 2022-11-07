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
    public class GetCommentByCommentId_Should
    {
        [TestMethod]
        public void Should_GetCommentByCommentId_When_IdIsValid()
        {
            // Arrange
            Comment expectedComment = TestHelper.TestComment;
            CommentDto expectedCommentDto = TestHelper.TestCommentDto;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentById(expectedComment.Id))
                .Returns(expectedComment);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComment))
                .Returns(expectedCommentDto);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            CommentDto actualCommentDto = commentServiceSut.GetById(expectedComment.Id);

            // Assert
            Assert.AreEqual(expectedCommentDto.CommentId, actualCommentDto.CommentId);
            Assert.AreEqual(expectedCommentDto.CommentContent, actualCommentDto.CommentContent);
            Assert.AreEqual(expectedCommentDto.Author, actualCommentDto.Author);

        }

        [TestMethod]
        public void ThrowException_When_CommentNotFound()
        {
            // Arrange
            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(c => c.GetCommentById(It.IsAny<int>()))
                .Throws(new EntityNotFoundException());

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => commentServiceSut.GetById(It.IsAny<int>()));
        }
    }
}
