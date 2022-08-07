﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface IBoard :INameable
    {
        IList<ITask> Tasks { get; }
        IList<IEventLog> EventLogs { get; }

        void AddTask(ITask task);

        string ViewActivity();

    }
}
