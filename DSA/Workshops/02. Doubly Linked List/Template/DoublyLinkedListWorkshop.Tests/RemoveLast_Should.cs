using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class RemoveLast_Should
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
            int value = testList.RemoveLast();

            // Assert
            Assert.AreEqual(3, value);
        }

        [TestMethod]
        public void RemoveLastElement_When_ListWithMultipleElements()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act
            testList.RemoveLast();

            // Assert
            List<int> expected = new List<int>(new int[] { 1, 2 });
            Assert.IsTrue(Utils.TestListMatchesExpected(testList, expected));
        }

        [TestMethod]
        public void ReturnCorrectValue_When_ListWithSingleElement()
        {
            // Arrange
            testList.AddFirst(1);

            // Act
            int value = testList.RemoveLast();

            // Assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void RemoveFirstElement_When_ListWithSingleElement()
        {
            // Arrange
            testList.AddFirst(1);

            // Act
            testList.RemoveLast();

            // Assert
            Assert.AreEqual(0, testList.Count);
        }

        [TestMethod]
        public void ThrowException_when_EmptyList()
        {
            // Arrange, Act, Assert
            Assert.ThrowsException<InvalidOperationException>(() => testList.RemoveLast());
        }

        [TestMethod]
        public void DecreaseCount()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2 });

            // Act
            testList.RemoveLast();

            // Assert
            Assert.AreEqual(1, testList.Count);
        }
    }
}
