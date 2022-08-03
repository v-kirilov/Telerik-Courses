using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string message)
            : base(message)
        {

        }
    }
}
