using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public class Team : ITeam
    {

        public const int NameMinLength = 5;
        public const int NameMaxLength = 15;
        public const string InvalidNameError = "Name must be between 5 and 15 characters long!";

        private readonly List<IBoard> boards = new List<IBoard>();
        private readonly List<IMember> members = new List<IMember>();


        public Team(string name)
        {
            Validator.ValidateIntRange(name.Length,NameMinLength,NameMaxLength,InvalidNameError);
            this.Name = name;
            //this.boards = boards;
            //this.members = members;

            
        }

        public string Name { get; }

        public IList<IBoard> Boards
        {
            get
            {
                return new List<IBoard>(boards);
            }
        }


        public IList<IMember> Members
        {
            get
            {
                return new List<IMember>(members);
            }
        }


    }
}
