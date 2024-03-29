﻿using ForumSystem.Helpers;
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

    public class GetAllPosts_Should
    {
        [TestMethod]
        public void ReturnCorrectListIfPosts_When_GetAll()
        {
            //Arrange
            List<Post> expectedList = new List<Post>
            {
                new Post
                {
                Id = 1,
                Title = "the title for the first test post",
                Content = "content for my first test post",
                UserId = 1,
                },
                new Post
                {
                Id = 2,
                Title = "the title for the second test post",
                Content = "content for my second test post",
                UserId = 1,
                }

            };
            List<PostDto> expectedListDto = new List<PostDto>
            {
                 new PostDto
                 {
                     Id = 1,
                     Title = "the title for the first test post",
                     Content = "content for my first test post",
                     UserId = 1,
                 },
                 new PostDto
                 {
                     Id = 2,
                     Title = "the title for the second test post",
                     Content = "content for my second test post",
                     UserId = 1,
                 },

            };

            var repoMock = new Mock<IPostsRepository>();
            repoMock
                .Setup(r => r.GetAll())
                .Returns(expectedList);

            var mmMOck = new Mock<IModelMapper>();
            mmMOck
                .Setup(x => x.MapPost(expectedList[0]))
                .Returns(expectedListDto[0]);
            mmMOck
                .Setup(x => x.MapPost(expectedList[1]))
                .Returns(expectedListDto[1]);

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act
            List<PostDto> actualList = postServiceUnderTest.GetAll();

            //assert
            Assert.AreEqual(expectedListDto[0].Id, actualList[0].Id);
            Assert.AreEqual(expectedListDto[1].Id, actualList[1].Id);
            Assert.AreEqual(expectedListDto[0].Title, actualList[0].Title);
            Assert.AreEqual(expectedListDto[1].Title, actualList[1].Title);
            Assert.AreEqual(expectedListDto[0].Content, actualList[0].Content);
            Assert.AreEqual(expectedListDto[1].Content, actualList[1].Content);
        }
    }
}
