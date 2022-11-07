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
using ForumSystem.Models.QueryParameters;

namespace ForumSystem.Tests.ServicesTests.CommentsServiceTests
{
    [TestClass]
    public class GetComments_Should
    {
        [TestMethod]
        public void Should_GetComments()
        {
            // Arrange
            List<Comment> expectedComments = TestHelper.TestListComments;
            List<CommentDto> expectedCommentDtos = TestHelper.TestListCommentDtos;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetAll())
                .Returns(expectedComments);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComments[0]))
                .Returns(expectedCommentDtos[0]);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComments[1]))
                .Returns(expectedCommentDtos[1]);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComments[2]))
                .Returns(expectedCommentDtos[2]);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            List<CommentDto> actualCommentDtos = commentServiceSut.GetAll();

            // Assert
            Assert.AreEqual(expectedCommentDtos[0], actualCommentDtos[0]);
            Assert.AreEqual(expectedCommentDtos[1], actualCommentDtos[1]);
            Assert.AreEqual(expectedCommentDtos[2], actualCommentDtos[2]);
        }

        [TestMethod]
        public void Should_GetFilteredComments()
        {
            // Arrange
            List<Comment> expectedComments = TestHelper.TestListComments;
            List<CommentDto> expectedCommentDtos = TestHelper.TestListCommentDtos;
            CommentQueryParameters commentQueryParameters = new CommentQueryParameters();

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.FilterBy(commentQueryParameters))
                .Returns(expectedComments);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComments[0]))
                .Returns(expectedCommentDtos[0]);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComments[1]))
                .Returns(expectedCommentDtos[1]);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComments[2]))
                .Returns(expectedCommentDtos[2]);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            List<CommentDto> actualCommentDtos = commentServiceSut.FilterBy(commentQueryParameters);

            // Assert
            Assert.AreEqual(expectedCommentDtos[0], actualCommentDtos[0]);
            Assert.AreEqual(expectedCommentDtos[1], actualCommentDtos[1]);
            Assert.AreEqual(expectedCommentDtos[2], actualCommentDtos[2]);
        }

    }
}
