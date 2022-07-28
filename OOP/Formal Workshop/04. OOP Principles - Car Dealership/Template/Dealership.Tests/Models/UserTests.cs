using Dealership.Models;
using Dealership.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dealership.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_Should_ImplementIUserInterface()
        {
            var type = typeof(User);
            var isAssignable = typeof(IUser).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "User class does not implement IUser interface!");
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_UsernameLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("u", "firstName", "lastName", "password", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_UsernameLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("abcdefghijklmnopqrstu", "firstName", "lastName", "password", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_UsernameIsInvalid()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("U$$ernam3", "firstName", "lastName", "password", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_FirstNameLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("username", "f", "lastName", "password", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_FirstNameLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("username", "abcdefghijklmnopqrstu", "lastName", "password", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_LastNameLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("username", "firstName", "l", "password", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_LastNameLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("username", "firstName", "abcdefghijklmnopqrstu", "password", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PasswordLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("username", "firstName", "lastName", "pass", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PasswordLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("username", "firstName", "lastName", "passwordpasswordpasswordpassword", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_PasswordIsInvalid()
        {
            Assert.ThrowsException<ArgumentException>(() => new User("username", "firstName", "lastName", "Pa-$$_w0rD", Role.Normal));
        }

        [TestMethod]
        public void Constructor_Should_CreateNewUser_When_ParametersAreCorrect()
        {
            // Arrange, Act
            User user = new User("username", "firstName", "lastName", "password", Role.Normal);

            // Assert
            Assert.AreEqual("username", user.Username);
        }

        [TestMethod]
        public void Vehicles_Should_ReturnCopyOfTheCollection()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.Normal);

            // Act
            user.Vehicles.Add(new Car("make", "model", 10, 5));

            // Assert
            Assert.AreEqual(0, user.Vehicles.Count);
        }

        [TestMethod]
        public void AddComment_Should_AddCommentToTheCollection()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.Normal);
            Car car = new Car("make", "model", 10, 5);
            user.AddVehicle(car);

            // Act
            user.AddComment(new Comment("content", "author"), car);

            // Assert
            Assert.AreEqual(1, car.Comments.Count);
        }

        [TestMethod]
        public void AddVehicle_Should_AddVehicle_WhenNormalUser()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.Normal);

            // Act
            user.AddVehicle(new Car("make", "model", 10, 5));

            // Assert
            Assert.AreEqual(1, user.Vehicles.Count);
        }

        [TestMethod]
        public void AddVehicle_Should_ThrowException_WhenNormalUserReachedLimit()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.Normal);
            user.AddVehicle(new Car("make1", "model1", 10, 5));
            user.AddVehicle(new Car("make2", "model2", 10, 5));
            user.AddVehicle(new Car("make3", "model3", 10, 5));
            user.AddVehicle(new Car("make4", "model4", 10, 5));
            user.AddVehicle(new Car("make5", "model5", 10, 5));

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => user.AddVehicle(new Car("make6", "model6", 10, 5)));
        }

        [TestMethod]
        public void AddVehicle_Should_ThrowException_WhenAdminUser()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.Admin);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => user.AddVehicle(new Car("make", "model", 10, 5)));
        }

        [TestMethod]
        public void AddVehicle_Should_AddVehicle_WhenVipUser()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.VIP);

            // Act
            user.AddVehicle(new Car("make", "model", 10, 5));

            // Assert
            Assert.AreEqual(1, user.Vehicles.Count);
        }

        [TestMethod]
        public void AddVehicle_Should_AddVehicle_WhenVipUserAndMoreThan5Vehicles()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.VIP);
            user.AddVehicle(new Car("make1", "model1", 10, 5));
            user.AddVehicle(new Car("make2", "model2", 10, 5));
            user.AddVehicle(new Car("make3", "model3", 10, 5));
            user.AddVehicle(new Car("make4", "model4", 10, 5));
            user.AddVehicle(new Car("make5", "model5", 10, 5));

            // Act
            user.AddVehicle(new Car("make6", "model6", 10, 5));

            // Assert
            Assert.AreEqual(6, user.Vehicles.Count);
        }

        [TestMethod]
        public void RemoveComment_Should_RemoveComment_When_AuthorMatch()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.Normal);
            Car car = new Car("make", "model", 10, 5);
            Comment comment = new Comment("content", "username");
            user.AddVehicle(car);
            user.AddComment(comment, car);

            // Act
            user.RemoveComment(comment, car);

            // Assert
            Assert.AreEqual(0, car.Comments.Count);
        }

        [TestMethod]
        public void RemoveComment_Should_ThrowException_When_AuthorNotMatch()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.Normal);
            Car car = new Car("make", "model", 10, 5);
            Comment comment = new Comment("content", "author");
            user.AddVehicle(car);
            user.AddComment(comment, car);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() => user.RemoveComment(comment, car));
        }

        [TestMethod]
        public void RemoveVehicle_Should_RemoveVehicle_FromTheCollection()
        {
            // Arrange
            User user = new User("username", "firstName", "lastName", "password", Role.Normal);
            Car car = new Car("make", "model", 10, 5);
            user.AddVehicle(car);
            
            // Act
            user.RemoveVehicle(car);

            // Assert
            Assert.AreEqual(0, user.Vehicles.Count);
        }
    }
}
