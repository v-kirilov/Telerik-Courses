using Dealership.Models;
using Dealership.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dealership.Tests.Models
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod]
        public void Comment_Should_ImplementICommentInterface()
        {
            var type = typeof(Comment);
            var isAssignable = typeof(IComment).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Comment class does not implement IComment interface!");
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_ContentLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Comment("ab", "author"));
        }

        [TestMethod]
        public void Constructor_Should_CreateNewComment_When_ParametersAreCorrect()
        {
            // Arrange, Act
            Comment comment = new Comment("content", "author");

            // Assert
            Assert.AreEqual("content", comment.Content);
        }
    }
}
