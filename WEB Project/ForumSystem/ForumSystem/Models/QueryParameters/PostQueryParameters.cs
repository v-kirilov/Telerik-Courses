namespace ForumSystem.Models.QueryParameters
{
    public class PostQueryParameters
    {
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string User { get; set; }
        public bool HasMostRecent { get; set; } = false;
        public bool HasMostPopular { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
