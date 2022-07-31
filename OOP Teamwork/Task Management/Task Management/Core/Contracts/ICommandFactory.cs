using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Task_Management.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandLine);
    }
}
