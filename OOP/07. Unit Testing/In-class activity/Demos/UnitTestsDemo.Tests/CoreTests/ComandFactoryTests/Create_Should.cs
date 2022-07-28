using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestsDemo.Core;

namespace UnitTestsDemo.Tests.CoreTests.ComandFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnCorrectTypeOfInstance()
        {
            //Arrange
            var commandName = "credit";
            var sut = new CommandFactory();

            //Act
            var result = sut.Create(commandName);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CreditCommand));
        }

        [TestMethod]
        public void Throw_When_ParamsAreInvalid()
        {
            //Arrange
            var commandName = "freemoney";
            var sut = new CommandFactory();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Create(commandName));
        }

        [TestMethod]
        public void ThrowWithCorrectMessage_When_ParamsAreInvalid()
        {
            //Arrange
            var commandName = "freemoney";
            var sut = new CommandFactory();

            //Act & Assert
            var result = Assert.ThrowsException<ArgumentException>(() => sut.Create(commandName));
            Assert.AreEqual("Command not found", result.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_When_ParamsAreInvalid_Ver2()
        {
            //Arrange
            var commandName = "freemoney";
            var sut = new CommandFactory();

            //Act
            sut.Create(commandName);
        }
    }
}
