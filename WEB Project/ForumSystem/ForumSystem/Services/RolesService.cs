using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository rolesRepository;

        public RolesService(IRolesRepository rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public List<Role> GetAll()
        {
            return this.rolesRepository.GetAll();
        }

        public Role GetById(int id)
        {
            return this.rolesRepository.GetById(id);
        }

        public Role GetByName(string name)
        {
            return this.rolesRepository.GetByName(name);
        }
    }
}
