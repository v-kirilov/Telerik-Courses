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

    public class DeletePost_Should
    {
        [TestMethod]
        public void DeletePost_When_IdIsValid()
        {
            //Arrange
            Post expectedPost = new Post
            {
                Id = 1,
                Title = "the title for the first test post",
                Content = "content for my first test post",
                UserId = 1,
            };

            var posts = new List<Post>() { expectedPost };

            var repoMock = new Mock<IPostsRepository>();
            repoMock
                .Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(expectedPost);
            repoMock
                .Setup(r => r.Delete(It.IsAny<int>()))
                .Callback(() => posts.Remove(expectedPost));

            var mmMOck = new Mock<IModelMapper>();

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act
            postServiceUnderTest.DeletePost(1);

            //assert
            CollectionAssert.DoesNotContain(posts, expectedPost);
        }
    }
}
