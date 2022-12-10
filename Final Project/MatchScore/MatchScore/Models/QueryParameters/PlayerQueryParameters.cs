using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Models.QueryParameters
{
    public class PlayerQueryParameters
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public string SportClub { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
