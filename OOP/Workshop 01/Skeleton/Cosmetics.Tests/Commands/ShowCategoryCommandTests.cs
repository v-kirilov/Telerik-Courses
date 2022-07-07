using System;
using Cosmetics.Commands;
using Cosmetics.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class ShowCategoryCommandTests
    {
        [TestMethod]
        [DataRow(ShowCategoryCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(ShowCategoryCommand.ExpectedNumberOfArguments + 1)]
        public void Should_ThrowException_When_ArgumentCountDifferentThanExpected(int testSize)
        {
            // Arrange
            ShowCategoryCommand command = new ShowCategoryCommand(TestUtilities.InitializeListWithSize(testSize), TestUtilities.InitializeRepository());

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }
    }
}