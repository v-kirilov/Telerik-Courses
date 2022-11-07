using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Exceptions
{
    public class UnauthorizedOperationException : ApplicationException
	{
		public UnauthorizedOperationException(string message)
			: base(message)
		{
		}
	}
}
