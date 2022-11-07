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

    public class GetPost_Should
    {
        [TestMethod]
        public void ReturnCorrectPost_When_IdIsValid()
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
            var repoMock = new Mock<IPostsRepository>();
            repoMock
                .Setup(r => r.GetById(1))
                .Returns(expectedPost);

            var mmMOck = new Mock<IModelMapper>();
            mmMOck
                .Setup(x => x.MapPost(expectedPost))
                .Returns(expectedPostDto);

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act
            PostDto actualPost = postServiceUnderTest.GetById(expectedPost.Id);

            //assert
            Assert.AreEqual(expectedPost.Id, actualPost.Id);
            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(expectedPost.Content, actualPost.Content);
        }
    }
}
