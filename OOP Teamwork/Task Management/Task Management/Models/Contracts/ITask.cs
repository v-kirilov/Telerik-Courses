using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface ITask : ICommentable
    {
        int Id { get; }

        string Title { get; }

        string Description { get; }

        IList<IEventLog> History { get; }

        string ViewHistory();
    }
}
