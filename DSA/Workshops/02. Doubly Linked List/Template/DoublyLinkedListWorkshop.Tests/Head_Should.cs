using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class Head_Should
    {
        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void ReturnFirstElement_When_NotEmptyList()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act & Assert
            Assert.AreEqual(1, testList.Head);
        }

        [TestMethod]
        public void ThrowException_When_EmptyList()
        {
            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => testList.Head);
        }
    }
}
