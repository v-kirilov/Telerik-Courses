using Cosmetics.Commands;
using Cosmetics.Models;
using Cosmetics.Tests.Helpers;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class CreateProductCommandTests
    {
        private IRepository repository;

        [TestInitialize]
        public void SetUp()
        {
            repository = TestUtilities.InitializeRepository();
        }

        [TestMethod]
        [DataRow(CreateProductCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(CreateProductCommand.ExpectedNumberOfArguments + 1)]
        public void Should_ThrowException_When_ArgumentCountDifferentThanExpected(int testSize)
        {
            // Arrange
            ICommand command = new CreateProductCommand(TestUtilities.InitializeListWithSize(testSize), repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => command.Execute());
        }

        [TestMethod]
        public void Should_ThrowException_When_ProductWithSameNameExists()
        {
            // Arrange
            List<string> commandParameters = new List<string>() { TestData.ProductData.ValidName, TestData.ProductData.ValidBrand, "2", GenderType.Unisex.ToString() };
            ICommand command = new CreateProductCommand(commandParameters, repository);
            command.Execute();

            // Act, Assert
            ICommand commandProductExists = new CreateProductCommand(commandParameters, repository);
            Assert.ThrowsException<ArgumentException>(() => commandProductExists.Execute());
        }

        [TestMethod]
        public void Should_Create_When_RequirementsAreSatisfied()
        {
            // Arrange
            ICommand command = new CreateProductCommand(new List<string>() { TestData.ProductData.ValidName, TestData.ProductData.ValidBrand, "2", GenderType.Unisex.ToString() }, repository);
            command.Execute();

            // Act, Assert
            Assert.AreEqual(1, repository.Products.Count);
        }
    }
}
