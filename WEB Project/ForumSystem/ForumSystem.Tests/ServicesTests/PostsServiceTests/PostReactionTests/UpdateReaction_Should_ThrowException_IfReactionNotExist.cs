using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ForumSystem.Exceptions;
using Microsoft.Extensions.Hosting;

namespace ForumSystem.Tests.ServicesTests.PostsServiceTests.PostReactionTests
{
    [TestClass]

    public class UpdateReaction_Should_ThrowException_IfReactionNotExist
    {
        [TestMethod]
        public void ThrowException_When_PostReactionDoesNotExist()
        {
            //Arrange
            Post expectedPost = new Post
            {
                Id = 1,
                Title = "the title for the first test post",
                Content = "content for my first test post",
                UserId = 1,
                Reactions = new List<PostReaction>()
            };
            User myAuthor = new User()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "JD",
                IsBlocked = false,
            };
            PostReaction expectedReaction = new PostReaction()
            {
                Id = 1,
                PostId = 1,
                UserId = 1,
                User = myAuthor,
                Reaction = Models.Enums.Reactions.Like,
            };

            var repoMock = new Mock<IPostsRepository>();

            repoMock
              .Setup(r => r.GetById(expectedPost.Id))
              .Throws(new EntityNotFoundException());

            var mmMOck = new Mock<IModelMapper>();

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act

            //assert
            Assert.ThrowsException<InvalidDataException>(() => postServiceUnderTest.UpdatePostReaction(1, myAuthor.Username, expectedReaction));

        }
    }
}
