using Cosmetics.Commands;
using Cosmetics.Core;
using Cosmetics.Tests.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Cosmetics.Core.Contracts;
using Cosmetics.Commands.Contracts;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class CreateCategoryCommandTests
    {
        private IRepository repository;

        [TestInitialize]
        public void SetUp()
        {
            repository = TestUtilities.InitializeRepository();
        }

        [TestMethod]
        [DataRow(CreateCategoryCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(CreateCategoryCommand.ExpectedNumberOfArguments + 1)]
        public void Should_ThrowException_When_ArgumentCountDifferentThanExpected(int testSize)
        {
            // Arrange
            ICommand command = new CreateCategoryCommand(TestUtilities.InitializeListWithSize(testSize), repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }

        [TestMethod]
        public void Should_ThrowException_When_CategoryWithSameNameExists()
        {
            // Arrange
            List<string> commandParameters = new List<string>() { TestData.CategoryData.ValidName };
            ICommand command = new CreateCategoryCommand(commandParameters, repository);
            command.Execute();

            // Act, Assert
            ICommand commandCategoryExists = new CreateCategoryCommand(commandParameters, repository);
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }

        [TestMethod]
        public void Should_Create_When_RequirementsAreSatisfied()
        {
            ICommand command = new CreateCategoryCommand(new List<string>() { TestData.CategoryData.ValidName }, repository);
            command.Execute();

            // Arrange, Act, Assert
            Assert.AreEqual(1, repository.Categories.Count);
        }
    }
}
