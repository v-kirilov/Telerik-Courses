using Dealership.Commands.Contracts;
using Dealership.Core;
using Dealership.Core.Contracts;
using Dealership.Models;
using Dealership.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dealership.Tests.Commands
{
    [TestClass]
    public class AddCommentTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;
        private User user;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
            this.user = new User("username", "first", "last", "password", Role.Normal);
            this.repository.AddUser(this.user);
            this.repository.LogUser(this.user);
        }

        [TestMethod]
        public void Execute_ShouldCreateNewComment_When_InputIsValid()
        {
            // Arrange
            this.user.AddVehicle(new Truck("make", "model", 10, 5));
            ICommand addComment = this.commandFactory.Create("AddComment content username 1");

            // Act
            addComment.Execute();

            //Assert
            IComment comment = this.repository.LoggedUser.Vehicles[0].Comments[0];
            Assert.AreEqual("content", comment.Content);
            Assert.AreEqual("username", comment.Author);
        }
    }
}
