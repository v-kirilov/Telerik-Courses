namespace MatchScore.Models.QueryParameters
{
    public class RequestQueryParameters
    {
        public string UserEmail { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
