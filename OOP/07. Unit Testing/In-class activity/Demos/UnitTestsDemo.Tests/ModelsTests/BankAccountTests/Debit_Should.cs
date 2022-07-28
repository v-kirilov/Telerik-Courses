using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestsDemo.Models;

namespace UnitTestsDemo.Tests.ModelsTests.BankAccountTests
{
    [TestClass]
    public class Debit_Should
    {
        [TestMethod]
        public void Throw_When_AmountIsLargerThanBalance()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;
            var sut = new BankAccount(customerName, balance);

            //Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sut.Debit(51));
        }

        [TestMethod]
        public void Throw_When_AmountIsNegative()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;
            var sut = new BankAccount(customerName, balance);

            //Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sut.Debit(-10));
        }

        [TestMethod]
        public void UpdateBalance_When_AmountIsEnough()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;
            var sut = new BankAccount(customerName, balance);

            //Act
            sut.Debit(20);

            //Assert
            Assert.AreEqual(30, sut.Balance);
        }
    }
}
