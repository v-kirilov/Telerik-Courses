using ForumSystem.Data;
using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Models.Enums;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IRolesRepository rolesRepository;
        private readonly ApplicationContext context;
        private readonly IPhoneNumbersRepository phoneNumbersRepository;

        public UsersRepository(IRolesRepository rolesRepository, ApplicationContext context, IPhoneNumbersRepository phoneNumbersRepository)
        {
            this.rolesRepository = rolesRepository;
            this.context = context;
            this.phoneNumbersRepository = phoneNumbersRepository;
        }

        public List<User> GetAll()
        {
            return this.GetUsers().ToList();
        }

        public User GetById(int id)
        {
            User user = this.GetUsers().Where(u => u.Id == id).FirstOrDefault();

            return user ?? throw new EntityNotFoundException($"User with id:{id} does not exist");
        }

        public User GetByUsername(string username)
        {
            User user = this.GetUsers().Where(u => u.Username == username).FirstOrDefault();

            return user ?? throw new EntityNotFoundException($"User with username:{username} does not exist");
        }

        public User GetByEmail(string email)
        {
            User user = this.GetUsers().Where(u => u.Email == email).FirstOrDefault();

            return user ?? throw new EntityNotFoundException($"User with email:{email} does not exist");
        }

        public User GetByFirstName(string firstname)
        {
            User user = this.GetUsers().Where(u => u.FirstName == firstname).FirstOrDefault();

            return user ?? throw new EntityNotFoundException($"User with first name:{firstname} does not exist");
        }

        public PaginatedList<User> FilterBy(UserQueryParameters filterParameters)
        {
            IQueryable<User> result = this.GetUsers();
            result = FilterByUsername(result, filterParameters.Username);
            result = FilterByEmail(result, filterParameters.Email);
            result = FilterByFirstName(result, filterParameters.FirstName);
            result = SortBy(result, filterParameters.SortBy);
            result = Order(result, filterParameters.SortOrder);
            int totalPages = (result.Count() + 1) / filterParameters.PageSize;
            result = Paginate(result, filterParameters.PageNumber, filterParameters.PageSize);

            return new PaginatedList<User>(result.ToList(), totalPages, filterParameters.PageNumber);
        }

        public User Create(User user)
        {
            user.Role = this.rolesRepository.GetByName("Default");
            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User UpdateFirstName(int id, string firstName)
        {
            var userToUpdate = this.GetById(id);
            userToUpdate.FirstName = firstName;
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public User UpdateLastName(int id, string lastName)
        {
            var userToUpdate = this.GetById(id);
            userToUpdate.LastName = lastName;
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public User UpdateProfilePicture(int id, string uniqueFileName)
        {
            var userToUpdate = this.GetById(id);
            userToUpdate.ProfilePicture = uniqueFileName;
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public User UpdateEmail(int id, string email)
        {
            var userToUpdate = this.GetById(id);
            userToUpdate.Email = email;
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

        public User UpdateRole(int id, string roleName)
        {
            var userToUpdate = this.GetById(id);
            userToUpdate.Role = this.rolesRepository.GetByName(roleName);
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public User UpdatePhoneNumber(int id, PhoneNumber phoneNumber)
        {
            var userToUpdate = this.GetById(id);

            if (phoneNumber != null)
            {
                userToUpdate.PhoneNumber = this.phoneNumbersRepository.GetById(userToUpdate.Id, phoneNumber.Id);
            }
            else
            {
                userToUpdate.PhoneNumber = null;
            }
            
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public User UpdateIsBlocked(int id, bool isBlocked)
        {
            var userToUpdate = this.GetById(id);
            userToUpdate.IsBlocked = isBlocked;
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public void Delete(int id)
        {
            var existingUser = this.GetById(id);
            existingUser.Posts.Clear();
            existingUser.Comments.Clear();
            this.context.Users.Remove(existingUser);
            this.context.SaveChanges();
        }

        private static IQueryable<User> FilterByUsername(IQueryable<User> users, string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return users.Where(user => user.Username.Contains(username));
            }
            else
            {
                return users;
            }
        }

        private static IQueryable<User> FilterByEmail(IQueryable<User> users, string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return users.Where(user => user.Email.Contains(email));
            }
            else
            {
                return users;
            }
        }

        private static IQueryable<User> FilterByFirstName(IQueryable<User> users, string firstName)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                return users.Where(user => user.FirstName.Contains(firstName));
            }
            else
            {
                return users;
            }
        }

        private static IQueryable<User> SortBy(IQueryable<User> users, string sortCriteria)
        {
            if (string.IsNullOrEmpty(sortCriteria))
            {
                return users;
            }
            switch (sortCriteria.ToLower())
            {
                case "username":
                    return users.OrderBy(user => user.Username);
                case "email":
                    return users.OrderBy(user => user.Email);
                case "firstname":
                    return users.OrderBy(user => user.FirstName);
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
            return this.context.Users
                    .Include(user => user.Role)
                    .Include(user => user.PhoneNumber)
                    .Include(user => user.Posts)
                        .ThenInclude(post => post.Reactions)
                    .Include(user => user.Comments)
                        .ThenInclude(comment => comment.Post)
                            .ThenInclude(post => post.User)
                                .ThenInclude(comment => comment.CommentReactions)
                    .Include(user => user.CommentReactions)
                    .Include(user => user.PostReactios);
        }
    }
}
