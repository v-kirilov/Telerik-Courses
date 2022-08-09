using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Feedback;
using Task_Management.Models;

namespace Task_Management.Tests.Models
{
    [TestClass]

    public class FeedbackTests
    {
        [TestMethod]
        public void ShouldThrowExceptionIfTitleIsTooShort()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title";
            string description = "description";
            int rating = 5;
            
            // Act
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(id, title, description,rating));

        }

        [TestMethod]
        public void ShouldThrowExceptionIfDescriptionIsTooShort()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "descr";
            int rating = 5;

            // Act
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(id, title, description, rating));

        }

        [TestMethod]
        public void ShouldThrowExceptionIfRatingIsOutOfRange()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 55;

            // Act
            // Assert
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(id, title, description, rating));

        }

        [TestMethod]
        public void ShouldChangeFeedbackRating()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 5;

            // Act
            IFeedback feedback = new Feedback(id, title, description, rating);
            feedback.Rating = 9;
            // Assert
            Assert.AreEqual(9, feedback.Rating);

        }

        [TestMethod]
        public void ShouldKeepFeedbackRating()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 5;

            // Act
            IFeedback feedback = new Feedback(id, title, description, rating);
            feedback.Rating = 5;
            // Assert
            Assert.AreEqual(5, feedback.Rating);

        }


        [TestMethod]
        public void ShouldChangeFeedbackStatus()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 5;

            // Act
            IFeedback feedback = new Feedback(id, title, description, rating);
            feedback.Status = Status.Done;
            // Assert
            Assert.AreEqual(Status.Done, feedback.Status);

        }

        [TestMethod]
        public void ShouldKeepFeedbackStatus()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 5;

            // Act
            IFeedback feedback = new Feedback(id, title, description, rating);
            feedback.Status = Status.New;
            // Assert
            Assert.AreEqual(Status.New, feedback.Status);

        }

        [TestMethod]
        public void ShouldCreateFeedbackWhenParametersAreValid()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 5;

            // Act
            IFeedback feedback = new Feedback(id,title, description, rating);
            // Assert
            Assert.AreEqual("title_Must_Be_Long",feedback.Title);

        }

        [TestMethod]
        public void ShouldSetInitialStatusToNew()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_Be_Long";
            string description = "description";
            int rating = 5;

            // Act
            IFeedback feedback = new Feedback(id, title, description, rating);
            // Assert
            Assert.AreEqual(Status.New, feedback.Status);

        }
    }
}
