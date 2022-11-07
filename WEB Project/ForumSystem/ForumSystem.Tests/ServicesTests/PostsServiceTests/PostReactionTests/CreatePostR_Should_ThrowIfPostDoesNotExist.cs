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

    public class CreatePostR_Should_ThrowIfPostDoesNotExist
    {
        [TestMethod]
        public void ThrowException_When_PostDoesNotExist()
        {
            //Arrange
            Post expectedPost = new Post
            {
                Id = 1,
                Title = "the title for the first test post",
                Content = "content for my first test post",
                UserId = 1,
                Reactions = new List<PostReaction>() {
                    new PostReaction
                    {
                        Id=1,
                        PostId=1,
                        UserId=1,
                        Reaction=Models.Enums.Reactions.Like,
                    }
                }
            };

            var repoMock = new Mock<IPostsRepository>();

            repoMock
              .Setup(r => r.GetIdOfPost(expectedPost.Id))
            .Throws(new EntityNotFoundException());

            var mmMOck = new Mock<IModelMapper>();

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act

            //assert
            Assert.ThrowsException<InvalidDataException>(() => postServiceUnderTest.CreatePostReaction(2, expectedPost.Reactions[0]));
        }
    }
}
