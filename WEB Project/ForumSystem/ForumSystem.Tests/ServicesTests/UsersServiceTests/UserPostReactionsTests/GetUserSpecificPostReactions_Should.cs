using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserPostReactionsTests
{
    [TestClass]
    public class GetUserSpecificPostReactions_Should
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

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostMock);

            // Act
            var actualPostReactions = sut.GetUserSpecificPostReactions(user.Id, post.Id);

            // Assert
            Assert.AreEqual(expectedPostReactions[0].Id, actualPostReactions[0].Id);
            Assert.AreEqual(expectedPostReactions[0].Reaction.ToString(), actualPostReactions[0].Reaction);
            Assert.AreEqual(expectedPostReactions[0].User.Username, actualPostReactions[0].Author);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void ThrowEntityNotFoundException_When_ParamsAreInvalid()
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

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(user.Id))
                            .Returns(user);
            var repoPostMock = new Mock<IPostsRepository>();
            repoPostMock.Setup(repo => repo.GetAll())
                            .Returns(posts);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostMock);

            // Act
            var actualPostReactions = sut.GetUserSpecificPostReactions(user.Id, post.Id);
        }
    }
}
