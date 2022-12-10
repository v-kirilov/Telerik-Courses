using System.Collections.Generic;

namespace MatchScore.Models
{
    public class PaginatedList<T>:List<T>
    {
        public int TotalPages { get; }
        public int PageNumber { get; }
        public PaginatedList()
        {

        }
        public PaginatedList(List<T> items,int totalPages, int pageNumber)
        {
            this.AddRange(items);
            this.TotalPages = totalPages;
            this.PageNumber = pageNumber;
        }
        public bool HasPrevPage
        {
            get
            {
                return this.PageNumber > 1;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return this.PageNumber < this.TotalPages;
            }
        }
    }
}
