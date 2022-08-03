using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface ICommentable
    {
        IList<IComment> Comments { get; }

        void AddComment(IComment comment);

        void RemoveComment(IComment comment);
    }
}
