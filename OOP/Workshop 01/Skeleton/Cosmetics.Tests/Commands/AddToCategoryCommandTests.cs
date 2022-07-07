using System;
using Cosmetics.Commands;
using Cosmetics.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class AddToCategoryCommandTests
    {
        [TestMethod]
        [DataRow(AddToCategoryCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(AddToCategoryCommand.ExpectedNumberOfArguments + 1)]
        public void Should_ThrowException_When_ArgumentCountDifferentThanExpected(int testSize)
        {
            // Arrange
            AddToCategoryCommand command = new AddToCategoryCommand(TestUtilities.InitializeListWithSize(testSize), TestUtilities.InitializeRepository());

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }
    }
}
