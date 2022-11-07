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
    public class UpdateComment_Should
    {
        [TestMethod]
        public void Should_UpdateComment_When_UserIsAuthorized()
        {
            // Arrange
            Comment commentBeforeUpdate = TestHelper.TestComment;
            Comment expectedComment = TestHelper.TestUpdatedComment;
            CommentDto expectedCommentDto = TestHelper.TestUpdatedCommentDto;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentById(commentBeforeUpdate.PostId, commentBeforeUpdate.Id))
                .Returns(commentBeforeUpdate);
            repositoryMock
                .Setup(r => r.Update(commentBeforeUpdate.PostId, commentBeforeUpdate.Id, expectedComment))
                .Returns(expectedComment);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComment))
                .Returns(expectedCommentDto);

            userAuthorCheckerMock
                .Setup(r => r.IsUserAuthor((int)commentBeforeUpdate.UserId, (int)expectedComment.UserId))
                .Returns(true);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            CommentDto updatedCommentDto = commentServiceSut.Update(commentBeforeUpdate.PostId, commentBeforeUpdate.Id, expectedComment, (int)commentBeforeUpdate.UserId);

            // Assert
            Assert.AreEqual(expectedCommentDto.CommentId, updatedCommentDto.CommentId);
            Assert.AreEqual(expectedCommentDto.CommentContent, updatedCommentDto.CommentContent);
            Assert.AreEqual(expectedCommentDto.Author, updatedCommentDto.Author);
        }

        //[TestMethod]
        public void ThrowException_When_UserIsNotAuthorized()
        {
            // Arrange
            Comment comment = TestHelper.TestComment;
            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            userAuthorCheckerMock
                .Setup(r => r.IsUserAuthor(It.IsAny<int>(), comment.Id))
                .Throws(new UnauthorizedOperationException("You are not authorized."));

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<UnauthorizedOperationException>(() => commentServiceSut.Update(It.IsAny<int>(), It.IsAny<int>(), comment, It.IsAny<int>()));
        }
    }
}
