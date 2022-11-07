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
    public class CreateCommentReaction_Should
    {
        [TestMethod]
        public void Should_CreateCommentReaction_When_UserHasNotReactedYet()
        {
            // Arrange
            CommentReaction expectedReaction = TestHelper.TestCommentReaction;
            CommentReactionDto expectedReactionDto = TestHelper.TestCommentReacitonDto;
            Comment comment = TestHelper.TestComment;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(comment);

            repositoryMock
                .Setup(r => r.CreateCommentReaction(expectedReaction))
                .Returns(expectedReaction);

            modelMapperMock
                .Setup(c => c.CommentReactionMap(expectedReaction))
                .Returns(expectedReactionDto);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            CommentReactionDto createdReaction = commentServiceSut.CreateCommentReaction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), expectedReaction);

            // Assert
            Assert.AreEqual(expectedReactionDto.Id, createdReaction.Id);
            Assert.AreEqual(expectedReactionDto.Author, createdReaction.Author);
            Assert.AreEqual(expectedReactionDto.Reaction, createdReaction.Reaction);
        }

        //[TestMethod]
        public void ThrowsException_When_UserHasAlreadyReacted()
        {
            // Arrange
            CommentReaction expectedReaction = TestHelper.TestCommentReaction;
            CommentReactionDto expectedReactionDto = TestHelper.TestCommentReacitonDto;
            Comment comment = TestHelper.TestComment;
            comment.CommentReactions.Add(expectedReaction);

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(comment);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
               modelMapperMock.Object);

            // Act && Assert
            Assert.ThrowsException<DuplicateEntityException>(() => commentServiceSut.CreateCommentReaction(comment.PostId, comment.Id,(int)comment.UserId, expectedReaction));

        }

        [TestMethod]
        public void Should_UpdateCommentReaction_When_UserHasAlreadyReactedOpposite()
        {
            // Arrange
            CommentReaction expectedReaction = TestHelper.TestCommentReaction;
            CommentReactionDto expectedReactionDto = TestHelper.TestCommentReacitonDto;
            Comment comment = TestHelper.TestComment;
            CommentReaction previousReaction = TestHelper.TestUpdatedCommentReaction;
            comment.CommentReactions.Add(previousReaction);

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentById(comment.PostId, comment.Id))
                .Returns(comment);

            repositoryMock
                .Setup(r => r.UpdateCommentReaction(comment.PostId, comment.Id,expectedReaction.Id, previousReaction))
                .Returns(expectedReaction);

            modelMapperMock
                .Setup(c => c.CommentReactionMap(expectedReaction))
                .Returns(expectedReactionDto);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            CommentReactionDto createdReaction = commentServiceSut.CreateCommentReaction(comment.PostId, comment.Id, expectedReaction.Id, previousReaction);

            // Assert
            Assert.AreEqual(expectedReactionDto.Id, createdReaction.Id);
            Assert.AreEqual(expectedReactionDto.Author, createdReaction.Author);
            Assert.AreEqual(expectedReactionDto.Reaction, createdReaction.Reaction);
        }
		[TestMethod]
        public void Should_DeleteCommentReaction_When_UserHasAlreadyReactedTheSame()
        {
            // Arrange
            CommentReaction expectedReaction = TestHelper.TestCommentReaction;
            CommentReactionDto expectedReactionDto = TestHelper.TestCommentReacitonDto;
            Comment comment = TestHelper.TestComment;
            comment.CommentReactions.Add(expectedReaction);

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetCommentById(comment.PostId, comment.Id))
                .Returns(comment);

            repositoryMock
                .Setup(r => r.UpdateCommentReaction(comment.PostId, comment.Id, expectedReaction.Id, expectedReaction))
                .Callback(() => comment.CommentReactions.Remove(expectedReaction));;

            modelMapperMock
                .Setup(c => c.CommentReactionMap(expectedReaction))
                .Returns(expectedReactionDto);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            CommentReactionDto createdReaction = commentServiceSut.CreateCommentReaction(comment.PostId, comment.Id, expectedReaction.Id, expectedReaction);

            // Assert
            CollectionAssert.DoesNotContain(comment.CommentReactions, expectedReaction);
        }
    }
}
