using ForumSystem.Data;
using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private const string InvalidNameErrorMessage = "This role name does not exist";
        private readonly ApplicationContext context;

        public RolesRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public List<Role> GetAll()
        {
            var roles = this.GetRoles().ToList();
            return roles;
        }

        public Role GetById(int id)
        {
            var role = this.GetRoles().FirstOrDefault(r => r.Id == id);
            return role ?? throw new EntityNotFoundException();
        }

        public Role GetByName(string name)
        {
            var role = this.GetRoles().FirstOrDefault(r => r.Name == name);
            return role ?? throw new EntityNotFoundException(InvalidNameErrorMessage);
        }

        private IQueryable<Role> GetRoles()
        {
            return this.context.Roles
                    .Include(role => role.Users);
        }
    }
}
