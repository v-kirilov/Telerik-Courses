using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Boarder.Models;
using Microsoft.VisualBasic;
using Boarder.Loggers;

namespace Boarder.Tests.Models
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void Does_Board_AddItem_Works_Correctly()
        {
            //Arrange
            var title = "This is a test title";
            var assignee = "TestUser";
            var dueDate = Convert.ToDateTime("01-01-2030");

            //Act
            var task = new Task(title, assignee, dueDate);
            Board.AddItem(task);
            //Assert
            Assert.AreEqual(1, Board.TotalItems);


        }
    }
}
