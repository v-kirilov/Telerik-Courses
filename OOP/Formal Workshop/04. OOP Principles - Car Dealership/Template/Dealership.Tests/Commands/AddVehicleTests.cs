using Dealership.Commands.Contracts;
using Dealership.Core;
using Dealership.Core.Contracts;
using Dealership.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dealership.Tests.Commands
{
    [TestClass]
    public class AddVehicleTests
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
        public void Execute_Should_CreateNewCar_When_InputIsValid()
        {
            // Arrange
            ICommand createVehicle = this.commandFactory.Create("AddVehicle Car make model 10 5");

            // Act
            createVehicle.Execute();

            //Assert
            Car car = (Car)this.repository.LoggedUser.Vehicles[0];
            Assert.AreEqual("make", car.Make);
            Assert.AreEqual("model", car.Model);
            Assert.AreEqual(10, car.Price);
            Assert.AreEqual(5, car.Seats);
        }

        [TestMethod]
        public void Execute_Should_CreateNewMotorcycle_When_InputIsValid()
        {
            // Arrange
            ICommand createVehicle = this.commandFactory.Create("AddVehicle Motorcycle make model 10 category");

            // Act
            createVehicle.Execute();

            //Assert
            Motorcycle motorcycle = (Motorcycle)this.repository.LoggedUser.Vehicles[0];
            Assert.AreEqual("make", motorcycle.Make);
            Assert.AreEqual("model", motorcycle.Model);
            Assert.AreEqual(10, motorcycle.Price);
            Assert.AreEqual("category", motorcycle.Category);
        }

        [TestMethod]
        public void Execute_Should_CreateNewTruck_When_InputIsValid()
        {
            // Arrange
            ICommand createVehicle = this.commandFactory.Create("AddVehicle Truck make model 10 5");

            // Act
            createVehicle.Execute();

            //Assert
            Truck truck = (Truck)this.repository.LoggedUser.Vehicles[0];
            Assert.AreEqual("make", truck.Make);
            Assert.AreEqual("model", truck.Model);
            Assert.AreEqual(10, truck.Price);
            Assert.AreEqual(5, truck.WeightCapacity);
        }
    }
}
