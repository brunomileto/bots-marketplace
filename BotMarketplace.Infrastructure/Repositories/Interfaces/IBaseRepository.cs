using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<(IEnumerable<T>, int totalResults)> GetAllAsync(int pageIndex, int pageSize);
        Task<T?> GetByIdAsync(string id);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(string id);
    }
}
