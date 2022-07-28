using Cosmetics.Commands;
using Cosmetics.Commands.Contracts;
using Cosmetics.Core;
using Cosmetics.Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class CreateCategoryCommandTests
    {
        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new Repository();
        }

        [TestMethod]
        public void Should_CreateCategory_When_InputIsValid()
        {
            // Arrange
            IList<string> commandParameters = new string[] { "Category1" }.ToList();
            ICommand command = new CreateCategoryCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.AreEqual(1, repository.Categories.Count);
            Assert.AreEqual("Category1", repository.Categories[0].Name);
        }

        [TestMethod]
        public void Should_ThrowException_When_InvalidArgumentsCount()
        {
            // Arrange
            IList<string> commandParameters = new string[] { "Category1", "InvalidParam" }.ToList();
            ICommand command = new CreateCategoryCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }

        [TestMethod]
        public void Should_ThrowException_When_NameExists()
        {
            // Arrange
            IList<string> commandParameters = new string[] { "Category1" }.ToList();
            ICommand command = new CreateCategoryCommand(commandParameters, repository);
            command.Execute();

            ICommand commandCategoryExists = new CreateCategoryCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => commandCategoryExists.Execute());
        }

    }
}
