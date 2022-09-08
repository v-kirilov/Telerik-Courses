using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BST.Tests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        private BinarySearchTree<int> testTree;

        private void PrepareTestTree()
        {
            this.testTree = new BinarySearchTree<int>(50);
            this.testTree.Insert(30);
            this.testTree.Insert(20);
            this.testTree.Insert(40);
            this.testTree.Insert(70);
            this.testTree.Insert(60);
            this.testTree.Insert(80);
            this.testTree.Insert(72);
            this.testTree.Insert(71);
        }

        [TestMethod]
        public void Constructor_Should_CreateTreeWithSingleNode()
        {
            // Arrange, Act
            this.testTree = new BinarySearchTree<int>(5);

            // Assert
            Assert.AreEqual(5, this.testTree.Value);
            Assert.IsNull(this.testTree.Left);
            Assert.IsNull(this.testTree.Right);
        }

        [TestMethod]
        public void Insert_Should_AddLeftNodeCorrectly_When_TreeHasRootOnly()
        {
            // Arrange
            this.testTree = new BinarySearchTree<int>(5);

            // Act
            this.testTree.Insert(4);

            // Assert
            Assert.AreEqual(4, this.testTree.Left.Value);
        }

        [TestMethod]
        public void Insert_Should_AddRightNodeCorrectly_When_TreeHasRootOnly()
        {
            //Arrange
            this.testTree = new BinarySearchTree<int>(5);

            // Act
            this.testTree.Insert(7);

            // Assert
            Assert.AreEqual(7, this.testTree.Right.Value);
        }

        [TestMethod]
        public void Insert_Should_AddLeftNodeCorrectly_When_TreeHasMultipleNodes()
        {
            // Arrange
            this.PrepareTestTree();

            // Act
            this.testTree.Insert(25);

            // Assert
            Assert.AreEqual(25, this.testTree.Left.Left.Right.Value);
        }

        [TestMethod]
        public void Insert_Should_AddRightNodeCorrectly_When_TreeHasMultipleNodes()
        {
            // Arrange
            this.PrepareTestTree();

            // Act
            this.testTree.Insert(65);

            // Assert
            Assert.AreEqual(65, this.testTree.Right.Left.Right.Value);
        }

        [TestMethod]
        public void Search_Should_ReturnFalse_When_SingleNotMatchingNode()
        {
            // Act
            this.testTree = new BinarySearchTree<int>(5);

            // Act, Assert
            Assert.IsFalse(this.testTree.Search(50));
        }

        [TestMethod]
        public void Search_Should_ReturnFalse_When_NodeDoesNotExist()
        {
            // Arrange
            this.PrepareTestTree();

            // Act, Assert
            Assert.IsFalse(this.testTree.Search(5));
        }

        [TestMethod]
        public void Search_Should_ReturnTrue_When_SingleMatchingNode()
        {
            // Arrange
            this.testTree = new BinarySearchTree<int>(5);

            // Act, Assert
            Assert.IsTrue(this.testTree.Search(5));
        }

        [TestMethod]
        public void Search_Should_ReturnTrue_When_TreeHasMultipleNodes()
        {
            this.PrepareTestTree();

            // Act, Assert
            Assert.IsTrue(this.testTree.Search(60));
        }

        [TestMethod]
        public void GetPreOrder_Should_ReturnElementsInRightSequence()
        {
            // Arrange
            this.PrepareTestTree();
            int[] expected = { 50, 30, 20, 40, 70, 60, 80, 72, 71 };

            // Act
            IList<int> result = this.testTree.GetPreOrder();

            // Assert
            CollectionAssert.AreEqual(expected, result.ToArray());
        }

        [TestMethod]
        public void GetInOrder_Should_ReturnElementsInRightSequence()
        {
            // Arrange
            this.PrepareTestTree();
            int[] expected = { 20, 30, 40, 50, 60, 70, 71, 72, 80 };

            // Act
            IList<int> result = this.testTree.GetInOrder();

            // Assert
            CollectionAssert.AreEqual(expected, result.ToArray());
        }

        [TestMethod]
        public void GetPostOrder_Should_ReturnElementsInRightSequence()
        {
            // Arrange
            this.PrepareTestTree();
            int[] expected = { 20, 40, 30, 60, 71, 72, 80, 70, 50 };

            // Act
            IList<int> result = this.testTree.GetPostOrder();

            // Assert
            CollectionAssert.AreEqual(expected, result.ToArray());
        }

        [TestMethod]
        public void GetBFS_Should_ReturnElementsInRightSequence()
        {
            // Arrange
            this.PrepareTestTree();
            int[] expected = { 50, 30, 70, 20, 40, 60, 80, 72, 71 };

            // Act
            IList<int> result = this.testTree.GetBFS();

            // Assert
            CollectionAssert.AreEqual(expected, result.ToArray());
        }

        [TestMethod]
        public void Height_Should_ReturnZero_When_TreeHasRootOnly()
        {
            // Arrange
            this.testTree = new BinarySearchTree<int>(5);

            // Act & Assert
            Assert.AreEqual(0, this.testTree.Height);
        }

        [TestMethod]
        public void Height_Should_ReturnCorrectHeight_When_TreeHasMultipleNodes()
        {
            // Arrange
            this.PrepareTestTree();

            // Act & Assert
            Assert.AreEqual(4, this.testTree.Height);
        }

        //Additional tests

        [TestMethod]
        public void Remove_Should_ReturnFalse_When_SingleNotMatchingItem()
        {
            // Arrange
            this.testTree = new BinarySearchTree<int>(5);

            // act, Assert
            Assert.IsFalse(this.testTree.Remove(6));
        }

        [TestMethod]
        public void Remove_Should_ReplaceRoot_When_ValueToBeRemovedIsInRoot()
        {
            // Arrange
            this.PrepareTestTree();

            // Act, Assert
            Assert.IsTrue(this.testTree.Remove(50));
            Assert.AreEqual(40, this.testTree.Value);
        }

        [TestMethod]
        public void Remove_Should_MaintainCorrectOrdering_When_ValuePresentAndHasRightChildOnly()
        {
            // Arrange
            this.PrepareTestTree();
            this.testTree.Insert(42);

            // Act
            this.testTree.Remove(40);

            // Assert
            Assert.AreEqual(42, this.testTree.Left.Right.Value);
        }

        [TestMethod]
        public void Remove_Should_MaintainCorrectOrdering_When_ValuePresentAndHasLeftChildOnly()
        {
            // Arrange
            this.PrepareTestTree();
            this.testTree.Insert(39);

            // Act
            this.testTree.Remove(40);

            // Assert
            Assert.AreEqual(39, this.testTree.Left.Right.Value);
        }

        [TestMethod]
        public void Remove_Should_MaintainCorrectOrdering_WhenValuePresentAndHasBothChildren()
        {
            // Arrange
            this.PrepareTestTree();
            // Act
            this.testTree.Remove(70);
            // Assert
            Assert.AreEqual(60, this.testTree.Right.Value);
            Assert.IsNull(this.testTree.Right.Left);
        }
    }
}
