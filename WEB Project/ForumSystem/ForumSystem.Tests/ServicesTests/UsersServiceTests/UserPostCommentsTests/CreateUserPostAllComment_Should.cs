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
    public class CreateUserPostAllComment_Should
    {
        [TestMethod]
        public void CreateCommentInPost_When_ParamsAreValid()
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

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(model => model.ToModel(commentDto))
                           .Returns(expectedComment);
            modelMapperMock.Setup(model => model.ToModel(commentDto, post.Id, defaultUser))
                           .Returns(expectedComment);
            var serviceCommentsMock = new Mock<ICommentsService>();
            serviceCommentsMock.Setup(service => service.Create(expectedComment))
                            .Returns(commentDto);
            

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock, modelMapperMock, serviceCommentsMock);

            // Act
            var actualComment = sut.CreateUserPostAllComment(defaultUser.Id, post.Id, commentDto, defaultUser);

            // Assert
            Assert.AreEqual(expectedComment.Id, actualComment.Id);
            Assert.AreEqual(expectedComment.CommentContent, actualComment.Content);
            Assert.AreEqual(expectedComment.User.Username, actualComment.Author);
        }

        [TestMethod]
        [ExpectedException(typeof(BlockedUserException))]
        public void ThrowBlockedUserException_When_ParamsAreInvalid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            defaultUser.IsBlocked = true;
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

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(model => model.ToModel(commentDto))
                           .Returns(expectedComment);
            modelMapperMock.Setup(model => model.ToModel(commentDto, post.Id, defaultUser))
                           .Returns(expectedComment);
            var serviceCommentsMock = new Mock<ICommentsService>();
            serviceCommentsMock.Setup(service => service.Create(expectedComment))
                            .Returns(commentDto);


            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock, modelMapperMock, serviceCommentsMock);

            // Act
            var actualComment = sut.CreateUserPostAllComment(defaultUser.Id, post.Id, commentDto, defaultUser);
        }
    }
}
