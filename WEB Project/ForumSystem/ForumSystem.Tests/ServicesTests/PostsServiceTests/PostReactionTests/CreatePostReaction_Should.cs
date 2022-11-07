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

    public class CreatePostReaction_Should
    {
        [TestMethod]
        public void ReturnCorrectPostReaction_When_IsCreated()
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
            PostReaction newPostReaction = new PostReaction()
            {
                Id = 2,
                PostId = 1,
                UserId = 2,
                Reaction = Models.Enums.Reactions.Like,
            };
            PostReactionDto newPostReactionDto = new PostReactionDto()
            {
                Id = 2,
                Reaction = "Like",
            };
            PostDto expectedPostDto = new PostDto
            {
                Id = 1,
                Title = "the title for the first test post",
                Content = "content for my first test post",
                UserId = 1,
                reactions = new List<PostReactionDto>()
                {
                    new PostReactionDto
                    {
                        Id=1,
                        Reaction="Like",
                    }
                }
            };
            var repoMock = new Mock<IPostsRepository>();

            repoMock
                .Setup(r => r.CreatePostReaction(newPostReaction))
                .Callback(() => expectedPost.Reactions.Add(newPostReaction))
                .Returns(newPostReaction);
            repoMock
              .Setup(r => r.GetIdOfPost(expectedPost.Id))
              .Returns(expectedPost.Id);
            repoMock
              .Setup(r => r.GetById(expectedPost.Id))
              .Returns(expectedPost);

            var mmMOck = new Mock<IModelMapper>();
            mmMOck
                .Setup(x => x.MapPostReaction(newPostReaction))
                .Returns(newPostReactionDto);

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act
            PostReactionDto actualReaction = postServiceUnderTest.CreatePostReaction(1, newPostReaction);
            //assert
            Assert.AreEqual(expectedPost.Reactions[1].Id, actualReaction.Id);
            Assert.AreEqual(expectedPost.Reactions[1].Reaction.ToString(), actualReaction.Reaction);

        }
    }
}
