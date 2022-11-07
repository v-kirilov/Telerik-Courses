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
    public class GetUserPostSpecificComment_Should
    {
        [TestMethod]
        public void GetComment_When_ParamsAreValid()
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

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock);

            // Act
            var actualComment = sut.GetUserPostSpecificComment(defaultUser.Id, post.Id, expectedComment.Id);

            // Assert
            Assert.AreEqual(expectedComment.Id, actualComment.Id);
            Assert.AreEqual(expectedComment.CommentContent, actualComment.Content);
            Assert.AreEqual(expectedComment.User.Username, actualComment.Author);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void ThrowEntityNotFoundException_When_ParamsAreInvalid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            var post = TestUserHelper.GetTestPost();
            var expectedComment = TestUserHelper.GetTestComment();
            var testCommentId = 4;

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

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostsMock);

            // Act
            var actualComment = sut.GetUserPostSpecificComment(defaultUser.Id, post.Id, testCommentId);
        }
    }
}
