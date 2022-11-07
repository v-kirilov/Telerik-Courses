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

    public class DeletePostReaction_Should
    {
        [TestMethod]
        public void DeletPostReaction_WhenIdAndAuthorIsValid()
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
                Reaction = Models.Enums.Reactions.Like,
            };
            expectedPost.Reactions.Add(expectedReaction);

            var repoMock = new Mock<IPostsRepository>();
            repoMock
              .Setup(r => r.GetById(expectedPost.Id))
              .Returns(expectedPost);
            repoMock
                .Setup(r => r.GetReactionByUsername(expectedPost, myAuthor.Username))
                .Returns(expectedPost.Reactions[0]);
            repoMock
                .Setup(r => r.DeleteReaction(expectedPost.Reactions[0]))
                .Callback(() => expectedPost.Reactions.RemoveRange(0, 1));

            var mmMOck = new Mock<IModelMapper>();

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act
            postServiceUnderTest.DeletePostReaction(1, myAuthor);
            //assert
            CollectionAssert.DoesNotContain(expectedPost.Reactions, expectedReaction);
        }
    }
}
