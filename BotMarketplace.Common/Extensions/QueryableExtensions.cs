using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BotMarketplace.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<(List<T>, int totalResults)> PaginateAsync<T>(this IQueryable<T> query, int pageNumber, int perPage) where T : class
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Page number must be greater than 0");

            if (perPage <= 0)
                throw new ArgumentException("Per page must be greater than 0");

            var totalResultsTask = query.CountAsync();
            var paginatedResultTask = query.Skip((pageNumber - 1) * perPage).Take(perPage).ToListAsync();
            await Task.WhenAll(totalResultsTask, paginatedResultTask);

            return (paginatedResultTask.Result, await totalResultsTask);
        }
    }
}
