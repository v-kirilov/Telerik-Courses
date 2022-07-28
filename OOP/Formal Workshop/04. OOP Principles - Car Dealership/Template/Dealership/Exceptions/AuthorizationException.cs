using System;

namespace Dealership.Exceptions
{
    public class AuthorizationException : ApplicationException
    {
        public AuthorizationException(string message)
            : base(message)
        {
        }
    }
}
