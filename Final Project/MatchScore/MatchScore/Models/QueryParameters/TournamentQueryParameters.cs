namespace MatchScore.QueryParameters
{
    public class TournamentQueryParameters
    {
        public string Title { get; set; }
        public int? UserId { get; set; }
        public string ManagerEmail { get; set; }
        public string Format { get; set; }
        public string Prize { get; set; }
        public string Status { get; set; }
        public string FullName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
