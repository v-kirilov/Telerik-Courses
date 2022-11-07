using System;

namespace ForumSystem.Exceptions
{
    public class InvalidDataException : ApplicationException
    {
        public InvalidDataException()
        {

        }

        public InvalidDataException(string message)
            : base(message)
        {

        }
    }
}
