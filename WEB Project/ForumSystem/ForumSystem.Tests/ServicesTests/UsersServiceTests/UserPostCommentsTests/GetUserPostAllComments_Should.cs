using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests.ServicesTests.UsersServiceTests.UserPostCommentsTests
{
    [TestClass]
    public class GetUserPostAllComments_Should
    {
        [TestMethod]
        public void GetCommentsInPost_When_ParamsAreValid()
        {
            // Arrange
            var defaultUser = TestUserHelper.GetTestDefaultUser();
            var post = TestUserHelper.GetTestPost();
            var comment = TestUserHelper.GetTestComment();

            post.User = defaultUser;
            post.UserId = defaultUser.Id;
            comment.User = defaultUser;
            comment.UserId = defaultUser.Id;
            comment.Post = post;
            comment.PostId = post.Id;
            post.User = defaultUser;
            post.UserId = defaultUser.Id;

            var expectedComments = new List<Comment>() { comment };
            var posts = new List<Post>() { post };

            var repoUsersMock = new Mock<IUsersRepository>();
            repoUsersMock.Setup(repo => repo.GetById(defaultUser.Id))
                         .Returns(defaultUser);
            var repoCommentsMock = new Mock<ICommentsRepository>();
            repoCommentsMock.Setup(repo => repo.GetCommentByPostId(post.Id))
                            .Returns(expectedComments);
            var repoPostsMock = new Mock<IPostsRepository>();
            repoPostsMock.Setup(repo => repo.GetAll())
                            .Returns(posts);

            var sut = TestUserHelper.InitializeUsersService(repoUsersMock, repoCommentsMock, repoPostsMock);

            // Act
            var actualComment = sut.GetUserPostAllComments(defaultUser.Id, post.Id);

            // Assert
            Assert.AreEqual(expectedComments[0].Id, actualComment[0].Id);
            Assert.AreEqual(expectedComments[0].CommentContent, actualComment[0].Content);
        }
    }
}
