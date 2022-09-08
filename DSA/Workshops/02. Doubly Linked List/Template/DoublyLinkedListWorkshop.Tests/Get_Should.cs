using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class Get_Should
    {
        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void ReturnElement_When_ExistingIndex()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act & Assert
            Assert.AreEqual(2, testList.Get(1));
        }

        [TestMethod]
        public void get_Should_ThrowException_When_NotExistingIndex()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3 });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList.Get(5));
        }
    }
}
