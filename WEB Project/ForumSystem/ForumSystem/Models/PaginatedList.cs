using System.Collections.Generic;

namespace ForumSystem.Models
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList()
        {

        }
        public PaginatedList(List<T> items,int totalPages, int pageNumber)
        {
            this.AddRange(items);
            TotalPages = totalPages;
            PageNumber = pageNumber;
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
        public int TotalPages { get; }
        public int PageNumber { get; }
    }
}
