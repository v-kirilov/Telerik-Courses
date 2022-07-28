using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestsDemo.Models;

namespace UnitTestsDemo.Tests.ModelsTests.BankAccountTests
{
    [TestClass]
    public class Credit_Should
    {
        [TestMethod]
        public void UpdateBalance_When_CorrectAmount()
        {
            //Arrange
            double amount = 100;
            var customerName = "Jack Mills";
            double balance = 50;
            var sut = new BankAccount(customerName, balance);

            //Act
            sut.Credit(amount);

            //Assert
            Assert.AreEqual(150, sut.Balance);
        }

        [TestMethod]
        public void Throw_When_AmountIsNegative()
        {
            //Arrange
            double amount = -100;
            var customerName = "Jack Mills";
            double balance = 50;
            var sut = new BankAccount(customerName, balance);

            //Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sut.Credit(amount));
        }
    }
}