using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;

namespace ForumSystem.Helpers
{
    public class UserAuthorChecker : IUserAuthorChecker
	{
		public bool IsUserAuthor(int userId, int commentUserId)
		{
			if (userId == commentUserId)
			{
				return true;
			}
			else
			{
				throw new UnauthorizedOperationException("You are not authorized.");
			}
		}
	}
}
