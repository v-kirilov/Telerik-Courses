using Dealership.Commands.Contracts;
using Dealership.Core;
using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dealership.Tests.Commands
{
    [TestClass]
    public class LoginTests
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
        public void Execute_Should_LoginUser_When_UserNotLoggedIn()
        {
            // Arrange
            User user = new User("username", "first", "last", "password", Role.Normal);
            this.repository.AddUser(user);
            ICommand login = this.commandFactory.Create("Login username password");

            // Act
            login.Execute();

            // Assert
            Assert.AreEqual(user.Username, this.repository.LoggedUser.Username);
        }

        [TestMethod]
        public void Execute_Should_Throw_When_PasswordIsWrong()
        {
            // Arrange
            User user = new User("username", "first", "last", "password", Role.Normal);
            this.repository.AddUser(user);
            ICommand login = this.commandFactory.Create("Login username foo");

            // Act, Assert
            Assert.ThrowsException<AuthorizationException>(() => login.Execute());
        }

        [TestMethod]
        public void Execute_Should_Throw_When_UserDoesNotExists()
        {
            // Arrange
            ICommand login = this.commandFactory.Create("Login username password");

            // Act, Assert
            Assert.ThrowsException<EntityNotFoundException>(() => login.Execute());
        }

        [TestMethod]
        public void Execute_Should_Throw_When_UserAlreadyLoggedIn()
        {
            // Arrange
            User user = new User("username", "first", "last", "password", Role.Normal);
            this.repository.AddUser(user);
            ICommand login = this.commandFactory.Create("Login username password");
            this.repository.LogUser(user);
            
            // Act, Assert
            Assert.ThrowsException<AuthorizationException>(() => login.Execute());
        }
    }
}
