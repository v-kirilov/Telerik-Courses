using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserPostCommentsTests
{
    [TestClass]
    public class UpdateUserPostSpecificComment_Should
    {
        [TestMethod]
        public void UpdateComment_When_ParamsAreValid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            var post = TestUserHelper.GetTestPost();
            var expectedComment = TestUserHelper.GetTestComment();

            var commentDto = new CommentDto()
            {
                CommentId = expectedComment.Id,
                CommentContent = expectedComment.CommentContent,
                Author = defaultUser.Username
            };

            post.User = defaultUser;
            post.UserId = defaultUser.Id;
            expectedComment.User = defaultUser;
            expectedComment.UserId = defaultUser.Id;
            expectedComment.Post = post;
            expectedComment.PostId = post.Id;
            post.User = defaultUser;
            post.UserId = defaultUser.Id;

            var expectedComments = new List<Comment>() { expectedComment };
            var posts = new List<Post>() { post };
            post.Comments = expectedComments;

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);
            repoPostsMock.Setup(repo => repo.GetById(post.Id))
                            .Returns(post);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.ToModel(commentDto, post.Id, defaultUser))
                            .Returns(expectedComment);
            var serviceCommentMock = new Mock<ICommentsService>();
            serviceCommentMock.Setup(service => service.Update(post.Id, expectedComment.Id, expectedComment, defaultUser.Id))
                                .Returns(commentDto);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock, modelMapperMock, serviceCommentMock);

            // Act
            var actualComment = sut.UpdateUserPostSpecificComment(defaultUser.Id, post.Id, expectedComment.Id ,defaultUser, commentDto);

            // Assert
            Assert.AreEqual(expectedComment.Id, actualComment.Id);
            Assert.AreEqual(expectedComment.CommentContent, actualComment.Content);
            Assert.AreEqual(expectedComment.User.Username, actualComment.Author);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_ParamsAreInvalid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            var anotherUser = TestUserHelper.GetTestAdminUser();
            var post = TestUserHelper.GetTestPost();
            var expectedComment = TestUserHelper.GetTestComment();

            var commentDto = new CommentDto()
            {
                CommentId = expectedComment.Id,
                CommentContent = expectedComment.CommentContent,
                Author = defaultUser.Username
            };

            post.User = defaultUser;
            post.UserId = defaultUser.Id;
            expectedComment.User = defaultUser;
            expectedComment.UserId = defaultUser.Id;
            expectedComment.Post = post;
            expectedComment.PostId = post.Id;
            post.User = defaultUser;
            post.UserId = defaultUser.Id;

            var expectedComments = new List<Comment>() { expectedComment };
            var posts = new List<Post>() { post };
            post.Comments = expectedComments;

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);
            repoPostsMock.Setup(repo => repo.GetById(post.Id))
                            .Returns(post);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.ToModel(commentDto, post.Id, defaultUser))
                            .Returns(expectedComment);
            var serviceCommentMock = new Mock<ICommentsService>();
            serviceCommentMock.Setup(service => service.Update(post.Id, expectedComment.Id, expectedComment, defaultUser.Id))
                                .Returns(commentDto);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock, modelMapperMock, serviceCommentMock);

            // Act
            var actualComment = sut.UpdateUserPostSpecificComment(defaultUser.Id, post.Id, expectedComment.Id, anotherUser, commentDto);
        }
    }
}
