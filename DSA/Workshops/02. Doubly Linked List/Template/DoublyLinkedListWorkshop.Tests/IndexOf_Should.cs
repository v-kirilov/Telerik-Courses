using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class IndexOf_Should
    {
        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void ReturnIndex_When_ListWithOneElement()
        {
            // Arrange & Act
            testList.AddFirst(3);
            int index = testList.IndexOf(3);

            // Assert
            Assert.AreEqual(0, index);
        }

        [TestMethod]
        public void ReturnIndex_When_ListWithManyElements()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3, 4 });

            // Act
            int index = testList.IndexOf(3);

            // Assert
            Assert.AreEqual(2, index);
        }

        [TestMethod]
        public void ReturnIndex_When_FirstElement()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3, 4 });

            // Act
            int index = testList.IndexOf(1);

            // Assert
            Assert.AreEqual(0, index);
        }

        [TestMethod]
        public void ReturnIndex_When_LastElement()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3, 4 });

            // Act
            int index = testList.IndexOf(4);

            // Assert
            Assert.AreEqual(3, index);
        }

        [TestMethod]
        public void ReturnNotFound_When_EmptyList()
        {
            // Act
            int index = testList.IndexOf(1);

            // Assert
            Assert.AreEqual(-1, index);
        }

        [TestMethod]
        public void ReturnNotFound_When_ElementNotExist()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act
            int index = testList.IndexOf(4);

            // Assert
            Assert.AreEqual(-1, index);
        }
    }
}
