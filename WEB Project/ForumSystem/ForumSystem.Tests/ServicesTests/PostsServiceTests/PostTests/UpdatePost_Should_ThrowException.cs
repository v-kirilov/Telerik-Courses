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

    public class UpdatePost_Should_ThrowException
    {
        [TestMethod]
        public void ThrowException_When_PostNotFound()
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

            var posts = new List<Post>() { expectedPost };

            var repoMock = new Mock<IPostsRepository>();
            repoMock
                .Setup(r => r.GetIdOfPost(expectedPost.Id))
                .Returns(2);

            var mmMOck = new Mock<IModelMapper>();

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act

            //assert
            Assert.ThrowsException<DuplicateEntityException>(() => postServiceUnderTest.Update(1, expectedPostDto));
        }
    }
}
