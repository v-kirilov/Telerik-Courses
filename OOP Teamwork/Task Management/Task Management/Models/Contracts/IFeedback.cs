using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Enums.Feedback;

namespace Task_Management.Models.Contracts
{
    public interface IFeedback : ITask
    {
        int Rating { get; }

        Status Status { get; }
    }
}
