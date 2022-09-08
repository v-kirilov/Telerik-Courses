using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class AddLast_Should
    {
        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void UpdateTail_When_EmptyList()
        {
            // Act
            testList.AddLast(5);

            // Assert
            Assert.AreEqual(5, testList.Tail);
        }

        [TestMethod]
        public void UpdateHead_When_EmptyList()
        {
            // Act
            testList.AddLast(5);

            // Assert
            Assert.AreEqual(5, testList.Head);
        }

        [TestMethod]
        public void addLast_Should_UpdateTail_When_NotEmptyList()
        {
            // Arrange
            testList.AddLast(5);

            // Act
            testList.AddLast(10);

            // Assert
            Assert.AreEqual(10, testList.Tail);
        }

        [TestMethod]
        public void MaintainCorrectOrder()
        {
            // Arrange & Act
            List<int> values = new List<int>(new int[] { 1, 2, 3, 4, 5 });

            foreach (int value in values)
            {
                testList.AddLast(value);
            }

            // Assert
            Assert.IsTrue(Utils.TestListMatchesExpected(testList, values));
        }

        [TestMethod]
        public void addLast_Should_UpdateCount()
        {
            // Arrange && Act
            testList.AddLast(5);
            testList.AddLast(5);

            // Assert
            Assert.AreEqual(2, testList.Count);
        }
    }
}
