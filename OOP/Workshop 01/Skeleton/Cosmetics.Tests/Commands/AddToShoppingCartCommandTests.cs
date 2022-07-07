using System;
using Cosmetics.Commands;
using Cosmetics.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class AddToShoppingCartCommandTests
    {
        [TestMethod]
        [DataRow(AddToShoppingCartCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(AddToShoppingCartCommand.ExpectedNumberOfArguments + 1)]
        public void Should_ThrowException_When_ArgumentCountDifferentThanExpected(int testSize)
        {
            // Arrange
            AddToShoppingCartCommand command = new AddToShoppingCartCommand(TestUtilities.InitializeListWithSize(testSize), TestUtilities.InitializeRepository());

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }
    }
}
