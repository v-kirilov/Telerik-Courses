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

namespace ForumSystem.Tests.ServicesTests.PostsServiceTests.PostTests
{
    [TestClass]

    public class UpdatePost_Should
    {
        [TestMethod]
        public void PostShouldUpdate_When_ParamAreValid()
        {
            //Arrange
            Post expectedPost = new Post
            {
                Id = 1,
                Title = "the title for the first test post",
                Content = "content for my first test post",
                UserId = 1,
            };
            PostDto expectedPostDto = new PostDto
            {
                Id = 1,
                Title = "the title for the first test post",
                Content = "content for my first test post",
                UserId = 1,
            };
            Post expectedUpdatedPost = new Post
            {
                Id = 1,
                Title = "the title for the first test Updated post",
                Content = "content for my first test Updated post",
                UserId = 1,
            };
            PostDto expectedUpdatedPostDto = new PostDto
            {
                Id = 1,
                Title = "the title for the first test Updated post",
                Content = "content for my first test Updated post",
                UserId = 1,
            };

            var posts = new List<Post>() { expectedPost };

            var repoMock = new Mock<IPostsRepository>();
            repoMock
                .Setup(r => r.GetIdOfPost(It.IsAny<int>()))
                .Returns(1);
            repoMock
                .Setup(r => r.UpdatePost(1, expectedPostDto))
                .Returns(expectedUpdatedPost);

            var mmMOck = new Mock<IModelMapper>();
            mmMOck
                .Setup(x => x.MapPost(expectedUpdatedPost))
                .Returns(expectedUpdatedPostDto);

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act
            PostDto actualPostDto = postServiceUnderTest.Update(1, expectedPostDto);

            //assert
            Assert.AreEqual(actualPostDto.Id, expectedPost.Id);
        }
    }
}
