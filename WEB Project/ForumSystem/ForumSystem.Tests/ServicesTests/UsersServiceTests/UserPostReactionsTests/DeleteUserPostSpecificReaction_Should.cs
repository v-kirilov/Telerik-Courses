using ForumSystem.Exceptions;
using ForumSystem.Models;
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
    public class DeleteUserPostSpecificReaction_Should
    {
        [TestMethod]
        public void DeleteReaction_When_ParamsAreValid()
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
            var servicePostMock = new Mock<IPostsService>();
            servicePostMock.Setup(service => service.DeletePostReaction(post.Id, user))
                            .Callback(() => expectedPostReactions.Remove(expectedPostReaction));

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostMock, servicePostMock);

            // Act
            sut.DeleteUserPostSpecificReaction(user.Id, post.Id, expectedPostReaction.Id, user);

            // Assert
            CollectionAssert.DoesNotContain(expectedPostReactions, expectedPostReaction);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedOperationException))]
        public void ThrowUnauthorizedOperationException_When_ParamsAreInvalid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var post = TestUserHelper.GetTestPost();
            var expectedPostReaction = TestUserHelper.GetTestPostReaction();

            var anotherUser = new User
            {
                Id = 7,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                Role = TestUserHelper.GetTestDefaultRole()
            };

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
            var servicePostMock = new Mock<IPostsService>();
            servicePostMock.Setup(service => service.DeletePostReaction(post.Id, user))
                            .Callback(() => expectedPostReactions.Remove(expectedPostReaction));

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostMock, servicePostMock);

            // Act
            sut.DeleteUserPostSpecificReaction(user.Id, post.Id, expectedPostReaction.Id, anotherUser);
        }
    }
}
