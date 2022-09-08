using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackQueueWorkshop.Stack;
using System;

namespace LinearDataStructures
{
    [TestClass]
    public class StackTests
    {
        private IStack<int> stack;

        [TestInitialize]
        public void InitTest()
        {
            //ToDo Select correct implementation
            //this.stack = new ArrayStack<int>();
            this.stack = new LinkedStack<int>();
        }

        [TestMethod]
        public void Push_Should_AddElementToTheTop_When_StackEmpty()
        {
            // Act
            this.stack.Push(1);
            // Assert
            Assert.AreEqual(1, this.stack.Peek());
        }

        [TestMethod]
        public void Push_Should_AddElementToTheTop_When_StackNotEmpty()
        {
            // Arrange
            this.stack.Push(1);
            // Act
            this.stack.Push(2);
            // Assert
            Assert.AreEqual(2, this.stack.Peek());
        }

        [TestMethod]
        public void Pop_Should_ThrowException_When_StackEmpty()
        {
            // Act, Assert
            Assert.ThrowsException<InvalidOperationException>(() => this.stack.Pop());
        }

        [TestMethod]
        public void Pop_Should_ReturnTop_When_StackNotEmpty()
        {
            // Arrange
            this.stack.Push(1);
            this.stack.Push(2);
            this.stack.Push(3);
            // Act, Assert
            Assert.AreEqual(3, this.stack.Pop());
        }

        [TestMethod]
        public void Peek_Should_ThrowException_When_StackEmpty()
        {
            // Act
            Assert.ThrowsException<InvalidOperationException>(() => this.stack.Peek());
        }

        [TestMethod]
        public void Peek_Should_ReturnTop_When_StackNotEmpty()
        {
            // Arrange
            this.stack.Push(1);
            this.stack.Push(2);
            this.stack.Push(3);
            // Act&&Assert
            Assert.AreEqual(3, this.stack.Peek());
        }

        [TestMethod]
        public void Size_Should_ReturnZero_When_StackEmpty()
        {
            // Act&&Assert
            Assert.AreEqual(0, this.stack.Size);
        }

        [TestMethod]
        public void Size_Should_ReturnProperValue_When_StackNotEmpty()
        {
            // Arrange
            this.stack.Push(1);
            this.stack.Push(2);
            this.stack.Push(3);
            // Act&&Assert
            Assert.AreEqual(3, this.stack.Size);
        }

        [TestMethod]
        public void IsEmpty_Should_ReturnTrue_When_StackEmpty()
        {
            // Assert
            Assert.IsTrue(this.stack.IsEmpty);
        }

        [TestMethod]
        public void IsEmpty_Should_ReturnFalse_When_StackNotEmpty()
        {
            // Act
            this.stack.Push(1);
            // Assert
            Assert.IsFalse(this.stack.IsEmpty);
        }
    }
}