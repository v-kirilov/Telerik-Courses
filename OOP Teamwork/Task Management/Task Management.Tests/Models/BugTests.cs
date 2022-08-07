using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;

namespace Task_Management.Tests.Models
{
    [TestClass]

    public class BugTests
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
            string steps = "steps";
            Severity severity = Severity.Critical;
            Priority priority = Priority.High;
            // Act

            // Assert

            Assert.ThrowsException<InvalidUserInputException>(() => new Bug(id, title, description, steps, priority, severity, member));

        }

        [TestMethod]
        public void ShouldThrowExceptionIfStepIsTooShort()
        {
            // Arrange

            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps";
            Severity severity = Severity.Critical;
            Priority priority = Priority.High;
            // Act
            // Assert

            Assert.ThrowsException<InvalidUserInputException>(() => new Bug(id, title, description, steps, priority, severity, member));

        }

        [TestMethod]
        public void ShouldThrowExceptionIfStepIsNullOrEmpty()
        {
            // Arrange

            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "";
            Severity severity = Severity.Critical;
            Priority priority = Priority.High;
            // Act
            // Assert

            Assert.ThrowsException<InvalidUserInputException>(() => new Bug(id, title, description, steps, priority, severity, member));

        }

        [TestMethod]
        public void ShouldChangeBugPriority()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            Severity severity = Severity.Critical;
            Priority priority = Priority.High;

            // Act
            Bug newBug = new Bug(id, title, description, steps, priority, severity, member);
            newBug.Priority = Priority.Low;
            // Assert
            Assert.AreEqual(Priority.Low, newBug.Priority);
        }

        [TestMethod]
        public void ShouldChangeBugSeverity()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            Severity severity = Severity.Critical;
            Priority priority = Priority.High;

            // Act
            Bug newBug = new Bug(id, title, description, steps, priority, severity, member);
            newBug.Severity = Severity.Major;
            // Assert
            Assert.AreEqual(Severity.Major, newBug.Severity);
        }

        [TestMethod]
        public void ShouldChangeBugStatus()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            Severity severity = Severity.Critical;
            Priority priority = Priority.High;

            // Act
            Bug newBug = new Bug(id, title, description, steps, priority, severity, member);
            newBug.Status = Status.Fixed;
            // Assert
            Assert.AreEqual(Status.Fixed, newBug.Status);
        }

        //[TestMethod]
        //public void ShouldChangeBugAssignee()
        //{
        //    // Arrange
        //    IMember Pesho = new Member("Pesho");
        //    IMember Tosho = new Member("Tosho");

        //    int id = 1;
        //    string title = "title_Must_be_Big";
        //    string description = "description";
        //    string steps = "steps_Must_Be_Long";
        //    Severity severity = Severity.Critical;
        //    Priority priority = Priority.High;

        //    // Act
        //    Bug newBug = new Bug(id, title, description, steps, priority, severity, Pesho);
        //    newBug.Assignee = Tosho;
        //    // Assert
        //    Assert.AreEqual(Tosho, newBug.Assignee);
        //}


        [TestMethod]
        public void ShouldCreateBugWhenParametersAreValid()
        {
            // Arrange
            string memberName = "member";
            IMember member = new Member(memberName);

            int id = 1;
            string title = "title_Must_be_Big";
            string description = "description";
            string steps = "steps_Must_Be_Long";
            Severity severity = Severity.Critical;
            Priority priority = Priority.High;

            // Act
            Bug newBug = new Bug(id, title, description, steps, priority, severity, member);

            // Assert
            Assert.AreEqual("title_Must_be_Big", newBug.Title);
        }

        //[TestMethod]
        //public void ShouldReturnRightType()
        //{
        //    // Arrange
        //    string memberName = "member";
        //    IMember member = new Member(memberName);

        //    int id = 1;
        //    string title = "title_Must_be_Big";
        //    string description = "description";
        //    string steps = "steps_Must_Be_Long";
        //    Severity severity = Severity.Critical;
        //    Priority priority = Priority.High;

        //    // Act
        //    Bug newBug = new Bug(id, title, description, steps, priority, severity, member);
        //    var result = newBug.GetTaskType();
        //    // Assert
        //    Assert.AreEqual("Bug", result);
        //}
    }
}
