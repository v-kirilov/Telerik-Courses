using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    [TestClass]
    public class Add_Should
    {
        private LinkedList<int> testList;

        [TestInitialize]
        public void initTest()
        {
            this.testList = new LinkedList<int>();
        }

        [TestMethod]
        public void InsertNode_InMiddle()
        {
            // Arrange            
            testList = Utils.CreateTestList(new int[] { 1, 2, 4, 5 });

            // Act
            testList.Add(2, 3);

            // Assert
            List<int> expected = new List<int>(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsTrue(Utils.TestListMatchesExpected(testList, expected));
        }

        [TestMethod]
        public void InsertNode_InEnd()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 2, 3, 4 });

            // Act
            testList.Add(4, 5);

            // Assert
            List<int> expected = new List<int>(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsTrue(Utils.TestListMatchesExpected(testList, expected));
        }

        [TestMethod]
        public void InsertNode_AfterFreshInsert()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 4, 5 });

            // Act
            testList.Add(1, 2);
            testList.Add(2, 3);

            // Assert
            List<int> expected = new List<int>(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsTrue(Utils.TestListMatchesExpected(testList, expected));
        }

        [TestMethod]
        public void UpdateCount()
        {
            // Arrange
            testList = Utils.CreateTestList(new int[] { 1, 3, 4 });

            // Act
            testList.Add(1, 2);

            // Assert
            Assert.AreEqual(4, testList.Count);
        }

        [TestMethod]
        public void ThrowException_When_InvalidIndex()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testList.Add(-2, 1));
        }

    }
}
