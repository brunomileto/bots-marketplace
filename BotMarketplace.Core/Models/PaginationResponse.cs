using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotMarketplace.Core.Models
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PerPage { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginationResponse(IEnumerable<T> items, int count, int pageNumber, int perPage)
        {
            TotalCount = count;
            PerPage = perPage;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)perPage);
            Items = items;
        }

        public PaginationResponse() { }
    }
}
