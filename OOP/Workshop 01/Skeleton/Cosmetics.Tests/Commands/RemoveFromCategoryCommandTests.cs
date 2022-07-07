using System;
using Cosmetics.Commands;
using Cosmetics.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class RemoveFromCategoryCommandTests
    {
        [TestMethod]
        [DataRow(RemoveFromCategoryCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(RemoveFromCategoryCommand.ExpectedNumberOfArguments + 1)]
        public void Should_ThrowException_When_ArgumentCountDifferentThanExpected(int testSize)
        {
            // Arrange
            RemoveFromCategoryCommand command = new RemoveFromCategoryCommand(TestUtilities.InitializeListWithSize(testSize), TestUtilities.InitializeRepository());

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }
    }
}
