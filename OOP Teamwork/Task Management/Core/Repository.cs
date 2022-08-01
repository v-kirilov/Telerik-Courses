using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;

namespace Task_Management.Core
{
    public class Repository : IRepository
    {
        private int currentId;

        private readonly List<ITeam> teams = new List<ITeam>();

        public Repository()
        {
            this.currentId = 0;
        }
        public List<ITeam> Teams
        {
            get
            {
                return new List<ITeam>(teams);
            }
        }

        public ITeam CreateTeam(string name)
        {
            if (this.teams.Any(x=>x.Name == name)==true) //==true is not necessary but added for readability
            {
                throw new InvalidUserInputException($"Team with name:{name} already exists! Please choose a different name.");
            }
            var team = new Team(name);
            this.teams.Add(team);
            return team;
        }
    }
}
