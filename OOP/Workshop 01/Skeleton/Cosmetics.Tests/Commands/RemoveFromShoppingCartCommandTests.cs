using System;
using Cosmetics.Commands;
using Cosmetics.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class RemoveFromShoppingCartCommandTests
    {
        [TestMethod]
        [DataRow(RemoveFromShoppingCartCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(RemoveFromShoppingCartCommand.ExpectedNumberOfArguments + 1)]
        public void Should_ThrowException_When_ArgumentCountDifferentThanExpected(int testSize)
        {
            // Arrange
            RemoveFromShoppingCartCommand command = new RemoveFromShoppingCartCommand(TestUtilities.InitializeListWithSize(testSize), TestUtilities.InitializeRepository());

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }
    }
}
