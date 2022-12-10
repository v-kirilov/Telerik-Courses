using System;

namespace MatchScore.Exceptions

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
