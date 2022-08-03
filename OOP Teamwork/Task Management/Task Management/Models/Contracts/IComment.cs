using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface IComment
    {
        string Content { get; }

        string Author { get; }

    }
}
