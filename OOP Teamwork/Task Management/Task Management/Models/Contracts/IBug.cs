using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Enums.Bug;

namespace Task_Management.Models.Contracts
{
    public interface IBug : ITask
    {
        string Steps { get; }

        Priority Priority { get; }
        
        Severity Severity { get; }

        Status Status { get; }

        IMember Assignee { get; }
    }
}
