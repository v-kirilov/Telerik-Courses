using MatchScore.Data;
using MatchScore.Exceptions;
using MatchScore.Models;
using MatchScore.QueryParameters;
using MatchScore.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private const string NotFoundUserIdErrorMessage = "User with id:{0} does not exist";
        private const string NotFoundUserEmailErrorMessage = "User with email:{0} does not exist";

        private readonly ApplicationContext context;
        private readonly IRolesRepository rolesRepository;

        public UsersRepository(ApplicationContext context, IRolesRepository rolesRepository)
        {
            this.context = context;
            this.rolesRepository = rolesRepository;
        }

        public List<User> GetAll()
        {
            return this.GetUsers().ToList();
        }

        public User GetById(int id)
        {
            User user = this.GetUsers().Where(u => u.Id == id).FirstOrDefault();

            return user ?? throw new EntityNotFoundException(string.Format(NotFoundUserIdErrorMessage, id));
        }

        public User GetByEmail(string email)
        {
            User user = this.GetUsers().Where(u => u.Email == email).FirstOrDefault();

            return user ?? throw new EntityNotFoundException(string.Format(NotFoundUserEmailErrorMessage, email));
        }

        public PaginatedList<User> FilterBy(UserQueryParameters filterParameters)
        {
            IQueryable<User> result = this.GetUsers();
            result = FilterByEmail(result, filterParameters.Email);
            result = FilterByRole(result, filterParameters.Role);
            result = SortBy(result, filterParameters.SortBy);
            result = Order(result, filterParameters.SortOrder);

            double totalPages = Math.Ceiling((1.0 * result.Count()) / filterParameters.PageSize);
            result = Paginate(result, filterParameters.PageNumber, filterParameters.PageSize);

            return new PaginatedList<User>(result.ToList(), (int)totalPages, filterParameters.PageNumber);
        }

        public User Create(User user)
        {
            var role = this.rolesRepository.GetByName("Default");
            user.RoleId = role.Id;
            user.Role = role;
            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User UpdateRole(int id, string roleName)
        {
            var role = this.rolesRepository.GetByName(roleName);
            var userToUpdate = this.GetById(id);
            userToUpdate.RoleId = role.Id;
            userToUpdate.Role = role;
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public User UpdatePassword(int id, string password)
        {
            var userToUpdate = this.GetById(id);
            userToUpdate.Password = password;
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public void Delete(int id)
        {
            var existingUser = this.GetById(id);
            existingUser.IsDeleted = true;

            this.context.Update(existingUser);
            this.context.SaveChanges();
        }

        private static IQueryable<User> FilterByEmail(IQueryable<User> users, string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return users.Where(user => user.Email.Contains(email));
            }

            return users;
        }

        private static IQueryable<User> FilterByRole(IQueryable<User> users, string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                return users.Where(user => user.Role.Name.Contains(roleName));
            }

            return users;
        }

        private static IQueryable<User> SortBy(IQueryable<User> users, string sortCriteria)
        {
            if (string.IsNullOrEmpty(sortCriteria))
            {
                return users;
            }
            switch (sortCriteria.ToLower())
            {
                case "email":
                    return users.OrderBy(user => user.Email);
                case "role":
                    return users.OrderBy(user => user.Role.Name);
                default:
                    return users;
            }
        }

        private static IQueryable<User> Order(IQueryable<User> users, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                return users;
            }
            switch (sortOrder.ToLower())
            {
                case "desc":
                    return users.Reverse();
                default:
                    return users;
            }
        }

        private static IQueryable<User> Paginate(IQueryable<User> users, int pageNumber, int pageSize)
        {
            return users.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        private IQueryable<User> GetUsers()
        {
            return this.context.Users.Where(u => u.IsDeleted == false)
                        .Include(u => u.Role)
                        .Include(u => u.Player)
                        .Include(u => u.Requests)
                        .Include(u => u.Matches)
                            .ThenInclude(m => m.Scores)
                                .ThenInclude(s => s.Player)
                        .Include(u => u.Tournaments);
        }

    }
}
