﻿using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
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
    public class CreateComment_Should
    {
        [TestMethod]
        public void Should_CreateComment_When_InputIsValid()
        {
            // Arrange
            Comment expectedComment = TestHelper.TestComment;
            CommentDto expectedCommentDto = TestHelper.TestCommentDto;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.Create(expectedComment))
                .Returns(expectedComment);

            modelMapperMock
                .Setup(c => c.ToDto(expectedComment))
                .Returns(expectedCommentDto);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            CommentDto actualCommentDto = commentServiceSut.Create(expectedComment);

            // Assert
            Assert.AreEqual(expectedCommentDto.CommentId, actualCommentDto.CommentId);
            Assert.AreEqual(expectedCommentDto.CommentContent, actualCommentDto.CommentContent);
            Assert.AreEqual(expectedCommentDto.Author, actualCommentDto.Author);
        }
    }
}