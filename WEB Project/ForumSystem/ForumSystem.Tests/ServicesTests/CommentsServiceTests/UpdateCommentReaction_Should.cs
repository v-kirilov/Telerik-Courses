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
using System.Linq;
using ForumSystem.Exceptions;

namespace ForumSystem.Tests.ServicesTests.CommentsServiceTests
{
    [TestClass]
    public class UpdateCommentReaction_Should
    {
        [TestMethod]
        public void Should_UpdateCommentReaction_When_UserIsAuthorized()
        {
            // Arrange
            CommentReaction reactionBeforeUpdate = TestHelper.TestCommentReaction;
            CommentReaction expectedReaction = TestHelper.TestUpdatedCommentReaction;
            CommentReactionDto expectedReactionDto = TestHelper.TestUpdatedCommentReacitonDto;
            List<CommentReaction> commentReactions = TestHelper.TestListCommentReactions;
            Comment comment = TestHelper.TestComment;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetAllCommentReactions())
                .Returns(commentReactions);

            repositoryMock
                .Setup(r => r.GetCommentById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(comment);

            repositoryMock
                .Setup(r => r.UpdateCommentReaction(It.IsAny<int>(), It.IsAny<int>(), reactionBeforeUpdate.Id, expectedReaction))
                .Returns(expectedReaction);

            modelMapperMock
                .Setup(c => c.CommentReactionMap(expectedReaction))
                .Returns(expectedReactionDto);

            userAuthorCheckerMock
                .Setup(r => r.IsUserAuthor((int)reactionBeforeUpdate.UserId, (int)expectedReaction.UserId))
                .Returns(true);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            CommentReactionDto updatedReaction = commentServiceSut.UpdateCommentReaction(It.IsAny<int>(), It.IsAny<int>(), reactionBeforeUpdate.Id, expectedReaction, (int)reactionBeforeUpdate.UserId);

            // Assert
            Assert.AreEqual(expectedReactionDto.Id, updatedReaction.Id);
            Assert.AreEqual(expectedReactionDto.Author, updatedReaction.Author);
            Assert.AreEqual(expectedReactionDto.Reaction, updatedReaction.Reaction);
        }

        [TestMethod]
        public void ThrowsException_When_UserIsNotAuthorized()
        {
            // Arrange
            CommentReaction reactionBeforeUpdate = TestHelper.TestCommentReaction;
            CommentReaction expectedReaction = TestHelper.TestUpdatedCommentReaction;
            List<CommentReaction> commentReactions = TestHelper.TestListCommentReactions;
           
            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetAllCommentReactions())
                .Returns(commentReactions);

            userAuthorCheckerMock
                .Setup(r => r.IsUserAuthor(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new UnauthorizedOperationException("You are not authorized."));

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
               modelMapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<UnauthorizedOperationException>(() => commentServiceSut.UpdateCommentReaction(It.IsAny<int>(), It.IsAny<int>(), reactionBeforeUpdate.Id, expectedReaction, (int)reactionBeforeUpdate.UserId));
        }

        [TestMethod]
        public void ThrowsException_When_ReactionIsNotFound()
        {
            // Arrange
            CommentReaction expectedReaction = TestHelper.TestUpdatedCommentReaction;

            var repositoryMock = new Mock<ICommentsRepository>();
            var modelMapperMock = new Mock<IModelMapper>();
            var userAuthorCheckerMock = new Mock<IUserAuthorChecker>();

            repositoryMock
                .Setup(r => r.GetAllCommentReactions())
                .Throws(new EntityNotFoundException("The reaction is not found."));

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
               modelMapperMock.Object);

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => commentServiceSut
                                         .UpdateCommentReaction(It.IsAny<int>(), It.IsAny<int>(),
                                         It.IsAny<int>(), expectedReaction, It.IsAny<int>()));
        }
    }
}
