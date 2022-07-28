using Cosmetics.Commands;
using Cosmetics.Commands.Contracts;
using Cosmetics.Core;
using Cosmetics.Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmetics.Tests.Commands
{
    [TestClass]
    public class CreateShampooCommandTests
    {
        private readonly string[] CreateShampooParameters = new string[] { "MyMan", "Nivea", "10.99", "Men", "1000", "EveryDay" };

        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new Repository();
        }

        [TestMethod]
        public void Should_AddProduct_When_InputIsValid()
        {
            // Arrange
            ICommand createShampoo = new CreateShampooCommand(CreateShampooParameters.ToList(), repository);

            // Act
            createShampoo.Execute();

            // Assert
            Assert.AreEqual(1, repository.Products.Count);
        }

        [TestMethod]
        public void Should_ThrowException_When_ShampooNameExists()
        {
            // Arrange
            ICommand createShampoo = new CreateShampooCommand(CreateShampooParameters.ToList(), repository);
            createShampoo.Execute();

            ICommand createDuplicateShampoo = new CreateShampooCommand(CreateShampooParameters.ToList(), repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => createDuplicateShampoo.Execute());
        }

        [TestMethod]
        public void Should_ThrowException_When_InvalidArgumentsCount()
        {
            // Arrange
            IList<string> commandParameters = new string[] { "MyMan", "Nivea", }.ToList();
            ICommand createShampooWrongInput = new CreateShampooCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => createShampooWrongInput.Execute());
        }
    }
}
