using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Core.Contracts
{
    public interface IRepository
    {
        List<ITeam> Teams { get; }
        

        public ITeam CreateTeam(string name);
        

       //TO DO
       //Do we need Tasks in here?
    }
}
