using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackQueueWorkshop.Queue;
using System;

namespace LinearDataStructures
{
    [TestClass]
    public class QueueTests
    {
        private IQueue<int> queue;

        [TestInitialize]
        public void before()
        {
            //ToDo Select correct implementation
            //this.queue = new ArrayQueue<int>();
            this.queue = new LinkedQueue<int>();
        }

        [TestMethod]
        public void Enqueue_Should_AddElementToTail_When_QueueEmpty()
        {
            // Act
            this.queue.Enqueue(1);
            // Assert
            Assert.AreEqual(1, this.queue.Peek());
        }

        [TestMethod]
        public void Enqueue_Should_AddElementToTail_When_QueueNotEmpty()
        {
            // Arrange
            this.queue.Enqueue(1);
            // Act
            this.queue.Enqueue(2);
            // Assert
            Assert.AreEqual(1, this.queue.Peek());
        }

        [TestMethod]
        public void Dequeue_Should_ThrowException_When_QueueEmpty()
        {
            // Act, Assert
            Assert.ThrowsException<InvalidOperationException>(() => this.queue.Dequeue());
        }

        [TestMethod]
        public void Dequeue_Should_RemoveHead_When_QueueNotEmpty()
        {
            // Arrange
            this.queue.Enqueue(1);
            this.queue.Enqueue(2);
            // Act, Assert
            Assert.AreEqual(1, this.queue.Dequeue());
        }

        [TestMethod]
        public void Peek_Should_ThrowException_When_QueueEmpty()
        {
            // Act, Assert
            Assert.ThrowsException<InvalidOperationException>(() => this.queue.Peek());
        }

        [TestMethod]
        public void Peek_Should_ReturnHead_When_QueueHasOneElement()
        {
            // Arrange
            this.queue.Enqueue(1);
            // Act, Assert
            Assert.AreEqual(1, this.queue.Peek());
        }

        [TestMethod]
        public void Peek_Should_ReturnHead_When_QueueHasMultipleElements()
        {
            // Arrange
            this.queue.Enqueue(1);
            this.queue.Enqueue(2);
            this.queue.Enqueue(3);
            // Act, Assert
            Assert.AreEqual(1, this.queue.Peek());
        }

        [TestMethod]
        public void Size_Should_ReturnZero_When_QueueEmpty()
        {
            // Act, Assert
            Assert.AreEqual(0, this.queue.Size);
        }

        [TestMethod]
        public void Size_Should_ReturnProperSize_When_QueueNotEmpty()
        {
            // Arrange
            this.queue.Enqueue(1);
            this.queue.Enqueue(2);
            // Act, Assert
            Assert.AreEqual(2, this.queue.Size);
        }

        [TestMethod]
        public void IsEmpty_Should_ReturnTrue_When_QueueEmpty()
        {
            // Act, Assert
            Assert.IsTrue(this.queue.IsEmpty);
        }


        [TestMethod]
        public void IsEmpty_Should_ReturnFalse_When_QueueNotEmpty()
        {
            // Arrange
            this.queue.Enqueue(1);
            // Act, Assert
            Assert.IsFalse(this.queue.IsEmpty);
        }
    }
}