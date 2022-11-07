using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ForumSystem.Tests.ServicesTests.CommentsServiceTests
{
    [TestClass]
    public class DeleteCommentReaction_Should
    {
        [TestMethod]
        public void Should_DeleteCommentReaction_When_UserIsAuthorized()
        {
            // Arrange
            List<CommentReaction> commentReactions = TestHelper.TestListCommentReactions;
            CommentReaction reactionToDelete = commentReactions[0];
            reactionToDelete.User = TestHelper.TestUser;
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
                .Setup(r => r.DeleteCommentReaction(It.IsAny<int>(), It.IsAny<int>(), reactionToDelete.Id))
                .Callback(() => commentReactions.Remove(reactionToDelete));

            userAuthorCheckerMock
                .Setup(r => r.IsUserAuthor((int)reactionToDelete.UserId, (int)reactionToDelete.UserId))
                .Returns(true);

            var commentServiceSut = new CommentsService(repositoryMock.Object, userAuthorCheckerMock.Object,
                modelMapperMock.Object);

            // Act
            commentServiceSut.DeleteCommentReaction(It.IsAny<int>(), It.IsAny<int>(), reactionToDelete.Id, It.IsAny<int>());

            // Assert
            CollectionAssert.DoesNotContain(commentReactions, reactionToDelete);

        }

        [TestMethod]
        public void ThrowsException_When_UserIsNotAuthorized()
        {
            // Arrange
            List<CommentReaction> commentReactions = TestHelper.TestListCommentReactions;
            CommentReaction reactionToDelete = commentReactions[0];

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
            Assert.ThrowsException<UnauthorizedOperationException>(() => commentServiceSut.DeleteCommentReaction(It.IsAny<int>(), It.IsAny<int>(), reactionToDelete.Id, It.IsAny<int>()));
        }
        
        [TestMethod]
        public void ThrowsException_When_ReactionIsNotFound()
        {
            // Arrange
            CommentReaction reactionToDelete = TestHelper.TestCommentReaction;

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
                                         .DeleteCommentReaction(It.IsAny<int>(), It.IsAny<int>(),
                                         It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}
