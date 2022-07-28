using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.Commands.Contracts
{
    public interface ICommand
    {
        string Execute();
    }
}
