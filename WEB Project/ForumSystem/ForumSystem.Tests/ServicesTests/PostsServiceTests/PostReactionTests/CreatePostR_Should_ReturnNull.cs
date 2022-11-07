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

    public class CreatePostR_Should_ReturnNull
    {
        [TestMethod]
        public void ReturnNull_When_PostReactionIsTheSame()
        {
            //Arrange
            User user = TestUserHelper.GetTestDefaultUser();
            Post expectedPost = new Post
            {
                Id = 1,
                Title = "the title for the first test post",
                Content = "content for my first test post",
                UserId = 1,
                User=user,
                Reactions = new List<PostReaction>() {
                    new PostReaction
                    {
                        Id=1,
                        PostId=1,
                        UserId=1,
                        User=user,
                        Reaction=Models.Enums.Reactions.Like,
                    }
                }
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
              .Setup(r => r.GetIdOfPost(expectedPost.Id))
              .Returns(expectedPost.Id);
            repoMock
              .Setup(r => r.GetById(expectedPost.Id))
              .Returns(expectedPost);
            repoMock
                .Setup(r => r.UpdatePostReaction(1, user.Username, expectedPost.Reactions[0]))
                .Returns(expectedPost.Reactions[0]);

            var mmMOck = new Mock<IModelMapper>();

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act

            //assert
            Assert.IsNull(postServiceUnderTest.CreatePostReaction(1, expectedPost.Reactions[0]));
           
        }
    }
}
