using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface ITeam :INameable
    {

        IList<IBoard> Boards { get; }
        IList<IMember> Members { get; }

        public void AddBoard(string boardName);
        public void AddBoard(IBoard board);
        public void AddMember(string memberName);
        public void AddMember(IMember member);

    }
}
