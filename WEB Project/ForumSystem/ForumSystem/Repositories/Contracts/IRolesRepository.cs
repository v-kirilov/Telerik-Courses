using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Repositories.Contracts
{
    public interface IRolesRepository
    {
        List<Role> GetAll();
        Role GetById(int id);
        Role GetByName(string name);
    }
}
