using System;

namespace MatchScore.Models.QueryParameters
{
    public class MatchQueryParameters
    {
        public string Date { get; set; }
        public string Participant { get; set; }
        public string Format { get; set; }
        public string Tournament { get; set; }
        public string DirectorEmail { get; set; }
        public string Status { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
