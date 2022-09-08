using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class Tail_Should
    {
        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void ReturnLastElement_When_NotEmptyList()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act & Assert
            Assert.AreEqual(3, testList.Tail);
        }

        [TestMethod]
        public void ThrowException_When_EmptyList()
        {
            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => testList.Tail);
        }
    }
}
