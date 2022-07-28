using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Boarder.Models;
using Microsoft.VisualBasic;

namespace Boarder.Tests.Models
{
    [TestClass]
    public class EventLogTest
    {

        [TestMethod]

        public void Throw_When_DescriptionIsNull()
        {
            //Arrange
            string nullDescr = null;

            //Act //Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EventLog(nullDescr));
        }
    }
}
