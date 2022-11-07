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
    public class CreateUserSpecificPostReaction_Should
    {
        [TestMethod]
        public void CreateReaction_When_ParamsAreValid()
        {
            // Arrange
            var user = TestUserHelper.GetTestDefaultUser();
            var post = TestUserHelper.GetTestPost();
            var expectedPostReaction = TestUserHelper.GetTestPostReaction();

            var postReactionDto = new PostReactionDto
            {
                Id = expectedPostReaction.Id,
                Reaction = expectedPostReaction.Reaction.ToString()
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
            var modelMapperMock = new Mock<IModelMapper>();
            modelMapperMock.Setup(mapper => mapper.MapPostReactionCreate(post.Id, user, postReactionDto))
                            .Returns(expectedPostReaction);
            var servicePostMock = new Mock<IPostsService>();
            servicePostMock.Setup(service => service.CreatePostReaction(post.Id, expectedPostReaction))
                            .Returns(postReactionDto);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoPostMock, servicePostMock, modelMapperMock);

            // Act
            var actualPostReaction = sut.CreateUserSpecificPostReaction(user.Id, post.Id, postReactionDto, user);

            // Assert
            Assert.AreEqual(expectedPostReaction.Id, actualPostReaction.Id);
            Assert.AreEqual(expectedPostReaction.Reaction.ToString(), actualPostReaction.Reaction);
            Assert.AreEqual(expectedPostReaction.User.Username, actualPostReaction.Author);
        }
    }
}
