using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestsDemo.Models;

namespace UnitTestsDemo.Tests.ModelsTests.BankAccountTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void SetCorrectCustomerName()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;

            //Act
            var sut = new BankAccount(customerName, balance);

            //Assert
            Assert.AreEqual(customerName, sut.CustomerName);
        }

        [TestMethod]
        public void SetCorrectBalance()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;

            //Act
            var sut = new BankAccount(customerName, balance);

            //Assert
            Assert.AreEqual(balance, sut.Balance);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            //Arrange
            var customerName = "Jack Mills";
            double balance = 50;

            //Act
            var sut = new BankAccount(customerName, balance);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(BankAccount));
        }
    }
}