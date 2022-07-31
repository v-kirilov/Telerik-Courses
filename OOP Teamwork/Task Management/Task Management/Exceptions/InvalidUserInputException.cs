using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Exceptions
{
    public class InvalidUserInputException : ApplicationException
    {
        public InvalidUserInputException(string message)
            : base(message)
        {
        }
    }
}
