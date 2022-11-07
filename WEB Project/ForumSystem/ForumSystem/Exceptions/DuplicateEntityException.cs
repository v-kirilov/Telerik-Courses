using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Exceptions
{
    public class DuplicateEntityException : ApplicationException
    {
        public DuplicateEntityException()
        {

        }
        public DuplicateEntityException(string message)
            : base(message)
        {

        }
    }
}
