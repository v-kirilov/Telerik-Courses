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
    public class CreateToothpasteCommandTests
    {
        private readonly string[] CreateToothpasteParameters = new string[] { "White", "Colgate", "10.99", "Men", "calcium,fluorid" };

        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new Repository();
        }

        [TestMethod]
        public void CreateToothpaste_Should_AddProduct_When_InputIsValid()
        {
            // Arrange
            ICommand createToothpaste = new CreateToothpasteCommand(CreateToothpasteParameters.ToList(), repository);

            // Act
            createToothpaste.Execute();

            // Assert
            Assert.AreEqual(1, repository.Products.Count);
        }

        [TestMethod]
        public void Should_ThrowException_When_ToothpasteNameExists()
        {
            // Arrange
            ICommand createToothpaste = new CreateToothpasteCommand(CreateToothpasteParameters.ToList(), repository);
            createToothpaste.Execute();

            ICommand createDuplicateToothpaste = new CreateToothpasteCommand(CreateToothpasteParameters.ToList(), repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => createDuplicateToothpaste.Execute());
        }

        [TestMethod]
        public void Should_ThrowException_When_InvalidArgumentsCount()
        {
            // Arrange
            IList<string> commandParameters = new string[] { "White", "Colgate", }.ToList();
            ICommand createToothpasteWrongInput = new CreateToothpasteCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => createToothpasteWrongInput.Execute());
        }
    }
}
