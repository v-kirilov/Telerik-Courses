using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class RemoveFirst_Should
    {
        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void ReturnCorrectValue_When_ListWithMultipleElements()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act
            int value = testList.RemoveFirst();

            // Assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void RemoveFirstElement_When_ListWithMultipleElements()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act
            testList.RemoveFirst();

            // Assert
            List<int> expected = new List<int>(new int[] { 2, 3 });
            Assert.IsTrue(Utils.TestListMatchesExpected(testList, expected));
        }

        [TestMethod]
        public void ReturnCorrectValue_When_ListWithSingleElement()
        {
            // Arrange
            testList.AddFirst(1);
            // Act
            int value = testList.RemoveFirst();

            // Assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void RemoveFirstElement_When_ListWithSingleElement()
        {
            // Arrange
            testList.AddFirst(1);

            // Act
            testList.RemoveFirst();

            // Assert
            Assert.AreEqual(0, testList.Count);
        }

        [TestMethod]
        public void ThrowException_When_EmptyList()
        {
            // Arrange, Act, Assert
            Assert.ThrowsException<InvalidOperationException>(() => testList.RemoveFirst());
        }

        [TestMethod]
        public void DecreaseCount()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2 });

            // Act
            testList.RemoveFirst();

            // Assert
            Assert.AreEqual(1, testList.Count);
        }
    }
}
