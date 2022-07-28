using Dealership.Models;
using Dealership.Models.Contracts;
using System.Collections.Generic;

namespace Dealership.Core.Contracts
{
    public interface IRepository
    {
        List<IUser> Users { get; }

        IUser LoggedUser { get; }

        public IUser CreateUser(string username, string firstName, string lastName, string password, Role role);

        void AddUser(IUser user);

        public bool UserExist(string username);

        IUser GetUser(string username);

        void LogUser(IUser user);

        public void LogOutUser();

        public ICar CreateCar(string make, string model, decimal price, int seats);

        public IMotorcycle CreateMotorcycle(string make, string model, decimal price, string category);

        public ITruck CreateTruck(string make, string model, decimal price, int weightCapacity);

        public IComment CreateComment(string content, string author);
    }
}
