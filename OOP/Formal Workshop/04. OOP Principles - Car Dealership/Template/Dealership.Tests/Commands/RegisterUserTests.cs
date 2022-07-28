using Dealership.Commands.Contracts;
using Dealership.Core;
using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dealership.Tests.Commands
{
    [TestClass]
    public class RegisterUserTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
        }

        [TestMethod]
        public void Execute_Should_Register_When_InputIsValid()
        {
            // Arrange
            ICommand registerCommand = this.commandFactory.Create("RegisterUser username first last password");

            // Act
            registerCommand.Execute();

            //Assert
            Assert.AreEqual(1, this.repository.Users.Count);
            Assert.AreEqual("username", this.repository.Users[0].Username);

        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_UserAlreadyExist()
        {
            // Arrange
            this.repository.AddUser(new User("username", "first", "last", "password", Role.Normal));
            ICommand registerCommand = this.commandFactory.Create("RegisterUser username first last password");

            // Act, Assert
            Assert.ThrowsException<AuthorizationException>(() => registerCommand.Execute());
        }
    }
}
