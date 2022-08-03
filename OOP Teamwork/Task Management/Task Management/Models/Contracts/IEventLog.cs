using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface IEventLog
    {
        string Description { get; }

        DateTime Time { get; }

        string ViewInfo();
    }
}
