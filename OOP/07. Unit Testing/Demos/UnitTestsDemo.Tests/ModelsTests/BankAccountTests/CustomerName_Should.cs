using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestsDemo.Models;

namespace UnitTestsDemo.Tests.ModelsTests.BankAccountTests
{
    [TestClass]
    public class CustomerName_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_When_ValueLessThanMin()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;
            var sut = new BankAccount(customerName, balance);

            //Act & Assert
            //Assert.ThrowsException<ArgumentException>(() => sut.CustomerName = new string('a', 3));
            sut.CustomerName = new string('a', 3);
        }

        [TestMethod]
        public void Throw_When_ValueLargerThanMax()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;
            var sut = new BankAccount(customerName, balance);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.CustomerName = new string('a', 11));
        }

        [TestMethod]
        public void ChangeValue_When_ValueIsCorrect()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;
            var sut = new BankAccount(customerName, balance);

            //Act
            sut.CustomerName = "Johnny";

            //Assert
            Assert.AreEqual("Johnny", sut.CustomerName);
        }
    }
}
