using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Services.Contracts
{
    public interface IRolesService
    {
        List<Role> GetAll();
        Role GetById(int id);
        Role GetByName(string name);
    }
}
