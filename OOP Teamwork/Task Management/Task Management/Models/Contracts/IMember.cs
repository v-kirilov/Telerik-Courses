using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface IMember : INameable
    {
        IList<ITask> Tasks { get; }
        IList<IEventLog> EventLogs { get; }

        void AddTask(ITask task);
        void RemoveTask(ITask task);

        string ViewActivity();
    }
}
