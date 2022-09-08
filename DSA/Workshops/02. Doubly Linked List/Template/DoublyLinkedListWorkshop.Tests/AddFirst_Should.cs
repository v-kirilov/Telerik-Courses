using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class AddFirst_Should
    {
        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void UpdateHead_When_EmptyList()
        {
            // Act
            testList.AddFirst(5);

            // Assert
            Assert.AreEqual(5, testList.Head);
        }

        [TestMethod]
        public void UpdateTail_When_EmptyList()
        {
            // Act
            testList.AddFirst(5);

            // Assert
            Assert.AreEqual(5, testList.Tail);
        }

        [TestMethod]
        public void UpdateHead_When_NotEmptyList()
        {
            // Arrange
            testList.AddFirst(5);

            // Act
            testList.AddFirst(10);

            // Assert
            Assert.AreEqual(10, testList.Head);
        }

        [TestMethod]
        public void MaintainCorrectOrder()
        {
            // Arrange & Act
            List<int> values = new List<int>(new int[] { 5, 4, 3, 2, 1 });

            foreach (int value in values)
            {
                testList.AddFirst(value);
            }

            // Assert
            List<int> expected = new List<int>(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsTrue(Utils.TestListMatchesExpected(testList, expected));
        }

        [TestMethod]
        public void UpdateCount()
        {
            // Act
            testList.AddFirst(1);
            testList.AddFirst(1);
            testList.AddFirst(1);

            // Assert
            Assert.AreEqual(testList.Count, 3);
        }
    }
}
