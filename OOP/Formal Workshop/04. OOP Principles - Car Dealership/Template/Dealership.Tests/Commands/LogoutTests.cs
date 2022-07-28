using Dealership.Commands.Contracts;
using Dealership.Core;
using Dealership.Core.Contracts;
using Dealership.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Tests.Commands
{
    [TestClass]
    public class LogoutTests
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
        public void Execute_Should_LogoutUser()
        {
            // Arrange
            User user = new User("username", "first", "last", "password", Role.Normal);
            this.repository.AddUser(user);
            this.repository.LogUser(user);
            ICommand logout = this.commandFactory.Create("Logout");

            // Act
            logout.Execute();

            // Assert
            Assert.IsNull(this.repository.LoggedUser);
        }
    }
}
