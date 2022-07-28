using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Boarder.Models;
using Microsoft.VisualBasic;
using Boarder.Loggers;

namespace Boarder.Tests.Models
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void ConsoleLogger_Shoud_DeriveFrom_ILogger()
        {
            //Arrange
            var type = typeof(ILogger);
            //Act
            //Assert

            var isAssignable = typeof(ILogger).IsAssignableFrom(type);
            Assert.IsTrue(isAssignable, "ConsoleLogger class does not derive from ILogger interface!");
        }
    }
}
