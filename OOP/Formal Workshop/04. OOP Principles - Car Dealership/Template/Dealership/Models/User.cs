
using Dealership.Models.Contracts;
using System.Linq;
using System.Collections.Generic;
using System;
using Dealership.Exceptions;
using System.Text;
using System.Drawing;

namespace Dealership.Models
{
    public class User : IUser
    {
        private const string UsernamePattern = "^[A-Za-z0-9]+$";
        private const string InvalidUsernameFormatError = "Username contains invalid symbols!";
        private const string InvalidUsernameLengthError = "Username must be between 2 and 20 characters long!";

        private const int NameMinLength = 2;
        private const int NameMaxLength = 20;
        private const string InvalidNameError = "name must be between 2 and 20 characters long!";

        private const int PasswordMinLength = 5;
        private const int PasswordMaxLength = 30;
        private const string PasswordPattern = "^[A-Za-z0-9@*_-]+$";
        private const string InvalidPasswordFormatError = "Username contains invalid symbols!";
        private const string InvalidPasswordLengthError = "Password must be between 5 and 30 characters long!";

        private const int MaxVehiclesToAdd = 5;

        private const string NotAnVipUserVehiclesAdd = "You are not VIP and cannot add more than {0} vehicles!";
        private const string AdminCannotAddVehicles = "You are an admin and therefore cannot add vehicles!";
        private const string YouAreNotTheAuthor = "You are not the author of the comment you are trying to remove!";
        private const string NoVehiclesHeader = "--NO VEHICLES--";


        private string username;
        private string firstName;
        private string lastName;
        public string password;
        private Role role;
        private readonly List<IVehicle> vehicles;



        public User(string username, string firstName, string lastName, string password, Role role)
        {
            Validator.ValidateSymbols(username, UsernamePattern, InvalidUsernameFormatError);
            Validator.ValidateIntRange(username.Length, NameMinLength, NameMaxLength, InvalidUsernameLengthError);

            Validator.ValidateIntRange(firstName.Length, NameMinLength, NameMaxLength, "Firstname must be between 2 and 20 characters long!");
            Validator.ValidateIntRange(lastName.Length, NameMinLength, NameMaxLength, "Lastname must be between 2 and 20 characters long!");

            Validator.ValidateSymbols(password, PasswordPattern, InvalidPasswordFormatError);
            Validator.ValidateIntRange(password.Length, PasswordMinLength, PasswordMaxLength, InvalidPasswordLengthError);


            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Role = role;
            this.vehicles = new List<IVehicle>();

        }

        public string Username
        {
            get;

        }

        public string FirstName
        {
            get;

        }

        public string LastName
        {
            get;

        }

        public string Password
        {
            get;

        }
        public Role Role
        {
            get
            {
                return this.role;
            }
            private set
            {
                this.role = value;
            }
        }

        public IList<IVehicle> Vehicles
        {

            get
            {
                return new List<IVehicle>(this.vehicles);
            }

        }

        public void AddVehicle(IVehicle vehicle)
        {
            if (this.role == Role.Admin)
            {
                throw new ArgumentException("You are an admin and therefore cannot add vehicles!");
            }
            if (this.role != Role.VIP && this.vehicles.Count >= 5)
            {
                throw new ArgumentException("You are not VIP and cannot add more than 5 vehicles!");

            }
            this.vehicles.Add(vehicle);
        }

        public void RemoveVehicle(IVehicle vehicle)
        {
            if (this.vehicles.Count<=0)
            {
                throw new ArgumentException("User has no vehicles.");
            }
            this.vehicles.Remove(vehicle);
        }

        public void AddComment(IComment commentToAdd, IVehicle vehicleToAddComment)
        {
            foreach (var vehicle in this.vehicles)
            {
                if (vehicle == vehicleToAddComment)
                {
                    vehicle.AddComment(commentToAdd);
                    return;
                }
            }

        }

        public void RemoveComment(IComment commentToRemove, IVehicle vehicleToRemoveComment)
        {
            if (this.Username != commentToRemove.Author)
            {
                throw new ArgumentException("You are not the author of the comment you are trying to remove!");
            }

            vehicleToRemoveComment.RemoveComment(commentToRemove);

        }

        public string PrintVehicles()
        {
            int counter = 1;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"--USER {this.Username}--");
            if (this.vehicles.Count <= 0)
            {
                sb.AppendLine($"--NO VEHICLES--");
            }else
            {
                foreach (var vehicle in this.vehicles)
                {

                    sb.AppendLine($"{counter}. {vehicle.MyType()}:");
                    sb.Append(vehicle);

                    counter++;
                }
            }
            
            return sb.ToString();
        }

        public override string ToString()
        {
            return (($"Username: {this.Username}, FullName: {this.FirstName} {this.LastName}, Role: {this.Role}"));
        }
    }
}
