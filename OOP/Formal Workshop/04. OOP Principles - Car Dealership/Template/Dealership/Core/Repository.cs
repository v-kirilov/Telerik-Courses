using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models;
using Dealership.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Dealership.Core
{
    public class Repository : IRepository
    {
        private readonly List<IUser> users;

        public Repository()
        {
            this.users = new List<IUser>();
            this.LoggedUser = null;
        }

        public List<IUser> Users
        {
            get
            {
                var usersCopy = new List<IUser>(this.users);
                return usersCopy;
            }
        }

        public IUser LoggedUser
        {
            get;
            private set;
        }

        public IUser CreateUser(string username, string firstName, string lastName, string password, Role role)
        {
            return new User(username, firstName, lastName, password, role);
        }

        public void AddUser(IUser user)
        {
            if (!this.users.Contains(user))
            {
                this.users.Add(user);
            }
        }

        public bool UserExist(string username)
        {
            bool result = false;
            foreach (IUser user in this.users)
            {
                if (user.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public IUser GetUser(string username)
        {
            foreach (IUser user in this.users)
            {
                if (user.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase))
                {
                    return user;
                }
            }
            throw new EntityNotFoundException($"There is no user with username {username}!");
        }

        public void LogUser(IUser user)
        {
            this.LoggedUser = user;
        }

        public void LogOutUser()
        {
            this.LoggedUser = null;
        }

        public ICar CreateCar(string make, string model, decimal price, int seats)
        {
            return new Car(make, model, price, seats);
        }

        public IMotorcycle CreateMotorcycle(string make, string model, decimal price, string category)
        {
            return new Motorcycle(make, model, price, category);
        }

        public ITruck CreateTruck(string make, string model, decimal price, int weightCapacity)
        {
            return new Truck(make, model, price, weightCapacity);
        }

        public IComment CreateComment(string content, string author)
        {
            return new Comment(content, author);
        }
    }
}
