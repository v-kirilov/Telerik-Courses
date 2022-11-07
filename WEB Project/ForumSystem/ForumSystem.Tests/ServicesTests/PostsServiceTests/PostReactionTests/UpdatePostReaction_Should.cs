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

    public class UpdatePostReaction_Should
    {
      
        [TestMethod]
        public void PostReactionShouldUpdate_When_ParamAreValid()
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
            PostReaction updatedReaction = new PostReaction()
            {
                Id = 1,
                PostId = 1,
                UserId = 1,
                User = myAuthor,
                Reaction = Models.Enums.Reactions.Like,
            };
            PostReactionDto updatedPostReactionDto = new PostReactionDto()
            {
                Id = 1,
                Reaction = "Dislike",
            };

            expectedPost.Reactions.Add(expectedReaction);

            var repoMock = new Mock<IPostsRepository>();

            repoMock
                .Setup(r => r.UpdatePostReaction(expectedPost.Id, myAuthor.Username, expectedReaction))
                .Returns(expectedReaction);
            repoMock
              .Setup(r => r.GetIdOfPost(expectedPost.Id))
              .Returns(expectedPost.Id);
            repoMock
              .Setup(r => r.GetById(expectedPost.Id))
              .Returns(expectedPost);

            var mmMOck = new Mock<IModelMapper>();
            mmMOck
                .Setup(x => x.MapPostReaction(expectedReaction))
                .Returns(updatedPostReactionDto);

            var postServiceUnderTest = new PostsService(repoMock.Object, mmMOck.Object);

            //Act
            PostReactionDto actualReaction = postServiceUnderTest.UpdatePostReaction(1, myAuthor.Username, expectedReaction);
            //assert
            Assert.AreEqual(expectedPost.Reactions[0].Id, actualReaction.Id);
            Assert.AreEqual(actualReaction.Reaction, updatedPostReactionDto.Reaction);
        }
    }
}
