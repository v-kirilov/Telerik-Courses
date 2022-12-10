using System;
using System.Runtime.Serialization;

namespace MatchScore.Exceptions
{
    [Serializable]
    public class UnauthorizedOperationException : ApplicationException
    {
        public UnauthorizedOperationException()
        {
        }

        public UnauthorizedOperationException(string message) : base(message)
        {
        }
    }
}