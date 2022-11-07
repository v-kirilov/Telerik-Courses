namespace ForumSystem.Helpers.Contracts
{
    public interface IUserAuthorChecker
    {
        bool IsUserAuthor(int userId, int commentUserId);
    }
}
