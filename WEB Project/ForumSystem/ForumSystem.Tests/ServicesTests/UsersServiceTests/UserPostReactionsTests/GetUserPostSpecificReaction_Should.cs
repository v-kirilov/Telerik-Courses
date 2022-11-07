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

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserPostReactionsTests
{
    [TestClass]
    public class GetUserPostSpecificReaction_Should
    {
        [TestMethod]
        public void GetReaction_When_ParamsAreValid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var post = TestUserHelper.GetTestPost();
            var expectedPostReaction = TestUserHelper.GetTestPostReaction();

            post.UserId = user.Id;
            post.User = user;
            expectedPostReaction.PostId = post.Id;
            expectedPostReaction.Post = post;
            expectedPostReaction.User = user;
            expectedPostReaction.UserId = user.Id;

            var expectedPostReactions = new List<PostReaction>() { expectedPostReaction };
            var posts = new List<Post>() { post };
            post.Reactions = expectedPostReactions;

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(user.Id))
                            .Returns(user);
            var repoPostMock = new Mock<IPostsRepository>();
            repoPostMock.Setup(repo => repo.GetAll())
                            .Returns(posts);
            repoPostMock.Setup(repo => repo.GetById(post.Id))
                            .Returns(post);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostMock);

            // Act
            var actualPostReaction = sut.GetUserPostSpecificReaction(user.Id, post.Id, expectedPostReaction.Id);

            // Assert
            Assert.AreEqual(expectedPostReaction.Id, actualPostReaction.Id);
            Assert.AreEqual(expectedPostReaction.Reaction.ToString(), actualPostReaction.Reaction);
            Assert.AreEqual(expectedPostReaction.User.Username, actualPostReaction.Author);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void ThrowEntityNotFoundException_When_ParamsAreInvalid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var post = TestUserHelper.GetTestPost();
            var expectedPostReaction = TestUserHelper.GetTestPostReaction();

            var anotherPost = new Post
            {
                Id = 5,
                Title = "TestTitle",
                Content = "TestContent"
            };

            post.UserId = user.Id;
            post.User = user;
            expectedPostReaction.PostId = anotherPost.Id;
            expectedPostReaction.Post = anotherPost;
            expectedPostReaction.User = user;
            expectedPostReaction.UserId = user.Id;

            var expectedPostReactions = new List<PostReaction>() { expectedPostReaction };
            var posts = new List<Post>() { post };
            anotherPost.Reactions = expectedPostReactions;

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(user.Id))
                            .Returns(user);
            var repoPostMock = new Mock<IPostsRepository>();
            repoPostMock.Setup(repo => repo.GetAll())
                            .Returns(posts);
            repoPostMock.Setup(repo => repo.GetById(post.Id))
                            .Returns(post);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostMock);

            // Act
            var actualPostReaction = sut.GetUserPostSpecificReaction(user.Id, post.Id, expectedPostReaction.Id);
        }
    }
}
