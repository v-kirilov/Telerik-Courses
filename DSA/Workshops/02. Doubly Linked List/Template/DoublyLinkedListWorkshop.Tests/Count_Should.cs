using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class Count_Should
    {

        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void size_Should_ReturnZero_When_EmptyList()
        {
            // Arrange, Act, Assert
            Assert.AreEqual(0, testList.Count);
        }

        [TestMethod]
        public void size_Should_ReturnSize_When_NotEmptyList()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act, Assert
            Assert.AreEqual(3, testList.Count);
        }
    }
}
