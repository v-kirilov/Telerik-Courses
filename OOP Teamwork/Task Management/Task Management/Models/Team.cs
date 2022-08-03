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

        public void AddBoard(IBoard board)
        {
            this.boards.Add(board);
        }
        public void AddBoard(string boardName)
        {
            var board = new Board(boardName);
            this.boards.Add(board);
        }

        public void AddMember(IMember member)
        {
            this.members.Add(member);
        }
        public void AddMember(string memberName)
        {
            var member = new Member(memberName);
            this.members.Add(member);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name}");

            return sb.ToString();
        }
    }
}
