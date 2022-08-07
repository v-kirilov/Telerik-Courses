using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Models.Contracts
{
    public interface IStory : ITask
    {
        Priority Priority { get; set; }

        Size Size { get; set; }

        Status Status { get; set; }

        IMember Assignee { get; set; }
    }
}
